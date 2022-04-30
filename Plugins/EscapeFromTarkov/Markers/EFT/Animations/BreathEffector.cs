using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Animations;

internal class BreathEffector : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var breathEffector = containers.FindContainerByFullName("EFT.Animations.BreathEffector");

        if (breathEffector is null)
            return false;

        breathEffector.CleanPropertyFieldNames();
        breathEffector.ExportNonObfuscatedSymbols();

        return true;
    }
}