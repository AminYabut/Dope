using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class PlayerBody : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerBodyContainer = containers.FindContainerByFullName("EFT.PlayerBody");

        if (playerBodyContainer is null)
            return false;

        var getSlotViewMethodDef = playerBodyContainer.TypeDef.FindMethod("GetSlotViewByItem");

        if (getSlotViewMethodDef is null)
            return false;

        var slotViewDef = getSlotViewMethodDef.ReturnType.TryGetTypeDef();

        if (slotViewDef is null)
            return false;
        

        var slotViewContainer = slotViewDef.ToMetadataContainer(containers);

        if (slotViewContainer is null)
            return false;
        
        slotViewContainer.Namespace = "EFT";
        slotViewContainer.Name = "SlotView";
        
        slotViewContainer.CleanPropertyFieldNames();
        slotViewContainer.ExportNonObfuscatedSymbols();
        
        playerBodyContainer.CleanPropertyFieldNames();
        playerBodyContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}