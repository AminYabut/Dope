using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects;

internal class VisorEffect : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var visorEffectContainer = containers.FindContainerByFullName("VisorEffect");

        if (visorEffectContainer is null)
            return false;

        visorEffectContainer.CleanPropertyFieldNames();
        visorEffectContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}