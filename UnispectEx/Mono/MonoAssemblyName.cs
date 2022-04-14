using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssemblyName {
        private MonoAssemblyName(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        internal string? Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoAssemblyNameName), 255);

        internal short? Major => _major ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMajor);
        internal short? Minor => _minor ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameMinor);
        internal short? Build => _build ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameBuild);
        internal short? Revision => _revision ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameRevision);
        internal short? Arch => _arch ??= _memory.Read<short>(Address + Offsets.MonoAssemblyNameArch);

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