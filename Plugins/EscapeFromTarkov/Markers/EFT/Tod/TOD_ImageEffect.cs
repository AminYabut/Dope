using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_ImageEffect : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var tod_ImageEffectContainer = containers.FindContainerByFullName("TOD_ImageEffect");

        if (tod_ImageEffectContainer is null)
            return false;
        
        tod_ImageEffectContainer.Namespace = "TOD";

        tod_ImageEffectContainer.CleanPropertyFieldNames();
        tod_ImageEffectContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}