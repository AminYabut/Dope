using System;
using System.Linq;

namespace UnispectEx {
    internal static class Program {
        internal static void Main(string[] args) {
            var memory = new LocalMemory();

            if (!memory.Attach("Unturned"))
                return;

            var inspector = new Inspector(memory);

            if (!inspector.Initialize("mono-2.0-bdwgc.dll"))
                return;

            foreach (var assembly in inspector.AppDomain.GetAssemblies()) {
                Console.WriteLine($"assembly: {assembly.AssemblyName.Name}");

                if (assembly.AssemblyName.Name != "Assembly-CSharp")
                    continue;

                foreach (var type in assembly.Image.Types()) {
                    foreach (var field in type.Fields()) {
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