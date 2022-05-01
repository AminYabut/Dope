using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ClientApplication : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var clientApplicationContainer = containers.FindContainerByFullName("EFT.ClientApplication");

        if (clientApplicationContainer is null)
            return false;

        clientApplicationContainer.CleanPropertyFieldNames();
        clientApplicationContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}