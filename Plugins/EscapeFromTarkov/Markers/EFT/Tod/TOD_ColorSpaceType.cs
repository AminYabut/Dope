using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_ColorSpaceType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var colorSpaceTypeContainer = containers.FindContainerByFullName("TOD_ColorSpaceType");

        if (colorSpaceTypeContainer is null)
            return false;

        colorSpaceTypeContainer.Namespace = "TOD";

        colorSpaceTypeContainer.CleanPropertyFieldNames();
        colorSpaceTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}