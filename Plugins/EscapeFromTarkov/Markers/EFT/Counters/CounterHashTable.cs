using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Counters;

internal class CounterHashTable : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var counterHashTableContainer = containers.FindContainerContainingFieldName("SessionToOverallMaxCounters");
        if (counterHashTableContainer is null)
            return false;

        counterHashTableContainer.Namespace = "EFT.Counters";
        counterHashTableContainer.Name = "CounterHashTable";
        
        counterHashTableContainer.CleanPropertyFieldNames();
        counterHashTableContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}