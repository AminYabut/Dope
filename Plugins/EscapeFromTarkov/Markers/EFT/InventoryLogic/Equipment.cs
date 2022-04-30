using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class Equipment : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var inventoryContainer = containers.FindContainerByFullName("EFT.Inventory");

        var equipmentControllerFieldContainer = inventoryContainer?.FindFieldContainerByName("Equipment");

        if (equipmentControllerFieldContainer is null)
            return false;

        var equipmentTypeDef = Helpers.GetFieldDefTypeTypeDef(equipmentControllerFieldContainer.FieldDef);

        if (equipmentTypeDef is null)
            return false;

        var equipmentContainer = equipmentTypeDef.ToMetadataContainer(containers);

        if (equipmentContainer is null)
            return false;

        equipmentContainer.Namespace = "EFT.InventoryLogic";
        equipmentContainer.Name = "Equipment";
        
        equipmentContainer.CleanPropertyFieldNames();
        equipmentContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
}