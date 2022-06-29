using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class PreloaderUI : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var preloaderUIContainer = containers.FindContainerByFullName("EFT.UI.PreloaderUI");
        if (preloaderUIContainer is null)
            return false;

        var sessionIdDef = FindSessionIdFieldDef(preloaderUIContainer.TypeDef);
        if (sessionIdDef is null)
            return false;

        sessionIdDef.Name = "_sessionId";
        
        preloaderUIContainer.CleanPropertyFieldNames();
        preloaderUIContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }

    FieldDef? FindSessionIdFieldDef(TypeDef preloaderUITypeDef) {
        var setSessionMethod = preloaderUITypeDef.Methods.FirstOrDefault(method => method.Name == "SetSessionId");
        if (setSessionMethod is null || !setSessionMethod.HasBody)
            return null;
        var instructions = setSessionMethod.Body.Instructions;
        for (int i = 0; i < instructions.Count - 2; ++i)
            if (instructions[i].OpCode == OpCodes.Ldarg_1 && instructions[i + 1].OpCode == OpCodes.Stfld)
                return instructions[i + 1].Operand as FieldDef;
        
        return null;
    }
}