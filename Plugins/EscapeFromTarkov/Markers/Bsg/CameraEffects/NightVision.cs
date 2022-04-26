using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class NightVision : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var nightVisionController = containers.FindContainerByFullName("BSG.CameraEffects.NightVision");

        if (nightVisionController is null)
            return false;

        nightVisionController.CleanPropertyFieldNames();
        nightVisionController.ExportNonObfuscatedSymbols();

        return true;
    }
}