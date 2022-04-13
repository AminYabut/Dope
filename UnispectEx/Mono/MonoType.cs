namespace UnispectEx.Mono {
    internal class MonoType {
        private MonoType(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal static MonoType Create(Memory memory, ulong address) {
            return new(memory, address);
        }

        private readonly Memory _memory;
    }
}