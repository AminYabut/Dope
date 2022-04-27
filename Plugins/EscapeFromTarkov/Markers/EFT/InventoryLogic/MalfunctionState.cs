using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class MalfunctionState : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var malfunctionStateContainer = containers.FindContainerByFullName("EFT.InventoryLogic.Weapon/EMalfunctionState");

        if (malfunctionStateContainer is null)
            return false;

        malfunctionStateContainer.CleanPropertyFieldNames();
        malfunctionStateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}