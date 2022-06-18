using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class UpdatableChatMember : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var updateableChatMemberContainer = containers.FindContainerByFullName("ChatShared.UpdatableChatMember");
        if (updateableChatMemberContainer is null)
            return false;

        var updateableChatMemberInfoContainer = containers.FindContainerByFullName("ChatShared.UpdatableChatMember/UpdatableChatMemberInfo");
        if (updateableChatMemberInfoContainer is null)
            return false;
        
        updateableChatMemberInfoContainer.Namespace = "EFT.UI";
        updateableChatMemberContainer.Namespace = "EFT.UI";
        
        updateableChatMemberContainer.CleanPropertyFieldNames();
        updateableChatMemberContainer.ExportNonObfuscatedSymbols();
        
        updateableChatMemberInfoContainer.CleanPropertyFieldNames();
        updateableChatMemberInfoContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}