using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using Microsoft.VisualBasic.FileIO;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class ItemHandsController : IMarker
{
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var itemHandsControllerContainer = containers.FindContainerByFullName("EFT.Player/ItemHandsController");

        if (itemHandsControllerContainer is null)
            return false;

        foreach(var field in itemHandsControllerContainer.Fields)
            if (field.FieldDef.FieldType.FullName == "EFT.InventoryLogic.Item")
                field.Name = "_item";
        
        itemHandsControllerContainer.Namespace = "EFT";

        itemHandsControllerContainer.CleanPropertyFieldNames();
        itemHandsControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}