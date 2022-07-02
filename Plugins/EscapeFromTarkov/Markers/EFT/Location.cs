using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Location : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var locationContainer = containers.FindContainerContainingMethodName("get_NightTimeAllowedLocations");
        if (locationContainer is null)
            return false;

        locationContainer.Namespace = "EFT";
        locationContainer.Name = "Location";
        
        locationContainer.CleanPropertyFieldNames();
        locationContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}