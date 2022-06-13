using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.CameraControl;

internal class CameraManager : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var cameraManagerContainer = containers.FindContainerContainingMethodName("get_OpticCameraManager");
        if (cameraManagerContainer is null)
            return false;

        cameraManagerContainer.Namespace = "EFT.CameraControl";
        cameraManagerContainer.Name = "CameraManager";
        
        cameraManagerContainer.CleanPropertyFieldNames();
        cameraManagerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}