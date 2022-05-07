using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.HealthSystem;

internal class BodyPartState : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var bodyPartStateContainer = containers.FindContainerByFullName("EFT.HealthSystem.HealthController/BodyPartState");

        if (bodyPartStateContainer is null)
            return false;

        bodyPartStateContainer.CleanPropertyFieldNames();
        bodyPartStateContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}