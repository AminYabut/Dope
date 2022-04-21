using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.Diz.Skinning; 

internal class Skin : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skinContainer = containers.FindContainerByFullName("Diz.Skinning.Skin");

        if (skinContainer is null)
            return false;

        skinContainer.CleanPropertyFieldNames();

        var skeletonFieldContainer = FindSkeletonFieldContainer(skinContainer);

        if (skeletonFieldContainer is null)
            return false;
        
        skeletonFieldContainer.Name = "_skeleton";

        skinContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindSkeletonFieldContainer(MetadataContainer skinContainer) {
        foreach (var fieldContainer in skinContainer.Fields) {
            var typeDefOrRef = fieldContainer.FieldDef.FieldType.TryGetTypeDefOrRef();

            if (typeDefOrRef is null)
                continue;
            
            if (!typeDefOrRef.IsTypeDef)
                continue;

            if (((TypeDef) typeDefOrRef).FullName == "Diz.Skinning.Skeleton")
                return fieldContainer;
        }

        return null;
    }
}