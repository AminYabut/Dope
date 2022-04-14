using System;
using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoClassField {
        private MonoClassField(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoType Type => _type ??= MonoType.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoClassFieldType));
        internal string Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoClassFieldName), 255);
        internal MonoClass Parent => _parent ??= MonoClass.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoClassFieldParent));
        internal int Offset => _offset ??= _memory.Read<int>(Address + Offsets.MonoClassFieldOffset);

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

        internal static MonoClassField Create(MemoryConnector memory, ulong address) {
            return new(memory, address);
        }

        private MonoType? _type;
        private string? _name;
        private MonoClass? _parent;
        private int? _offset;
        private int? _token;
        private readonly MemoryConnector _memory;
    }
}