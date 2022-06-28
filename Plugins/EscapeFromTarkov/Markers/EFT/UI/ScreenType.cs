using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI; 

internal class ScreenType : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var screenTypeController = containers.FindContainerByFullName("EFT.UI.Screens.EScreenType");
        if (screenTypeController is null)
            return false;
        
        screenTypeController.Namespace = "EFT.UI";
        screenTypeController.Name = "EScreenType";

        screenTypeController.CleanPropertyFieldNames();
        screenTypeController.ExportNonObfuscatedSymbols();

        return true;
    }
}