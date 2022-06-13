using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_Time : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var tod_TimeContainer = containers.FindContainerByFullName("TOD_Time");

        if (tod_TimeContainer is null)
            return false;

        tod_TimeContainer.Namespace = "TOD";

        tod_TimeContainer.CleanPropertyFieldNames();
        tod_TimeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}