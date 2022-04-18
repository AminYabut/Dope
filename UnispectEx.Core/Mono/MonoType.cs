using UnispectEx.Core.Util;

namespace UnispectEx.Core.Mono {
    public class MonoType {
        private MonoType(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        public MonoClass MonoClass => _class ??= MonoClass.Create(_memory, _memory.Read<ulong>(Address + Offsets.MonoTypeData), _cache);

        internal static MonoType Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            lock (cache.MonoTypeLockObject) {
                if (cache.TryGetType(address, out var type))
                    return type!;

                var result = new MonoType(memory, address, cache);

                cache.Cache(address, result);

                return result;
            }
        }

        private MonoClass? _class;
        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}