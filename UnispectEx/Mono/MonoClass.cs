using System;
using System.Collections.Generic;

namespace UnispectEx.Mono {
    internal class MonoClass {
        private MonoClass(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal string FullName => Namespace != string.Empty ? $"{Namespace}.{Name}" : Name;

        internal string Name { get; private init; }
        internal string Namespace { get; private init; }

        internal uint FirstMethodIdx => _firstMethodIdx ??= _memory.Read<uint>(Address + 0xF8);
        internal uint FirstFieldIdx => _firstFieldIdx ??= _memory.Read<uint>(Address + 0xF8);

        internal uint MethodCount => _methodCount ??= _memory.Read<uint>(Address + Offsets.MonoClassDefMethodCount);
        internal uint FieldCount => _fieldCount ??= _memory.Read<uint>(Address + Offsets.MonoClassDefFieldCount);

        internal int Token => _token ??= _memory.Read<int>(Address + Offsets.MonoClassTypeToken);

        internal IEnumerable<MonoClassField> Fields() {
            var fields = _memory.Read<ulong>(Address + Offsets.MonoClassFields);
            for (uint i = 0; i < FieldCount; ++i)
                yield return MonoClassField.Create(_memory, fields + i * 0x20);
        }

        internal static MonoClass Create(Memory memory, ulong address) {
            var name = memory.ReadString(memory.Read<ulong>(address + Offsets.MonoClassName), 255);
            var namespaceName = memory.ReadString(memory.Read<ulong>(address + Offsets.MonoClassNamespace), 255);

            return new(memory, address) {
                Name = name,
                Namespace = namespaceName,
            };
        }

        private int? _token;
        private uint? _firstMethodIdx;
        private uint? _firstFieldIdx;
        private uint? _methodCount;
        private uint? _fieldCount;
        private readonly Memory _memory;
    }
}