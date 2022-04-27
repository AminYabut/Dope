using System;
using UnispectEx.Core.Util;

namespace UnispectEx.Core.Mono; 

public class MonoClassField {
    private MonoClassField(MemoryConnector memory, ulong address, MonoObjectCache cache) {
        Address = address;

        _memory = memory;
        _cache = cache;
    }

    public override string ToString() => FullName ?? "<ERROR_READING_NAME>";

    internal ulong Address { get; }

    public string? FullName => $"{Type.MonoClass.FullName} {Parent.Name}::{Name}";

    public MonoType Type => _type ??= MonoType.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoClassFieldType), _cache);
    public string Name => _name ??= _memory.ReadString(_memory.Read<ulong>(Address + Offsets.MonoClassFieldName), 255);
    public MonoClass Parent => _parent ??= MonoClass.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoClassFieldParent), _cache);

    public int Offset {
        get {
            if (_offset.HasValue)
                return _offset.Value;
            
            var offset = _memory.Read<int>(Address + Offsets.MonoClassFieldOffset);

            if (offset == -1)
                return offset;

            var type = Type;

            if (Parent.IsValueType && !type.IsStatic && !type.IsLiteral)
                offset -= 0x10;

            _offset = offset;

            return offset;
        }
    }

    public int Token {
        get {
            if (_token.HasValue)
                return _token.Value;

            var parent = Parent;
            var fieldCount = parent.FieldCount;

            if (fieldCount == 0)
                throw new InvalidOperationException("parent has no fields!");

            uint idx = 0;
            foreach (var field in parent.Fields()) {
                if (field.Address == Address)
                    break;
                    
                ++idx;
            }

            var token = (int) (idx + parent.FirstFieldIdx + 1) | 0x4000000;

            _token = token;

            return token;
        }
    }

    internal static MonoClassField Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
        lock (cache.MonoClassFieldLockObject) {
            if (cache.TryGetField(address, out var field))
                return field!;

            var result = new MonoClassField(memory, address, cache);

            cache.Cache(address, result);

            return result;
        }
    }

    private MonoType? _type;
    private string? _name;
    private MonoClass? _parent;
    private int? _offset;
    private int? _token;
    private readonly MemoryConnector _memory;
    private readonly MonoObjectCache _cache;
}