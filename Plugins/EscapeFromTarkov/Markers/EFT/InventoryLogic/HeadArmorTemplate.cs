using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class HeadArmorTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var headArmorTemplateContainer = containers.FirstOrDefault(container => container.TypeDef.Fields.Any(field => field.Name == "BluntThroughput") &&
                                                                            container.TypeDef.Fields.Any(field => field.Name == "BlindnessProtection"));

        if (headArmorTemplateContainer is null)
            return false;

        headArmorTemplateContainer.Namespace = "EFT.InventoryLogic";
        headArmorTemplateContainer.Name = "HeadArmorTemplate";
        
        headArmorTemplateContainer.CleanPropertyFieldNames();
        headArmorTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}