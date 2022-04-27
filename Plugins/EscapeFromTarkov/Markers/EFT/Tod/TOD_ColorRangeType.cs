using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod; 

internal class TOD_ColorRangeType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var colorRangeTypeContainer = containers.FindContainerByFullName("TOD_ColorRangeType");

        if (colorRangeTypeContainer is null)
            return false;

        colorRangeTypeContainer.Namespace = "TOD";

        colorRangeTypeContainer.CleanPropertyFieldNames();
        colorRangeTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}