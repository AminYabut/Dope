using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive;

internal class WorldInteractiveObject : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldInteractiveObjectContainer = containers.FindContainerByFullName("EFT.Interactive.WorldInteractiveObject");

        if (worldInteractiveObjectContainer is null)
            return false;

        worldInteractiveObjectContainer.CleanPropertyFieldNames();
        worldInteractiveObjectContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}