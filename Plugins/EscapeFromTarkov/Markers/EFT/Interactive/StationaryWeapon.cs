using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class StationaryWeapon : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var stationaryWeaponContainer = containers.FindContainerByFullName("EFT.Interactive.StationaryWeapon");

        if (stationaryWeaponContainer is null)
            return false;

        stationaryWeaponContainer.CleanPropertyFieldNames();
        stationaryWeaponContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}