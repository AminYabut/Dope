using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class CommonUI : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var commonUIContainer = containers.FindContainerByFullName("EFT.UI.CommonUI");

        if (commonUIContainer is null)
            return false;
        
        commonUIContainer.CleanPropertyFieldNames();
        commonUIContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}