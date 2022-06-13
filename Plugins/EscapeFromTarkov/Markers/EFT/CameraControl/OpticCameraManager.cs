using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.CameraControl;

internal class OpticCameraManager : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var opticCameraManagerDef = containers.FindMethodDefByName("get_OpticCameraManager");
        if (opticCameraManagerDef is null)
            return false;

        var opticCameraManagerTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(opticCameraManagerDef.ReturnType);
        if (opticCameraManagerTypeDef is null)
            return false;
        
        var opticCameraManagerContainer = opticCameraManagerTypeDef.ToMetadataContainer(containers);
        if (opticCameraManagerContainer is null)
            return false;

        var thermalDef = findThermalFieldDef(opticCameraManagerTypeDef);
        if (thermalDef is null)
            return false;

        thermalDef.Name = "_thermalVision";

        opticCameraManagerContainer.Namespace = "EFT.CameraControl";
        opticCameraManagerContainer.Name = "OpticCameraManager";
        
        opticCameraManagerContainer.CleanPropertyFieldNames();
        opticCameraManagerContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    FieldDef? findThermalFieldDef(TypeDef opticCameraManagerTypeDef) {
        return opticCameraManagerTypeDef.Fields.FirstOrDefault(field => field.FieldType.FullName == "BSG.CameraEffects.ThermalVision");
    }
}