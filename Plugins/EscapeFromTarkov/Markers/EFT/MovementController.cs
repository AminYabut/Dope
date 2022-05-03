using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class MovementController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var property = playerContainer?.TypeDef.FindProperty("MovementContext");

        var typeDefOrRef = property?.GetMethod.ReturnType.ToTypeDefOrRef();

        if (typeDefOrRef is null || !typeDefOrRef.IsTypeDef)
            return false;

        var type = (TypeDef) typeDefOrRef;

        var movementContextControllerContainer = containers.FirstOrDefault(x => x.TypeDef == type);

        if (movementContextControllerContainer is null)
            return false;

        var viewAngleFieldDef = FindViewAngleFieldDef(type);
        if (viewAngleFieldDef is null)
            return false;

        viewAngleFieldDef.Name = "_currentViewAngles";

        type.Namespace = "EFT";
        type.Name = "MovementController";

        movementContextControllerContainer.CleanPropertyFieldNames();
        movementContextControllerContainer.ExportNonObfuscatedSymbols();

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
}