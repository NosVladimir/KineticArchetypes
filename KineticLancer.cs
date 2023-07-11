using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.FactLogic;
using JetBrains.Annotations;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Kingmaker.View;
using Kingmaker.Visual.Animation.Actions;
using Kingmaker;
using UnityEngine;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Pathfinding;
using Pathfinding;

namespace KineticArchetypes
{
    internal class KineticLancer
    {
        internal const string ArchetypeName = "KineticLancerArchetype";
        internal const string ArchetypeDisplayName = "KineticLancer.Name";
        internal const string ArchetypeDescription = "KineticLancer.Description";
        internal const string ArchetypeGuid = "022742CC-A0CD-414C-98EE-D87F24AE5607";

        internal const string LeapName = "KineticLancer.Leap";
        internal const string LeapGuid = "EA781602-1BF9-44E5-A6A7-6EFE275B1C10";
        internal const string LeapDescription = "KineticLancer.Leap.Description";

        internal const string KineticLeapName = "KineticLancer.KineticLeap";
        internal const string KineticLeapGuid = "6F5544A8-FD68-4EC2-A79F-9F27D3042C3A";
        internal const string KineticLeapDescription = "KineticLancer.KineticLeap.Description";

        internal const string DragoonDiveName = "KineticLancer.DragoonDive";
        internal const string DragoonDiveGuid = "ABD06AAB-F08F-49FA-9C08-948D043E4B49";
        internal const string DragoonDiveDescription = "KineticLancer.DragoonDive.Description";

        internal const string DragoonLeapName = "KineticLancer.DragoonLeap";
        internal const string DragoonLeapGuid = "9D6CCD93-F1F5-4BB0-95CF-04320BA0DEED";
        internal const string DragoonLeapDescription = "KineticLancer.DragoonLeap.Description";

        internal const string KineticLanceName = "KineticLancer.KineticLance";
        internal const string KineticLanceGuid = "1ED5F6A0-3EAB-401B-8BE5-BC64CAF864B5";
        internal const string KineticLanceDescription = "KineticLancer.KineticLance.Description";

        internal const string DragoonFrenzyName = "KineticLancer.DragoonFrenzy";
        internal const string DragoonFrenzyGuid = "BDF36347-1219-49D8-9BEE-0C056E9008AE";
        internal const string DragoonFrenzyDescription = "KineticLancer.DragoonFrenzy.Description";

        internal const string ImpalingCrashName = "KineticLancer.ImpalingCrash";
        internal const string ImpalingCrashGuid = "AB7BFFA8-F1F7-4109-A247-591E23ED5FD2";
        internal const string ImpalingCrashDescription = "KineticLancer.ImpalingCrash.Description";

        internal const string ImpossibleLeapName = "KineticLancer.ImpossibleLeap";
        internal const string ImpossibleLeapGuid = "F523507A-0A12-45EC-BC8F-9C45BBB1646D";
        internal const string ImpossibleLeapDescription = "KineticLancer.ImpossibleLeap.Description";

        internal const string FuriousDragoonName = "KineticLancer.FuriousDragoon";
        internal const string FuriousDragoonGuid = "92919E8E-7726-4BC6-8C9C-64D87A1CE61F";
        internal const string FuriousDragoonDescription = "KineticLancer.FuriousDragoon.Description";

        internal const string BrutalDragoonName = "KineticLancer.BrutalDragoon";
        internal const string BrutalDragoonGuid = "CB90C2E8-7B91-4690-B231-53C694CD8CF4";
        internal const string BrutalDragoonDescription = "KineticLancer.BrutalDragoon.Description";

        internal const string KineticBarbsName = "KineticLancer.KineticBarbs";
        internal const string KineticBarbsGuid = "F60BD7CC-2732-42A2-AF63-2D89872D3AB4";
        internal const string KineticBarbsDescription = "KineticLancer.KineticBarbs.Description";

