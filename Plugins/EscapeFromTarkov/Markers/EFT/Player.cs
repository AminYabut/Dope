using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class Player : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        if (playerContainer is null)
            return false;

        playerContainer.CleanPropertyFieldNames();
        playerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}