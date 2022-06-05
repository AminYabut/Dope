using System.Collections.Immutable;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.InventoryLogic; 

internal class Ammo : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var bulletSimulatorContainer = containers.FindContainerByFullName("BulletSimulator");
        if (bulletSimulatorContainer is null)
            return false;
        
        var ammoFieldContainer = FindAmmoFieldContainer(bulletSimulatorContainer);
        if (ammoFieldContainer is null)
            return false;

        var ammoTypeDef = Helpers.GetFieldDefTypeTypeDef(ammoFieldContainer.FieldDef);
        if (ammoTypeDef is null)
            return false;

        var ammoContainer = ammoTypeDef.ToMetadataContainer(containers);
        if (ammoContainer is null)
            return false;

        ammoContainer.Namespace = "EFT.InventoryLogic";
        ammoContainer.Name = "Ammo";

        ammoContainer.CleanPropertyFieldNames();
        ammoContainer.ExportNonObfuscatedSymbols();

        return true;
    }

    private MetadataFieldContainer? FindAmmoFieldContainer(MetadataContainer bulletSimulatorContainer)
    {
        foreach (var fieldContainer in bulletSimulatorContainer.Fields)
            if (fieldContainer.Name == "Ammo")
                return fieldContainer;

        return null;
    }
}