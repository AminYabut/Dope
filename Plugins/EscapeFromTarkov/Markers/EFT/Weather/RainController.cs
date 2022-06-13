using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Weather;

internal class RainController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var rainControllerContainer = containers.FindContainerByFullName("RainController");

        if (rainControllerContainer is null)
            return false;

        rainControllerContainer.Namespace = "EFT.Weather";
        
        rainControllerContainer.CleanPropertyFieldNames();
        rainControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}