using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.UI; 

internal class ScreenController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var screenController = containers.FirstOrDefault(container => container.
            TypeDef.Methods.Any(method => method.Name == "UpdateScreenFrameRate") && !container.TypeDef.IsInterface);
        
        if (screenController is null)
            return false;
        
        screenController.Namespace = "EFT.UI";
        screenController.Name = "ScreenController";

        screenController.CleanPropertyFieldNames();
        screenController.ExportNonObfuscatedSymbols();

        return true;
    }
}