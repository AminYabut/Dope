using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class MotionEffector : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var motionEffectorContainer = containers.FindContainerByFullName("MotionEffector");

        if (motionEffectorContainer is null)
            return false;

        motionEffectorContainer.Namespace = "EFT";

        motionEffectorContainer.CleanPropertyFieldNames();
        motionEffectorContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}