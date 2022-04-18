﻿using System;
using System.Collections.Immutable;
using System.Linq;

using dnlib.DotNet;

using UnispectEx.Inspector;
using UnispectEx.Util;

namespace UnispectEx {
    internal static class Program {
        private static bool DumpAndSerialize(ImmutableList<MetadataContainer> containers, string outputDirectory) {
            var processor = new DefaultDumpProcessor();
            if (!processor.Initialize(containers))
                return false;

            if (!processor.Mark())
                return false;

            if (!processor.Serialize(new DefaultDumpSerializer()))
                return false;

            return processor.Write(containers, AppContext.BaseDirectory);
        }

        internal static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("[*] UnispectEx <process> <Assembly-CSharp.dll path>");

                return;
            }

            var processName = args[0];
            var assemblyPath = args[1];

            var memory = new LocalMemory();

            if (!memory.Attach(processName))
                return;

            var inspector = new UnityProcess(memory);

            if (!inspector.Initialize("mono-2.0-bdwgc.dll"))
                return;

            var assembly = inspector.Domain!.GetAssemblies().FirstOrDefault(x => x.AssemblyName.Name == "Assembly-CSharp")!;
            var module = ModuleDefMD.Load(assemblyPath);

            var correlatedClasses = Helpers.CorrelateClasses(module.GetTypes(), assembly.Image.Types());
            var containers = correlatedClasses.Select(x => new MetadataContainer(x.Item1, x.Item2)).ToImmutableList();

            Console.WriteLine(DumpAndSerialize(containers, AppContext.BaseDirectory)
                ? "[*] Successfully dumped and serialized!"
                : "[-] Failed to dump and serialize!");
        }
    }
}