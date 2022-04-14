using System;
using System.Linq;
using UnispectEx.Inspector;
using UnispectEx.Util;

namespace UnispectEx {
    internal static class Program {
        internal static void Main(string[] args) {
            var memory = new LocalMemory();

            if (!memory.Attach("Unturned"))
                return;

            var inspector = new MonoProcess(memory);

            if (!inspector.Initialize("mono-2.0-bdwgc.dll"))
                return;

            foreach (var assembly in inspector.Domain.GetAssemblies()) {
                Console.WriteLine($"assembly: {assembly.AssemblyName.Name}");

                if (assembly.AssemblyName.Name != "Assembly-CSharp")
                    continue;

                foreach (var type in assembly.Image.Types()) {
                    if (type.FullName != "SDG.Unturned.Dedicator")
                        continue;

                    foreach (var field in type.Fields()) {
                        var fieldTypeClassName = field.Type.MonoClass.Name;

                        var token = field.Token;

                        if (field.Name != string.Empty) {
                            Console.WriteLine($"field: {field.Name}:{field.Offset}");
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}