using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class ThermalVision : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var thermalVisionController = containers.FindContainerByFullName("ThermalVision");

        if (thermalVisionController is null)
            return false;

        var materialDef = thermalVisionController.Fields.FirstOrDefault(field => field.FieldDef.FieldType.FullName == "UnityEngine.Material")?.FieldDef;
        if (materialDef is null)
            return false;

        materialDef.Name = "_Material";
        
        thermalVisionController.Namespace = "BSG.CameraEffects";

        thermalVisionController.CleanPropertyFieldNames();
        thermalVisionController.ExportNonObfuscatedSymbols();

        return true;
    }
}