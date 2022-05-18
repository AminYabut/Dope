using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Visual; 

internal class TorsoSkin : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var torsoSkinContainer = containers.FindContainerByFullName("EFT.Visual.TorsoSkin");

        if (torsoSkinContainer is null)
            return false;

        torsoSkinContainer.CleanPropertyFieldNames();
        torsoSkinContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}