using System;

using UnispectEx.Mono;
using UnispectEx.Pe;
using UnispectEx.Pe.Constants;
using UnispectEx.Util;

namespace UnispectEx.Inspector {
    internal class MonoProcess {
        internal MonoProcess(MemoryConnector memory) {
            _memory = memory;
        }

        internal MonoAppDomain Domain { get; private set; }

        internal bool Initialize(string monoDll) {
            var domain = GetRootDomain(monoDll);

            if (domain == null)
                return false;

            Domain = domain;

            return true;
        }

        private MonoAppDomain GetRootDomain(string monoDll) {
            var dll = _memory.GetModule(monoDll);

            if (dll == 0)
                return null;

            PeFile mono;

            try {
                mono = PeFile.Create(_memory, dll);
            } catch (InvalidOperationException exception) {
                return null;
            }

            var directory = mono.GetDataDirectory(DataDirectory.Export);

            if (directory.VirtualAddress == 0 || directory.Size == 0)
                return null;

            var export = mono.GetExport("mono_get_root_domain");

            if (export == 0)
                return null;

            // 48 8B 05 ? ? ? ? mov rax, cs:mono_root_domain
            // C3               ret
            var offset = _memory.Read<uint>(export + 0x3);
            var domain = _memory.Read<ulong>(export + 0x7 + offset);

            return MonoAppDomain.Create(_memory, domain);
        }

        private readonly MemoryConnector _memory;
    }
}