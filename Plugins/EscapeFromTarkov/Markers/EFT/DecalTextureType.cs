using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class DecalTextureType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var decalTextureTypeContainer = containers.FindContainerByFullName("EDecalTextureType");

        if (decalTextureTypeContainer is null)
            return false;

        decalTextureTypeContainer.CleanPropertyFieldNames();
        decalTextureTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}