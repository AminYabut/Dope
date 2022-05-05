﻿using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class LootItem : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var lootItemContainer = containers.FindContainerByFullName("EFT.InventoryLogic.LootItem");

        if (lootItemContainer is null)
            return false;

        lootItemContainer.CleanPropertyFieldNames();
        lootItemContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}