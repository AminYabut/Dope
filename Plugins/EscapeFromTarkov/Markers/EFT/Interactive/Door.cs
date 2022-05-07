using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class Door : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var DoorContainer = containers.FindContainerByFullName("EFT.Interactive.Door");

        if (DoorContainer is null)
            return false;

        DoorContainer.CleanPropertyFieldNames();
        DoorContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}