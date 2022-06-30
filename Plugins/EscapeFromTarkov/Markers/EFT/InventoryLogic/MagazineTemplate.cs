using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class MagazineTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var magazineTemplateContainer = containers.FindContainerContainingFieldName("VisibleAmmoRangesString");
        if (magazineTemplateContainer is null)
            return false;

        magazineTemplateContainer.Namespace = "EFT.InventoryLogic";
        magazineTemplateContainer.Name = "MagazineTemplate";
        
        magazineTemplateContainer.CleanPropertyFieldNames();
        magazineTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}