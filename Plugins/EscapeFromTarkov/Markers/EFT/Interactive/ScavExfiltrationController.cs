using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class ScavExfiltrationPoint : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var scavExfiltrationPointContainer = containers.FindContainerByFullName("EFT.Interactive.ScavExfiltrationPoint");

        if (scavExfiltrationPointContainer is null)
            return false;

        scavExfiltrationPointContainer.CleanPropertyFieldNames();
        scavExfiltrationPointContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}