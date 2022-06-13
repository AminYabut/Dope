using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_Components : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var tod_ComponentsParametersContainer = containers.FindContainerByFullName("TOD_Components");

        if (tod_ComponentsParametersContainer is null)
            return false;

        tod_ComponentsParametersContainer.Namespace = "TOD";

        tod_ComponentsParametersContainer.CleanPropertyFieldNames();
        tod_ComponentsParametersContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}