using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Bsg.CameraEffects; 

internal class ValuesCoefs : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var valuesCoefsController = containers.FindContainerByFullName("ValuesCoefs");

        if (valuesCoefsController is null)
            return false;

        valuesCoefsController.Namespace = "BSG.CameraEffects";

        valuesCoefsController.CleanPropertyFieldNames();
        valuesCoefsController.ExportNonObfuscatedSymbols();

        return true;
    }
}