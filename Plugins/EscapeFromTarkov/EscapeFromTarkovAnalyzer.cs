using System.Collections.Immutable;

using EscapeFromTarkov.Markers;
using EscapeFromTarkov.Markers.Bsg.CameraEffects;
using EscapeFromTarkov.Markers.Diz.Skinning;
using EscapeFromTarkov.Markers.EFT;
using EscapeFromTarkov.Markers.EFT.Animations;
using EscapeFromTarkov.Markers.EFT.Ballistics;
using EscapeFromTarkov.Markers.EFT.EnvironmentEffect;
using EscapeFromTarkov.Markers.EFT.Interactive;
using EscapeFromTarkov.Markers.EFT.InventoryLogic;
using EscapeFromTarkov.Markers.EFT.Network;
using EscapeFromTarkov.Markers.EFT.Tod;
using EscapeFromTarkov.Markers.EFT.Visual;

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
        // BSG.CameraEffects
        new NightVision(),
        new TextureMask(),
        new ThermalVision(),
        new VisorEffect(),

        // Diz.Skinning
        new Skeleton(),
        new Skin(),

        // EFT
        new GameWorld(),
        new ClientGameWorld(),
        new ClientLocalGameWorld(),

        new EFTHardSettings(),

        new InventoryController(),
        new Inventory(),

        new LeanType(),

        new MemberCategory(),
        new MovementController(),
        new MotionEffector(),
        
        new BifacialTransform(),

        new Player(),
        new PlayerBody(),
        
        new PhysicalController(),
        new RegenerativeController(),

        new PlayerSide(),

        new Profile(),
        new ProfileInfo(),
        new ProfileSettings(),

        new WildSpawnType(),

        new FirearmController(),

        new SkillManager(),

        new PrecisionSkill(),
        new BoolSkill(),

        new InteractionInfo(),
        new DamageInfo(),
        new HitInfo(),

        new BodyRenderer(),
        new DecalTextureType(),

        new AnimatorMask(),
        new BodyPart(),
        new Throwable(),
        new UpdateMode(),
        new UpdateQueue(),
        
        new ClientApplication(),
        new MainApplication(),

        new HideoutController(),

        // EFT.Animations
        new BreathEffector(),
        new ProceduralWeaponAnimation(),
        new ProceduralWeaponAnimationMask(),

        // EFT.Ballistics
        new BallisticsCalculator(),
        new BallisticCollider(),
        new BaseBallistic(),
        new MaterialType(),
        new Shot(),
        new TrajectoryInfo(),

        // EFT.EnvironmentType
        new EnvironmentType(),

        // EFT.Interactive
        new ExfiltrationController(),
        new ExfiltrationPoint(),
        new ExfiltrationStatus(),
        new ExitTriggerSettings(),
        new InteractionParameters(),

        // EFT.InventoryLogic
        new AmmoTemplate(),
        new EquipmentContainer(),
        new EquipmentSlot(),
        new LootItem(),
        new Item(),
        new ItemTemplate(),
        new MalfunctionState(),
        new Slot(),
        new Weapon(),
        
        // EFT.Network
        new Backend(),
        new Session(),

        // TOD
        new TOD_AtmosphereParameters(),
        new TOD_CloudQualityType(),
        new TOD_ColorRangeType(),
        new TOD_ColorSpaceType(),
        new TOD_CycleParameters(),
        new TOD_MeshQualityType(),
        new TOD_Sky(),
        new TOD_SkyQualityType(),

        // Visual
        new LoddedSkin()
    };
}