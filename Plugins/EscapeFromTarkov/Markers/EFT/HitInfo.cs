using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class HitInfo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var movementControllerContainer = containers.FindContainerByFullName("EFT.MovementController");

        var property = movementControllerContainer?.TypeDef.FindProperty("GroundHit");

        var typeDefOrRef = property?.GetMethod.ReturnType.ToTypeDefOrRef();

        if (typeDefOrRef is null || !typeDefOrRef.IsTypeDef)
            return false;

        var type = (TypeDef) typeDefOrRef;

        var hitInfoContainer = type.ToMetadataContainer(containers);

        if (hitInfoContainer is null)
            return false;

        type.Namespace = "EFT";
        type.Name = "HitInfo";

        hitInfoContainer.CleanPropertyFieldNames();
        hitInfoContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}