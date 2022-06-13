using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class BackendConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var sessionContainer = containers.FindContainerByFullName("EFT.Network.LiveBackend/EFT.Network.Session");

        if (sessionContainer is null)
            return false;

        var backendConfigFieldContainer = sessionContainer.FindFieldContainerByName("_backEndConfig");
        if (backendConfigFieldContainer is null)
            return false;

        var backendConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(backendConfigFieldContainer.FieldDef);
        if (backendConfigTypeDef is null)
            return false;

        var backendConfigContainer = backendConfigTypeDef.ToMetadataContainer(containers);
        if (backendConfigContainer is null)
            return false;
        
        var configFieldContainer = backendConfigContainer.FindFieldContainerByName("Config");
        if (configFieldContainer is null)
            return false;

        var configTypeDef = Helpers.GetFieldDefTypeTypeDef(configFieldContainer.FieldDef);
        if (configTypeDef is null)
            return false;

        var configContainer = configTypeDef.ToMetadataContainer(containers);
        if (configContainer is null)
            return false;
        
        configContainer.Namespace = "EFT.Network";
        configContainer.Name = "Config";
        
        configContainer.CleanPropertyFieldNames();
        configContainer.ExportNonObfuscatedSymbols();
        
        backendConfigContainer.Namespace = "EFT.Network";
        backendConfigContainer.Name = "BackendConfig";
        
        backendConfigContainer.CleanPropertyFieldNames();
        backendConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}