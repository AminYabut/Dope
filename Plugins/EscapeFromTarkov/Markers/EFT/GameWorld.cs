using System.Collections.Immutable;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using Microsoft.VisualBasic.FileIO;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class GameWorld : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("EFT.GameWorld");

        if (worldContainer is null)
            return false;

        var interactableObjectsDef = FindInteractableObjectField(worldContainer.TypeDef);
        if (interactableObjectsDef is null)
            return false;

        interactableObjectsDef.Name = "_interactableObjects";

        var worldDef = FindWorldField(worldContainer.TypeDef);
        if (worldDef is null)
            return false;

        worldDef.Name = "_world";
        
        worldContainer.CleanPropertyFieldNames();
        worldContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    private FieldDef? FindInteractableObjectField(TypeDef worldContainerTypeDef) {
        return worldContainerTypeDef.Fields.FirstOrDefault(field => field.FieldType.IsGenericInstanceType && field.FieldType.FullName.Contains("EFT.Interactive.WorldInteractiveObject"));
    }
    
    private FieldDef? FindWorldField(TypeDef worldContainerTypeDef)
    {
        var disposeMethodDef = worldContainerTypeDef.FindMethod("Dispose");
        if (disposeMethodDef is null)
            return null;

        if (!disposeMethodDef.HasBody)
            return null;

        var instructions = disposeMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldarg_0 &&
                instructions[i + 1].OpCode == OpCodes.Ldnull &&
                instructions[i + 2].OpCode == OpCodes.Stfld &&
                instructions[i + 3].OpCode == OpCodes.Ret) {
                FieldDef? field = instructions[i + 2].Operand as FieldDef;
                if (field is null)
                    continue;
                
                return field;
            }
        }
        return null;
    }
}