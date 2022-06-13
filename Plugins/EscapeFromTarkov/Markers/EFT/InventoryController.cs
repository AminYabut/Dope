using System.Collections.Immutable;
using System.Linq;

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class InventoryController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var inventoryControllerFieldContainer = playerContainer?.FindFieldContainerByName("_inventoryController");

        if (inventoryControllerFieldContainer is null)
            return false;

        var inventoryControllerTypeDef = inventoryControllerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (inventoryControllerTypeDef is null)
            return false;

        var inventoryControllerContainer = inventoryControllerTypeDef.ToMetadataContainer(containers);

        if (inventoryControllerContainer is null)
            return false;

        var itemLimitsDef = FindItemLimitsField(inventoryControllerTypeDef);
        if (itemLimitsDef is null)
            return false;

        itemLimitsDef.Name = "_LimitedItems";
        
        inventoryControllerTypeDef.Namespace = "EFT";
        inventoryControllerTypeDef.Name = "InventoryController";

        inventoryControllerContainer.CleanPropertyFieldNames();
        inventoryControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    private FieldDef? FindItemLimitsField(TypeDef inventoryControllerContainer)
    {
        MethodDef? isLimitedMethodDef = null;

        foreach (var method in inventoryControllerContainer.Methods) {
            if (method.Parameters.Count is not 4)
                continue;
            if (method.Name != "IsLimitedAtAddress" ||
                method.Parameters[1].Name != "templateId" ||
                method.Parameters[2].Name != "address" || 
                method.Parameters[3].Name != "limit") 
                continue;
            isLimitedMethodDef = method;
            break;
        }

        if (isLimitedMethodDef is null)
            return null;

        if (!isLimitedMethodDef.HasBody)
            return null;
        
        var instructions = isLimitedMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldarg_0 && 
                instructions[i + 1].OpCode == OpCodes.Ldfld && 
                instructions[i + 2].OpCode == OpCodes.Ldarg_1) {
                FieldDef? field = instructions[i + 1].Operand as FieldDef;
                if (field is null)
                    continue;
                return field;
            }
        }
        return null;
    }
}