using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class UIList : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var inventoryControllerContainer = containers.FindContainerByFullName("EFT.InventoryController");
        if (inventoryControllerContainer is null)
            return false;
         
        var uiListFieldContainer = inventoryControllerContainer.FindFieldContainerByName("SearchOperations");
        if (uiListFieldContainer is null)
            return false;

        var uiListTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(uiListFieldContainer.FieldDef.FieldType);
        if (uiListTypeDef is null)
            return false;
        
        var uiListContainer = uiListTypeDef.ToMetadataContainer(containers);
        if (uiListContainer is null)
            return false;
        
        uiListContainer.Namespace = "EFT.UI";
        uiListContainer.Name = "UIList";

        FieldDef? listDef = uiListContainer.TypeDef.Fields.FirstOrDefault(field => field.FullName.Contains("System.Collections.Generic.List`1"));
        if (listDef is null)
            return false;
        
        listDef.Name = "_list";
        
        uiListContainer.CleanPropertyFieldNames();
        uiListContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}