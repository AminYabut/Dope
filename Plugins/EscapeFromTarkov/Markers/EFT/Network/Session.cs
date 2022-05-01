using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class Session : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var backendContainer = containers.FindContainerByFullName("EFT.Network.LiveBackend");

        if (backendContainer is null)
            return false;

        var sessionFieldContainer = backendContainer.FindFieldContainerByName("_session");
        if (sessionFieldContainer is null)
            return false;

        var sessionTypeDef = Helpers.GetFieldDefTypeTypeDef(sessionFieldContainer.FieldDef);
        if (sessionTypeDef is null)
            return false;

        var sessionContainer = sessionTypeDef.ToMetadataContainer(containers);
        if (sessionContainer is null)
            return false;

        sessionContainer.Namespace = "EFT.Network";
        sessionContainer.Name = "Session";
        
        sessionContainer.CleanPropertyFieldNames();
        sessionContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}