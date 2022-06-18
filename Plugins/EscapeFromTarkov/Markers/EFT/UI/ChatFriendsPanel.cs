using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class ChatFriendsPanel : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var chatFriendsPanelContainer = containers.FindContainerByFullName("EFT.UI.Chat.ChatFriendsPanel");
        if (chatFriendsPanelContainer is null)
            return false;
        
        FieldDef? socialNetworkDef = FindSocialNetworkFieldDef(chatFriendsPanelContainer.TypeDef);
        if (socialNetworkDef is null)
            return false;

        socialNetworkDef.Name = "_socialNetwork";
        
        var socialNetworkTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(socialNetworkDef.FieldType);
        if (socialNetworkTypeDef is null)
            return false;
        
        var socialNetworkContainer = socialNetworkTypeDef.ToMetadataContainer(containers);
        if (socialNetworkContainer is null)
            return false;

        socialNetworkContainer.Namespace = "EFT.UI";
        socialNetworkContainer.Name = "SocialNetwork";

        chatFriendsPanelContainer.Namespace = "EFT.UI";
        chatFriendsPanelContainer.Name = "ChatFriendsPanel";
        
        chatFriendsPanelContainer.CleanPropertyFieldNames();
        chatFriendsPanelContainer.ExportNonObfuscatedSymbols();

        socialNetworkContainer.CleanPropertyFieldNames();
        socialNetworkContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
    
    FieldDef? FindSocialNetworkFieldDef(TypeDef chatFriendsPanelTypeDef) {
        MethodDef? updateMethod = chatFriendsPanelTypeDef.Methods.FirstOrDefault(method => method.Name == "Update");
        if (updateMethod is null || !updateMethod.HasBody)
            return null;

        var instructions = updateMethod.Body.Instructions;
        for (var i = 0; i < instructions.Count - 2; ++i)
        {
            if (instructions[i].OpCode != OpCodes.Ldarg_0 ||
                instructions[i + 1].OpCode != OpCodes.Ldfld ||
                instructions[i + 2].OpCode != OpCodes.Ldarg_0 ||
                instructions[i + 3].OpCode != OpCodes.Ldfld)
                continue;
            
            FieldDef? field = instructions[i + 1].Operand as FieldDef;
            if (field is null)
                continue;
                
            return field;
        }
        return null;
    }
}