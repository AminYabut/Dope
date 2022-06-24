using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class ServerBackend : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var serverBackendContainer = containers.FindContainerContainingMethodName("get_BackendCacheDir");
        if (serverBackendContainer is null)
            return false;

        var configMethodDef = serverBackendContainer.FindMethodDefByName("get_Config");
        if (configMethodDef is null)
            return false;

        var configDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(configMethodDef.ReturnType);
        if (configDef is null)
            return false;

        var serverConfigContainer = configDef.ToMetadataContainer(containers);
        if (serverConfigContainer is null)
            return false;
        
        serverConfigContainer.Namespace = "EFT.Network";
        serverConfigContainer.Name = "ServerConfig";
        
        serverBackendContainer.Namespace = "EFT.Network";
        serverBackendContainer.Name = "ServerBackend";
        
        serverBackendContainer.CleanPropertyFieldNames();
        serverBackendContainer.ExportNonObfuscatedSymbols();
        
        serverConfigContainer.CleanPropertyFieldNames();
        serverConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}