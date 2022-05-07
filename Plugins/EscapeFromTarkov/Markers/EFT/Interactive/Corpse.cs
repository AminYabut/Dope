using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

internal class Corpse : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var CorpseContainer = containers.FindContainerByFullName("EFT.Interactive.Corpse");

        if (CorpseContainer is null)
            return false;

        CorpseContainer.CleanPropertyFieldNames();
        CorpseContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}