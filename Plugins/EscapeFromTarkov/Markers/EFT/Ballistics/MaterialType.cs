using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics; 

internal class MaterialType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var materialTypeContainer = containers.FindContainerByFullName("EFT.Ballistics.MaterialType");

        if (materialTypeContainer is null)
            return false;

        materialTypeContainer.CleanPropertyFieldNames();
        materialTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}