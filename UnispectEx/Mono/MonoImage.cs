using System.Collections.Generic;
using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoImage {
        private MonoImage(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        internal IEnumerable<MonoClass> Types() {
            var cacheAddress = Address + Offsets.MonoImageClassCache;

            var size = _memory.Read<int>(cacheAddress + Offsets.HashTableSize);
            var table = _memory.Read<ulong>(cacheAddress + Offsets.HashTableTable);

            for (uint i = 0; i < size; i++) {
                for (var it = _memory.Read<ulong>(table + i * 0x8);
                     it != 0;
                     it = _memory.Read<ulong>(it + Offsets.MonoClassDefNextCache)) {
                    yield return MonoClass.Create(_memory, it, _cache);
                }
            }
        }

        internal static MonoImage Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            lock (cache.MonoImageLockObject) {
                if (cache.TryGetImage(address, out var image))
                    return image!;

                var result = new MonoImage(memory, address, cache);

                cache.Cache(address, result);

                return result;
            }
        }

        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}