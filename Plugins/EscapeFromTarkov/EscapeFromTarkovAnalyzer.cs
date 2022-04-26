using System.Collections.Immutable;

using EscapeFromTarkov.Markers;
using EscapeFromTarkov.Markers.Diz.Skinning;
using EscapeFromTarkov.Markers.EFT;
using EscapeFromTarkov.Markers.EFT.Ballistics;
using EscapeFromTarkov.Markers.EFT.Interactive;
using EscapeFromTarkov.Markers.EFT.Sky;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov;

internal class EscapeFromTarkovAnalyzer : IDumpAnalyzer {
    public bool Analyze(ImmutableList<MetadataContainer> containers) {
        foreach (var marker in _markers) {
            if (!marker.Mark(containers))
                return false;
        }

        return true;
    }

    private readonly IMarker[] _markers = {
        // EFT
        new GameWorld(),
        new ClientGameWorld(),
        new ClientLocalGameWorld(),

        new EFTHardSettings(),
        new InventoryController(),
        new Inventory(),
        new MovementContext(),
        new Player(),
        new PlayerBody(),

        // EFT.Ballistics
        new BallisticsCalculator(),
        new BallisticCollider(),
        new BaseBallistic(),

        // EFT.Interactive
        new ExfiltrationController(),

        // EFT.Sky
        new TOD_AtmosphereParameters(),
        new TOD_CycleParameters(),
        new TOD_Sky(),

        // Diz.Skinning
        new Skeleton(),
        new Skin()
    };
}