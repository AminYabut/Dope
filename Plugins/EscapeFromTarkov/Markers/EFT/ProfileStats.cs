using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ProfileStats : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var profileStatsContainer = containers.FindContainerContainingFieldName("OverallCounters");
        if (profileStatsContainer is null)
            return false;

        profileStatsContainer.Namespace = "EFT";
        profileStatsContainer.Name = "ProfileStats";

        profileStatsContainer.CleanPropertyFieldNames();
        profileStatsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}