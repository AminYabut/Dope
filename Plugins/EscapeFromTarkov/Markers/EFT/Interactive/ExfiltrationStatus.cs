using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class ExfiltrationStatus : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var exfiltrationStatusContainer = containers.FindContainerByFullName("EFT.Interactive.EExfiltrationStatus");

        if (exfiltrationStatusContainer is null)
            return false;

        exfiltrationStatusContainer.CleanPropertyFieldNames();
        exfiltrationStatusContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}