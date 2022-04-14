using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssembly {
        private MonoAssembly(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoAssemblyName AssemblyName => _assemblyName ??= MonoAssemblyName.Create(_memory, Address + Offsets.MonoAssemblyName);
        internal MonoImage Image => _image ??= MonoImage.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoAssemblyImage));

        internal static MonoAssembly Create(MemoryConnector memory, ulong address) {
            return new(memory, address);
        }

        private MonoAssemblyName? _assemblyName;
        private MonoImage? _image;
        private readonly MemoryConnector _memory;
    }
}