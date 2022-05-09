using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.HealthSystem;

internal class ValueStruct : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var healthValueContainer = containers.FindContainerByFullName("EFT.HealthSystem.ValueStruct");

        if (healthValueContainer is null)
            return false;

        healthValueContainer.CleanPropertyFieldNames();
        healthValueContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}