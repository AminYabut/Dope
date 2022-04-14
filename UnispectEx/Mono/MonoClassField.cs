using System;

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

        internal int Token {
            get {
                if (_token.HasValue)
                    return _token.Value;
                
                var parent = Parent;
                var fieldCount = parent.FieldCount;

                if (fieldCount == 0)
                    throw new InvalidOperationException("parent has no fields!");

                uint idx = 0;
                foreach (var field in parent.Fields()) {
                    ++idx;
                    if (field.Address == Address)
                        break;
                }
                
                var token = (int) (idx + parent.FirstFieldIdx + 1) | 0x4000000;
                
                _token = token;

                return token;
            }
        }

        internal static MonoClassField Create(Memory memory, ulong address) {
            var type = MonoType.Create(memory, memory.Read<ulong>(address + Offsets.MonoClassFieldType));
            var name = memory.ReadString(memory.Read<ulong>(address + Offsets.MonoClassFieldName), 255);
            var parent = MonoClass.Create(memory, memory.Read<ulong>(address + Offsets.MonoClassFieldParent));
            var offset = memory.Read<int>(address + Offsets.MonoClassFieldOffset);

            return new (memory, address) {
                Type = type,
                Name = name,
                Parent = parent,
                Offset = offset
            };
        }

        private int? _token;
        private readonly Memory _memory;
    }
}