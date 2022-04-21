using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics; 

internal class BallisticsCalculator : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ballisticsCalculatorContainer = containers.FindContainerByFullName("EFT.Ballistics.BallisticsCalculator");

        if (ballisticsCalculatorContainer is null)
            return false;

        ballisticsCalculatorContainer.CleanPropertyFieldNames();
        ballisticsCalculatorContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}