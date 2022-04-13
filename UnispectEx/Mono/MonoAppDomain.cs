using System.Collections.Generic;

namespace UnispectEx.Mono {
    public class MonoAppDomain {
        internal MonoAppDomain(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal IEnumerable<MonoAssembly> GetAssemblies() {
            var assemblies = _memory.Read<ulong>(Address + Offsets.DomainDomainAssemblies);

            for (var it = assemblies; it != 0; it = _memory.Read<ulong>(it + 0x8)) {
                var assembly = _memory.Read<ulong>(it);

                if (assembly == 0)
                    continue;

                yield return MonoAssembly.Create(_memory, assembly);
            }
        }

        internal static MonoAppDomain Create(Memory memory, ulong address) {
            return new(memory, address);
        }

        private readonly Memory _memory;
    }
}