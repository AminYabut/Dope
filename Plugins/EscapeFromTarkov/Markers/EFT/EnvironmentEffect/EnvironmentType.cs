using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.EnvironmentEffect; 

internal class EnvironmentType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var environmentTypeContainer = containers.FindContainerByFullName("EFT.EnvironmentEffect.EnvironmentType");

        if (environmentTypeContainer is null)
            return false;

        environmentTypeContainer.CleanPropertyFieldNames();
        environmentTypeContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}