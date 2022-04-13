using System;
using System.Collections.Generic;

namespace UnispectEx.Mono {
    internal class MonoClass {
        private MonoClass(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal string FullName => Namespace != String.Empty ? $"{Namespace}.{Name}" : Name;

        internal string Name { get; private init; }
        internal string Namespace { get; private init; }

        internal IEnumerable<MonoClassField> Fields() {
            var fields = _memory.Read<ulong>(Address + Offsets.MonoClassFields);
            var fieldsCount = _memory.Read<uint>(Address + Offsets.MonoClassFieldsCount);

            for (uint i = 0; i < fieldsCount; ++i)
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

        private readonly Memory _memory;
    }
}