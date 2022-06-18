using System.Collections.Immutable;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.CameraControl;

internal class HighLightMesh : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var highLightMeshContainer = containers.FindContainerByFullName("HighLightMesh");
        if (highLightMeshContainer is null)
            return false;

        var cameraDef = highLightMeshContainer.TypeDef.Fields.FirstOrDefault(field => field.FieldType.TypeName == "Camera");
        if (cameraDef is null)
            return false;

        cameraDef.Name = "_Camera";
        
        var materialDef = highLightMeshContainer.TypeDef.Fields.FirstOrDefault(field => field.FieldType.TypeName == "Material");
        if (materialDef is null)
            return false;

        materialDef.Name = "_TheMaterial";
        
        var cachedTransformDef = highLightMeshContainer.TypeDef.Fields.FirstOrDefault(field => field.FieldType.TypeName == "Transform" && field.Name != "Target");
        if (cachedTransformDef is null)
            return false;

        cachedTransformDef.Name = "_cachedTransform";
        

        highLightMeshContainer.Namespace = "EFT.CameraControl";
        
        highLightMeshContainer.CleanPropertyFieldNames();
        highLightMeshContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}