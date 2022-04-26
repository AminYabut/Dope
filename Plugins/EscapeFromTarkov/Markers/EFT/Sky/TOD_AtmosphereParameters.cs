using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Sky;

internal class TOD_AtmosphereParameters : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var atmosphereParametersContainer = containers.FindContainerByFullName("TOD_AtmosphereParameters");

        if (atmosphereParametersContainer is null)
            return false;

        atmosphereParametersContainer.Namespace = "EFT";

        atmosphereParametersContainer.CleanPropertyFieldNames();
        atmosphereParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}