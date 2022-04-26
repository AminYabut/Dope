using System.Collections.Immutable;
using System.Linq;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class InventoryController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var inventoryControllerFieldContainer = playerContainer?.FindFieldContainerByName("_inventoryController");

        if (inventoryControllerFieldContainer is null)
            return false;

        var inventoryControllerTypeDef = inventoryControllerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (inventoryControllerTypeDef is null)
            return false;

        var inventoryControllerContainer = inventoryControllerTypeDef.ToMetadataContainer(containers);

        if (inventoryControllerContainer is null)
            return false;

        inventoryControllerTypeDef.Namespace = "EFT";
        inventoryControllerTypeDef.Name = "InventoryController";

        inventoryControllerContainer.CleanPropertyFieldNames();
        inventoryControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}