using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class ExitTriggerSettings : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var exitTriggerSettingsContainer = containers.FindContainerByFullName("EFT.Interactive.ExitTriggerSettings");

        if (exitTriggerSettingsContainer is null)
            return false;

        exitTriggerSettingsContainer.CleanPropertyFieldNames();
        exitTriggerSettingsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}