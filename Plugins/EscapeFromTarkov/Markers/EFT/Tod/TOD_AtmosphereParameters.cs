using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_AtmosphereParameters : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var atmosphereParametersContainer = containers.FindContainerByFullName("TOD_AtmosphereParameters");

        if (atmosphereParametersContainer is null)
            return false;

        atmosphereParametersContainer.Namespace = "TOD";

        atmosphereParametersContainer.CleanPropertyFieldNames();
        atmosphereParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}