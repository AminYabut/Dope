using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class TurnAwayEffector : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var turnAwayEffectorContainer = containers.FindContainerByFullName("TurnAwayEffector");

        if (turnAwayEffectorContainer is null)
            return false;

        turnAwayEffectorContainer.Namespace = "EFT";

        turnAwayEffectorContainer.CleanPropertyFieldNames();
        turnAwayEffectorContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}