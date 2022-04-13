namespace UnispectEx.Mono {
    internal class MonoAssemblyName {
        private MonoAssemblyName(Memory memory, ulong address) {
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

        internal static MonoAssemblyName Create(Memory memory, ulong address) {
            var name = memory.ReadString(memory.Read<ulong>(address + Offsets.AssemblyNameName), 255);
            var major = memory.Read<short>(address + Offsets.AssemblyNameMajor);
            var minor = memory.Read<short>(address + Offsets.AssemblyNameMinor);
            var build = memory.Read<short>(address + Offsets.AssemblyNameBuild);
            var revision = memory.Read<short>(address + Offsets.AssemblyNameRevision);
            var arch = memory.Read<short>(address + Offsets.AssemblyNameArch);

            return new(memory, address) {
                Name = name,
                Major = major,
                Minor = minor,
                Build = build,
                Revision = revision,
                Arch = arch
            };
        }

        private readonly Memory _memory;
    }
}