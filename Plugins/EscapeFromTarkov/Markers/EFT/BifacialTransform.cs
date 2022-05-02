using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class BifacialTransform : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var bifacialTransformContainer = containers.FindContainerByFullName("EFT.BifacialTransform");

        if (bifacialTransformContainer is null)
            return false;

        bifacialTransformContainer.CleanPropertyFieldNames();
        bifacialTransformContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}