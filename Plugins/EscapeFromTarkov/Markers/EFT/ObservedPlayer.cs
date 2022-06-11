using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class ObservedPlayer : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var observedPlayer = containers.FindContainerByFullName("EFT.ObservedPlayer");

        if (observedPlayer is null)
            return false;

        var isDisconnectedDef = FindIsDisconnectedField(observedPlayer.TypeDef);
        if (isDisconnectedDef is null)
            return false;

        isDisconnectedDef.Name = "_isDisconnected";
        
        observedPlayer.CleanPropertyFieldNames();
        observedPlayer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private FieldDef? FindIsDisconnectedField(TypeDef observedPlayerContainerTypeDef)
    {
        MethodDef? processPlayerMethodDef = null;

        foreach (var method in observedPlayerContainerTypeDef.Methods) {
            if (method.Parameters.Count is not 4)
                continue;
            if (method.Parameters[1].Name == "deltaTime" &&
                method.Parameters[2].Name == "framePlayerInfo" &&
                method.Parameters[3].Name == "deathIsClose") {
                processPlayerMethodDef = method;
                break;
            }
        }

        if (processPlayerMethodDef is null)
            return null;

        if (!processPlayerMethodDef.HasBody)
            return null;
        
        var instructions = processPlayerMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldfld && 
                instructions[i + 1].OpCode == OpCodes.Stloc_3 && 
                instructions[i + 2].OpCode == OpCodes.Ldloc_3) {
                FieldDef? field = instructions[i].Operand as FieldDef;
                if (field is null)
                    continue;
                if (field.Name != "IsDisconnected")
                    continue;

                return (instructions[i + 4].Operand as FieldDef);
            }

        }

        return null;
    }
}