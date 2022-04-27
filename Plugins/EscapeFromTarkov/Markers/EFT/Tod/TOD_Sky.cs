﻿using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_Sky : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skyContainer = containers.FindContainerByFullName("TOD_Sky");

        if (skyContainer is null)
            return false;
        
        skyContainer.Namespace = "TOD";

        skyContainer.CleanPropertyFieldNames();
        skyContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}