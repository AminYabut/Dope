using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class WeaponTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var weaponTemplateContainer = containers.FindContainerByFullName("EFT.InventoryLogic.WeaponTemplate");

        if (weaponTemplateContainer is null)
            return false;

        weaponTemplateContainer.CleanPropertyFieldNames();
        weaponTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}