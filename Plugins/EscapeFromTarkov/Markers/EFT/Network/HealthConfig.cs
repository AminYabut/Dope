using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 
internal class HealthConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var configContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (configContainer is null)
            return false;

        var healthConfigFieldContainer = FindHealthFieldContainer(configContainer);
        if (healthConfigFieldContainer is null)
            return false;

        var healthConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(healthConfigFieldContainer.FieldDef);
        if (healthConfigTypeDef is null)
            return false;

        var healthConfigContainer = healthConfigTypeDef.ToMetadataContainer(containers);
        if (healthConfigContainer is null)
            return false;
        
        healthConfigContainer.Namespace = "EFT.Network";
        healthConfigContainer.Name = "healthConfig";
       
        healthConfigContainer.CleanPropertyFieldNames();
        healthConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindHealthFieldContainer(MetadataContainer configContainer) {
        foreach (var fieldContainer in configContainer.Fields) {
            if (fieldContainer.Name == "Health")
                return fieldContainer;
        }

        return null;
    }
}