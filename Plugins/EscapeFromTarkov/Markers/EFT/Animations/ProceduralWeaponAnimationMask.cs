using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Animations; 

internal class ProceduralWeaponAnimationMask : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var proceduralWeaponAnimationMaskContainer = containers.FindContainerByFullName("EFT.Animations.EProceduralAnimationMask");

        if (proceduralWeaponAnimationMaskContainer is null)
            return false;

        proceduralWeaponAnimationMaskContainer.CleanPropertyFieldNames();
        proceduralWeaponAnimationMaskContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}