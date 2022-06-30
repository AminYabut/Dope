using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class MagazineItem : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var magazineItemContainer = containers.FindContainerContainingMethodName("IsAmmoCompatible");

        if (magazineItemContainer is null)
            return false;

        magazineItemContainer.Namespace = "EFT.InventoryLogic";
        magazineItemContainer.Name = "MagazineItem";
        
        magazineItemContainer.CleanPropertyFieldNames();
        magazineItemContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}