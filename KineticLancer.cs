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
using Kingmaker.UnitLogic.Buffs;
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
using System.Collections;
using TurnBased.Controllers;
using System.Net;
using Kingmaker.AreaLogic.Cutscenes;
using Kingmaker.Controllers.Units;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.Visual.Animation.Kingmaker;
using System.CodeDom.Compiler;
using Epic.OnlineServices;
using Kingmaker.Visual.Particles;
using Kingmaker.Visual.Particles.FxSpawnSystem;
using Owlcat.Runtime.Visual.RenderPipeline.RendererFeatures.FogOfWar;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Designers;
using Kingmaker.RuleSystem.Rules;
using static Kingmaker.GameModes.GameModeType;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.Enums;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using System.Drawing.Text;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.Blueprints.TurnBasedModifiers;
using Kingmaker.UI.Models.Log;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Items;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils.Types;
using Kingmaker.RuleSystem;

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

        internal const string KineticLeapSwiftName = "KineticLancer.KineticLeapSwift";
        internal const string KineticLeapSwiftGuid = "CA936192-0C91-45EC-9467-7233D8428921";
        internal const string KineticLeapSwiftDescription = "KineticLancer.KineticLeapSwift.Description";
        internal const string KineticLeapSwiftBuffName = "KineticLancer.KineticLeapSwiftBuff";
        internal const string KineticLeapSwiftBuffGuid = "14C15AA6-6926-485D-A857-FFF2C41DE393";

        internal const string DragoonDiveName = "KineticLancer.DragoonDive";
        internal const string DragoonDiveGuid = "ABD06AAB-F08F-49FA-9C08-948D043E4B49";
        internal const string DragoonDiveDescription = "KineticLancer.DragoonDive.Description";
        internal const string DragoonDiveAbilityName = "KineticLancer.DragoonDiveAbility";
        internal const string DragoonDiveAbilityGuid = "0E141F8E-E233-442D-B268-6F5E93C40C79";
        internal const string DragoonDiveAbilityDescription = "KineticLancer.DragoonDiveAbility.Description";

        internal const string DragoonLeapName = "KineticLancer.DragoonLeap";
        internal const string DragoonLeapGuid = "9D6CCD93-F1F5-4BB0-95CF-04320BA0DEED";
        internal const string DragoonLeapDescription = "KineticLancer.DragoonLeap.Description";

        internal const string KineticSpearName = "KineticLancer.KineticSpear";
        internal const string KineticSpearGuid = "1ED5F6A0-3EAB-401B-8BE5-BC64CAF864B5";
        internal const string KineticSpearDescription = "KineticLancer.KineticSpear.Description";
        internal const string KineticSpearAbilityName = "KineticLancer.KineticSpearAbility";
        internal const string KineticSpearAbilityGuid = "C379BE62-38DC-4357-8A2B-7B58217F0CF7";
        internal const string KineticSpearAbilityDescription = "KineticLancer.KineticSpearAbility.Description";
        internal const string KineticSpearBuffName = "KineticLancer.KineticSpearBuff";
        internal const string KineticSpearBuffGuid = "24D124C2-DDD6-4E70-8E0A-054E55444244";
        internal const string KineticSpearRealBuffName = "KineticLancer.KineticSpearRealBuff";
        internal const string KineticSpearRealBuffGuid = "59DB4ADB-9D5A-40BC-B72B-6AE4A79F932E";

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

            // 念力倒钩-恶心重击，念力鱼叉-精准射击
            var kineticLeap = CreateKineticLeap();
            var dragoonDive = CreateDragoonDive();
            var dragoonLeap = CreateDragoonLeap();
            var kineticSpear = CreateKineticSpear();
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
                .AddToAddFeatures(5, kineticSpear)
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

            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.GatherPowerModeLow)
                .AddUniqueComponent(new KineticLancerCannotGatherPower(), ComponentMerge.Fail, null).Configure();
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.GatherPowerModeMedium)
                .AddUniqueComponent(new KineticLancerCannotGatherPower(), ComponentMerge.Fail, null).Configure();
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.GatherPowerModeHigh)
                .AddUniqueComponent(new KineticLancerCannotGatherPower(), ComponentMerge.Fail, null).Configure();
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
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetAutoUseIsForbidden()
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(false)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .AddComponent<AbilityDragoonDive>()
                .Configure();

            var swiftBuff = BuffConfigurator.New(KineticLeapSwiftBuffName, KineticLeapSwiftBuffGuid)
                .SetDisplayName(KineticLeapSwiftName)
                .SetDescription(KineticLeapSwiftDescription)
                .SetIcon(FeatureSelectionRefs.RogueTalentSelection.Reference.Get().Icon)
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Replace)
                .SetFxOnStart(BuffRefs.GraceBuff.Reference.Get().FxOnStart)
                .AddNotDispelable()
                .AddCondition(UnitCondition.ImmuneToAttackOfOpportunity)
                .Configure();

            var swiftAbility = AbilityConfigurator.New(KineticLeapSwiftName, KineticLeapSwiftGuid)
                .SetDisplayName(KineticLeapSwiftName)
                .SetDescription(KineticLeapSwiftDescription)
                .SetIcon(FeatureSelectionRefs.RogueTalentSelection.Reference.Get().Icon)
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Personal)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffWithDurationSeconds(swiftBuff, 6f, toCaster: true))
                .Configure();

            return FeatureConfigurator.New(KineticLeapName, KineticLeapGuid)
                .SetDisplayName(KineticLeapName)
                .SetDescription(KineticLeapDescription)
                .SetIcon(AbilityRefs.ExpeditiousRetreat.Reference.Get().Icon)
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeLow.Reference.Get())
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeMedium.Reference.Get())
                .AddRemoveFeatureOnApply(ActivatableAbilityRefs.GatherPowerModeHigh.Reference.Get())
                .SetIsClassFeature()
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { leap, swiftAbility })
                .Configure();
        }

        private static BlueprintFeature CreateDragoonDive()
        {
            // Reduce kinetic blade cost by 1
            var reduceBladeCost = new AddKineticistBurnModifier
            {
                Value = -1,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var ability = AbilityConfigurator.New(DragoonDiveAbilityName, DragoonDiveAbilityGuid)
                .SetDisplayName(DragoonDiveAbilityName)
                .SetDescription (DragoonDiveAbilityDescription)
                .SetIcon(AbilityRefs.FeatherStep.Reference.Get().Icon)
                .AddAbilityRequirementCanMove()
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Unlimited)
                .SetCanTargetPoint(false)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(false)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(false)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .AddComponent(new AbilityIsFullRoundInTurnBased() { FullRoundIfTurnBased = true })
                .AddComponent<AbilityDragoonDive>()
                .AddComponent(new MustHaveEquippedKineticBlade())
                .Configure();

            return FeatureConfigurator.New(DragoonDiveName, DragoonDiveGuid)
                .SetDisplayName(DragoonDiveName)
                .SetDescription(DragoonDiveDescription)
                .SetIcon(AbilityRefs.FeatherStep.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddComponent(reduceBladeCost)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability, FeatureRefs.KineticBladeInfusion.Reference.Get() })
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

        private static BlueprintFeature CreateKineticSpear()
        {
            var buff = BuffConfigurator.New(KineticSpearBuffName, KineticSpearBuffGuid)
                .SetDisplayName(KineticSpearAbilityName)
                .SetDescription(KineticSpearAbilityDescription)
                .SetIcon(AbilityRefs.CrusadersEdge.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();

            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 2,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var realBuff = BuffConfigurator.New(KineticSpearRealBuffName, KineticSpearRealBuffGuid)
                .SetFlags(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddComponent(new KineticSpearCritComponent())
                .AddStatBonus(ModifierDescriptor.UntypedStackable, stat: StatType.Reach, value: 5)
                .AddNotDispelable()
                .Configure();

            var ability = AbilityConfigurator.New(KineticSpearAbilityName, KineticSpearAbilityGuid)
                .SetDisplayName(KineticSpearAbilityName)
                .SetDescription(KineticSpearAbilityDescription)
                .SetIcon(AbilityRefs.CrusadersEdge.Reference.Get().Icon)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(buff, isNotDispelable: true))
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Personal)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            return FeatureConfigurator.New(KineticSpearName, KineticSpearGuid)
                .SetDisplayName(KineticSpearName)
                .SetDescription(KineticSpearDescription)
                .SetIcon(AbilityRefs.CrusadersEdge.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        private static BlueprintFeature CreateDragoonFrenzy()
        {
            return FeatureConfigurator.New(DragoonFrenzyName, DragoonFrenzyGuid)
                .SetDisplayName(DragoonFrenzyName)
                .SetDescription(DragoonFrenzyDescription)
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
            BlueprintTool.Get<BlueprintFeature>(DragoonDiveAbilityGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;
        }
    }

    internal class AbilityDragoonDive : AbilityCustomLogic, IAbilityTargetRestriction
    {
        private bool isActivating = false;

        public override bool IsEngageUnit { get { return true; } }

        public override void Cleanup(AbilityExecutionContext context)
        {
            isActivating = false;
        }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            UnitEntityData caster = context.Caster;
            UnitEntityData mount = caster.GetSaddledUnit();
            if (mount != null || isActivating || caster.State.HasCondition(UnitCondition.CantMove))
                yield break;

            caster.View.StopMoving();
            isActivating = true;

            IEnumerator routine = Routine(context, caster, target);
            while (routine.MoveNext())
                yield return null;

            caster.View.StopMoving();
            isActivating = false;
            UnitPlaceOnGroundController.ForcedTick(caster);
        }

        private IEnumerator Routine(AbilityExecutionContext context, UnitEntityData caster, TargetWrapper target)
        {
            KineticLancer.Logger.Info($"Leap from {caster.Position} to {target}");

            Vector3 initial = caster.Position;
            Vector3 end = target.Point;
            Vector3 delta = end - initial;

            // Start animation
            float totalTime = delta.magnitude / caster.CombatSpeedMps / 4f;

            UnitAnimationActionHandle animationHandle = caster.View.AnimationManager?.CreateHandle(UnitAnimationType.CastSpell);
            if (animationHandle != null)
            {
                animationHandle.CastStyle = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                animationHandle.CastingTime = totalTime + 1f;
                caster.View.AnimationManager.Execute(animationHandle);
            }

            // End at a closer point if target is a unit
            float distReduction = 0f;
            if (target.Unit != null)
            {
                float attackRange = caster.GetFirstWeapon()?.AttackRange.Meters ?? 0f;
                distReduction = caster.View.Corpulence + target.Unit.View.Corpulence + attackRange / 2f;
            }

            // Quadratic curve y = a(x-x3)^2+y3 that passes through initial (0, 0) and rotated end (x2, y2)
            // sign = -1 -> peak between start and end; sign = 1 -> peak before start or after end (not used)
            CalculateCurve(-1, initial, end, out float a, out float x2, out float x3, out float y2, out float y3, out float sin, out float cos, distReduction);
            delta = new Vector3(x2 * cos, y2, x2 * sin);
            end = delta + initial;

            // Wait for a short moment before jump starts
            float timeSinceStart = 0f;
            while (timeSinceStart < 0.2f)
            {
                timeSinceStart += Game.Instance.TimeController.GameDeltaTime;
                yield return null;
            }
            timeSinceStart = 0f;

            // Spawn a one-shot fx
            var fx = FxHelper.SpawnFxOnPoint(BuffRefs.GatherPowerAirBuff.Reference.Get().FxOnStart.Load(), initial, true, Quaternion.identity);
            while (timeSinceStart < 0.5f)
            {
                timeSinceStart += Game.Instance.TimeController.GameDeltaTime;
                yield return null;
            }
            timeSinceStart = 0f;
            FxHelper.Destroy(fx);

            // Add Charge buff
            caster.AddBuff(BuffRefs.ChargeBuff.Reference.Get(), caster, 5.5f.Seconds());

            // Make skill check after jump starts, DC = horizontal + 4*vertical, /4 for impossible leap and /2 for dragoon leap
            int DC = (int)Math.Round((x2 + 4f * Math.Abs(end.y - initial.y)) / Feet.FeetToMetersRatio);
            if (caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.ImpossibleLeapGuid)) != null)
                DC /= 4;
            else if (caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonLeapGuid)) != null)
                DC /= 2;
            RuleSkillCheck ruleSkillCheck = new(caster, StatType.SkillMobility, DC)
            {
                ShowAnyway = true,
                Voice = RuleSkillCheck.VoicingType.All
            };

            // Racial bonus from base speed
            ruleSkillCheck.Bonus.AddModifier(new ModifiableValue.Modifier()
            {
                ModValue = (caster.Stats.Speed.ModifiedValue - 30) / 10 * 4,
                ModDescriptor = ModifierDescriptor.Racial,
                Source = context.Ability.Fact
            });

            // Bonus from kinetic leap
            foreach (var buff in caster.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(KineticLancer.KineticLeapSwiftBuffName))
                {
                    ruleSkillCheck.Bonus.AddModifier(new ModifiableValue.Modifier()
                    {
                        ModValue = caster.Stats.SkillMobility.BaseValue < 10 ? 10 : 20,
                        ModDescriptor = ModifierDescriptor.UntypedStackable,
                        Source = context.Ability.Fact
                    });
                    break;
                }
            }

            // Bonus from dragoon leap
            if (caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonLeapGuid)) !=  null &&
                caster.Progression.GetClassData(CharacterClassRefs.KineticistClass.Reference.Get()) != null)
            {
                int cl = caster.Progression.GetClassLevel(CharacterClassRefs.KineticistClass.Reference.Get());
                ruleSkillCheck.Bonus.AddModifier(new ModifiableValue.Modifier()
                {
                    ModValue = cl,
                    ModDescriptor = ModifierDescriptor.UntypedStackable,
                    Source = context.Ability.Fact
                });
            }

            RuleStatCheck ruleStatCheck2 = GameHelper.TriggerSkillCheck(ruleSkillCheck, context);
            int checkResult = ruleStatCheck2.RollResult;

            // Reduce jump distance proportionally to failed DC
            if (checkResult < DC)
            {
                end = delta * checkResult / DC + initial;
                totalTime *= (float)checkResult / DC;
                CalculateCurve(-1, initial, end, out a, out x2, out x3, out _, out y3, out sin, out cos);
            }

            float leapPercent, newX, newY, newZ;
            Vector3 newPos = Vector3.zero;

            // While there is still distance to move
            while ((end - caster.Position).magnitude > 0.1)
            {
                // Interrupt the leap if used toybox teleport
                if (newPos != Vector3.zero && caster.Position != newPos)
                {
                    caster.View.AnimationManager?.StopActions(UnitAnimationType.CastSpell);
                    yield break;
                }

                // Finish the leap on exceeding totalTime, just in case frame rate is too low
                if (timeSinceStart > totalTime)
                    yield break;

                timeSinceStart += Game.Instance.TimeController.GameDeltaTime;

                // Use a non-linear percentage for faster start and slower end
                leapPercent = timeSinceStart / totalTime;
                newX = x2 / 7f * ((leapPercent - 2f) * (leapPercent - 2f) * (leapPercent - 2f) + 8f);
                newY = a * (newX - x3) * (newX - x3) + y3;
                newZ = newX * sin;
                newX *= cos;

                // Translocate
                newPos = new Vector3(newX, newY, newZ) + initial;
                caster.Position = newPos;
                caster.CombatState.PreventAttacksOfOpporunityNextFrame = true;
                caster.View.transform.position = newPos;

                yield return null;
            }

            // Land in prone if DC fails by 5 or more
            if (DC - checkResult > 4)
            {
                caster.View.AnimationManager?.StopActions(UnitAnimationType.CastSpell);
                caster.Descriptor.State.Prone.ShouldBeActive = true;
            }
            // Make attack if it's dragoon dive
            else if (target.Unit != null && context.Ability.Blueprint.ToString().Equals(KineticLancer.DragoonDiveAbilityName))
            {
                UnitAttack attack = new(target.Unit);
                attack.IgnoreCooldown();
                attack.Init(caster);
                attack.IsCharge = true;

                // Full attack for dragoon frenzy
                attack.ForceFullAttack = caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonFrenzyGuid)) != null;
                caster.Commands.AddToQueueFirst(attack);
            }
        }

        private void CalculateCurve(int sign, Vector3 start, Vector3 end, out float a, out float x2, out float x3, out float y2, out float y3, out float sin, out float cos, float x2Reduction = 0f)
        {
            Vector3 deltaPos = end - start;
            x2 = (float)Math.Sqrt(deltaPos.x * deltaPos.x + deltaPos.z * deltaPos.z);
            cos = deltaPos.x / x2;
            sin = deltaPos.z / x2;
            y2 = deltaPos.y;
            x2 -= x2Reduction;

            if (x2 <= 0f)
                x2 = 0.01f;
            if (y2 == 0f)
                y2 = 0.01f;

            y3 = Math.Min(10, x2 * 0.15f) + Math.Max(0, y2);
            a = (float)((x2 * (y2 - 2 * y3) + sign * 2 * Math.Sqrt(x2 * x2 * y3 * (-y2 + y3))) / x2 / x2 / x2);
            x3 = (float)((x2 * y3 + sign * Math.Sqrt(x2 * x2 * y3 * (-y2 + y3))) / y2);
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
                failReason = BlueprintRoot.Instance.LocalizedTexts.Reasons.UnavailableGeneric;
                return false;
            }

            if (LineOfSightGeometry.Instance.HasObstacle(caster.Position, targetWrapper.Point))
            {
                failReason = BlueprintRoot.Instance.LocalizedTexts.Reasons.ObstacleBetweenCasterAndTarget;
                return false;
            }
            
            failReason = null;
            return true;
        }
    }

    internal class KineticLancerCannotGatherPower : ActivatableAbilityRestriction
    {
        public override bool IsAvailable()
        {
            return Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonDiveGuid)) == null;
        }
    }

    internal class MustHaveEquippedKineticBlade : BlueprintComponent, IAbilityCasterRestriction
    {
        public string GetAbilityCasterRestrictionUIText()
        {
            return LocalizedTexts.Instance.Reasons.SpecificWeaponRequired.ToString(() =>
            {
                GameLogContext.Text = LocalizedTexts.Instance.WeaponCategories.GetText(WeaponCategory.KineticBlast);
            });
        }

        public bool IsCasterRestrictionPassed(UnitEntityData caster)
        {
            UnitPartKineticist unitPartKineticist = caster.Parts.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
                return false;

            ItemEntity maybeItem = caster.Body.PrimaryHand.MaybeItem;
            bool flag = maybeItem is ItemEntityWeapon obj && obj.Blueprint.Category == WeaponCategory.KineticBlast;

            if (!flag)
                return false;

            return true;
        }
    }

    internal class KineticSpearCritComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            if (evt.Weapon != null && evt.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                evt.CriticalEdgeBonus += 1;
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}
