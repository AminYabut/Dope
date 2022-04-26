using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class PlayerSide : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerSideContainer = containers.FindContainerByFullName("EFT.EPlayerSide");

        if (playerSideContainer is null)
            return false;

        playerSideContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}