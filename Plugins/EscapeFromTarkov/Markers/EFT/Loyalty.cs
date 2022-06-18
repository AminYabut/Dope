using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class Loyalty : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var loyaltyContainer = containers.FindContainerContainingMethodName("MarkAsCanBeFreeKilled");

        if (loyaltyContainer is null)
            return false;
        
        loyaltyContainer.Namespace = "EFT";
        loyaltyContainer.Name = "Loyalty";

        loyaltyContainer.CleanPropertyFieldNames();
        loyaltyContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}