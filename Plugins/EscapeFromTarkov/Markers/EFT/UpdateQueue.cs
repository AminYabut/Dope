using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class UpdateQueue : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var updateQueueContainer = containers.FindContainerByFullName("EFT.EUpdateQueue");

        if (updateQueueContainer is null)
            return false;

        updateQueueContainer.CleanPropertyFieldNames();
        updateQueueContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}