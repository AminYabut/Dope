using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class MainApplication : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var mainApplicationContainer = containers.FindContainerByFullName("EFT.MainApplication");

        if (mainApplicationContainer is null)
            return false;

        mainApplicationContainer.CleanPropertyFieldNames();
        mainApplicationContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}