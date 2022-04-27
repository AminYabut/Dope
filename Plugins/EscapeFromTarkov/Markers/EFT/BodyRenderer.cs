using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class BodyRenderer : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerBodyContainer = containers.FindContainerByFullName("EFT.PlayerBody");

        var bodyRenderersFieldContainer = playerBodyContainer?.FindFieldContainerByName("_bodyRenderers");

        var bodyRendererTypeDef = bodyRenderersFieldContainer?.FieldDef.FieldType.ScopeType as TypeDef;

        var bodyRendererContainer = bodyRendererTypeDef?.ToMetadataContainer(containers);

        if (bodyRendererContainer is null)
            return false;

        bodyRendererContainer.Namespace = "EFT";
        bodyRendererContainer.Name = "BodyRenderer";

        bodyRendererContainer.CleanPropertyFieldNames();
        bodyRendererContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}