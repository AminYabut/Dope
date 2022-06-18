using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI;

internal class SocialNetwork : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var socialNetworkContainer = containers.FindContainerByFullName("EFT.UI.SocialNetwork");
        if (socialNetworkContainer is null)
            return false;

        socialNetworkContainer.CleanPropertyFieldNames();
        socialNetworkContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    
}