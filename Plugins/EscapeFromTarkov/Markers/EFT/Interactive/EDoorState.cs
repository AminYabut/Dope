using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class EDoorState : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var doorStateContainer = containers.FindContainerByFullName("EFT.Interactive.EDoorState");

        if (doorStateContainer is null)
            return false;
        
        
        doorStateContainer.CleanPropertyFieldNames();
        doorStateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}