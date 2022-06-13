using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Tod;

internal class TOD_Scattering : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var tod_ScatteringContainer = containers.FindContainerByFullName("TOD_Scattering");

        if (tod_ScatteringContainer is null)
            return false;
        
        tod_ScatteringContainer.Namespace = "TOD";


        var materialDef = tod_ScatteringContainer.TypeDef.Fields.FirstOrDefault(field => field.FieldType.FullName == "UnityEngine.Material");
        if (materialDef is null)
            return false;

        materialDef.Name = "_scatteringMaterial";
        
        tod_ScatteringContainer.CleanPropertyFieldNames();
        tod_ScatteringContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}