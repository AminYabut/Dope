using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class EquipableItemTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var equipableItemTemplateContainer = containers.FindContainerContainingFieldName("CanPutIntoDuringTheRaid");
        if (equipableItemTemplateContainer is null)
            return false;

        equipableItemTemplateContainer.Namespace = "EFT.InventoryLogic";
        equipableItemTemplateContainer.Name = "EquipableItemTemplate";
        
        equipableItemTemplateContainer.CleanPropertyFieldNames();
        equipableItemTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}