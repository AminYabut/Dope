using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Counters;

internal class CounterValueType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var counterValueTypeContainer = containers.FindContainerByFullName("EFT.Counters.CounterValueType");
        if (counterValueTypeContainer is null)
            return false;
        
        counterValueTypeContainer.CleanPropertyFieldNames();
        counterValueTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}