using System.Collections.Generic;

using UnispectEx.Util;

namespace UnispectEx.Mono {
    public class MonoAppDomain {
        internal MonoAppDomain(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            Address = address;

            _memory = memory;
            _cache = cache;
        }

        internal ulong Address { get; }

        internal IEnumerable<MonoAssembly> GetAssemblies() {
            var assemblies = _memory.Read<ulong>(Address + Offsets.DomainDomainAssemblies);

            for (var it = assemblies; it != 0; it = _memory.Read<ulong>(it + 0x8)) {
                var assembly = _memory.Read<ulong>(it);

                if (assembly == 0)
                    continue;

                yield return MonoAssembly.Create(_memory, assembly, _cache);
            }
        }

        internal static MonoAppDomain? Create(MemoryConnector memory, ulong address, MonoObjectCache cache) {
            return new(memory, address, cache);
        }

        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache;
    }
}