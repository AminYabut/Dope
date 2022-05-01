using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics;

internal class BaseBallistic : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var baseBallisticContainer = containers.FindContainerByFullName("BaseBallistic");

        if (baseBallisticContainer is null)
            return false;

        baseBallisticContainer.ExportNonObfuscatedSymbols();

        var surfaceSoundContainer = containers.FindContainerByFullName("BaseBallistic/ESurfaceSound");

        if (surfaceSoundContainer is null)
            return false;

        surfaceSoundContainer.Namespace = "EFT.Ballistics";
        
        surfaceSoundContainer.ExportNonObfuscatedSymbols();

        baseBallisticContainer.Namespace = "EFT";

        return true;
    }
}