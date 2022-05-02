using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class Slot : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var slotContainer = containers.FindContainerByFullName("EFT.InventoryLogic.Slot");

        if (slotContainer is null)
            return false;

        slotContainer.CleanPropertyFieldNames();
        slotContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}