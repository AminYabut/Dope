using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod; 

internal class TOD_SkyQualityType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skyQualityTypeContainer = containers.FindContainerByFullName("TOD_SkyQualityType");

        if (skyQualityTypeContainer is null)
            return false;

        skyQualityTypeContainer.Namespace = "TOD";

        skyQualityTypeContainer.CleanPropertyFieldNames();
        skyQualityTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}