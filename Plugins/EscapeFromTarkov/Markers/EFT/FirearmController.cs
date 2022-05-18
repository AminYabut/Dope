using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class FirearmController: IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var firearmControllerContainer = containers.FindContainerByFullName("EFT.Player/FirearmController");

        if (firearmControllerContainer is null)
            return false;

        firearmControllerContainer.Namespace = "EFT";

        var (moaDef, shotGunDispersionDef) = FindMoaAndShotgunDispersionFields(firearmControllerContainer.TypeDef);

        if (moaDef is null || shotGunDispersionDef is null)
            return false;

        moaDef.Name = "_moa";

        shotGunDispersionDef.Name = "_shotgunDispersion";
        
        firearmControllerContainer.CleanPropertyFieldNames();
        firearmControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private Tuple<FieldDef?, FieldDef?> FindMoaAndShotgunDispersionFields(TypeDef firearmControllerContainerTypeDef) {
        var weaponModifiedTargetMethodDef = firearmControllerContainerTypeDef.FindMethod("WeaponModified");

        if (weaponModifiedTargetMethodDef is null)
            return new (null, null);

        if (!weaponModifiedTargetMethodDef.HasBody)
            return new (null, null);

        FieldDef? moaDef = null;
        FieldDef? shotgunDispersionDef = null;

        var instructions = weaponModifiedTargetMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Callvirt) {
                var method = instructions[i].Operand as MethodDef;
                FieldDef? field = instructions[i + 1].Operand as FieldDef;
                if (field is null && instructions[i + 1].OpCode == OpCodes.Br)
                    field = ((Instruction)instructions[i + 1].Operand).Operand as FieldDef;

                if (method is null || field is null)
                    continue;

                var fieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(field.FieldType);

                if (fieldTypeTypeDef is null)
                    continue;
                
                if (moaDef is null && method.Name == "GetTotalCenterOfImpact" &&
                    fieldTypeTypeDef.FullName == "System.Single")
                    moaDef = field;
                
                if (shotgunDispersionDef is null && method.Name == "get_TotalShotgunDispersion" &&
                    fieldTypeTypeDef.FullName == "System.Single")
                    shotgunDispersionDef = field;
            }

            if (moaDef is not null && shotgunDispersionDef is not null)
                return new Tuple<FieldDef?, FieldDef?>(moaDef, shotgunDispersionDef);
        }

        return new (null, null);
    }
}