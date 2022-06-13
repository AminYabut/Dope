using System.Collections.Immutable;
using System.Xml.Schema;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class AfkMonitor : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var raidControllerContainer = containers.FindContainerByFullName("EFT.RaidController");

        if (raidControllerContainer is null)
            return false;

        var afkMonitorDef = FindAfkMonitorField(raidControllerContainer.TypeDef);

        if (afkMonitorDef is null)
            return false;
        
        afkMonitorDef.Name = "_afkMonitor";
        
        var afkMonitorFieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(afkMonitorDef.FieldType);
        
        if (afkMonitorFieldTypeTypeDef is null)
            return false;
        
        var afkMonitorContainer = afkMonitorFieldTypeTypeDef.ToMetadataContainer(containers);
        
        if (afkMonitorContainer is null)
            return false;

       var afkSecondsDef = FindAfkSecondsField(afkMonitorContainer.TypeDef);
       if (afkSecondsDef is null)
           return false;

       afkSecondsDef.Name = "_afkSeconds";
       
       afkMonitorContainer.Namespace = "EFT";
       afkMonitorContainer.Name = "AfkMonitor";
        
       afkMonitorContainer.CleanPropertyFieldNames();
       afkMonitorContainer.ExportNonObfuscatedSymbols();
       
       raidControllerContainer.CleanPropertyFieldNames();
       raidControllerContainer.ExportNonObfuscatedSymbols();
       
       return true;
    }
    
    private FieldDef? FindAfkMonitorField(TypeDef raidControllerContainerTypeDef) {
        var stopAfkMonitorMethodDef = raidControllerContainerTypeDef.FindMethod("StopAfkMonitor");

        if (stopAfkMonitorMethodDef is null)
            return null;

        if (!stopAfkMonitorMethodDef.HasBody)
            return null;
        
        var instructions = stopAfkMonitorMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldfld && instructions[i + 1].OpCode == OpCodes.Callvirt && instructions[i + 2].OpCode == OpCodes.Ret) {
                FieldDef? field = instructions[i].Operand as FieldDef;
                if (field is null)
                    continue;
                
                return field;
            }

        }

        return null;
    }
    
    private FieldDef? FindAfkSecondsField(TypeDef afkMonitorContainerTypeDef) {
        var startAfkMonitorMethodDef = afkMonitorContainerTypeDef.FindMethod("Start");

        if (startAfkMonitorMethodDef is null)
            return null;

        if (!startAfkMonitorMethodDef.HasBody)
            return null;
        
        var instructions = startAfkMonitorMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Call && instructions[i + 1].OpCode == OpCodes.Ldfld && instructions[i + 2].OpCode == OpCodes.Stfld) {
                FieldDef? field = instructions[i+2].Operand as FieldDef;
                if (field is null)
                    continue;
                
                return field;
            }

        }

        return null;
    }
}