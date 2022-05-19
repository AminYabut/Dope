using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Grid : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var gridViewContainer = containers.FindContainerByFullName("EFT.UI.DragAndDrop.GridView");

        if (gridViewContainer is null)
            return false;

        var gridFieldContainer = gridViewContainer.FindFieldContainerByName("Grid");
        
        if (gridFieldContainer is null)
            return false;
        
        var gridTypeDef = gridFieldContainer.FieldDef.FieldType.ScopeType as TypeDef;
        
        if (gridTypeDef is null)
            return false;

        var gridContainer = gridTypeDef.ToMetadataContainer(containers);

        if (gridContainer is null)
            return false;

        gridContainer.Namespace = "EFT.InventoryLogic";
        gridContainer.Name = "Grid";
        
        gridContainer.CleanPropertyFieldNames();
        gridContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
}