using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoType {
        private MonoType(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoClass MonoClass => _class ??= MonoClass.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoTypeData));

        internal static MonoType Create(MemoryConnector memory, ulong address) {
            return new(memory, address);
        }

        private MonoClass? _class;
        private readonly MemoryConnector _memory;
    }
}