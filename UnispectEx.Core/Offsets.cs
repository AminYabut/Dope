namespace UnispectEx.Core;

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
    internal const int MonoClassBits = 0x20;
    internal const int MonoClassName = 0x48;
    internal const int MonoClassNamespace = 0x50;
    internal const int MonoClassTypeToken = 0x58;
    internal const int MonoClassFields = 0x98;

    // MonoClassDef
    internal const int MonoClassDefClass = 0x0;
    internal const int MonoClassDefFlags = 0xF0;
    internal const int MonoClassDefFirstMethodIdx = 0xF4;
    internal const int MonoClassDefFirstFieldIdx = 0xF8;
    internal const int MonoClassDefMethodCount = 0xFC;
    internal const int MonoClassDefFieldCount = 0x100;
    internal const int MonoClassDefNextCache = 0x108;

    // _MonoClassField
    internal const int MonoClassFieldType = 0x0;
    internal const int MonoClassFieldName = 0x8;
    internal const int MonoClassFieldParent = 0x10;
    internal const int MonoClassFieldOffset = 0x18;

    // _MonoType
    internal const int MonoTypeData = 0x0;
    internal const int MonoTypeAttributes = 0x8;

    // _MonoInternalHashTable
    internal const int MonoInternalHashTableSize = 0x18;
    internal const int MonoInternalHashTableTable = 0x20;
}