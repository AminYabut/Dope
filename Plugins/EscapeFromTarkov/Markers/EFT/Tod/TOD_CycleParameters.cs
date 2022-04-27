using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_CycleParameters : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var cycleParametersContainer = containers.FindContainerByFullName("TOD_CycleParameters");

        if (cycleParametersContainer is null)
            return false;

        cycleParametersContainer.Namespace = "TOD";

        cycleParametersContainer.CleanPropertyFieldNames();
        cycleParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}