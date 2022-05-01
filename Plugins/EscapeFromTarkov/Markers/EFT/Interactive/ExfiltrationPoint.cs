using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class ExfiltrationPoint : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var exfiltrationPointContainer = containers.FindContainerByFullName("EFT.Interactive.ExfiltrationPoint");

        if (exfiltrationPointContainer is null)
            return false;

        exfiltrationPointContainer.CleanPropertyFieldNames();
        exfiltrationPointContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}