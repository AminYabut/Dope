using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class StaminaConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var configContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (configContainer is null)
            return false;

        var staminaConfigFieldContainer = FindStaminaFieldContainer(configContainer);
        if (staminaConfigFieldContainer is null)
            return false;

        var staminaConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(staminaConfigFieldContainer.FieldDef);
        if (staminaConfigTypeDef is null)
            return false;

        var staminaConfigContainer = staminaConfigTypeDef.ToMetadataContainer(containers);
        if (staminaConfigContainer is null)
            return false;
        
        staminaConfigContainer.Namespace = "EFT.Network";
        staminaConfigContainer.Name = "StaminaConfig";
       
        staminaConfigContainer.CleanPropertyFieldNames();
        staminaConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private MetadataFieldContainer? FindStaminaFieldContainer(MetadataContainer configContainer) {
        foreach (var fieldContainer in configContainer.Fields) {
            if (fieldContainer.Name == "Stamina")
                return fieldContainer;
        }

        return null;
    }
}