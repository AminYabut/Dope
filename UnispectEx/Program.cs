using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using dnlib.DotNet;

using UnispectEx.Analyzers;
using UnispectEx.Processors;
using UnispectEx.Serializers;

using UnispectEx.Core;
using UnispectEx.Core.Inspector;
using UnispectEx.Core.Util;

namespace UnispectEx; 

internal static class Program {
    private static IEnumerable<IPlugin> GetPlugins() {
        var pluginDirectory = Path.Join(AppContext.BaseDirectory, "plugins");

        if (!Directory.Exists(pluginDirectory))
            Directory.CreateDirectory(pluginDirectory);

        foreach (var file in Directory.GetFiles(pluginDirectory, "*.dll")) {
            var assembly = Assembly.LoadFile(file);

            foreach (var pluginType in assembly.GetTypes().Where(typeof(IPlugin).IsAssignableFrom))
                yield return (IPlugin) Activator.CreateInstance(pluginType)!;
        }
    }

    private static bool ProcessAndSerialize(ImmutableList<MetadataContainer> containers, string outputDirectory) {
        var processor = new DefaultDumpProcessor();
        if (!processor.Initialize(containers))
            return false;

        if (!processor.Serialize(new DefaultDumpSerializer()))
            return false;

        return processor.Write(outputDirectory);
    }

    internal static void Main(string[] args) {
        if (args.Length < 2) {
            Console.WriteLine("*** https://github.com/coconutbird/UnispectEx ***");
            Console.WriteLine("[*] UnispectEx <process> <dll> <plugin:optional>");

            return;
        }

        var processName = args[0];
        var dllName = args[1];
        var pluginName = args.Length == 3 ? args[2] : null;

        var memory = new LocalMemory();

        if (!memory.Attach(processName))
            return;

        var inspector = new UnityProcess(memory);

        if (!inspector.Initialize("mono-2.0-bdwgc.dll"))
            return;

        var dataDirectory = inspector.DataDirectory();

        if (dataDirectory is null)
            return;

        var managedDirectory = Path.Join(dataDirectory, "Managed");

        var moduleContext = ModuleDef.CreateModuleContext();

        var assemblyResolver = (AssemblyResolver) moduleContext.AssemblyResolver;

        assemblyResolver.PreSearchPaths.Add(managedDirectory);
        assemblyResolver.EnableTypeDefCache = true;

        var assembly = inspector.Domain!.GetAssemblies().FirstOrDefault(x => x.AssemblyName.Name == dllName)!;
        var module = ModuleDefMD.Load(Path.Join(managedDirectory, $"{dllName}.dll"));

        module.Context = moduleContext;

        assemblyResolver.AddToCache(module);

        assemblyResolver.PreSearchPaths.Add(managedDirectory);

        foreach (var reference in module.GetAssemblyRefs()) {
            try {
                var referenceModule = ModuleDefMD.Load(Path.Join(managedDirectory, $"{reference.Name}.dll"),
                    moduleContext);

                referenceModule.Context = moduleContext;

                assemblyResolver.AddToCache(referenceModule);
            } catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
        }

        var correlatedClasses = Helpers.CorrelateClasses(module.GetTypes(), assembly.Image.Types());
        var containers = correlatedClasses.Select(x => new MetadataContainer(x.Item1, x.Item2)).ToImmutableList();

        IDumpAnalyzer? analyzer;
        if (pluginName is not null) {
            var plugin = GetPlugins().FirstOrDefault(x => x.Name == pluginName);

            if (plugin is null)
                return;

            analyzer = plugin.DumpAnalyzers.FirstOrDefault();

            if (analyzer is null) {
                Console.WriteLine("[-] invalid plugin name!");

                return;
            }
        } else {
            analyzer = new DefaultDumpAnalyzer();
        }

        if (!analyzer.Analyze(containers)) {
            Console.WriteLine("[-] Analysis failed!");

            return;
        }

        Console.WriteLine(ProcessAndSerialize(containers, AppContext.BaseDirectory)
            ? "[*] Successfully dumped and serialized!"
            : "[-] Failed to dump and serialize!");
    }
}