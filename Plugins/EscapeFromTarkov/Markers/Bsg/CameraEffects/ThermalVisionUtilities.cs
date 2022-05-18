using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class ThermalVisionUtilities : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var thermalVisionUtilitiesController = containers.FindContainerByFullName("ThermalVisionUtilities");

        if (thermalVisionUtilitiesController is null)
            return false;

        thermalVisionUtilitiesController.Namespace = "BSG.CameraEffects";

        thermalVisionUtilitiesController.CleanPropertyFieldNames();
        thermalVisionUtilitiesController.ExportNonObfuscatedSymbols();

        return true;
    }
}