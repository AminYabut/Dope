using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class AmmoTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ammoTemplateContainer = containers.FindContainerByFullName("EFT.InventoryLogic.AmmoTemplate");

        if (ammoTemplateContainer is null)
            return false;

        ammoTemplateContainer.CleanPropertyFieldNames();
        ammoTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}