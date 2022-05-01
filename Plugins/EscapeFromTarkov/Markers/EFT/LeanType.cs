using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class LeanType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var leanTypeContainer = containers.FindContainerByFullName("EFT.Player/LeanType");

        if (leanTypeContainer is null)
            return false;

        leanTypeContainer.Namespace = "EFT";

        leanTypeContainer.CleanPropertyFieldNames();
        leanTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}