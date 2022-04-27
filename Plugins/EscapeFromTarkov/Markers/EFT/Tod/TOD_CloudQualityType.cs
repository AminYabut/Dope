using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod; 

internal class TOD_CloudQualityType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var colorRangeTypeContainer = containers.FindContainerByFullName("TOD_CloudQualityType");

        if (colorRangeTypeContainer is null)
            return false;

        colorRangeTypeContainer.Namespace = "TOD";

        colorRangeTypeContainer.CleanPropertyFieldNames();
        colorRangeTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}