using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class ArmorComponent : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var armorComponentContainer = containers.FindContainerByFullName("EFT.InventoryLogic.ArmorComponent");

        if (armorComponentContainer is null)
            return false;

        armorComponentContainer.CleanPropertyFieldNames();
        armorComponentContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}