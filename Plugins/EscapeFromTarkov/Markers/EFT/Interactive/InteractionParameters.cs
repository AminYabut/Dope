using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class InteractionParameters : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.MovementController");

        var interactionParametersFieldContainer = playerContainer?.FindFieldContainerByName("InteractionParameters");

        if (interactionParametersFieldContainer is null)
            return false;

        var interactionParametersTypeDef = interactionParametersFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (interactionParametersTypeDef is null)
            return false;

        var interactionParametersContainer = interactionParametersTypeDef.ToMetadataContainer(containers);

        if (interactionParametersContainer is null)
            return false;

        interactionParametersTypeDef.Namespace = "EFT.Interactive";
        interactionParametersTypeDef.Name = "InteractionParameters";

        interactionParametersContainer.CleanPropertyFieldNames();
        interactionParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}