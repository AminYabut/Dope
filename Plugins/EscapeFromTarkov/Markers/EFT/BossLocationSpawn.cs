using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class BossLocationSpawn : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var bossLocationSpawnContainer = containers.FindContainerByFullName("BossLocationSpawn");

        if (bossLocationSpawnContainer is null)
            return false;

        bossLocationSpawnContainer.Namespace = "EFT";
        
        bossLocationSpawnContainer.CleanPropertyFieldNames();
        bossLocationSpawnContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}