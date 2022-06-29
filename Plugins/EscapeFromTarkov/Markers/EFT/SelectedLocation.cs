using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class SelectedLocation : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var raidControllerContainer = containers.FindContainerByFullName("EFT.RaidController");

        if (raidControllerContainer is null)
            return false;

        var selectedLocationFieldDef = raidControllerContainer.Fields.FirstOrDefault(field => field.Name == "_selectedLocation")?.FieldDef;
        if (selectedLocationFieldDef is null)
            return false;

        var selectedLocationTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(selectedLocationFieldDef.FieldType);
        if (selectedLocationTypeDef is null)
            return false;
        
        var selectedLocationContainer = selectedLocationTypeDef.ToMetadataContainer(containers);
        if (selectedLocationContainer is null)
            return false;

        selectedLocationContainer.Namespace = "EFT";
        selectedLocationContainer.Name = "SelectedLocation";
        
        selectedLocationContainer.CleanPropertyFieldNames();
        selectedLocationContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}