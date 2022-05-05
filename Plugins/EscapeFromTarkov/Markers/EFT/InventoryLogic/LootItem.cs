using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class LootItem : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var equipmentContainerContainer = containers.FindContainerByFullName("EFT.InventoryLogic.EquipmentContainer");

        if (equipmentContainerContainer is null)
            return false;

        var lootItemTypeDef = equipmentContainerContainer.TypeDef.BaseType as TypeDef;

        if (lootItemTypeDef is null)
            return false;

        var lootItemContainer = lootItemTypeDef.ToMetadataContainer(containers);

        if (lootItemContainer is null)
            return false;

        lootItemTypeDef.Namespace = "EFT.InventoryLogic";
        lootItemTypeDef.Name = "LootItem";

        lootItemContainer.CleanPropertyFieldNames();
        lootItemContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}