        internal const string KineticHarpoonName = "KineticLancer.KineticHarpoon";
        internal const string KineticHarpoonGuid = "1EADD640-66AA-4162-93D5-FB6C2367B7C1";
        internal const string KineticHarpoonDescription = "KineticLancer.KineticHarpoon.Description";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.KineticLancer");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure Esoteric Blade", e);
            }
        }

        private static void ConfigureEnabled() 
        {
            Logger.Info($"Configuring {ArchetypeName}");

            var archetype =
                ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.KineticistClass)
                    .SetLocalizedName(ArchetypeDisplayName)
                    .SetLocalizedDescription(ArchetypeDescription);

            // 念力之跃-脚底抹油
            // 念力长枪-圣战之刃/飓风之弓/远程法打
            // 龙骑飞袭-轻羽步，龙骑飞跃-大步流星，龙骑狂热-群体轻羽步，无极之跃-高等大步流星，暴怒龙骑-失控狂暴，凶残龙骑-引导怒气
            // 穿刺撞击-放电，念力倒钩-恶心重击，念力鱼叉-精准射击
            var kineticLeap = CreateKineticLeap();
            var dragoonDive = CreateDragoonDive();
            var dragoonLeap = CreateDragoonLeap();
            var kineticLance = CreateKineticLance();
            var dragoonFrenzy = CreateDragoonFrenzy();
            var impalingCrash = CreateImpalingCrash();
            var impossibleLeap = CreateImpossibleLeap();
            var furiousDragoon = CreateFuriousDragoon();
            var brutalDragoon = CreateBrutalDragoon();

            archetype
                .AddToRemoveFeatures(1, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(2, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(5, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(8, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(9, FeatureRefs.MetakinesisMaximizedFeature.ToString())
                .AddToRemoveFeatures(11, FeatureRefs.Supercharge.ToString())
                .AddToRemoveFeatures(13, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(17, FeatureSelectionRefs.InfusionSelection.ToString())

                .AddToAddFeatures(1, kineticLeap)
                .AddToAddFeatures(1, dragoonDive)
                .AddToAddFeatures(2, dragoonLeap)
                .AddToAddFeatures(5, kineticLance)
                .AddToAddFeatures(8, dragoonFrenzy)
                .AddToAddFeatures(9, impalingCrash)
                .AddToAddFeatures(11, impossibleLeap)
                .AddToAddFeatures(13, furiousDragoon)
                .AddToAddFeatures (17, brutalDragoon)
                .Configure();

            var KineticistClass = CharacterClassRefs.KineticistClass.Reference.Get();

            UIGroup uiGroup = new();
            uiGroup.Features.Add(dragoonDive);
            uiGroup.Features.Add(dragoonLeap);
            uiGroup.Features.Add(dragoonFrenzy);
            uiGroup.Features.Add(impalingCrash);
            uiGroup.Features.Add(impossibleLeap);
            uiGroup.Features.Add(furiousDragoon);
            uiGroup.Features.Add(brutalDragoon);
            var oldUIGroup = KineticistClass.Progression.UIGroups;
            int num = oldUIGroup.Length;
            var newUIGroup = new UIGroup[num + 1];
            if (num > 0)
                Array.Copy(oldUIGroup, newUIGroup, num);
            newUIGroup[num] = uiGroup;
            KineticistClass.Progression.UIGroups = newUIGroup;

        }

        private static BlueprintFeature CreateKineticLeap()
        {
            var leap = AbilityConfigurator.New(LeapName, LeapGuid)
                .SetDisplayName(LeapName)
                .SetDescription(LeapDescription)
                .SetIcon(AbilityRefs.ExpeditiousRetreat.Reference.Get().Icon)
                .AddAbilityRequirementCanMove()
                .SetType(AbilityType.Physical)
                .SetRange(AbilityRange.Unlimited)
                .SetCanTargetPoint(true)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(false)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .AddComponent<AbilityDragoonDive>()
                .Configure();

            return FeatureConfigurator.New(KineticLeapName, KineticLeapGuid)
                .SetDisplayName(KineticLeapName)
                .SetDescription(KineticLeapDescription)
                .SetIcon(AbilityRefs.ExpeditiousRetreat.Reference.Get().Icon)
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeLow.Reference.Get())
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeMedium.Reference.Get())
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeHigh.Reference.Get())
                .SetIsClassFeature()
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { leap.ToReference<BlueprintUnitFactReference>() })
                .Configure();
        }

        private static BlueprintFeature CreateDragoonDive()
        {
            return FeatureConfigurator.New(DragoonDiveName, DragoonDiveGuid)
                .SetDisplayName(DragoonDiveName)
                .SetDescription(DragoonDiveDescription)
                .SetIcon(AbilityRefs.FeatherStep.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateDragoonLeap()
        {
            return FeatureConfigurator.New(DragoonLeapName, DragoonLeapGuid)
                .SetDisplayName(DragoonLeapName)
                .SetDescription(DragoonLeapDescription)
                .SetIcon(AbilityRefs.Longstrider.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateKineticLance()
        {
            return FeatureConfigurator.New(KineticLanceName, KineticLanceGuid)
                .SetDisplayName(KineticLanceName)
                .SetDescription(KineticLanceDescription)
                .SetIcon(AbilityRefs.CrusadersEdge.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateDragoonFrenzy()
        {
            return FeatureConfigurator.New(DragoonFrenzyName, DragoonFrenzyGuid)
                .SetDisplayName(DragoonFrenzyName)
                .SetDescription(DragoonLeapDescription)
                .SetIcon(AbilityRefs.FeatherStepMass.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateImpalingCrash()
        {
            return FeatureConfigurator.New(ImpalingCrashName, ImpalingCrashGuid)
                .SetDisplayName(ImpalingCrashName)
                .SetDescription(ImpalingCrashDescription)
                .SetIcon(AbilityRefs.Jolt.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateImpossibleLeap()
        {
            return FeatureConfigurator.New(ImpossibleLeapName, ImpossibleLeapGuid)
                .SetDisplayName(ImpossibleLeapName)
                .SetDescription(ImpossibleLeapDescription)
                .SetIcon(AbilityRefs.LongstriderGreater.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateFuriousDragoon()
        {
            return FeatureConfigurator.New(FuriousDragoonName, FuriousDragoonGuid)
                .SetDisplayName(FuriousDragoonName)
                .SetDescription(FuriousDragoonDescription)
                .SetIcon(FeatureRefs.DemonRageForcedRageFeature.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateBrutalDragoon()
        {
            return FeatureConfigurator.New(BrutalDragoonName, BrutalDragoonGuid)
                .SetDisplayName(BrutalDragoonName)
                .SetDescription(BrutalDragoonDescription)
                .SetIcon(AbilityRefs.ChannelRage.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        internal static void HandleOtherMods()
        {

        }
    }

    internal class AbilityDragoonDive : AbilityCustomLogic, IAbilityTargetRestriction
    {
        public override bool IsEngageUnit { get { return true; } }

        public override void Cleanup(AbilityExecutionContext context)
        {
            // context.Caster.View.AgentASP.AvoidanceDisabled = false;
            context.Caster.View.AgentASP.MaxSpeedOverride = null;
            // context.Caster.State.Features.IsUntargetable.Release();
        }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            UnitEntityData caster = context.Caster;
            Vector3 startPoint = caster.Position;
            caster.View.StopMoving();
            // AnimationActionHandle handle = caster.View.AnimationManager.CreateHandle(TakeOffAnimation.Load(false, false));
            // caster.View.AnimationManager.Execute(handle);

            KineticLancer.Logger.Info($"target {target}");
            Vector3 endPoint = target.m_Point;
            // caster.State.Features.IsUntargetable.Retain();
            caster.Translocate(endPoint, new float?(caster.GetLookAtAngle(endPoint)));
            // handle = caster.View.AnimationManager.CreateHandle(LandingAnimation.Load(false, false));
            // caster.View.AnimationManager.Execute(handle);

            caster.View.AgentASP.ForcePath((Path)(object)new ForcedPath(new List<Vector3> { startPoint, endPoint }), disableApproachRadius: true);

            yield break;
        }

        public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
        {
            CheckTargetRestriction(caster, target, out LocalizedString localizedString);
            return localizedString;
        }

        public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper targetWrapper)
        {
            return CheckTargetRestriction(caster, targetWrapper, out _);
        }

        private bool CheckTargetRestriction(UnitEntityData caster, TargetWrapper targetWrapper, [CanBeNull] out LocalizedString failReason)
        {
            if (caster.RiderPart || caster.SaddledPart)
            {
                failReason = BlueprintRoot.Instance.LocalizedTexts.Reasons.AbilityDisabled;
                return false;
            }
            
            failReason = null;
            return true;
        }
    }
}
