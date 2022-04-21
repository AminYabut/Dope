using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Interactive; 

public class ExfiltrationController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var worldContainer = containers.FindContainerByFullName("EFT.GameWorld");

        if (worldContainer is null)
            return false;

        var property = worldContainer.TypeDef.FindProperty("ExfiltrationController");

        var typeDefOrRef = property?.GetMethod.ReturnType.ToTypeDefOrRef();

        if (typeDefOrRef is null || !typeDefOrRef.IsTypeDef)
            return false;

        var type = (TypeDef) typeDefOrRef;

        var exfiltrationControllerContainer = type.ToMetadataContainer(containers);

        if (exfiltrationControllerContainer is null)
            return false;
        
        type.Namespace = "EFT.Interactive";
        type.Name = "ExfiltrationController";

        exfiltrationControllerContainer.CleanPropertyFieldNames();
        exfiltrationControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}