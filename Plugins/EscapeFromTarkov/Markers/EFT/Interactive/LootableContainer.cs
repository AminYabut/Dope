using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive;

internal class LootableContainer : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var lootableContainerContainer = containers.FindContainerByFullName("EFT.Interactive.LootableContainer");

        if (lootableContainerContainer is null)
            return false;

        lootableContainerContainer.CleanPropertyFieldNames();
        lootableContainerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}