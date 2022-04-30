using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class WildSpawnType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var wildSpawnTypeContainer = containers.FindContainerByFullName("EFT.WildSpawnType");

        if (wildSpawnTypeContainer is null)
            return false;

        wildSpawnTypeContainer.CleanPropertyFieldNames();
        wildSpawnTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}