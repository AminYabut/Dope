using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ClientLocalGameWorld : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("ClientLocalGameWorld");

        if (worldContainer is null)
            return false;

        worldContainer.Namespace = "EFT";
        
        worldContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}