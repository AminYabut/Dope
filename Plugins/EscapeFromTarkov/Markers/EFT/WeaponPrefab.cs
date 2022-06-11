using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class WeaponPrefab : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var weaponPrefabContainer = containers.FindContainerByFullName("WeaponPrefab");

        if (weaponPrefabContainer is null)
            return false;

        weaponPrefabContainer.Namespace = "EFT";
        
        weaponPrefabContainer.CleanPropertyFieldNames();
        weaponPrefabContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}