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

        var typeDefOrRef = inventoryControllerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef();

        if (!typeDefOrRef.IsTypeDef)
            return false;

        var type = typeDefOrRef.ResolveTypeDef();

        if (type is null)
            return false;

        var inventoryControllerContainer = containers.FirstOrDefault(x => x.TypeDef == type);

        if (inventoryControllerContainer is null)
            return false;

        type.Namespace = "EFT";
        type.Name = "InventoryController";

        inventoryControllerContainer.CleanPropertyFieldNames();
        inventoryControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}