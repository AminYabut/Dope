using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics; 

internal class BallisticPreset : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ballisticsPresetContainer = containers.FindContainerByFullName("BallisticPreset");

        if (ballisticsPresetContainer is null)
            return false;

        ballisticsPresetContainer.Namespace = "EFT.Ballistics";

        ballisticsPresetContainer.CleanPropertyFieldNames();
        ballisticsPresetContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}