using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class InertiaConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var configContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (configContainer is null)
            return false;

        var inertiaConfigFieldContainer = FindInertiaFieldContainer(configContainer);
        if (inertiaConfigFieldContainer is null)
            return false;

        var inertiaConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(inertiaConfigFieldContainer.FieldDef);
        if (inertiaConfigTypeDef is null)
            return false;

        var inertiaConfigContainer = inertiaConfigTypeDef.ToMetadataContainer(containers);
        if (inertiaConfigContainer is null)
            return false;
        
       inertiaConfigContainer.Namespace = "EFT.Network";
       inertiaConfigContainer.Name = "InertiaConfig";
       
       inertiaConfigContainer.CleanPropertyFieldNames();
       inertiaConfigContainer.ExportNonObfuscatedSymbols();

        return true;
    }
    
    private MetadataFieldContainer? FindInertiaFieldContainer(MetadataContainer configContainer) {
        foreach (var fieldContainer in configContainer.Fields) {
            if (fieldContainer.Name == "Inertia")
                return fieldContainer;
        }

        return null;
    }
}