using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class NetworkGame : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var networkGameContainer = containers.FindContainerByFullName("EFT.NetworkGame");

        if (networkGameContainer is null)
            return false;

        foreach (var field in networkGameContainer.Fields)
            if (field.FieldDef.FieldType.FullName == "EFT.ChannelCombined")
                field.Name = "ChannelCombined";
        
        
        networkGameContainer.Namespace = "EFT.Network";
        networkGameContainer.Name = "NetworkGame";
        
        networkGameContainer.CleanPropertyFieldNames();
        networkGameContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}