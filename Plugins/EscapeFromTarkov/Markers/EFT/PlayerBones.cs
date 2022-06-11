using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class PlayerBones : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerBonesContainer = containers.FindContainerByFullName("PlayerBones");

        if (playerBonesContainer is null)
            return false;

        playerBonesContainer.Namespace = "EFT";
        
        playerBonesContainer.CleanPropertyFieldNames();
        playerBonesContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}