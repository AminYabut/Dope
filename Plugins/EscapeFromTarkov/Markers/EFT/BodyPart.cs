using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class BodyPart : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var bodyPartContainer = containers.FindContainerByFullName("EBodyPart");

        if (bodyPartContainer is null)
            return false;
        
        bodyPartContainer.Namespace = "EFT";

        bodyPartContainer.CleanPropertyFieldNames();
        bodyPartContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}