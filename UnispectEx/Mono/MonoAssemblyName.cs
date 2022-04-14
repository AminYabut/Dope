using UnispectEx.Util;

namespace UnispectEx.Mono {
    internal class MonoAssemblyName {
        private MonoAssemblyName(MemoryConnector memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal string Name { get; private init; }
        internal short Major { get; private init; }
        internal short Minor { get; private init; }
        internal short Build { get; private init; }
        internal short Revision { get; private init; }
        internal short Arch { get; private init; }

        internal static MonoAssemblyName Create(MemoryConnector memory, ulong address) {
            var name = memory.ReadString(memory.Read<ulong>(address + Offsets.MonoAssemblyNameName), 255);
            var major = memory.Read<short>(address + Offsets.MonoAssemblyNameMajor);
            var minor = memory.Read<short>(address + Offsets.MonoAssemblyNameMinor);
            var build = memory.Read<short>(address + Offsets.MonoAssemblyNameBuild);
            var revision = memory.Read<short>(address + Offsets.MonoAssemblyNameRevision);
            var arch = memory.Read<short>(address + Offsets.MonoAssemblyNameArch);

            return new(memory, address) {
                Name = name,
                Major = major,
                Minor = minor,
                Build = build,
                Revision = revision,
                Arch = arch
            };
        }

        private readonly MemoryConnector _memory;
    }
}