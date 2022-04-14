namespace UnispectEx.Pe.Models {
    internal class Export {
        internal Export(string name, uint rva) {
            Name = name;
            Rva = rva;
        }

        internal string Name { get; }
        internal uint Rva { get; }
    }
}