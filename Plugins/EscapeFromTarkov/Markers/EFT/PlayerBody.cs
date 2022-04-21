using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class PlayerBody : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerBodyContainer = containers.FindContainerByFullName("EFT.PlayerBody");

        if (playerBodyContainer is null)
            return false;

        playerBodyContainer.CleanPropertyFieldNames();
        playerBodyContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}