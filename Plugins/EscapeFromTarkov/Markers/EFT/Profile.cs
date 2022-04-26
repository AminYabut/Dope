using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Profile : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var profileContainer = containers.FindContainerByFullName("EFT.Profile");

        if (profileContainer is null)
            return false;

        profileContainer.CleanPropertyFieldNames();
        profileContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}