using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class EquipmentSlot : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var equipmentSlotContainer = containers.FindContainerByFullName("EFT.InventoryLogic.EquipmentSlot");

        if (equipmentSlotContainer is null)
            return false;

        equipmentSlotContainer.CleanPropertyFieldNames();
        equipmentSlotContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}