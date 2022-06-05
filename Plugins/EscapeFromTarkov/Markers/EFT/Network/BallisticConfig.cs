using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network;

internal class BallisticConfig : IMarker
{
    public bool Mark(ImmutableList<MetadataContainer> containers)
    {
        var ballisticContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (ballisticContainer is null)
            return false;

        var ballisticConfigFieldContainer = FindStaminaFieldContainer(ballisticContainer);
        if (ballisticConfigFieldContainer is null)
            return false;

        var ballisticConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(ballisticConfigFieldContainer.FieldDef);
        if (ballisticConfigTypeDef is null)
            return false;

        var ballisticConfigContainer = ballisticConfigTypeDef.ToMetadataContainer(containers);
        if (ballisticConfigContainer is null)
            return false;

        ballisticConfigContainer.Namespace = "EFT.Network";
        ballisticConfigContainer.Name = "BallisticConfig";

        ballisticConfigContainer.CleanPropertyFieldNames();
        ballisticConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private MetadataFieldContainer? FindStaminaFieldContainer(MetadataContainer configContainer) {
        foreach (var fieldContainer in configContainer.Fields) {
            if (fieldContainer.Name == "Ballistic")
                return fieldContainer;
        }

        return null;
    }
}