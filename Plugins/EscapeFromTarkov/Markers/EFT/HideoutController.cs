using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class HideoutController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var clientApplicationContainer = containers.FindContainerByFullName("EFT.ClientApplication");

        if (clientApplicationContainer is null)
            return false;

        var hideoutControllerFieldContainer = clientApplicationContainer.FindFieldContainerByName("_hideout");

        if (hideoutControllerFieldContainer is null)
            return false;

        var hideoutControllerTypeDef = Helpers.GetFieldDefTypeTypeDef(hideoutControllerFieldContainer.FieldDef);

        if (hideoutControllerTypeDef is null)
            return false;

        var hideoutControllerContainer = hideoutControllerTypeDef.ToMetadataContainer(containers);

        if (hideoutControllerContainer is null)
            return false;

        var inventoryController =  hideoutControllerTypeDef.Fields.FirstOrDefault(x =>
            x.FieldType.ScopeType.FullName == "EFT.InventoryController");

        if (inventoryController is null)
            return false;

        inventoryController.Name = "_inventoryController";

        hideoutControllerContainer.Namespace = "EFT";
        hideoutControllerContainer.Name = "HideoutController";

        hideoutControllerContainer.CleanPropertyFieldNames();
        hideoutControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}