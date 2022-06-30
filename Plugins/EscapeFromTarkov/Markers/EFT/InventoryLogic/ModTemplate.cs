using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class ModTemplate : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var modTemplateContainer = containers.FindContainerByFullName("EFT.InventoryLogic.ModTemplate");
        if (modTemplateContainer is null)
            return false;
        
        modTemplateContainer.CleanPropertyFieldNames();
        modTemplateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}