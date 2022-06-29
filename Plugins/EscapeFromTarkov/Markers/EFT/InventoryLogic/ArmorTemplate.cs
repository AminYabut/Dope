using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class ArmorTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        //var armorTemplateContainer = containers.FindContainerContainingFieldName("BluntThroughput");
        var armorTemplateContainer = containers.FirstOrDefault(container => container.TypeDef.Fields.Any(field => field.Name == "BluntThroughput") &&
                                                                            !container.TypeDef.Fields.Any(field => field.Name == "BlindnessProtection"));

        if (armorTemplateContainer is null)
            return false;

        armorTemplateContainer.Namespace = "EFT.InventoryLogic";
        armorTemplateContainer.Name = "ArmorTemplate";
        
        armorTemplateContainer.CleanPropertyFieldNames();
        armorTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}