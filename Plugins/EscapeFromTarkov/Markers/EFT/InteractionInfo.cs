using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class InteractionInfo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var movementControllerContainer = containers.FindContainerByFullName("EFT.MovementController");

        if (movementControllerContainer is null)
            return false;

        var interactionInfoFieldContainer = movementControllerContainer.FindFieldContainerByName("InteractionInfo");

        if (interactionInfoFieldContainer is null)
            return false;
        
        var interactionInfoTypeDef = interactionInfoFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (interactionInfoTypeDef is null)
            return false;
        
        var interactionInfoContainer = interactionInfoTypeDef.ToMetadataContainer(containers);

        if (interactionInfoContainer is null)
            return false;

        interactionInfoContainer.Namespace = "EFT";
        interactionInfoContainer.Name = "InteractionInfo";

        interactionInfoContainer.CleanPropertyFieldNames();
        interactionInfoContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}