using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class RaidSettings : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var raidSettingsContainer = containers.FindContainerByFullName("EFT.RaidSettings");
        if (raidSettingsContainer is null)
            return false;

        raidSettingsContainer.CleanPropertyFieldNames();
        raidSettingsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}