using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_CloudParameters : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var cloudParametersContainer = containers.FindContainerByFullName("TOD_CloudParameters");

        if (cloudParametersContainer is null)
            return false;

        cloudParametersContainer.Namespace = "TOD";

        cloudParametersContainer.CleanPropertyFieldNames();
        cloudParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}