using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Visual; 

internal class Dress : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var dressContainer = containers.FindContainerByFullName("EFT.Visual.Dress");

        if (dressContainer is null)
            return false;

        dressContainer.CleanPropertyFieldNames();
        dressContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}