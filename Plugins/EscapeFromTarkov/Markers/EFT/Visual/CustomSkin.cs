using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Visual; 

internal class CustomSkin : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var customSkinContainer = containers.FindContainerContainingFullName("EFT.Visual.CustomSkin");

        if (customSkinContainer is null)
            return false;

        customSkinContainer.Name = "CustomSkin";
        customSkinContainer.CleanPropertyFieldNames();
        customSkinContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}