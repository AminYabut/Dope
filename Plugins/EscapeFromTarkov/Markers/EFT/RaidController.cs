using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class RaidController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var mainApplicationContainer = containers.FindContainerByFullName("EFT.MainApplication");

        if (mainApplicationContainer is null)
            return false;
        
        var raidControllerDef = FindRaidControllerField(mainApplicationContainer.TypeDef);
        if (raidControllerDef is null)
            return false;
        
        raidControllerDef.Name = "_raidController";

        var raidControllerFieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(raidControllerDef.FieldType);
        if (raidControllerFieldTypeTypeDef is null)
            return false;
        
        var raidControllerContainer = raidControllerFieldTypeTypeDef.ToMetadataContainer(containers);
        if (raidControllerContainer is null)
            return false;
        
        raidControllerContainer.Namespace = "EFT";
        raidControllerContainer.Name = "RaidController";
        
        foreach (var field in raidControllerContainer.Fields)
            if (field.FieldDef.FieldSig.Type.FullName == "JsonType.EDateTime")
                field.Name = "_selectedDateTime";
        
        raidControllerContainer.CleanPropertyFieldNames();
        raidControllerContainer.ExportNonObfuscatedSymbols();
        
        mainApplicationContainer.CleanPropertyFieldNames();
        mainApplicationContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
    
    private FieldDef? FindRaidControllerField(TypeDef mainApplicationContainer) {
        foreach (var field in mainApplicationContainer.Fields)
        {
            var fieldType = field.FieldType;
            if (fieldType is null)
                continue;

            var fieldTypeDef = fieldType.TryGetTypeDef();
            if (fieldTypeDef is null)
                continue;
            
            var methods = fieldTypeDef.Methods;
            if (methods is null)
                continue;
            
            foreach (var method in methods)
                if (UTF8String.Equals(method.Name, "get_IsInSession")) 
                    return field;
        }
        return null;
    }
}