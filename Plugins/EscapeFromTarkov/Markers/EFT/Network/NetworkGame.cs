using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class NetworkGame : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var networkGameContainer = containers.FindContainerByFullName("EFT.NetworkGame");

        if (networkGameContainer is null)
            return false;

        var field = networkGameContainer.Fields.FirstOrDefault(field => field.FieldDef.FieldType.FullName == "EFT.ChannelCombined")?.FieldDef;
        if (field is null)
            return false;
        
        field.Name = "ChannelCombined";
        
        networkGameContainer.Namespace = "EFT.Network";
        networkGameContainer.Name = "NetworkGame";
        
        networkGameContainer.CleanPropertyFieldNames();
        networkGameContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}