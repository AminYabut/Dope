namespace UnispectEx.Mono {
    internal class MonoAssembly {
        private MonoAssembly(Memory memory, ulong address) {
            Address = address;

            _memory = memory;
        }

        internal ulong Address { get; }

        internal MonoAssemblyName AssemblyName { get; private init; }
        internal MonoImage Image { get; private init; }

        internal static MonoAssembly Create(Memory memory, ulong address) {
            var assemblyName = MonoAssemblyName.Create(memory, address + Offsets.AssemblyName);
            var image = MonoImage.Create(memory, memory.Read<ulong>(address + Offsets.AssemblyImage));

            return new(memory, address) {
                Image = image,
                AssemblyName = assemblyName
            };
        }

        private readonly Memory _memory;
    }
}