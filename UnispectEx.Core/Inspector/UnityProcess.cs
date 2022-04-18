using System;

using UnispectEx.Core.Mono;
using UnispectEx.Core.Pe;
using UnispectEx.Core.Util;

namespace UnispectEx.Core.Inspector {
    public class UnityProcess {
        public UnityProcess(MemoryConnector memory) {
            _memory = memory;
        }

        public MonoAppDomain? Domain { get; private set; }

        public bool Initialize(string monoDll) {
            var domain = GetRootDomain(monoDll);

            if (domain is null)
                return false;

            Domain = domain;

            return true;
        }

        public MonoAppDomain? GetRootDomain(string monoDll) {
            var dll = _memory.GetModule(monoDll);

            if (dll == 0)
                return null;

            PeFile mono;

            try {
                mono = PeFile.Create(_memory, dll);
            } catch (Exception exception) {
                if (exception is not InvalidOperationException)
                    throw;
                
                return null;
            }

            var export = mono.GetExport("mono_get_root_domain");

            if (export == 0)
                return null;

            // 48 8B 05 ? ? ? ? mov rax, cs:mono_root_domain
            // C3               ret
            var offset = _memory.Read<uint>(export + 0x3);
            var domain = _memory.Read<ulong>(export + 0x7 + offset);

            return MonoAppDomain.Create(_memory, domain, _cache);
        }

        private readonly MemoryConnector _memory;
        private readonly MonoObjectCache _cache = new();
    }
}