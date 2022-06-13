using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Weather;

internal class WeatherController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var weatherControllerContainer = containers.FindContainerByFullName("EFT.Weather.WeatherController");

        if (weatherControllerContainer is null)
            return false;
        
        var tod_ScatteringDef = weatherControllerContainer.TypeDef.Fields.FirstOrDefault(field => field.FieldType.FullName == "TOD.TOD_Scattering");
        if (tod_ScatteringDef is null)
            return false;

        tod_ScatteringDef.Name = "_tod_scattering";
        
        weatherControllerContainer.CleanPropertyFieldNames();
        weatherControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}