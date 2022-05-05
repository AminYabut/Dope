using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class ItemOwner : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var itemOwnerInterfaceContainer = containers.FindContainerByFullName("EFT.InventoryLogic.IItemOwner");
        if (itemOwnerInterfaceContainer is null)
            return false;

        var itemOwnerContainer = containers.FirstOrDefault(x => Helpers.TypeImplementsInterface(x.TypeDef, itemOwnerInterfaceContainer.TypeDef));
        if (itemOwnerContainer is null)
            return false;

        itemOwnerContainer.Namespace = "EFT.InventoryLogic.IItemOwner";
        itemOwnerContainer.Name = "ItemOwner";
        
        itemOwnerContainer.CleanPropertyFieldNames();
        itemOwnerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}