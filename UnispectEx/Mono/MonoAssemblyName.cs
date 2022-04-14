using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssemblyName {
        private MonoAssemblyName(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal string? Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoAssemblyNameName), 255);

        internal short? Major => _major ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMajor);
        internal short? Minor => _minor ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMinor);
        internal short? Build => _build ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameBuild);
        internal short? Revision => _revision ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameRevision);
        internal short? Arch => _arch ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameArch);

        internal static MonoAssemblyName Create(MemoryConnector memory, ulong address) {
            return new(memory, address);
        }

        private string? _name;
        private short? _major;
        private short? _minor;
        private short? _build;
        private short? _revision;
        private short? _arch;
        private readonly MemoryConnector _memory;
    }
}