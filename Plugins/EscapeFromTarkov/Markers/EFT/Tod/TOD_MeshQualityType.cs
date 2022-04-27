using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_MeshQualityType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var meshQualityTypeContainer = containers.FindContainerByFullName("TOD_MeshQualityType");

        if (meshQualityTypeContainer is null)
            return false;

        meshQualityTypeContainer.Namespace = "TOD";

        meshQualityTypeContainer.CleanPropertyFieldNames();
        meshQualityTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}