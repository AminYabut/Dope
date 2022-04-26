using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class ItemTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var itemTemplateContainer = containers.FindContainerByFullName("EFT.InventoryLogic.ItemTemplate");

        if (itemTemplateContainer is null)
            return false;

        itemTemplateContainer.CleanPropertyFieldNames();
        itemTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}