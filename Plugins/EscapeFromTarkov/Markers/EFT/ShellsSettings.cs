using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ShellsSettings : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var shellsSettingsContainer = containers.FindContainerByFullName("EFT.EFTHardSettings/ShellsSettings");

        if (shellsSettingsContainer is null)
            return false;

        shellsSettingsContainer.Namespace = "EFT";
        shellsSettingsContainer.Name = "ShellsSettings";

        shellsSettingsContainer.CleanPropertyFieldNames();
        shellsSettingsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}