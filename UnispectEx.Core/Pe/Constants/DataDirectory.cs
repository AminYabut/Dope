namespace UnispectEx.Core.Pe.Constants; 

internal enum DataDirectory {
    Export = 0,
    Entry,
    Resource,
    Security,
    BaseRelocation,
    Debug,
    Architecture,
    GlobalPtr,
    Tls,
    LoadConfig,
    BoundImport,
    Iat,
    DelayImport,
    ComDescriptor
}