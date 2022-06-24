using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class MovementController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var movementContextControllerContainer = containers.FindContainerContainingMethodName("CalculateLookAtDirection");
        if (movementContextControllerContainer is null)
            return false;

        var speedLimitContainer = containers.FindContainerByFullName("EFT.Player/ESpeedLimit");
        if (speedLimitContainer is null)
            return false;
        
        var viewAngleFieldDef = FindViewAngleFieldDef(movementContextControllerContainer.TypeDef);
        if (viewAngleFieldDef is null)
            return false;

        viewAngleFieldDef.Name = "_currentViewAngles";
        
        var stateSpeedLimitDef = FindStateSpeedLimit(movementContextControllerContainer.TypeDef);
        if (stateSpeedLimitDef is null)
            return false;

        stateSpeedLimitDef.Name = "_stateSpeedLimits";

        FieldDef? physicalConditionDef = movementContextControllerContainer.Fields.FirstOrDefault(x => x.FieldDef.FieldType.FullName == "EFT.EPhysicalCondition")?.FieldDef;
        if (physicalConditionDef is null)
            return false;

        physicalConditionDef.Name = "_physicalCondition";
        
        movementContextControllerContainer.Namespace = "EFT";
        movementContextControllerContainer.Name = "MovementController";

        movementContextControllerContainer.CleanPropertyFieldNames();
        movementContextControllerContainer.ExportNonObfuscatedSymbols();

        speedLimitContainer.Namespace = "EFT";

        speedLimitContainer.CleanPropertyFieldNames();
        speedLimitContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }

    private FieldDef? FindViewAngleFieldDef(TypeDef movementController) {
        var method = movementController.FindMethod("RestorePreviousYaw");
        if (method is null)
            return null;

        if (!method.HasBody)
            return null;

        var instructions = method.Body.Instructions;
        for (var i = 0; i < instructions.Count - 4; ++i) {
            if (instructions[i].OpCode != OpCodes.Ldflda ||
                instructions[i + 1].OpCode != OpCodes.Ldarg_0 ||
                instructions[i + 2].OpCode != OpCodes.Ldflda ||
                instructions[i + 3].OpCode != OpCodes.Ldfld ||
                instructions[i + 4].OpCode != OpCodes.Stfld)
                continue;

            if (OpCodes.Ldflda == OpCodes.Stfld)
                return null;

            var field = instructions[i].Operand as FieldDef;
            if (field is null)
                continue;

            if (field.FieldType.ScopeType.ToString() == "UnityEngine.Vector2")
                return field;
        }

        return null;
    }

    private FieldDef? FindStateSpeedLimit(TypeDef movementController)
    {
        MethodDef? addStateMethodDef = movementController.Methods.FirstOrDefault(method => method.Name == "AddStateSpeedLimit");
        if (addStateMethodDef is null || !addStateMethodDef.HasBody)
            return null;
        var instructions = addStateMethodDef.Body.Instructions;
        for (int i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode != OpCodes.Ldarg_0 &&
                instructions[i+1].OpCode != OpCodes.Ldfld &&
                instructions[i+2].OpCode != OpCodes.Ldarg_2)
                continue;
            var field = instructions[i + 1].Operand as FieldDef;
            if (field is null)
                return null;
            return field;
        }
        
        return null;
    }
}