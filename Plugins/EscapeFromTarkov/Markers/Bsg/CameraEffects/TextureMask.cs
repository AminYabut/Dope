using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class TextureMask : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var textureMaskController = containers.FindContainerByFullName("BSG.CameraEffects.TextureMask");

        if (textureMaskController is null)
            return false;

        textureMaskController.CleanPropertyFieldNames();
        textureMaskController.ExportNonObfuscatedSymbols();

        return true;
    }
}