using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Inventory : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var inventoryControllerContainer = containers.FindContainerByFullName("EFT.InventoryController");

        if (inventoryControllerContainer is null)
            return false;

        var type = inventoryControllerContainer.TypeDef;

        var property = type.FindProperty("Inventory");

        if (property is null)
            return false;

        var typeDefOrRef = property.GetMethod.ReturnType.ToTypeDefOrRef();

        if (!typeDefOrRef.IsTypeDef)
            return false;

        type = (TypeDef) typeDefOrRef;

        var inventoryContainer = type.ToMetadataContainer(containers);

        if (inventoryContainer is null)
            return false;
        
        type.Namespace = "EFT";
        type.Name = "Inventory";

        inventoryContainer.CleanPropertyFieldNames();
        inventoryContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}