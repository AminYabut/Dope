using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Counters;

internal class CounterDictionary : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var counterDictionaryContainer = containers.FindContainerContainingMethodName("MaxLong");
        if (counterDictionaryContainer is null)
            return false;

        counterDictionaryContainer.Namespace = "EFT.Counters";
        counterDictionaryContainer.Name = "CounterDictionary";
        
        counterDictionaryContainer.CleanPropertyFieldNames();
        counterDictionaryContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}