using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Visual; 

internal class LoddedSkin : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var loddedSkinContainer = containers.FindContainerByFullName("EFT.Visual.LoddedSkin");

        if (loddedSkinContainer is null)
            return false;

        loddedSkinContainer.CleanPropertyFieldNames();
        loddedSkinContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}