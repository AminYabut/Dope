using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class UpdateMode : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var updateModeContainer = containers.FindContainerByFullName("EFT.Player/EUpdateMode");

        if (updateModeContainer is null)
            return false;

        updateModeContainer.Namespace = "EFT";

        updateModeContainer.CleanPropertyFieldNames();
        updateModeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}