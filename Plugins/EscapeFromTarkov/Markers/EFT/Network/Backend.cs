using System.Collections.Immutable;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using EscapeFromTarkov.Extensions;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class Backend : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var clientApplicationContainer = containers.FindContainerByFullName("EFT.ClientApplication");

        if (clientApplicationContainer is null)
            return false;

        var backendFieldContainer = clientApplicationContainer.FindFieldContainerByName("_backEnd");
        if (backendFieldContainer is null)
            return false;

        var backendInterfaceTypeDef = Helpers.GetFieldDefTypeTypeDef(backendFieldContainer.FieldDef);
        if (backendInterfaceTypeDef is null)
            return false;

        backendInterfaceTypeDef.Namespace = "EFT.Network";
        backendInterfaceTypeDef.Name = "IBackend";

        var module = backendInterfaceTypeDef.Module;
        var backendTypeDef = module.GetTypes().Where(x => Helpers.TypeImplementsInterface(x, backendInterfaceTypeDef))
            .FirstOrDefault(x => {
                var manualUpdateMethod = x.FindMethod("ManualUpdate");
                if (manualUpdateMethod is null)
                    return false;

                if (!manualUpdateMethod.HasBody)
                    return false;

                return manualUpdateMethod.Body.Instructions[0].OpCode != OpCodes.Ret;
            });

        if (backendTypeDef is null)
            return false;

        var backendContainer = backendTypeDef.ToMetadataContainer(containers);
        if (backendContainer is null)
            return false;

        backendContainer.Namespace = "EFT.Network";
        backendContainer.Name = "LiveBackend";

        backendContainer.CleanPropertyFieldNames();
        backendContainer.ExportNonObfuscatedSymbols();
        
        return true;
    }
}