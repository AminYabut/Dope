using System.Collections.Generic;

using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoClass {
        private MonoClass(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        public override string ToString() {
            return FullName ?? "<ERROR_READING_NAME>";
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
            if (fields == 0)
                yield break;
            
            for (uint i = 0; i < FieldCount; ++i)
                yield return MonoClassField.Create(_memory, fields + i * 0x20, _cache);
        }

        internal static MonoClass Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            lock (cache.MonoClassLockObject) {
                if (cache.TryGetClass(address, out var monoClass))
                    return monoClass!;

                var result = new MonoClass(memory, address, cache);

                cache.Cache(address, result);

                return result;
            }
        }

        private string? _name;
        private string? _namespace;
        private uint? _firstMethodIdx;
        private uint? _firstFieldIdx;
        private uint? _methodCount;
        private uint? _fieldCount;
        private int? _token;
        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}