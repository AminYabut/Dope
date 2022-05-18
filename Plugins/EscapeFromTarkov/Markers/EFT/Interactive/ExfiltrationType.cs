using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class ExfiltrationType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var exfiltrationTypeContainer = containers.FindContainerByFullName("EFT.Interactive.EExfiltrationType");

        if (exfiltrationTypeContainer is null)
            return false;

        exfiltrationTypeContainer.CleanPropertyFieldNames();
        exfiltrationTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}