using System.Collections.Immutable;

using EscapeFromTarkov.Markers;
using EscapeFromTarkov.Markers.Bsg.CameraEffects;
using EscapeFromTarkov.Markers.Diz.Skinning;
using EscapeFromTarkov.Markers.EFT;
using EscapeFromTarkov.Markers.EFT.Animations;
using EscapeFromTarkov.Markers.EFT.Ballistics;
using EscapeFromTarkov.Markers.EFT.CameraControl;
using EscapeFromTarkov.Markers.EFT.EnvironmentEffect;
using EscapeFromTarkov.Markers.EFT.HealthSystem;
using EscapeFromTarkov.Markers.EFT.Interactive;
using EscapeFromTarkov.Markers.EFT.InventoryLogic;
using EscapeFromTarkov.Markers.EFT.Network;
using EscapeFromTarkov.Markers.EFT.Tod;
using EscapeFromTarkov.Markers.EFT.UI;
using EscapeFromTarkov.Markers.EFT.Visual;
using EscapeFromTarkov.Markers.EFT.Weather;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov;

internal class EscapeFromTarkovAnalyzer : IDumpAnalyzer {
    public bool Analyze(ImmutableList<MetadataContainer> containers) {
        foreach (var marker in _markers)
            if (!marker.Mark(containers))
                return false;
        
        return true;
    }

    private readonly IMarker[] _markers = {
        // BSG.CameraEffects
        new NightVision(),
        new TextureMask(),
        new ThermalVision(),
        new VisorEffect(),
        new ThermalVisionUtilities(),
        new ValuesCoefs(),

        // Diz.Skinning
        new Skeleton(),
        new Skin(),

        // EFT
        new World(),
        new GameWorld(),
        new ClientGameWorld(),
        new ClientLocalGameWorld(),

        new EFTHardSettings(),
        new ShellsSettings(),

        new InventoryController(),
        new Inventory(),

        new LeanType(),

        new MemberCategory(),
        new MovementController(),
        new MotionEffector(),
        
        new BifacialTransform(),

        new Player(),
        new ObservedPlayer(),
        new PlayerBody(),
        
        new PhysicalController(),
        new RegenerativeController(),
        
        new PlayerSide(),

        new Profile(),
        new ProfileInfo(),
        new ProfileSettings(),
        new Loyalty(),

        new WildSpawnType(),

        new FirearmController(),
        new ItemHandsController(),

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
        
        new TurnAwayEffector(),

        new RaidController(),
        new SelectedLocation(),
        new AfkMonitor(),
        
        new WeaponPrefab(),
        
        new PlayerBones(),
        
        //EFT.CameraControl
        new CameraManager(),
        new OpticCameraManager(),
        new OpticSight(),
        new HighLightMesh(),
        
        // EFT.Animations
        new BreathEffector(),
        new ProceduralWeaponAnimation(),
        new ProceduralWeaponAnimationMask(),

        // EFT.Ballistics
        new BallisticsCalculator(),
        new BallisticCollider(),
        new BaseBallistic(),
        new MaterialType(),
        new BallisticPreset(),
        new Shot(),
        new TrajectoryInfo(),

        // EFT.EnvironmentEffect
        new EnvironmentType(),
        
        // EFT.HealthSystem
        new BodyPartState(),
        new HealthValue(),
        new ValueStruct(),
        new HealthController(),

        // EFT.Interactive
        new ExfiltrationController(),
        new ExfiltrationPoint(),
        new ScavExfiltrationPoint(),
        new ExfiltrationType(),
        new ExfiltrationStatus(),
        new ExitTriggerSettings(),
        new InteractionParameters(),
        new LootItem(),
        new Corpse(),
        new WorldInteractiveObject(),
        new EDoorState(),
        new Door(),
        new LootableContainer(),
        new Switch(),
        new StationaryWeapon(),

        // EFT.InventoryLogic
        new Ammo(),
        new AmmoTemplate(),
        new EquipmentContainer(),
        new EquipmentSlot(),
        new ItemContainer(),
        new Item(),
        new ItemTemplate(),
        new MalfunctionState(),
        new Grid(),
        new Slot(),
        new Weapon(),
        new WeaponTemplate(),
        new ItemController(),
        new StackSlot(),
        new ArmorItem(),
        new TacticalRigArmorItem(),
        new ArmorTemplate(),
        new ArmorComponent(),
        new HeadArmorTemplate(),
        new EquipableItemTemplate(),
        
        // EFT.Network
        new Backend(),
        new Session(),
        new GameServer(),
        new NetworkGame(),
        new ChannelCombined(),
        new BackendConfig(),
        new InertiaConfig(),
        new StaminaConfig(),
        new BallisticConfig(),
        new HealthConfig(),
        new FallingConfig(),
        new RagFairConfig(),
        new ServerBackend(),

        // TOD
        new TOD_AtmosphereParameters(),
        new TOD_CloudQualityType(),
        new TOD_ColorRangeType(),
        new TOD_ColorSpaceType(),
        new TOD_CycleParameters(),
        new TOD_MeshQualityType(),
        new TOD_Sky(),
        new TOD_SkyQualityType(),
        new TOD_Scattering(),
        new TOD_ImageEffect(),
        new TOD_Components(),
        new TOD_Time(),
        new TOD_CloudParameters(),
        new TOD_Weather(),
        
        // EFT.Weather
        new WeatherController(),
        new RainController(),
        new WeatherDebug(),

        // EFT.UI
        new CommonUI(),
        new UpdatableChatMember(),
        new ChatScreen(),
        new ChatFriendsPanel(),
        //new SocialNetwork(),
        new PreloaderUI(),
        new UIList(),
        new ScreenController(),
        new ScreenType(),
        
        // Visual
        new LoddedSkin(),
        new CustomSkin(),
        new Dress(),
        new SkinDress(),
        new TorsoSkin()
    };
}