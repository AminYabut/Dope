using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class DamageInfo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var inventoryControllerFieldContainer = playerContainer?.FindFieldContainerByName("LastDamageInfo");

        if (inventoryControllerFieldContainer is null)
            return false;

        var inventoryControllerTypeDef = Helpers.GetFieldDefTypeTypeDef(inventoryControllerFieldContainer.FieldDef);

        if (inventoryControllerTypeDef is null)
            return false;

        var inventoryControllerContainer = inventoryControllerTypeDef.ToMetadataContainer(containers);

        if (inventoryControllerContainer is null)
            return false;

        inventoryControllerTypeDef.Namespace = "EFT";
        inventoryControllerTypeDef.Name = "DamageInfo";

        inventoryControllerContainer.CleanPropertyFieldNames();
        inventoryControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}