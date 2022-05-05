using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Throwable : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var throwableContainer = containers.FindContainerByFullName("Throwable");

        if (throwableContainer is null)
            return false;

        throwableContainer.Namespace = "EFT";

        throwableContainer.CleanPropertyFieldNames();
        throwableContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}