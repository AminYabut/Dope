using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 
internal class FallingConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var healthConfigContainer = containers.FindContainerByFullName("EFT.Network.Config/EFT.Network.healthConfig");
        if (healthConfigContainer is null)
            return false;

        var fallingConfigFieldContainer = FindFallingFieldContainer(healthConfigContainer);
        if (fallingConfigFieldContainer is null)
            return false;

        var fallingConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(fallingConfigFieldContainer.FieldDef);
        if (fallingConfigTypeDef is null)
            return false;

        var fallingConfigContainer = fallingConfigTypeDef.ToMetadataContainer(containers);
        if (fallingConfigContainer is null)
            return false;
        
        fallingConfigContainer.Namespace = "EFT.Network";
        fallingConfigContainer.Name = "fallingConfig";
       
        fallingConfigContainer.CleanPropertyFieldNames();
        fallingConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindFallingFieldContainer(MetadataContainer healthConfigContainer) {
        foreach (var fieldContainer in healthConfigContainer.Fields) {
            if (fieldContainer.Name == "Falling")
                return fieldContainer;
        }

        return null;
    }
}