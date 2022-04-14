namespace UnispectEx {
    internal static class Offsets {
        // _MonoDomain
        internal const int DomainDomainAssemblies = 0xC8;

        // _MonoAssembly
        internal const int MonoAssemblyName = 0x10;
        internal const int MonoAssemblyImage = 0x60;
        
        // _MonoImage
        internal const int MonoImageClassCache = 0x4C0;
        
        // _MonoAssemblyName
        internal const int MonoAssemblyNameName = 0x0;
        internal const int MonoAssemblyNameMajor = 0x40;
        internal const int MonoAssemblyNameMinor = 0x42;
        internal const int MonoAssemblyNameBuild = 0x44;
        internal const int MonoAssemblyNameRevision = 0x46;
        internal const int MonoAssemblyNameArch = 0x48;
        
        // _MonoClass
        internal const int MonoClassName = 0x48;
        internal const int MonoClassNamespace = 0x50;
        internal const int MonoClassFields = 0x98;
        internal const int MonoClassFieldsCount = 0x100;
        
        // _MonoClassField
        internal const int MonoClassFieldType = 0x0;
        internal const int MonoClassFieldName = 0x8;
        internal const int MonoClassFieldParent = 0x10;
        internal const int MonoClassFieldOffset = 0x18;
        
        // _MonoType
        internal const int MonoTypeData = 0x0;
        
        // _MonoInternalHashTable
        internal const int HashTableNext = 0x10;
        internal const int HashTableSize = 0x18;
        internal const int HashTableTable = 0x20;

        // ???
        internal const int ClassNextClassCache = 0x108;   // MonoClassDef.NextClassCache
    }
}