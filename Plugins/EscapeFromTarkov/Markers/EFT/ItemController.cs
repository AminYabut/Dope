using System.Collections.Immutable;
using System.Linq;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class ItemController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        
        var lootItemContainer = containers.FindContainerByFullName("EFT.Interactive.LootItem");

        var ItemControllerFieldContainer = lootItemContainer?.FindFieldContainerByName("ItemOwner");

        if (ItemControllerFieldContainer is null)
            return false;

        var ItemControllerTypeDef = ItemControllerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (ItemControllerTypeDef is null)
            return false;

        var ItemControllerContainer = ItemControllerTypeDef.ToMetadataContainer(containers);

        if (ItemControllerContainer is null)
            return false;

        ItemControllerTypeDef.Namespace = "EFT.InventoryLogic";
        ItemControllerTypeDef.Name = "ItemController";

        ItemControllerContainer.CleanPropertyFieldNames();
        ItemControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}