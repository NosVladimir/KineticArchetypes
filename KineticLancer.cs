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
using JetBrains.Annotations;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Kingmaker.View;
using Kingmaker;
using UnityEngine;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections;
using TurnBased.Controllers;
using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.Visual.Animation.Kingmaker;
using Kingmaker.Visual.Particles;
using Owlcat.Runtime.Visual.RenderPipeline.RendererFeatures.FogOfWar;
using Kingmaker.Designers;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.Enums;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.Blueprints.TurnBasedModifiers;
using Kingmaker.Items;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using HarmonyLib;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Visual;
using Kingmaker.TurnBasedMode.Controllers;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.ActivatableAbilities;

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
        internal const string DragoonDiveBurnBuffName = "KineticLancer.DragoonDiveBurnBuff";
        internal const string DragoonDiveBurnBuffGuid = "C7D9AA8C-FC3E-403E-9288-34E5B796F6F0";

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
        internal const string ImpalingCrashAbilityName = "KineticLancer.ImpalingCrashAbility";
        internal const string ImpalingCrashAbilityGuid = "9B7511D4-9794-4694-9581-605DECAD12B9";
        internal const string ImpalingCrashAbilityDescription = "KineticLancer.ImpalingCrashAbility.Description";
        internal const string ImpalingCrashBuffName = "KineticLancer.ImpalingCrashBuff";
        internal const string ImpalingCrashBuffGuid = "4F740AA2-27B4-4BB0-A0AF-CE356AB0497A";
        internal const string ImpalingCrashBurnBuffName = "KineticLancer.ImpalingCrashBurnBuff";
        internal const string ImpalingCrashBurnBuffGuid = "EF6A471D-F78E-4706-AC99-8FB9F024AB91";
        internal const string ImpalingCrashRealBuffName = "KineticLancer.ImpalingCrashRealBuff";
        internal const string ImpalingCrashRealBuffGuid = "9DB41706-664E-4280-A9F0-570484F99774";
        internal const string ImpalingCrashDebuffName = "KineticLancer.ImpalingCrashDebuff";
        internal const string ImpalingCrashDebuffGuid = "32CACCEF-7D38-435F-BC24-0E972D8D4D3B";
        internal const string ImpalingCrashDebuffDescription = "KineticLancer.ImpalingCrashDebuff.Description";

        internal const string ImpossibleLeapName = "KineticLancer.ImpossibleLeap";
        internal const string ImpossibleLeapGuid = "F523507A-0A12-45EC-BC8F-9C45BBB1646D";
        internal const string ImpossibleLeapDescription = "KineticLancer.ImpossibleLeap.Description";

        internal const string FuriousDragoonName = "KineticLancer.FuriousDragoon";
        internal const string FuriousDragoonGuid = "92919E8E-7726-4BC6-8C9C-64D87A1CE61F";
        internal const string FuriousDragoonDescription = "KineticLancer.FuriousDragoon.Description";
        internal const string FuriousDragoonAbilityName = "KineticLancer.FuriousDragoonAbility";
        internal const string FuriousDragoonAbilityGuid = "B6CB5BD8-47A2-4D8E-9E36-9698366982FD";
        internal const string FuriousDragoonAbilityDescription = "KineticLancer.FuriousDragoonAbility.Description";
        internal const string FuriousDragoonBuffName = "KineticLancer.FuriousDragoonBuff";
        internal const string FuriousDragoonBuffGuid = "DB3EA8E5-0216-4295-8F51-FDF713262835";
        internal const string FuriousDragoonBurnBuffName = "KineticLancer.FuriousDragoonBurnBuff";
        internal const string FuriousDragoonBurnBuffGuid = "4F00476F-12AC-4DCE-AADC-8EC96F8C7627";
        internal const string FuriousDragoonRealBuffName = "KineticLancer.FuriousDragoonRealBuff";
        internal const string FuriousDragoonRealBuffGuid = "0901456A-66D9-4577-A926-9A3B24789196";

        internal const string BrutalDragoonName = "KineticLancer.BrutalDragoon";
        internal const string BrutalDragoonGuid = "CB90C2E8-7B91-4690-B231-53C694CD8CF4";
        internal const string BrutalDragoonDescription = "KineticLancer.BrutalDragoon.Description";
        internal const string BrutalDragoonRealBuffName = "KineticLancer.BrutalDragoonRealBuff";
        internal const string BrutalDragoonRealBuffGuid = "9F0A793B-B818-4CC5-BABA-B0F9381B6251";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.KineticLancer");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure Kinetic Lancer", e);
            }
        }

        private static void ConfigureEnabled() 
        {
            Logger.Info($"Configuring {ArchetypeName}");

            var archetype =
                ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.KineticistClass)
                    .SetLocalizedName(ArchetypeDisplayName)
                    .SetLocalizedDescription(ArchetypeDescription);

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

            // Remove automatically add KineticBladeEnableBuff for blade burn abilities
            // So that patches in the end won't allow for free kb attacks
            foreach (var bladeBurnAbility in KineticDuelist.allBlades)
                AbilityConfigurator.For(bladeBurnAbility).RemoveComponents(c => c is AbilityEffectRunAction).Configure();
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
                .AddAbilityRequirementHasCondition(new UnitCondition[] { UnitCondition.Fatigued, UnitCondition.Exhausted, UnitCondition.Entangled }, not: true)
                .AddComponent<AbilityDragoonDive>()
                .Configure();

            var swiftBuff = BuffConfigurator.New(KineticLeapSwiftBuffName, KineticLeapSwiftBuffGuid)
                .SetDisplayName(KineticLeapSwiftName)
                .SetDescription(KineticLeapSwiftDescription)
                .SetIcon(FeatureSelectionRefs.RogueTalentSelection.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
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
                .SetActionType(UnitCommand.CommandType.Swift)
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

            // Gather power-alike buff that reduces burn by 1 or 2
            var buff = BuffConfigurator.New(DragoonDiveBurnBuffName, DragoonDiveBurnBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(new DragoonDiveBurnReduction())
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().RemoveSelf(),
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast,
                    triggerBeforeAttack: false,
                    onlyHit: false)
                .AddNotDispelable()
                .Configure();

            var ability = AbilityConfigurator.New(DragoonDiveAbilityName, DragoonDiveAbilityGuid)
                .SetDisplayName(DragoonDiveAbilityName)
                .SetDescription(DragoonDiveAbilityDescription)
                .SetIcon(AbilityRefs.FeatherStep.Reference.Get().Icon)
                .AddAbilityRequirementCanMove()
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Unlimited)
                .SetCanTargetPoint(false)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(false)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .AddAbilityRequirementHasCondition(new UnitCondition[] { UnitCondition.Fatigued, UnitCondition.Exhausted, UnitCondition.Entangled }, not: true)
                .AddComponent(new AbilityIsFullRoundInTurnBased() { FullRoundIfTurnBased = true })
                .AddComponent<AbilityDragoonDive>()
                .AddComponent(new MustHaveEquippedKineticBlade())
                .AddComponent(new DragoonDiveBurnDisplay())
                //.AddComponent(new AbilityCustomVitalStrike()) // Maybe?
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
                .AddComponent(new ReactivateKineticBladeComponent())
                .Configure();

            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 2,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var realBuff = BuffConfigurator.New(KineticSpearRealBuffName, KineticSpearRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddComponent(new KineticSpearCritComponent())
                .AddStatBonus(ModifierDescriptor.UntypedStackable, stat: StatType.Reach, value: 5)
                .AddNotDispelable()
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(KineticSpearAbilityName, KineticSpearAbilityGuid)
                .SetDisplayName(KineticSpearAbilityName)
                .SetDescription(KineticSpearAbilityDescription)
                .SetIcon(AbilityRefs.CrusadersEdge.Reference.Get().Icon)
                .SetBuff(buff)
                // .SetDeactivateImmediately() // No deactivate immediately, because of infinite AoOs
                .SetDoNotTurnOffOnRest()
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
            // Restrict metakinesis maximize
            FeatureConfigurator.For(FeatureRefs.MetakinesisMasterMaximize)
                .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                .Configure();

            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 1,
                BurnType = KineticistBurnType.Metakinesis,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var burnBuff = BuffConfigurator.New(ImpalingCrashBurnBuffName, ImpalingCrashBurnBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().RemoveSelf(),
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast,
                    triggerBeforeAttack: false)
                .AddNotDispelable()
                .Configure();

            var realBuff = BuffConfigurator.New(ImpalingCrashRealBuffName, ImpalingCrashRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(new ImpalingCrashApplyDebuffComponent())
                .AddComponent(new RemoveNextRoundAfterAttak())
                .AddNotDispelable()
                .Configure();
            
            var debuff = BuffConfigurator.New(ImpalingCrashDebuffName, ImpalingCrashDebuffGuid)
                .SetDisplayName(ImpalingCrashDebuffName)
                .SetDescription(ImpalingCrashDebuffDescription)
                .SetIcon(AbilityRefs.Jolt.Reference.Get().Icon)
                .AddComponent(new ImpalingCrashDebuffComponent())
                .AddCondition(UnitCondition.CantUseStandardActions)
                .AddNotDispelable()
                .Configure();

            var buff = BuffConfigurator.New(ImpalingCrashBuffName, ImpalingCrashBuffGuid)
                .SetDisplayName(ImpalingCrashAbilityName)
                .SetDescription(ImpalingCrashAbilityDescription)
                .SetIcon(AbilityRefs.Jolt.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(ImpalingCrashAbilityName, ImpalingCrashAbilityGuid)
                .SetDisplayName(ImpalingCrashAbilityName)
                .SetDescription(ImpalingCrashAbilityDescription)
                .SetIcon(AbilityRefs.Jolt.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDoNotTurnOffOnRest()
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(ImpalingCrashName, ImpalingCrashGuid)
                .SetDisplayName(ImpalingCrashName)
                .SetDescription(ImpalingCrashDescription)
                .SetIcon(AbilityRefs.Jolt.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
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
            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 1,
                BurnType = KineticistBurnType.Metakinesis,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var burnBuff = BuffConfigurator.New(FuriousDragoonBurnBuffName, FuriousDragoonBurnBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().RemoveSelf(),
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast,
                    triggerBeforeAttack: false)
                .AddNotDispelable()
                .Configure();

            var realBuff = BuffConfigurator.New(FuriousDragoonRealBuffName, FuriousDragoonRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddBuffExtraAttack(haste: false, number: 1, penalized: false)
                .AddComponent(new RemoveNextRoundAfterAttak())
                .AddNotDispelable()
                .Configure();

            var buff = BuffConfigurator.New(FuriousDragoonBuffName, FuriousDragoonBuffGuid)
                .SetDisplayName(FuriousDragoonAbilityName)
                .SetDescription(FuriousDragoonAbilityDescription)
                .SetIcon(FeatureRefs.DemonRageForcedRageFeature.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(FuriousDragoonAbilityName, FuriousDragoonAbilityGuid)
                .SetDisplayName(FuriousDragoonAbilityName)
                .SetDescription(FuriousDragoonAbilityDescription)
                .SetIcon(FeatureRefs.DemonRageForcedRageFeature.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDoNotTurnOffOnRest()
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(FuriousDragoonName, FuriousDragoonGuid)
                .SetDisplayName(FuriousDragoonName)
                .SetDescription(FuriousDragoonDescription)
                .SetIcon(FeatureRefs.DemonRageForcedRageFeature.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        private static BlueprintFeature CreateBrutalDragoon()
        {
            BuffConfigurator.New(BrutalDragoonRealBuffName, BrutalDragoonRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(new RemoveNextRoundAfterAttak())
                .AddComponent(new BrutalDragoonExtraDamage())
                .AddNotDispelable()
                .Configure();

            return FeatureConfigurator.New(BrutalDragoonName, BrutalDragoonGuid)
                .SetDisplayName(BrutalDragoonName)
                .SetDescription(BrutalDragoonDescription)
                .SetIcon(AbilityRefs.ChannelRage.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        internal static void HandleOtherMods()
        {
            BlueprintTool.Get<BlueprintFeature>(DragoonDiveGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;
            BlueprintTool.Get<BlueprintBuff>(KineticSpearRealBuffGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;
            BlueprintTool.Get<BlueprintBuff>(ImpalingCrashBurnBuffGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;
            BlueprintTool.Get<BlueprintBuff>(FuriousDragoonBurnBuffGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;

            // Solution to make blade AoO compatible with DarkCodex's Patch_AllowAoO
            // If DarkCodex is enabled, this will make the save permenantly dependent on DarkCodex
            var CreateAddMechanicsFeature = AccessTools.Method("CodexLib.Helper, CodexLib:CreateAddMechanicsFeature", new[] { Type.GetType("CodexLib.MechanicFeature, CodexLib") });
            var comp = CreateAddMechanicsFeature?.Invoke(null, new object[] { 8 }) as BlueprintComponent;
            if (comp != null)
                BuffConfigurator.For(KineticSpearRealBuffGuid).AddComponent(comp).Configure();

            // Fix whip to delay deactivate
            if (BlueprintTool.TryGet<BlueprintActivatableAbility>("a95f76e9cb344bef8d8bc3839d1a75dd", out var whip))
                whip.DeactivateImmediately = false;
        }
    }

    internal class AbilityDragoonDive : AbilityCustomLogic, IAbilityTargetRestriction
    {
        private bool isActivating = false;

        public override bool IsEngageUnit { get { return true; } }

        public override void Cleanup(AbilityExecutionContext context)
        {
            isActivating = false;
            context.Caster.State.Features.IsUntargetable.Release();
        }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            UnitEntityData caster = context.Caster;
            caster.State.Features.IsUntargetable.Retain();
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

            UnitPartKineticist kineticist = caster.Parts.Get<UnitPartKineticist>();
            if (kineticist == null)
                yield break;

            Vector3 initial = caster.Position;
            Vector3 end = target.Point;
            Vector3 delta = end - initial;

            // Check if things are activated
            bool kineticLeap = false, impalingCrash = false, furiousDragoon = false, vitalBlade = false ,
                brutalDragoon = caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.BrutalDragoonGuid)) != null;
            foreach (var buff in caster.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(KineticLancer.KineticLeapSwiftBuffName))
                    kineticLeap = true;
                if (buff.Blueprint.ToString().Equals(KineticLancer.ImpalingCrashBuffName))
                    impalingCrash = true;
                if (buff.Blueprint.ToString().Equals(KineticLancer.FuriousDragoonBuffName))
                    furiousDragoon = true;
                if (buff.Blueprint.ToString().Equals(KineticistGeneral.VitalBladeRealBuffName))
                    vitalBlade = true;
            }

            // Apply dragoon dive burn buffs and real buffs
            int dice = 0, diceMult = 1, bonus = 0, dmg, mainStatMod = kineticist?.MainStatBonus ?? 0;
            if (context.Ability.Blueprint.ToString().Equals(KineticLancer.DragoonDiveAbilityName))
            {
                caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.KineticLeapSwiftBuffGuid), caster, 6.Seconds());
                kineticLeap = true;
                caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.DragoonDiveBurnBuffGuid), caster);

                if (impalingCrash)
                {
                    caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.ImpalingCrashBurnBuffGuid), caster);
                    var impalingCrashRealBuff = caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.ImpalingCrashRealBuffGuid), caster);
                    var components = caster.Body.PrimaryHand.MaybeItem?.Blueprint.GetComponent<WeaponKineticBlade>()?.GetBlastAbility(caster).Blueprint.Components;
                    
                    // Calculate values for impaling crash to apply debuffs
                    foreach (var component in components ?? new BlueprintComponent[] {} )
                    {
                        if (component is ContextRankConfig config)
                        {
                            if (config.m_Type == AbilityRankType.DamageDice)
                                dice = config.GetValue(context);
                            else if (config.m_Type == AbilityRankType.DamageBonus)
                                bonus = config.GetValue(context);
                        }
                        
                        else if (component is ContextCalculateSharedValue sharedValue && 
                            sharedValue.ValueType == AbilitySharedValue.Damage && 
                            sharedValue.Value.BonusValue.ValueType == ContextValueType.Rank &&
                            sharedValue.Value.BonusValue.ValueRank == AbilityRankType.DamageDice)
                        {
                            diceMult = 2;
                        }
                    }
                    
                    dmg = dice * diceMult + bonus;
                    var impalingCrashApplyDebuffComponent = impalingCrashRealBuff.GetComponent<ImpalingCrashApplyDebuffComponent>();
                    impalingCrashApplyDebuffComponent.DMG = dmg;
                    impalingCrashApplyDebuffComponent.MainStatMod = mainStatMod;
                }

                if (furiousDragoon)
                {
                    caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.FuriousDragoonBurnBuffGuid), caster);
                    caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.FuriousDragoonRealBuffGuid), caster);
                }

                if (brutalDragoon)
                {
                    caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.BrutalDragoonRealBuffGuid), caster);
                }

                if (vitalBlade)
                {
                    caster.AddBuff(BlueprintTool.Get<BlueprintBuff>(EsotericBlade.VitalStrikeKineticBladeBuffGuid), caster);
                }
            }

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
            if (caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.ImpossibleLeapGuid)) != null &&
                kineticist.AcceptedBurn > 2)
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
            if (kineticLeap)
                ruleSkillCheck.Bonus.AddModifier(new ModifiableValue.Modifier()
                {
                    ModValue = caster.Stats.SkillMobility.BaseValue < 10 ? 10 : 20,
                    ModDescriptor = ModifierDescriptor.UntypedStackable,
                    Source = context.Ability.Fact
                });

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
                foreach (var buff in caster.Buffs)
                    if (buff.Blueprint.ToString().Equals(KineticLancer.DragoonDiveBurnBuffName))
                        buff.SetDuration(TimeSpan.FromSeconds(0));
                yield break; // No attack or other effects if prone
            }
            // Make attack if it's dragoon dive
            else if (target.Unit != null && context.Ability.Blueprint.ToString().Equals(KineticLancer.DragoonDiveAbilityName))
            {
                UnitAttack attack = new(target.Unit);
                attack.IgnoreCooldown();
                attack.Init(caster);
                attack.IsCharge = true;

                // Full attack for dragoon frenzy | Don't full attack if using Vital Blade
                attack.ForceFullAttack = caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonFrenzyGuid)) != null && caster.Buffs.GetBuff(BlueprintTool.Get<BlueprintBuff>(KineticistGeneral.VitalBladeRealBuffGuid)) == null;
                //caster.Commands.AddToQueueFirst(attack);
                caster.Commands.Run(attack);
            }

            // If using vital blade and Dragoon Dive
            if (target.Unit == null || !context.Ability.Blueprint.ToString().Equals(KineticLancer.DragoonDiveAbilityName) || !vitalBlade)
                yield break;

            var bladeComponents = caster.Body.PrimaryHand.MaybeItem?.Blueprint.GetComponent<WeaponKineticBlade>()?.GetBlastAbility(caster).Blueprint.Components;

            // Calculate values for Dragoon Dive w/ Vital Blade Damage (minumum) Same as impaling crash *See above*
            foreach (var component in bladeComponents ?? new BlueprintComponent[] { })
            {
                if (component is ContextRankConfig config)
                {
                    if (config.m_Type == AbilityRankType.DamageDice)
                        dice = config.GetValue(context);
                    else if (config.m_Type == AbilityRankType.DamageBonus)
                        bonus = config.GetValue(context);
                }

                else if (component is ContextCalculateSharedValue sharedValue &&
                    sharedValue.ValueType == AbilitySharedValue.Damage &&
                    sharedValue.Value.BonusValue.ValueType == ContextValueType.Rank &&
                    sharedValue.Value.BonusValue.ValueRank == AbilityRankType.DamageDice)
                {
                    diceMult = 2;
                }
            }
            var DMG = dice * diceMult + bonus;
            DamageTypeDescription damageTypeDescription = new() { Type = DamageType.Direct };
            BaseDamage baseDamage = damageTypeDescription.GetDamageDescriptor(DiceFormula.Zero, DMG).CreateDamage();
            baseDamage.SourceFact = caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonDiveGuid));

            foreach (UnitEntityData unit in Game.Instance.State.Units)
            {
                if (unit.IsUnitInRange(target.Point, 5.Feet().Meters, true) && unit.IsEnemy(caster) && unit != target.Unit)
                {
                    RuleDealDamage ruleDealDamage = new(caster, unit, new DamageBundle(baseDamage))
                    {
                        DisablePrecisionDamage = true,
                        Reason = baseDamage.SourceFact
                    };
                    context.TriggerRule(ruleDealDamage);
                }
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
        private LocalizedString failReason;

        public string GetAbilityCasterRestrictionUIText()
        {
            return failReason;
        }

        public bool IsCasterRestrictionPassed(UnitEntityData caster)
        {
            UnitPartKineticist unitPartKineticist = caster.Parts.Get<UnitPartKineticist>();
            if (unitPartKineticist == null)
            {
                failReason = LocalizedTexts.Instance.Reasons.NoResources;
                return false;
            }

            ItemEntity maybeItem = caster.Body.PrimaryHand.MaybeItem;
            bool flag = maybeItem is ItemEntityWeapon obj && obj.Blueprint.Category == WeaponCategory.KineticBlast;

            if (!flag)
            {
                failReason = LocalizedTexts.Instance.Reasons.SpecificWeaponRequired;
                return false;
            }

            failReason = null;
            return true;
        }
    }

    internal class DragoonDiveBurnDisplay : AbilityKineticist
    {
        public static int CalculateCostForBlade(UnitEntityData unit, WeaponKineticBlade weaponKB)
        {
            int bladeBurn = weaponKB.ActivationAbility.GetComponent<AbilityKineticist>().CalculateCost(weaponKB.GetActivationAbility(unit));

            int dragoonReduction = 1;
            if (unit.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.ImpossibleLeapGuid)) != null)
                dragoonReduction = 2;

            int impalingCrash = 0, impalingCrashBurn = 0, furiousDragoon = 0, furiousDragoonBurn = 0;
            foreach (var buff in unit.Buffs)
            {
                var buffString = buff.Blueprint.ToString();
                if (buffString.Equals(KineticLancer.DragoonDiveBurnBuffName))  // Avoid counting twice
                    dragoonReduction = 0;
                if (buffString.Equals(KineticLancer.ImpalingCrashBuffName))
                    impalingCrash = 1;
                if (buffString.Equals(KineticLancer.ImpalingCrashBurnBuffName))  // Avoid counting twice
                    impalingCrashBurn = 1;
                if (buffString.Equals(KineticLancer.FuriousDragoonBuffName))
                    furiousDragoon = 1;
                if (buffString.Equals(KineticLancer.FuriousDragoonBurnBuffName))  // Avoid counting twice
                    furiousDragoonBurn = 1;
            }

            return Math.Max(0, bladeBurn - dragoonReduction + impalingCrash - impalingCrashBurn + furiousDragoon - furiousDragoonBurn);
        }

        public override int CalculateCost(AbilityData ability)
        {
            var unit = ability.Caster.Unit;
            if (!(unit.Body.PrimaryHand.MaybeItem is ItemEntityWeapon obj && obj.Blueprint.Category == WeaponCategory.KineticBlast))
                return -1;

            var weaponKB = obj.Blueprint.GetComponent<WeaponKineticBlade>();
            if (weaponKB == null)
                return -1;

            return CalculateCostForBlade(unit, weaponKB);
        }

        public override string GetAbilityRestrictionUIText()
        {
            return LocalizedTexts.Instance.Reasons.KineticNotEnoughBurnLeft;
        }

        public override bool IsAbilityRestrictionPassed(AbilityData ability)
        {
            var kineticist = ability.Caster.Unit.Parts.Get<UnitPartKineticist>();
            int burn = CalculateCost(ability);
            if (kineticist == null || burn > kineticist.LeftBurn || burn > kineticist.LeftBurnThisRound)
                return false;
            return true;
        }

        public override void Spend(AbilityData ability)
        {
        }
    }

    internal class DragoonDiveBurnReduction : UnitFactComponentDelegate, IKineticistCalculateAbilityCostHandler, IGlobalSubscriber, ISubscriber, IUnitSubscriber
    {
        public void HandleKineticistCalculateAbilityCost(UnitDescriptor caster, BlueprintAbility abilityBlueprint, ref KineticistAbilityBurnCost cost)
        {
            if (!caster.Equals(Owner.Descriptor))
                return;
            int num = 1;
            if (caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.ImpossibleLeapGuid)) != null)
                num = 2;
            cost.GatherPower += num;
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

    internal class RemoveNextRoundAfterAttak : UnitBuffComponentDelegate, ITickEachRound, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public bool Attacked = false;

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            Attacked = true;
        }

        public void OnNewRound()
        {
            // Remove buff on new round, if not first round of combat
            if (Attacked)
                Owner.Buffs.RemoveFact(Fact.Blueprint);
        }
    }

    internal class ImpalingCrashApplyDebuffComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public int DMG = 0, MainStatMod = 0;

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (!evt.AttackRoll.IsHit || evt.Weapon.Blueprint.Category != WeaponCategory.KineticBlast)
                return;

            var debuff = evt.Target.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.ImpalingCrashDebuffGuid), evt.Initiator, MainStatMod.Rounds().Seconds).GetComponent<ImpalingCrashDebuffComponent>();
            debuff.DMG = DMG;
            debuff.MainStatMod = MainStatMod;
            debuff.Initiator = evt.Initiator;           
            if (CombatController.IsInTurnBasedCombat())
                debuff.AttachedRound = Game.Instance.TurnBasedCombatController.RoundNumber;
        }
    }

    internal class ImpalingCrashDebuffComponent : UnitFactComponentDelegate, ITickEachRound
    {
        public int DMG = 0, MainStatMod = 0;
        public UnitEntityData Initiator;
        public int AttachedRound = -1;

        public void OnNewRound()
        {
            if (!CombatController.IsInTurnBasedCombat() || Game.Instance.TurnBasedCombatController.RoundNumber > AttachedRound)
                CheckAndRemove();
        }

        private void CheckAndRemove()
        {
            DamageTypeDescription damageTypeDescription = new() { Type = DamageType.Direct };
            BaseDamage baseDamage = damageTypeDescription.GetDamageDescriptor(DiceFormula.Zero, DMG).CreateDamage();
            baseDamage.SourceFact = Initiator.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.ImpalingCrashGuid));
            RuleDealDamage ruleDealDamage = new(Initiator, Owner, new DamageBundle(baseDamage))
            {
                DisablePrecisionDamage = true,
                Reason = baseDamage.SourceFact
            };
            Context.TriggerRule(ruleDealDamage);
            RuleSkillCheck ruleSkillCheck = new(Owner, StatType.Strength, 10 + 2 * MainStatMod) { ShowAnyway = true };
            Context.TriggerRule(ruleSkillCheck);

            if (ruleSkillCheck.Success)
                Owner.Buffs.RemoveFact(Fact.Blueprint);
        }
    }

    internal class BrutalDragoonExtraDamage : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {
            var ability = evt.Reason.Context?.SourceAbility;
            if (ability == null)
                return;
            UnitPartKineticist unitPartKineticist = Owner.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
                return;
            BlueprintAbility blueprintAbility = SimpleBlueprintExtendAsObject.Or(ability.Parent, null) ?? ability;
            if ((bool)blueprintAbility && unitPartKineticist.Blasts.Contains(blueprintAbility) && evt.DamageBundle.First != null)
                evt.DamageBundle.First.AddModifier(evt.DamageBundle.First.Dice.ModifiedValue.Rolls, 
                    Owner.GetFact(BlueprintTool.Get<BlueprintFeature>(KineticLancer.BrutalDragoonGuid)));
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {
        }
    }


    // Following are patches to make sure kinetic blades do not deactivate when dragoon dive is available
    // And should deactivate on performing a normal kinetic blade attack, not when a unit wants to attack
    // These also prevent unit stuck forever if there is indeed no burn left for blade when atk is issued
    [HarmonyPatch(typeof(UnitUseAbility))]
    public class Patch_UnitUseAbility
    {
        // Unit can always use blade burn ability 
        [HarmonyPatch(nameof(UnitUseAbility.CanStart), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool Prefix1(UnitUseAbility __instance, ref bool __result)
        {
            var caster = __instance.Ability.Caster.Unit;
            var kineticist = caster.Parts.Get<UnitPartKineticist>();
            BlueprintItemWeapon bladeBP = caster.Body.PrimaryHand.MaybeWeapon?.Blueprint;
            WeaponKineticBlade blade = bladeBP?.GetComponent<WeaponKineticBlade>();
            if (kineticist == null || blade == null)
                return true;
            if (blade.m_ActivationAbility.Get() == __instance.Ability.Blueprint)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(AbilityKineticist))]
    public class Patch_AbilityKineticist
    {
        // Blade burn ability always passes restrictions check
        [HarmonyPatch(nameof(AbilityKineticist.IsAbilityRestrictionPassed))]
        [HarmonyPrefix]
        public static bool Prefix1(AbilityKineticist __instance, ref bool __result, AbilityData ability)
        {
            var caster = ability.Caster.Unit;
            var kineticist = caster.Parts.Get<UnitPartKineticist>();
            BlueprintItemWeapon bladeBP = caster.Body.PrimaryHand.MaybeWeapon?.Blueprint;
            WeaponKineticBlade blade = bladeBP?.GetComponent<WeaponKineticBlade>();
            if (kineticist == null || blade == null)
                return true;
            if (blade.m_ActivationAbility.Get() == ability.Blueprint)
            {
                __result = true;
                return false;
            }
            return true;
        }

        // Blade burn ability will deactivate blade when burn insufficient, instead of stuck forever
        [HarmonyPatch(nameof(AbilityKineticist.Spend))]
        [HarmonyPrefix]
        public static bool Prefix2(AbilityKineticist __instance, AbilityData ability)
        {
            var caster = ability.Caster.Unit;
            var kineticist = caster.Parts.Get<UnitPartKineticist>();
            BlueprintItemWeapon bladeBP = caster.Body.PrimaryHand.MaybeWeapon?.Blueprint;
            WeaponKineticBlade blade = bladeBP?.GetComponent<WeaponKineticBlade>();
            if (kineticist == null || blade == null)
                return true;
            if (blade.m_ActivationAbility.Get() == ability.Blueprint && (AbilityKineticist.CalculateAbilityBurnCost(blade.GetActivationAbility(caster))?.Total ?? 0) > kineticist.LeftBurnThisRound)
            {
                caster.Commands.InterruptAll();
                foreach (var activatable in caster.Descriptor.ActivatableAbilities.RawFacts)
                {
                    if (activatable.IsOn && activatable.Blueprint.Buff?.GetComponent<AddKineticistBlade>()?.Blade == bladeBP)
                    {
                        activatable.SetIsOn(value: false, null);
                        activatable.Stop(forceRemovedBuff: true);
                        break;
                    }
                }
                caster.Commands.Queue.Clear();

                // TODO: Find better ways of dealing with TB action bar
                if (CombatController.IsInTurnBasedCombat())
                {
                    var states = Game.Instance.TurnBasedCombatController.CurrentTurn.GetActionsStates(caster);
                    states.Move.m_MovementActivityStateCurrent = CombatAction.ActivityState.Used;
                    states.Move.m_AttackActivityStateCurrent = CombatAction.ActivityState.Used;
                    states.Move.m_AbilityActivityStateCurrent = CombatAction.ActivityState.Used;
                    states.Move.m_MovementActivityStatePredicted = CombatAction.ActivityState.Used;
                    states.Move.m_AttackActivityStatePredicted = CombatAction.ActivityState.Used;
                    states.Move.m_AbilityActivityStatePredicted = CombatAction.ActivityState.Used;
                    states.Move.CurrentAbility = null;
                    states.Move.PredictedAbility = null;
                    states.Move.Type = CombatAction.UsageType.ChangeWeapon;
                    states.Standard.m_MovementActivityStateCurrent = CombatAction.ActivityState.Available;
                    states.Standard.m_AttackActivityStateCurrent = CombatAction.ActivityState.Available;
                    states.Standard.m_AbilityActivityStateCurrent = CombatAction.ActivityState.Available;
                    states.Standard.m_MovementActivityStatePredicted = CombatAction.ActivityState.Available;
                    states.Standard.m_AttackActivityStatePredicted = CombatAction.ActivityState.Available;
                    states.Standard.m_AbilityActivityStatePredicted = CombatAction.ActivityState.Available;
                    states.Standard.Type = CombatAction.UsageType.None;
                    states.Standard.CurrentAbility = null;
                    states.Standard.PredictedAbility = null;
                    states.Clear();
                }

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(KineticistController))]
    public class Patch_KineticistController
    {
        // Prevent KineticBladeEnableBuff on insufficient burn
        [HarmonyPrefix]
        [HarmonyPatch(nameof(KineticistController.TryActivateKineticBlade))]
        public static bool Prefix1(KineticistController __instance, [CanBeNull] UnitPartKineticist kineticist, RuleCastSpell rule)
        {
            if ((bool)kineticist)
            {
                BlueprintItemWeapon bladeBP = kineticist.Owner.Body.PrimaryHand.MaybeWeapon?.Blueprint;
                WeaponKineticBlade blade = bladeBP?.GetComponent<WeaponKineticBlade>();
                if (blade != null && blade.ActivationAbility == rule.Spell.Blueprint && (AbilityKineticist.CalculateAbilityBurnCost(blade.GetActivationAbility(kineticist.Owner))?.Total ?? 0) > kineticist.LeftBurnThisRound)
                {
                    kineticist.RemoveBladeActivatedBuff();
                    return false;
                }
            }
            return true;
        }
    }

    // Patch the BLOODY BloodyFaceController so it doesn't throw any warnings
    [HarmonyPatch(typeof(BloodyFaceController))]
    public class Patch_BloodyFaceController
    {
        [HarmonyPatch(nameof(BloodyFaceController.Init))]
        [HarmonyPrefix]
        public static bool Prefix1(BloodyFaceController __instance)
        {
            if (__instance.MyEntity == null && __instance.gameObject.GetComponent<UnitEntityView>() == null)
            {
                __instance.enabled = false;
                return false;
            }
            return true;
        }
    }
}
