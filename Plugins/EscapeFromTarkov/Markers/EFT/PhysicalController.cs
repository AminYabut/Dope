using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class PhysicalController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        if (playerContainer is null)
            return false;

        var physicalFieldContainer = playerContainer.FindFieldContainerByName("Physical");

        if (physicalFieldContainer is null)
            return false;

        var physicalControllerTypeDef = Helpers.GetFieldDefTypeTypeDef(physicalFieldContainer.FieldDef);

        if (physicalControllerTypeDef is null)
            return false;
        
        var physicalControllerContainer = physicalControllerTypeDef.ToMetadataContainer(containers);

        if (physicalControllerContainer is null)
            return false;

        physicalControllerContainer.Namespace = "EFT";
        physicalControllerContainer.Name = "PhysicalController";

        physicalControllerContainer.CleanPropertyFieldNames();
        physicalControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}