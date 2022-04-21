using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class MovementContext : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        var property = playerContainer?.TypeDef.FindProperty("MovementContext");

        var typeDefOrRef = property?.GetMethod.ReturnType.ToTypeDefOrRef();

        if (typeDefOrRef is null || !typeDefOrRef.IsTypeDef)
            return false;

        var type = (TypeDef) typeDefOrRef;

        var movementContextControllerContainer = containers.FirstOrDefault(x => x.TypeDef == type);

        if (movementContextControllerContainer is null)
            return false;

        type.Namespace = "EFT";
        type.Name = "MovementContext";

        movementContextControllerContainer.CleanPropertyFieldNames();
        movementContextControllerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}