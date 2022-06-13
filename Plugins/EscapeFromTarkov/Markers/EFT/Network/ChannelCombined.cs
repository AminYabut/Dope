using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class ChannelCombined : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var networkGameContainer = containers.FindContainerByFullName("EFT.ChannelCombined");

        if (networkGameContainer is null)
            return false;

        var (pingNetworkQualityWatcher, lossNetworkQualityWatcher) = FindPingAndLossFields(networkGameContainer.TypeDef);

        if (pingNetworkQualityWatcher is null || lossNetworkQualityWatcher is null)
            return false;
        
        pingNetworkQualityWatcher.Name = "pingNetworkQualityWatcher";

        lossNetworkQualityWatcher.Name = "_lossNetworkQualityWatcher";
        
        var pingNetworkQualityWatcherFieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(pingNetworkQualityWatcher.FieldType);
        
        if (pingNetworkQualityWatcherFieldTypeTypeDef is null)
            return false;
        
        var networkQualityWatcherContainer = pingNetworkQualityWatcherFieldTypeTypeDef.ToMetadataContainer(containers);
        
        if (networkQualityWatcherContainer is null)
            return false;
        
        foreach (var field in networkQualityWatcherContainer.Fields)
            if (field.FieldDef.FieldType.FullName == "System.Boolean")
                field.Name = "_isEnabled";
        
        networkQualityWatcherContainer.Fields[0].Name = "_networkQualityParam";
            
        var networkQualityParamFieldTypeTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(networkQualityWatcherContainer.Fields[0].FieldDef.FieldType);
        
        if (networkQualityParamFieldTypeTypeDef is null)
            return false;
        
        var networkQualityParamContainer = networkQualityParamFieldTypeTypeDef.ToMetadataContainer(containers);
        
        if (networkQualityParamContainer is null)
            return false;
        
        networkGameContainer.Namespace = "EFT.Network";
        
        networkGameContainer.CleanPropertyFieldNames();
        networkGameContainer.ExportNonObfuscatedSymbols();

        networkQualityWatcherContainer.Namespace = "EFT.Network";
        networkQualityWatcherContainer.Name = "NetworkQualityWatcher";
        
        networkQualityWatcherContainer.CleanPropertyFieldNames();
        networkQualityWatcherContainer.ExportNonObfuscatedSymbols();
        
        networkQualityParamContainer.Namespace = "EFT.Network";
        networkQualityParamContainer.Name = "NetworkQualityParam";
        
        networkQualityParamContainer.CleanPropertyFieldNames();
        networkQualityParamContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private Tuple<FieldDef?, FieldDef?> FindPingAndLossFields(TypeDef networkGameContainerTypeDef) {
        var qualityWatcherMethodDef = networkGameContainerTypeDef.FindMethod("set_NetworkQualityWatcherStarted");

        if (qualityWatcherMethodDef is null)
            return new (null, null);

        if (!qualityWatcherMethodDef.HasBody)
            return new (null, null);

        FieldDef? ping = null;
        FieldDef? loss = null;

        var instructions = qualityWatcherMethodDef.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i) {
            if (instructions[i].OpCode == OpCodes.Ldarg_0 && instructions[i + 1].OpCode == OpCodes.Ldfld && instructions[i + 2].OpCode == OpCodes.Brfalse_S) {
                FieldDef? field = instructions[i + 1].Operand as FieldDef;
                if (field is null)
                    continue;
                
                //Instruction? initalBranchInstruction = instructions[i + 2].Operand as Instruction;
                //if (initalBranchInstruction is null)
                //    continue;
                //
                //Instruction? secondBranchInstruction = initalBranchInstruction.Operand as Instruction;
                //if (secondBranchInstruction is null)
                //    continue;
                //
                //Instruction? resultingJumpOpcode = secondBranchInstruction.Operand as Instruction;
                //if (resultingJumpOpcode is null)
                //    continue;
                
                if (loss is null) // && resultingJumpOpcode.OpCode == OpCodes.Ret 
                    loss = field;
            }

            if (instructions[i].OpCode == OpCodes.Ldarg_0 && instructions[i + 1].OpCode == OpCodes.Ldfld && instructions[i + 2].OpCode == OpCodes.Br_S) {
                FieldDef? field = instructions[i + 1].Operand as FieldDef;
                if (field is null)
                    continue;
                
                if (ping is null)
                    ping = field;
            }
            
            if (ping is not null && loss is not null)
                return new Tuple<FieldDef?, FieldDef?>(ping, loss);
        }

        return new (null, null);
    }
}