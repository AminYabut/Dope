using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.HealthSystem;

internal class HealthController : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var playerContainer = containers.FindContainerByFullName("EFT.Player");

        if (playerContainer is null)
            return false;

        var healthControllerProperty = playerContainer.TypeDef.FindProperty("ActiveHealthController");
        
        if (healthControllerProperty is null)
            return false;

        var healthControllerGetter = healthControllerProperty.GetMethod;
        
        if (healthControllerGetter is null)
            return false;

        var activeHealthControllerTypeDef = healthControllerGetter.ReturnType.ScopeType as TypeDef;
        
        if (activeHealthControllerTypeDef is null)
            return false;
        
        var healthControllerTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(activeHealthControllerTypeDef.BaseType.ToTypeSig());
        
        if (healthControllerTypeDef is null)
            return false;

        var activeHealthControllerContainer = activeHealthControllerTypeDef.ToMetadataContainer(containers);
        
        if (activeHealthControllerContainer is null)
            return false;
        
        var healthControllerContainer = healthControllerTypeDef.ToMetadataContainer(containers);
        
        if (healthControllerContainer is null)
            return false;
        
        activeHealthControllerContainer.Namespace = "EFT.HealthSystem";
        activeHealthControllerContainer.Name = "ActiveHealthController";
        
        activeHealthControllerContainer.CleanPropertyFieldNames();
        activeHealthControllerContainer.ExportNonObfuscatedSymbols();
        
        healthControllerContainer.Namespace = "EFT.HealthSystem";
        healthControllerContainer.Name = "HealthController";
        
        healthControllerContainer.CleanPropertyFieldNames();
        healthControllerContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
}