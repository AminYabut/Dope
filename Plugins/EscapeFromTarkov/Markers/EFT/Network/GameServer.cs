using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Network; 

internal class GameServer : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var sessionContainer = containers.FindContainerByFullName("EFT.Network.LiveBackend/EFT.Network.Session");

        if (sessionContainer is null)
            return false;

        var gameServerArrayFieldContainer = sessionContainer.FindFieldContainerByName("_gameServers");
        if (gameServerArrayFieldContainer is null)
            return false;

        var gameServerTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(gameServerArrayFieldContainer.FieldDef.FieldType.Next);
        if (gameServerTypeDef is null)
            return false;

        var serverTypeDef = gameServerTypeDef.BaseType as TypeDef;
        if (serverTypeDef is null)
            return false;
        
        var gameServerContainer = gameServerTypeDef.ToMetadataContainer(containers);
        if (gameServerContainer is null)
            return false;
        
        var serverContainer = serverTypeDef.ToMetadataContainer(containers);
        if (serverContainer is null)
            return false;
        
        serverContainer.Namespace = "EFT.Network";
        serverContainer.Name = "Server";
        
        serverContainer.CleanPropertyFieldNames();
        serverContainer.ExportNonObfuscatedSymbols();

        gameServerContainer.Namespace = "EFT.Network";
        gameServerContainer.Name = "GameServer";
        
        gameServerContainer.CleanPropertyFieldNames();
        gameServerContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}