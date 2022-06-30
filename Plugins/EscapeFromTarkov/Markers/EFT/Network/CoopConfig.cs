using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 
internal class CoopConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var configContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (configContainer is null)
            return false;

        var coopConfigFieldDef =
            configContainer.Fields.FirstOrDefault(field => field.Name == "CoopSettings")?.FieldDef;
        if (coopConfigFieldDef is null)
            return false;

        var coopConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(coopConfigFieldDef);
        if (coopConfigTypeDef is null)
            return false;

        var coopConfigContainer = coopConfigTypeDef.ToMetadataContainer(containers);
        if (coopConfigContainer is null)
            return false;
        
        coopConfigContainer.Namespace = "EFT.Network";
        coopConfigContainer.Name = "CoopConfig";
       
        coopConfigContainer.CleanPropertyFieldNames();
        coopConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindFallingFieldContainer(MetadataContainer healthConfigContainer) {
        foreach (var fieldContainer in healthConfigContainer.Fields) {
            if (fieldContainer.Name == "Falling")
                return fieldContainer;
        }

        return null;
    }
}