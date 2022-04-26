using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ProfileInfo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var profileContainer = containers.FindContainerByFullName("EFT.Profile");

        if (profileContainer is null)
            return false;

        var profileInfoFieldContainer = profileContainer.FindFieldContainerByName("Info");

        if (profileInfoFieldContainer is null)
            return false;
        
        var profileInfoTypeDef = profileInfoFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (profileInfoTypeDef is null)
            return false;
        
        var profileInfoContainer = profileInfoTypeDef.ToMetadataContainer(containers);

        if (profileInfoContainer is null)
            return false;

        profileInfoContainer.Namespace = "EFT";
        profileInfoContainer.Name = "ProfileInfo";

        profileInfoContainer.CleanPropertyFieldNames();
        profileInfoContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}