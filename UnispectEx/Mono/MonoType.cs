namespace UnispectEx.Mono {
    internal class MonoType {
        private MonoType(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoClass MonoClass { get; private init; }

        internal static MonoType Create(Memory memory, ulong address) {
            var monoClass = MonoClass.Create(memory, memory.Read<ulong>(address + Offsets.MonoTypeData));

            return new(memory, address) {
                MonoClass = monoClass
            };
        }

        private readonly Memory _memory;
    }
}