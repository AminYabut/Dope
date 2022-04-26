using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class FirearmController: IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var firearmControllerContainer = containers.FindContainerByFullName("EFT.Player/FirearmController");

        if (firearmControllerContainer is null)
            return false;

        firearmControllerContainer.CleanPropertyFieldNames();
        firearmControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}