﻿using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT;

internal class BoolSkill : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var skillManagerContainer = containers.FindContainerByFullName("EFT.SkillManager");

        if (skillManagerContainer is null)
            return false;

        var aimMasterSpeedContainer = skillManagerContainer.FindFieldContainerByName("AimMasterElite");

        if (aimMasterSpeedContainer is null)
            return false;
        
        var profileInfoTypeDef = aimMasterSpeedContainer.FieldDef.FieldType.ToTypeDefOrRef() as TypeDef;

        if (profileInfoTypeDef is null)
            return false;
        
        var skillBuffContainer = profileInfoTypeDef.ToMetadataContainer(containers);

        if (skillBuffContainer is null)
            return false;

        skillBuffContainer.Namespace = "EFT";
        skillBuffContainer.Name = "BoolSkill";

        skillBuffContainer.CleanPropertyFieldNames();
        skillBuffContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}