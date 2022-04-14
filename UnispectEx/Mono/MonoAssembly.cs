using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssembly {
        private MonoAssembly(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        internal MonoAssemblyName AssemblyName => _assemblyName ??= MonoAssemblyName.Create(_memory, Address + Offsets.MonoAssemblyName, _cache);
        internal MonoImage Image => _image ??= MonoImage.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoAssemblyImage), _cache);

        internal static MonoAssembly Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            lock (cache.MonoAssemblyLockObject) {
                if (cache.TryGetAssembly(address, out var assembly))
                    return assembly!;

                var result = new MonoAssembly(memory, address, cache);

                cache.Cache(address, result);

                return result;
            }
        }

        private MonoAssemblyName? _assemblyName;
        private MonoImage? _image;
        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}