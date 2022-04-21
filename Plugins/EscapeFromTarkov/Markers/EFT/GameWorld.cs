using System.Collections.Immutable;
using System.Linq;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class GameWorld : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("EFT.GameWorld");

        if (worldContainer is null)
            return false;

        worldContainer.CleanPropertyFieldNames();
        worldContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}