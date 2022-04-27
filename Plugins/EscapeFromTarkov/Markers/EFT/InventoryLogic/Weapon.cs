using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class Weapon : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var weaponContainer = containers.FindContainerByFullName("EFT.InventoryLogic.Weapon");

        if (weaponContainer is null)
            return false;

        weaponContainer.CleanPropertyFieldNames();
        weaponContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}