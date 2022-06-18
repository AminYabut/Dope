using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class ChatScreen : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var chatScreenContainer = containers.FindContainerByFullName("EFT.UI.Chat.ChatScreen");

        if (chatScreenContainer is null)
            return false;

        chatScreenContainer.Namespace = "EFT.UI";
        
        chatScreenContainer.CleanPropertyFieldNames();
        chatScreenContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}