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

        FieldDef? selectedDef = raidControllerContainer.Fields.FirstOrDefault(field => field.FieldDef.FieldSig.Type.FullName == "JsonType.EDateTime")?.FieldDef;
        if (selectedDef is null)
            return false;
        
        selectedDef.Name = "_selectedDateTime";

        FieldDef? commonUIDef = raidControllerContainer.Fields.FirstOrDefault(field => field.FieldDef.FieldType.FullName == "EFT.UI.CommonUI")?.FieldDef;
        if (commonUIDef is null)
            return false;

        commonUIDef.Name = "_commonUI";
        
        FieldDef? preloaderUIDef = raidControllerContainer.Fields.FirstOrDefault(field => field.FieldDef.FieldType.FullName == "EFT.UI.PreloaderUI")?.FieldDef;
        if (preloaderUIDef is null)
            return false;

        preloaderUIDef.Name = "_preloaderUI";
        
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