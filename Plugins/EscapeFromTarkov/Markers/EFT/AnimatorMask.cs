using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class AnimatorMask : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var animatorMaskContainer = containers.FindContainerByFullName("EFT.Player/EAnimatorMask");

        if (animatorMaskContainer is null)
            return false;

        animatorMaskContainer.Namespace = "EFT";

        animatorMaskContainer.CleanPropertyFieldNames();
        animatorMaskContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}