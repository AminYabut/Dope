using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class ThermalVision : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var thermalVisionController = containers.FindContainerByFullName("ThermalVision");

        if (thermalVisionController is null)
            return false;

        thermalVisionController.CleanPropertyFieldNames();
        thermalVisionController.ExportNonObfuscatedSymbols();

        return true;
    }
}