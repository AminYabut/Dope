using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Diz.Skinning;

internal class Skeleton : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skeletonContainer = containers.FindContainerByFullName("Diz.Skinning.Skeleton");

        if (skeletonContainer is null)
            return false;

        skeletonContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}