using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class Item : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var itemContainer = containers.FindContainerByFullName("EFT.InventoryLogic.Item");

        if (itemContainer is null)
            return false;

        itemContainer.CleanPropertyFieldNames();
        itemContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}