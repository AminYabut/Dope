using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Weather;

internal class WeatherDebug : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var weatherDebugContainer = containers.FindContainerByFullName("EFT.Weather.WeatherDebug");

        if (weatherDebugContainer is null)
            return false;

        weatherDebugContainer.Namespace = "EFT.Weather";
        
        weatherDebugContainer.CleanPropertyFieldNames();
        weatherDebugContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}