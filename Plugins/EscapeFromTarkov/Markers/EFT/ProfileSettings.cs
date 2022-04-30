using System.Collections.Immutable;

using dnlib.DotNet;

using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class ProfileSettings : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var profileInfoContainer = containers.FindContainerByFullName("EFT.ProfileInfo");

        if (profileInfoContainer is null)
            return false;

        var profileSettingsFieldContainer = profileInfoContainer.FindFieldContainerByName("Settings");

        if (profileSettingsFieldContainer is null)
            return false;
        
        var profileSettingsTypeDef = profileSettingsFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (profileSettingsTypeDef is null)
            return false;
        
        var profileSettingsContainer = profileSettingsTypeDef.ToMetadataContainer(containers);

        if (profileSettingsContainer is null)
            return false;

        profileSettingsContainer.Namespace = "EFT";
        profileSettingsContainer.Name = "ProfileSettings";

        profileSettingsContainer.CleanPropertyFieldNames();
        profileSettingsContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}