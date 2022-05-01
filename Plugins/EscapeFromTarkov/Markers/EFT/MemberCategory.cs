using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class MemberCategory : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var memberCategoryContainer = containers.FindContainerByFullName("EMemberCategory");

        if (memberCategoryContainer is null)
            return false;

        memberCategoryContainer.Namespace = "EFT";

        memberCategoryContainer.CleanPropertyFieldNames();
        memberCategoryContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}