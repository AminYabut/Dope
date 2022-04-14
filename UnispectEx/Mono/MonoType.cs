using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoType {
        private MonoType(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoClass MonoClass { get; private init; }

        internal static MonoType Create(MemoryConnector memory, ulong address) {
            var monoClass = MonoClass.Create(memory, memory.Read<ulong>(address + Offsets.MonoTypeData));

            return new(memory, address) {
                MonoClass = monoClass
            };
        }

        private readonly MemoryConnector _memory;
    }
}