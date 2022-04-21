using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Inventory : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var inventoryControllerFieldContainer = playerContainer?.FindFieldContainerByName("_inventoryController");

        if (inventoryControllerFieldContainer is null)
            return false;

        var typeDefOrRef = inventoryControllerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef();

        if (!typeDefOrRef.IsTypeDef)
            return false;
        
        var type = typeDefOrRef.ResolveTypeDef();

        if (type is null)
            return false;

        var inventoryControllerContainer = containers.FirstOrDefault(x => x.TypeDef == type);

        if (inventoryControllerContainer is null)
            return false;

        var property = type.FindProperty("Inventory");

        if (property is null)
            return false;

        typeDefOrRef = property.GetMethod.ReturnType.ToTypeDefOrRef();

        if (!typeDefOrRef.IsTypeDef)
            return false;

        type = (TypeDef) typeDefOrRef;

        var inventoryContainer = type.ToMetadataContainer(containers);

        if (inventoryContainer is null)
            return false;
        
        type.Namespace = "EFT";
        type.Name = "Inventory";

        inventoryContainer.CleanPropertyFieldNames();
        inventoryContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}