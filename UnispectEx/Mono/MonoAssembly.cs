using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssembly {
        private MonoAssembly(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoAssemblyName AssemblyName { get; private init; }
        internal MonoImage Image { get; private init; }

        internal static MonoAssembly Create(MemoryConnector memory, ulong address) {
            var assemblyName = MonoAssemblyName.Create(memory, address + Offsets.MonoAssemblyName);
            var image = MonoImage.Create(memory, memory.Read<ulong>(address + Offsets.MonoAssemblyImage));

            return new(memory, address) {
                Image = image,
                AssemblyName = assemblyName
            };
        }

        private readonly MemoryConnector _memory;
    }
}