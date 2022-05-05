using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class StackSlot : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var stackSlotContainer = containers.FindContainerByFullName("EFT.InventoryLogic.StackSlot");

        if (stackSlotContainer is null)
            return false;

        stackSlotContainer.CleanPropertyFieldNames();
        stackSlotContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}