using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Counters;

internal class CounterTag : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var counterTagContainer = containers.FindContainerByFullName("EFT.Counters.CounterTag");
        if (counterTagContainer is null)
            return false;
        
        counterTagContainer.CleanPropertyFieldNames();
        counterTagContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}