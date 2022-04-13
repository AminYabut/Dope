namespace UnispectEx {
    internal static class Offsets {
        // _MonoDomain
        internal const int DomainDomainAssemblies = 0xC8;

        // _MonoAssembly
        internal const int AssemblyName = 0x10;
        internal const int AssemblyImage = 0x60;
        
        // _MonoImage
        internal const int ImageClassCache = 0x4C0;
        
        // _MonoAssemblyName
        internal const int AssemblyNameName = 0x0;
        internal const int AssemblyNameMajor = 0x40;
        internal const int AssemblyNameMinor = 0x42;
        internal const int AssemblyNameBuild = 0x44;
        internal const int AssemblyNameRevision = 0x46;
        internal const int AssemblyNameArch = 0x48;
        
        // _MonoClass
        internal const int MonoClassFields = 0x98;
        internal const int MonoClassFieldsCount = 0x100;
        
        // _MonoInternalHashTable
        internal const int HashTableNext = 0x10;
        internal const int HashTableSize = 0x18;
        internal const int HashTableTable = 0x20;

        // ???
        internal const int ClassNextClassCache = 0x108;   // MonoClassDef.NextClassCache
    }
}