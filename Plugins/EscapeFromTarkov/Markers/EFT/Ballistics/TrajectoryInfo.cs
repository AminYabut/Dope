using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Ballistics;

internal class TrajectoryInfo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var ballisticsShotContainer = containers.FindContainerByFullName("EFT.Ballistics.Shot");

        if (ballisticsShotContainer is null)
            return false;

        var trajectoryInfoFieldContainer = ballisticsShotContainer.FindFieldContainerByName("TrajectoryInfo");

        if (trajectoryInfoFieldContainer is null)
            return false;

        var trajectoryInfoFieldDef = trajectoryInfoFieldContainer.FieldDef;

        var trajectoryInfoTypeDef = trajectoryInfoFieldDef.FieldType.ScopeType as TypeDef;

        if (trajectoryInfoTypeDef is null)
            return false;

        var trajectoryInfoContainer = trajectoryInfoTypeDef.ToMetadataContainer(containers);

        if (trajectoryInfoContainer is null)
            return false;
        
        trajectoryInfoTypeDef.Namespace = "EFT.Ballistics";
        trajectoryInfoTypeDef.Name = "TrajectoryInfo";
        
        trajectoryInfoContainer.CleanPropertyFieldNames();
        trajectoryInfoContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}