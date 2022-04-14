using System.Collections.Generic;

namespace UnispectEx.Mono {
    internal class MonoImage {
        private MonoImage(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
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
                    yield return MonoClass.Create(_memory, it);
                }
            }
        }

        internal static MonoImage Create(Memory memory, ulong address) {
            return new(memory, address);
        }

        private readonly Memory _memory;
    }
}