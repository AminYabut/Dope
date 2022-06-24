using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic;

internal class TacticalRigArmorItem : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var itemTypeTableDef = containers.FindMethodDefByName("GetItemType");
        if (itemTypeTableDef is null || !itemTypeTableDef.HasBody)
            return false;

        var tacticalRigArmorItemTypeDef = FindArmorRigItem(itemTypeTableDef);
        if (tacticalRigArmorItemTypeDef is null)
            return false;

        var tacticalRigArmorItemContainer = tacticalRigArmorItemTypeDef.ToMetadataContainer(containers);
        if (tacticalRigArmorItemContainer is null)
            return false;

        tacticalRigArmorItemContainer.Namespace = "EFT.InventoryLogic";
        tacticalRigArmorItemContainer.Name = "TacticalRigArmorItem";
        
        tacticalRigArmorItemContainer.CleanPropertyFieldNames();
        tacticalRigArmorItemContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    TypeDef? FindArmorRigItem(MethodDef itemTypeTableDef) {
        var instructions = itemTypeTableDef.Body.Instructions;
        for (int i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldc_I4_S && (sbyte)instructions[i].Operand == 9) {
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