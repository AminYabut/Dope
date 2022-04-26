using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics;

internal class Shot : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ballisticsCalculatorContainer = containers.FindContainerByFullName("EFT.Ballistics.BallisticsCalculator");

        if (ballisticsCalculatorContainer is null)
            return false;

        var typeDef = ballisticsCalculatorContainer.TypeDef;

        var createShotMethodDef = typeDef.FindMethod("CreateShot");

        if (createShotMethodDef is null)
            return false;

        var shotTypeDef = createShotMethodDef.ReturnType.TryGetTypeDef();

        if (shotTypeDef is null)
            return false;

        shotTypeDef.Namespace = "EFT.Ballistics";
        shotTypeDef.Name = "Shot";

        var shotContainer = shotTypeDef.ToMetadataContainer(containers);

        if (shotContainer is null)
            return false;

        shotContainer.CleanPropertyFieldNames();
        shotContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}