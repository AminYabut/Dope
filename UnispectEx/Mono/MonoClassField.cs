namespace UnispectEx.Mono {
    internal class MonoClassField {
        private MonoClassField(Memory memory, ulong address) {
            Address = address;
            
            _memory = memory;
        }
        
        internal ulong Address { get; }

        internal MonoType Type { get; private init; }
        internal string Name { get; private init; }
        internal MonoClass Parent { get; private init; }
        internal int Offset { get; private init; }

        internal static MonoClassField Create(Memory memory, ulong address) {
            var type = MonoType.Create(memory, memory.Read<ulong>(address + 0x0));
            var name = memory.ReadString(memory.Read<ulong>(address + 0x8), 255);
            var parent = MonoClass.Create(memory, memory.Read<ulong>(address + 0x10));
            var offset = memory.Read<int>(address + 0x18);

            return new (memory, address) {
                Type = type,
                Name = name,
                Parent = parent,
                Offset = offset
            };
        }

        private readonly Memory _memory;
    }
}