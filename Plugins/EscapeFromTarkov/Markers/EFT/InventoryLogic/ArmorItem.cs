using System.Collections.Immutable;
using System.Runtime.InteropServices;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class ArmorItem : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var itemTypeTableDef = containers.FindMethodDefByName("GetItemType");
        if (itemTypeTableDef is null || !itemTypeTableDef.HasBody)
            return false;

        var armorItemTypeDef = FindArmorItem(itemTypeTableDef);
        if (armorItemTypeDef is null)
            return false;

        var armorItemContainer = armorItemTypeDef.ToMetadataContainer(containers);
        if (armorItemContainer is null)
            return false;

        armorItemContainer.Namespace = "EFT.InventoryLogic";
        armorItemContainer.Name = "ArmorItem";
        
        armorItemContainer.CleanPropertyFieldNames();
        armorItemContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    TypeDef? FindArmorItem(MethodDef itemTypeTableDef) {
        var instructions = itemTypeTableDef.Body.Instructions;
        for (int i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldc_I4_8) { //&& (sbyte)instructions[i].Operand == 9
                var armorToken = instructions[i - 5].Operand as TypeDef;
                if (armorToken is null)
                    continue;
                if (!armorToken.Fields.Any(field => field.Name == "Armor"))
                    continue;
                return armorToken;
            }
        }
        return null;
    }
}