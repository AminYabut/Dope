using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Animations; 

internal class ProceduralWeaponAnimation : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var proceduralWeaponAnimationContainer = containers.FindContainerByFullName("EFT.Animations.ProceduralWeaponAnimation");

        if (proceduralWeaponAnimationContainer is null)
            return false;

        var opticCalibrationPointField = FindOpticCalibrationPointField(proceduralWeaponAnimationContainer.TypeDef);

        if (opticCalibrationPointField is null)
            return false;

        opticCalibrationPointField.Name = "_opticCalibrationPoint";

        proceduralWeaponAnimationContainer.CleanPropertyFieldNames();
        proceduralWeaponAnimationContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private FieldDef? FindOpticCalibrationPointField(TypeDef proceduralWeaponAnimationTypeDef) {
        var calculateLocalSightTargetMethodDef = proceduralWeaponAnimationTypeDef.FindMethod("CalculateLocalSightTarget");

        if (calculateLocalSightTargetMethodDef is null)
            return null;

        if (!calculateLocalSightTargetMethodDef.HasBody)
            return null;

        var instructions = calculateLocalSightTargetMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Callvirt && instructions[i + 1].OpCode == OpCodes.Stfld) {
                var method = instructions[i].Operand as MethodDef;
                var field = instructions[i + 1].Operand as FieldDef;

                if (method is null || field is null)
                    continue;

                var fieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(field.FieldType);

                if (fieldTypeTypeDef is null)
                    continue;

                if (method.Name == "GetCurrentOpticCalibrationPoint" &&
                    fieldTypeTypeDef.FullName == "UnityEngine.Vector3")
                    return field;
            }
        }

        return null;
    }
}