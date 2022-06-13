using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_Weather : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var tod_WeatherContainer = containers.FindContainerByFullName("TOD_Weather");

        if (tod_WeatherContainer is null)
            return false;

        tod_WeatherContainer.Namespace = "TOD";

        tod_WeatherContainer.CleanPropertyFieldNames();
        tod_WeatherContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}