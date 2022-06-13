using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.CameraControl;

internal class OpticSight : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var opticSightContainer = containers.FindContainerByFullName("EFT.CameraControl.OpticSight");
        if (opticSightContainer is null)
            return false;

        opticSightContainer.CleanPropertyFieldNames();
        opticSightContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}