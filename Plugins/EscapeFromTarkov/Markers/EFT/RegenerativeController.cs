using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class RegenerativeController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var physicalController = containers.FindContainerByFullName("EFT.PhysicalController");

        if (physicalController is null)
            return false;

        var staminaFieldContainer = physicalController.FindFieldContainerByName("Stamina");

        if (staminaFieldContainer is null)
            return false;

        var physicalInfoTypeDef = Helpers.GetFieldDefTypeTypeDef(staminaFieldContainer.FieldDef);

        if (physicalInfoTypeDef is null)
            return false;
        
        var regenerativeController = physicalInfoTypeDef.ToMetadataContainer(containers);

        if (regenerativeController is null)
            return false;

        regenerativeController.Namespace = "EFT";
        regenerativeController.Name = "RegenerativeController";

        regenerativeController.CleanPropertyFieldNames();
        regenerativeController.ExportNonObfuscatedSymbols();

        return true;
    }
}