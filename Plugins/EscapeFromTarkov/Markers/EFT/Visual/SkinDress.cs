using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Visual; 

internal class SkinDress : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skinDressContainer = containers.FindContainerByFullName("EFT.Visual.SkinDress");

        if (skinDressContainer is null)
            return false;

        skinDressContainer.CleanPropertyFieldNames();
        skinDressContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}