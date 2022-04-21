using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ClientGameWorld : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("EFT.ClientGameWorld");

        if (worldContainer is null)
            return false;

        worldContainer.CleanPropertyFieldNames();
        worldContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}