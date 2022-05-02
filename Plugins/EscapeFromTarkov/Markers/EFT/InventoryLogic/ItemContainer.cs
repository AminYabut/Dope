using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class ItemContainer : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var equipmentContainerContainer = containers.FindContainerByFullName("EFT.InventoryLogic.EquipmentContainer");

        if (equipmentContainerContainer is null)
            return false;

        var itemContainerTypeDef = equipmentContainerContainer.TypeDef.BaseType as TypeDef;

        if (itemContainerTypeDef is null)
            return false;

        var itemContainerContainer = itemContainerTypeDef.ToMetadataContainer(containers);

        if (itemContainerContainer is null)
            return false;

        itemContainerTypeDef.Namespace = "EFT.InventoryLogic";
        itemContainerTypeDef.Name = "ItemContainer";

        itemContainerContainer.CleanPropertyFieldNames();
        itemContainerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}