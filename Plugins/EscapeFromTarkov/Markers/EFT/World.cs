using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class World : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("EFT.World");

        if (worldContainer is null)
            return false;

        var interactableObjects = findInteractableObjectsFieldDef(worldContainer.TypeDef);
        if (interactableObjects is null)
            return false;

        interactableObjects.Name = "_interactableObjects";
        
        worldContainer.CleanPropertyFieldNames();
        worldContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    FieldDef? findInteractableObjectsFieldDef(TypeDef worldContainerTypeDef) {
        var findDoorMethodDef = worldContainerTypeDef.FindMethod("FindDoor");
        if (findDoorMethodDef is null)
            return null;

        if (!findDoorMethodDef.HasBody)
            return null;

        var instructions = findDoorMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldarg_0 && instructions[i + 1].OpCode == OpCodes.Ldfld && instructions[i + 2].OpCode == OpCodes.Ldarg_1) {
                FieldDef? field = instructions[i + 1].Operand as FieldDef;
                if (field is null)
                    continue;
                
                return field;
            }
        }
        return null;
    }
}