using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class EFTHardSettings : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var eftHardSettingsContainer = containers.FindContainerByFullName("EFTHardSettings");

        if (eftHardSettingsContainer is null)
            return false;
        
        eftHardSettingsContainer.Namespace = "EFT";

        eftHardSettingsContainer.CleanPropertyFieldNames();
        eftHardSettingsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}