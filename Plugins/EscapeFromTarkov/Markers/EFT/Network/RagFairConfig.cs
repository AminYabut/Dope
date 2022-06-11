using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 
internal class RagFairConfig : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var configContainer = containers.FindContainerByFullName("EFT.Network.Config");
        if (configContainer is null)
            return false;

        var ragFairConfigFieldContainer = FindRagFairFieldContainer(configContainer);
        if (ragFairConfigFieldContainer is null)
            return false;

        var ragFairConfigTypeDef = Helpers.GetFieldDefTypeTypeDef(ragFairConfigFieldContainer.FieldDef);
        if (ragFairConfigTypeDef is null)
            return false;

        var ragFairConfigContainer = ragFairConfigTypeDef.ToMetadataContainer(containers);
        if (ragFairConfigContainer is null)
            return false;

        TypeSig? offerTypeSig = null;
        foreach (var field in ragFairConfigContainer.Fields)
            if (field.FieldDef.Name == "maxActiveOfferCount") {
                offerTypeSig = field.FieldDef.FieldType.Next;
                break;
            }

        if (offerTypeSig is null)
            return false;
        
        var offerTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(offerTypeSig);
        if (offerTypeDef is null)
            return false;

        var offerContainer = offerTypeDef.ToMetadataContainer(containers);
        if (offerContainer is null)
            return false;
        
        ragFairConfigContainer.Namespace = "EFT.Network";
        ragFairConfigContainer.Name = "RagFairConfig";
       
        ragFairConfigContainer.CleanPropertyFieldNames();
        ragFairConfigContainer.ExportNonObfuscatedSymbols();
        
        offerContainer.Namespace = "EFT.Network";
        offerContainer.Name = "Offer";
       
        offerContainer.CleanPropertyFieldNames();
        offerContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindRagFairFieldContainer(MetadataContainer configContainer) {
        foreach (var fieldContainer in configContainer.Fields) {
            if (fieldContainer.Name == "RagFair")
                return fieldContainer;
        }

        return null;
    }
}