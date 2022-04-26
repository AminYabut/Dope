using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT; 

internal class SkillManager : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var profileContainer = containers.FindContainerByFullName("EFT.Profile");

        if (profileContainer is null)
            return false;

        var skillManagerFieldContainer = profileContainer.FindFieldContainerByName("Skills");

        if (skillManagerFieldContainer is null)
            return false;
        
        var skillManagerTypeDef = skillManagerFieldContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (skillManagerTypeDef is null)
            return false;
        
        var skillManagerContainer = skillManagerTypeDef.ToMetadataContainer(containers);

        if (skillManagerContainer is null)
            return false;

        skillManagerContainer.Namespace = "EFT";
        skillManagerContainer.Name = "SkillManager";

        skillManagerContainer.CleanPropertyFieldNames();
        skillManagerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}