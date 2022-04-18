using UnispectEx.Core.Util;

namespace UnispectEx.Core.Mono {
    public class MonoAssemblyName {
        private MonoAssemblyName(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        public string? Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoAssemblyNameName), 255);

        public short? Major => _major ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMajor);
        public short? Minor => _minor ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMinor);
        public short? Build => _build ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameBuild);
        public short? Revision => _revision ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameRevision);
        public short? Arch => _arch ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameArch);

        internal static MonoAssemblyName Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            lock (cache.MonoAssemblyNameLockObject) {
                if (cache.TryGetAssemblyName(address, out var assemblyName))
                    return assemblyName!;

                var result = new MonoAssemblyName(memory, address, cache);

                cache.Cache(address, result);

                return result;
            }
        }

        private string? _name;
        private short? _major;
        private short? _minor;
        private short? _build;
        private short? _revision;
        private short? _arch;
        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}