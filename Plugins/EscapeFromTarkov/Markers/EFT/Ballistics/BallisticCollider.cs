using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics; 

internal class BallisticCollider : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ballisticsColliderContainer = containers.FindContainerByFullName("EFT.Ballistics.BallisticCollider");

        if (ballisticsColliderContainer is null)
            return false;

        ballisticsColliderContainer.CleanPropertyFieldNames();
        ballisticsColliderContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}