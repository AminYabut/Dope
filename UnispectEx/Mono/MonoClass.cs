using System;
using System.Collections.Generic;
using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoClass {
        private MonoClass(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal string? FullName => Namespace != string.Empty ? $"{Namespace}.{Name}" : Name;

        internal string? Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoClassName), 255);
        internal string? Namespace => _namespace ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoClassNamespace), 255);

        internal uint FirstMethodIdx => _firstMethodIdx ??= _memory.Read<uint>(Address + Offsets.MonoClassDefFirstMethodIdx);
        internal uint FirstFieldIdx => _firstFieldIdx ??= _memory.Read<uint>(Address + Offsets.MonoClassDefFirstFieldIdx);

        internal uint MethodCount => _methodCount ??= _memory.Read<uint>(Address + Offsets.MonoClassDefMethodCount);
        internal uint FieldCount => _fieldCount ??= _memory.Read<uint>(Address + Offsets.MonoClassDefFieldCount);

        internal int Token => _token ??= _memory.Read<int>(Address + Offsets.MonoClassTypeToken);

        internal IEnumerable<MonoClassField> Fields() {
            var fields = _memory.Read<ulong>(Address + Offsets.MonoClassFields);
            for (uint i = 0; i < FieldCount; ++i)
                yield return MonoClassField.Create(_memory, fields + i * 0x20);
        }

        internal static MonoClass Create(MemoryConnector memory, ulong address) {
            return new(memory, address);
        }

        private string? _name;
        private string? _namespace;
        private uint? _firstMethodIdx;
        private uint? _firstFieldIdx;
        private uint? _methodCount;
        private uint? _fieldCount;
        private int? _token;
        private readonly MemoryConnector _memory;
    }
}