using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class Switch : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var switchContainer = containers.FindContainerByFullName("EFT.Interactive.Switch");

        if (switchContainer is null)
            return false;

        switchContainer.CleanPropertyFieldNames();
        switchContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}