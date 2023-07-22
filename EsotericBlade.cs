using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Controllers;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Newtonsoft.Json;
using Kingmaker.View;
using Kingmaker.View.Equipment;

namespace KineticArchetypes
{
    internal class EsotericBlade
    {
        internal const string ArchetypeName = "EsotericBladeArchetype";
        internal const string ArchetypeDisplayName = "EsotericBlade.Name";
        internal const string ArchetypeDescription = "EsotericBlade.Description";
        internal const string ArchetypeGuid = "ADF1E1C8-357F-4C4E-B2F0-3D5584DE4B7F";

        internal const string ElementSelectionName = "EsotericBlade.ElementSelection";
        internal const string ElementSelectionGuid = "10321627-CD14-4926-9A38-C6C8A7F09564";
        internal const string KineticBladeName = "EsotericBlade.KineticBlade";
        internal const string KineticBladeGuid = "84C81187-9F14-4AE9-A855-15F9E6531A63";
        internal const string KineticBladeDescription = "EsotericBlade.KineticBlade.Description";

        internal const string WeaponTrainingKBName = "EsotericBlade.WeaponTrainingKineticBlade";
        internal const string WeaponTrainingKBGuid = "599347D9-2C80-406B-B47A-7598F7303615";
        internal const string WeaponTrainingKBDescription = "EsotericBlade.WeaponTrainingKineticBlade.Description";

        internal const string EmptyResourceName = "EsotericBlade.EmptyResource";
        internal const string EmptyResourceGuid = "65129688-FD7F-4267-B3D2-30F6AE4C2A01";

        internal const string ConstantEnergyName = "EsotericBlade.ConstantEnergy";
        internal const string ConstantEnergyGuid = "1FAF41F1-5BDA-48A7-B61C-71390635B57C";
        internal const string ConstantEnergyDescription = "EsotericBlade.ConstantEnergy.Description";

        internal const string VitalStrikeKineticBladeBuffName = "EsotericBlade.VitalStrikeKineticBladeBuff";
        internal const string VitalStrikeKineticBladeBuffGuid = "E2734902-5CC4-4F4C-8866-A7D9EAD92F8E";

        internal const string CondensedEnergyName = "EsotericBlade.CondensedEnergy";
        internal const string CondensedEnergyGuid = "7AD69CF2-4B68-4C55-97AF-2A7B7E7E0091";
        internal const string CondensedEnergyDescription = "EsotericBlade.CondensedEnergy.Description";

        internal const string ChangeKineticBladeShapeName = "KineticArchetypes.ChangeKineticBladeShape";
        internal const string ChangeKineticBladeShapeGuid = "43A3B05D-4801-4D02-9433-D7C7A5A0C5E7";
        internal const string ChangeKineticBladeShapeDescription = "KineticArchetypes.ChangeKineticBladeShape.Description";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.EsotericBlade");

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
                ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.FighterClass)
                    .SetLocalizedName(ArchetypeDisplayName)
                    .SetLocalizedDescription(ArchetypeDescription);

            var kineticBlade = CreateKineticBlade();
            var constantEnergy = CreateConstantEnergy();
            var weaponTrainingKB = CreateWeaponTrainingKineticBlade();
            var condensedEnergy = CreateCondensedEnergy();

            archetype
                .AddToRemoveFeatures(1, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(4, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(12, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(20, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(5, FeatureSelectionRefs.WeaponTrainingSelection.ToString())
                .AddToRemoveFeatures(9, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())
                .AddToRemoveFeatures(13, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())
                .AddToRemoveFeatures(17, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())

                .AddToAddFeatures(1, ProgressionRefs.KineticBlastProgression.ToString())
                .AddToAddFeatures(1, kineticBlade)
                .AddToAddFeatures(4, constantEnergy)
                .AddToAddFeatures(5, weaponTrainingKB)
                .AddToAddFeatures(9, weaponTrainingKB)
                .AddToAddFeatures(12, condensedEnergy)
                .AddToAddFeatures(13, weaponTrainingKB)
                .AddToAddFeatures(17, weaponTrainingKB)
                .Configure();

            var FighterClass = CharacterClassRefs.FighterClass.Reference.Get();
            UIGroup uiGroup0 = new();
            uiGroup0.Features.Add(weaponTrainingKB);
            
            var oldUIGroup = FighterClass.Progression.UIGroups;
            int num = oldUIGroup.Length;
            var newUIGroup = new UIGroup[num + 1];
            if (num > 0)
                Array.Copy(oldUIGroup, newUIGroup, num);
            newUIGroup[num] = uiGroup0;
            FighterClass.Progression.UIGroups = newUIGroup;


            ProgressionConfigurator.For(ProgressionRefs.KineticBlastProgression)
                .AddToArchetypes(ArchetypeGuid)
                .Configure();

            var changeKBShape = AbilityConfigurator.New(ChangeKineticBladeShapeName, ChangeKineticBladeShapeGuid)
                .SetDisplayName(ChangeKineticBladeShapeName)
                .SetDescription(ChangeKineticBladeShapeDescription)
                .SetIcon(AbilityRefs.MagicDomainBaseAbility.Reference.Get().Icon)
                .AddComponent(new RememberWeaponShape())
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Personal)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            FeatureConfigurator.For(FeatureRefs.KineticBladeInfusion)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { changeKBShape.ToReference<BlueprintUnitFactReference>() })
                .Configure();
        }

        private static BlueprintFeature CreateKineticBlade()
        {
            var reduceBladeCost = new AddKineticistBurnModifier
            {
                Value = -1,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = new BlueprintAbilityReference[]
                {
                    AbilityRefs.KineticBladeColdBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeElectricBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeFireBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                }
            };

            // Fix Earth and Fire progressions
            ProgressionConfigurator.For(ProgressionRefs.EarthBlastProgression)
                .AddFeatureIfHasFact(FeatureRefs.EarthBlastFeature.ToString(), FeatureRefs.EarthBlastFeature.ToString(), true)
                .Configure();

            ProgressionConfigurator.For(ProgressionRefs.FireBlastProgression)
                .AddFeatureIfHasFact(FeatureRefs.FireBlastFeature.ToString(), FeatureRefs.FireBlastFeature.ToString(), true)
                .SetIcon(AbilityRefs.ScorchingRay.Reference.Get().Icon)
                .Configure();

            var allElements = new Blueprint<BlueprintFeatureReference>[]
            {
                ProgressionRefs.ElectricBlastProgression.Reference.Get(),
                ProgressionRefs.FireBlastProgression.Reference.Get(),
                ProgressionRefs.ColdBlastProgression.Reference.Get()
            };

            var emptyResource = AbilityResourceConfigurator
                .New(EmptyResourceName, EmptyResourceGuid)
                .SetMax(0)
                .Configure();

            var addKineticistPart = new AddKineticistPart()
            {
                m_Class = CharacterClassRefs.FighterClass.Cast<BlueprintCharacterClassReference>().Reference,
                MainStat = StatType.Strength,
                m_MaxBurn = emptyResource.ToReference<BlueprintAbilityResourceReference>(),
                m_MaxBurnPerRound = emptyResource.ToReference<BlueprintAbilityResourceReference>(),
                m_GatherPowerAbility = AbilityRefs.GatherPower.Cast<BlueprintAbilityReference>().Reference,
                m_GatherPowerBuff1 = BuffRefs.GatherPowerBuffI.Cast<BlueprintBuffReference>().Reference,
                m_GatherPowerBuff2 = BuffRefs.GatherPowerBuffII.Cast<BlueprintBuffReference>().Reference,
                m_GatherPowerBuff3 = BuffRefs.GatherPowerBuffIII.Cast<BlueprintBuffReference>().Reference,
                m_Blasts = FeatureRefs.BurnFeature.Reference.Get().GetComponent<AddKineticistPart>().m_Blasts,
                m_BladeActivatedBuff = BuffRefs.KineticBladeEnableBuff.Cast<BlueprintBuffReference>().Reference,
                m_CanGatherPowerWithShieldBuff = null
            };

            var elementSelection = FeatureSelectionConfigurator.New(ElementSelectionName, ElementSelectionGuid)
                .SetDisplayName(ElementSelectionName)
                .SetAllFeatures(allElements)
                .Configure();

            var feature = FeatureSelectionConfigurator.New(KineticBladeName, KineticBladeGuid)
                .SetDisplayName(KineticBladeName)
                .SetDescription(KineticBladeDescription)
                .SetIcon(AbilityRefs.BlessWeapon.Reference.Get().Icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>
                {
                    FeatureRefs.KineticBladeInfusion.Cast<BlueprintUnitFactReference>().Reference,
                    AbilityRefs.GatherPower.Cast<BlueprintUnitFactReference>().Reference,
                })
                .AddComponent(reduceBladeCost)
                .SetAllFeatures(allFeatures: elementSelection)
                .AddAbilityResources(0, emptyResource)
                .AddComponent(addKineticistPart)
                .AddProficiencies(weaponProficiencies: new WeaponCategory[] { WeaponCategory.KineticBlast })
                // Add additional spell peneration for caster level
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { CharacterClassRefs.FighterClass.ToString() }))
                .AddSpellPenetrationBonus(checkFact: false, value: ContextValues.Rank())
                .SetIsClassFeature()
                .Configure();

            return feature;
        }

        private static BlueprintFeature CreateWeaponTrainingKineticBlade()
        {
            return FeatureConfigurator.New(WeaponTrainingKBName, WeaponTrainingKBGuid)
                .SetDisplayName(WeaponTrainingKBName)
                .SetDescription(WeaponTrainingKBDescription)
                .SetIcon(AbilityRefs.BlessWeapon.Reference.Get().Icon)
                .AddWeaponGroupAttackBonus(attackBonus: 1, descriptor: ModifierDescriptor.WeaponTraining, multiplyByContext: false, weaponGroup: WeaponFighterGroup.None)
                .AddComponent(new AddWeaponTrainingKBDamage())
                .AddWeaponTraining()
                .SetRanks(10)
                .SetGroups(FeatureGroup.WeaponTraining)
                .SetIsClassFeature(true)
                .SkipAddToSelections()
                .Configure();
        }

        private static BlueprintFeature CreateConstantEnergy()
        {
            FeatureConfigurator.For(FeatureRefs.CleavingFinish).AddComponent(new CleavingFinishForKineticBlade()).Configure();

            BuffConfigurator.New(VitalStrikeKineticBladeBuffName, VitalStrikeKineticBladeBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.VitalStrikeAbility).AddComponent(new VitalStrikeForKineticBlade()).Configure();
            AbilityConfigurator.For(AbilityRefs.VitalStrikeAbilityImproved).AddComponent(new VitalStrikeForKineticBlade()).Configure();
            AbilityConfigurator.For(AbilityRefs.VitalStrikeAbilityGreater).AddComponent(new VitalStrikeForKineticBlade()).Configure();

            return FeatureConfigurator.New(ConstantEnergyName, ConstantEnergyGuid)
                .SetDisplayName(ConstantEnergyName)
                .SetDescription(ConstantEnergyDescription)
                .SetIcon(FeatureRefs.EldritchFontEldritchSurge.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure(); ;
        }

        private static BlueprintFeature CreateCondensedEnergy() 
        {
            return FeatureConfigurator.New(CondensedEnergyName, CondensedEnergyGuid)
                .SetDisplayName(CondensedEnergyName)
                .SetDescription(CondensedEnergyDescription)
                .SetIcon(FeatureRefs.EldritchFontGreaterSurge.Reference.Get().Icon)
                .AddComponent(new EnergyBladeCritsBonusComponent())
                .SetIsClassFeature()
                .Configure();
        }

        private static void AddModBladeToFeatures()
        {
            var modBladeBurnGuids = new string[]
            {
                "4471efdc8c1440faba7110675ddb31af", // Negative
                "22d5001563c74bdfa6e0c5602fd11eef", // Positive
            };
            var modBlades = new List<BlueprintAbilityReference>();
            for (int i = 0; i < modBladeBurnGuids.Length; i++)
            {
                bool got = BlueprintTool.TryGet(modBladeBurnGuids[i], out BlueprintAbility bp);
                if (got)
                {
                    Logger.Info($"Mod blade {bp} found, adding refs to EsotericBlade");
                    modBlades.Add(bp.ToReference<BlueprintAbilityReference>());
                }
                else
                    Logger.Info($"Mod blade {modBladeBurnGuids[i]} not found");
            }
            var allBlades = BlueprintTool.GetRef<BlueprintFeatureReference>(KineticBladeGuid)
                .Get().GetComponent<AddKineticistBurnModifier>().m_AppliableTo;
            if (modBlades.Count != 0)
                allBlades = allBlades.Concat(modBlades).ToArray();
            BlueprintTool.Get<BlueprintFeature>(KineticBladeGuid).GetComponent<AddKineticistBurnModifier>().m_AppliableTo = allBlades;
        }

        private static void AddModBlastProgressionToSelection()
        {
            var modBlastProgressionGuids = new string[]
            {
                "21b063289b4f4c7783a24b179a0ea3c0", // Negative
                "d0d8d2bb86d44473bd24ceb34f0ef6ea", // Positive
            };
            var modBlasts = new List<Blueprint<BlueprintFeatureReference>>();
            for (int i = 0; i < modBlastProgressionGuids.Length; i++)
            {
                bool got = BlueprintTool.TryGet(modBlastProgressionGuids[i], out BlueprintFeature bp);
                if (got)
                {
                    Logger.Info($"Mod blast {bp} found, adding refs to EsotericBlade");
                    modBlasts.Add(bp.ToReference<BlueprintFeatureReference>());
                }
                else
                    Logger.Info($"Mod blast {modBlastProgressionGuids[i]} not found");
            }
            FeatureSelectionConfigurator.For(ElementSelectionGuid)
                .AddToAllFeatures(modBlasts.ToArray())
                .Configure();
        }

        private static void AddModBlastToKineticistPart()
        {
            BlueprintTool.Get<BlueprintFeature>(KineticBladeGuid).GetComponent<AddKineticistPart>().m_Blasts =
                FeatureRefs.BurnFeature.Reference.Get().GetComponent<AddKineticistPart>().m_Blasts;
        }

        public static void HandleOtherMods()
        {
            AddModBladeToFeatures();
            AddModBlastProgressionToSelection();
            AddModBlastToKineticistPart();

            // Solution to make blade AoO compatible with DarkCodex's Patch_AllowAoO
            // If DarkCodex is enabled, this will make the save permenantly dependent on DarkCodex
            var CreateAddMechanicsFeature = AccessTools.Method("CodexLib.Helper, CodexLib:CreateAddMechanicsFeature", new[] { Type.GetType("CodexLib.MechanicFeature, CodexLib") });
            var comp = CreateAddMechanicsFeature?.Invoke(null, new object[] { 8 }) as BlueprintComponent;
            if (comp != null)
                FeatureConfigurator.For(ConstantEnergyGuid).AddComponent(comp).Configure();
        }
    }

    [HarmonyPatch(typeof(UnitPartWeaponTraining))]
    public class Patch_UnitPartWeaponTraining
    {
        [HarmonyPatch(nameof(UnitPartWeaponTraining.GetWeaponRank), new Type[] { typeof(ItemEntityWeapon) })]
        [HarmonyPrefix]
        [HarmonyPriority(Priority.High)]
        public static bool Prefix(UnitPartWeaponTraining __instance, ref int __result, ItemEntityWeapon weapon)
        {
            if (weapon == null)
            {
                __result = 0;
                return false;
            }

            foreach (EntityFact weaponTraining in __instance.WeaponTrainings)
            {
                foreach (EntityFactComponent component in weaponTraining.Components)
                {
                    WeaponFighterGroup? weaponFighterGroup = (component.SourceBlueprintComponent as WeaponGroupAttackBonus)?.WeaponGroup;
                    if (weaponFighterGroup.HasValue && weaponFighterGroup == 0 && weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    {
                        __result = weaponTraining.GetRank();
                        return false;
                    }
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(WeaponGroupAttackBonus))]
    public class Patch_WeaponGroupAttackBonus
    {
        [HarmonyPatch(nameof(WeaponGroupAttackBonus.OnEventAboutToTrigger), new Type[] { typeof(RuleCalculateAttackBonusWithoutTarget) })]
        [HarmonyPrefix]
        [HarmonyPriority(Priority.High)]
        public static bool Prefix(WeaponGroupAttackBonus __instance, RuleCalculateAttackBonusWithoutTarget evt)
        {
            if (__instance.WeaponGroup == 0 && evt.Weapon != null && evt.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
            {
                evt.AddModifier(bonus: __instance.AttackBonus * __instance.Fact.GetRank(), source: __instance.Fact, 
                    descriptor: ModifierDescriptor.WeaponTraining);
                return false;
            }
            return true;
        }
    }

    internal class AddWeaponTrainingKBDamage : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            if (evt.SourceAbility == null)
                return;
            UnitPartKineticist unitPartKineticist = Owner.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
                return;
            BlueprintAbility blueprintAbility = SimpleBlueprintExtendAsObject.Or(evt.SourceAbility.Parent, null) ?? evt.SourceAbility;
            if ((bool)blueprintAbility && unitPartKineticist.Blasts.Contains(blueprintAbility))
                evt.DamageBundle.First?.AddModifier(Fact.GetRank(), Fact);
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }
    }

    [HarmonyPatch(typeof(AddKineticistPart))]
    public class Patch_AddKineticistPart
    {
        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.AddKineticistPart");

        [HarmonyPatch(nameof(AddKineticistPart.OnTurnOn))]
        [HarmonyPrefix]
        public static bool Prefix1(AddKineticistPart __instance)
        {
            var kineticistPart = __instance.Owner.Get<UnitPartKineticist>();
            if (kineticistPart != null)
            {
                var burnResource = kineticistPart.m_Settings.MaxBurn;
                var got = BlueprintTool.TryGet(EsotericBlade.EmptyResourceGuid, out BlueprintAbilityResource bpEmptyResource);
                if (got && burnResource.AssetGuid.m_Guid.ToString().Equals(bpEmptyResource.AssetGuid.m_Guid.ToString()))
                {
                    Logger.Info($"Empty resource found, removing current kineticist part to add new kineticist part");
                   __instance.Owner.Remove<UnitPartKineticist>();
                    return true;
                }
                return false;
            }

            return true;
        }
    }

    internal class CleavingFinishForKineticBlade : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, IInitiatorRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
        }

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            var ability = evt.Reason?.Ability?.Blueprint;
            if (ability != null && evt.Initiator == Owner &&
                Owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(EsotericBlade.ConstantEnergyGuid)) != null &&
                ability.GetComponent<AbilityKineticist>() != null && ability.GetComponent<AbilityDeliveredByWeapon>() != null &&
                evt.Target.HPLeft < 0 && evt.Target.HPLeft + evt.Result > 0)
            {
                // Check for cleaving finish cooldown
                bool cooldown = false;
                foreach (var buff in Owner.Buffs)
                    if (buff.Blueprint == BuffRefs.CleavingFinishCooldown.Reference.Get())
                        cooldown = true;
                if (cooldown)
                    return;

                // Make new attack
                var weapon = Owner.Body.PrimaryHand.Weapon;
                if (weapon is null || weapon.Blueprint.Type.Category != WeaponCategory.KineticBlast)
                    return;
                UnitEntityData newTarget = ContextActionMeleeAttack.SelectTarget(Owner, weapon.AttackRange.Meters, true, evt.Target);
                if (newTarget is null)
                    return;

                RuleAttackWithWeapon ruleAttackWithWeapon = new(Owner, newTarget, weapon, 0)
                {
                    Reason = Context,
                    ExtraAttack = true,
                    AttackNumber = 0,
                    AttacksCount = 1
                };
                Context.TriggerRule(ruleAttackWithWeapon);
                
                // Add cooldown if not having improved feature
                if (Owner.GetFeature(FeatureRefs.ImprovedCleavingFinish.Reference) is null)
                    Owner.AddBuff(BuffRefs.CleavingFinishCooldown.Reference.Get(), Owner, duration: 6.Seconds());
            }
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            // Remove cooldown on first attack
            var buffs = Owner.Buffs.Enumerable.ToArray();
            if (evt.IsFullAttack && evt.IsFirstAttack)
                foreach (var buff in buffs)
                    if (buff.Blueprint.ToString().Equals(BuffRefs.CleavingFinishCooldown.Reference.Get().Name))
                        buff.Remove();
        }
    }

    internal class VitalStrikeForKineticBlade : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            var ability = evt.Reason?.Ability?.Blueprint;
            if (ability == null || evt.Initiator != Owner ||
                Owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(EsotericBlade.ConstantEnergyGuid)) == null ||
                ability.GetComponent<AbilityKineticist>() == null || ability.GetComponent<AbilityDeliveredByWeapon>() == null)
                return;

            var vitalStrikeMod = Fact.Blueprint.GetComponent<AbilityCustomVitalStrike>().VitalStrikeMod;
            var mythicFact = Owner.HasFact(Fact.Blueprint.GetComponent<AbilityCustomVitalStrike>().MythicBlueprint);
            var rowdy = Owner.HasFact(Fact.Blueprint.GetComponent<AbilityCustomVitalStrike>().RowdyFeature);

            bool hasBuff = false;
            foreach (var buff in Owner.Buffs)
                if (buff.Blueprint.ToString().Equals(EsotericBlade.VitalStrikeKineticBladeBuffName))
                {
                    hasBuff = true;
                    if (buff.TimeLeft > 1.Seconds())
                        buff.SetDuration(1.Seconds());
                    break;
                }

            if (!hasBuff)
                return;

            BaseDamage damage = evt.DamageBundle.WeaponDamage;
            damage.PostCritIncrements.AddDiceModifier((vitalStrikeMod - 1) * damage.Dice.ModifiedValue.Rolls, Fact);
            int bonus = damage.Bonus;
            var weaponTraining = Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(EsotericBlade.WeaponTrainingKBGuid));
            if (weaponTraining != null)
                bonus += weaponTraining.GetRank();
            if (mythicFact)
                damage.PostCritIncrements.AddBonusModifier(bonus * (vitalStrikeMod - 1), Fact);

            int sneakAttackDiceCount = evt.Initiator.Descriptor.Stats.SneakAttack.ModifiedValue;
            if (rowdy && sneakAttackDiceCount > 0)
            {
                DamageDescription damageDescription2 = new() { SourceFact = Fact };
                DamageTypeDescription typeDescription = damage.CreateTypeDescription();
                damageDescription2.TypeDescription = new DamageTypeDescription
                {
                    Common = new DamageTypeDescription.CommomData
                    {
                        Alignment = typeDescription.Common.Alignment,
                        Precision = true,
                        Reality = typeDescription.Common.Reality
                    },
                    Energy = typeDescription.Energy,
                    Physical = new DamageTypeDescription.PhysicalData
                    {
                        Enhancement = typeDescription.Physical.Enhancement,
                        EnhancementTotal = typeDescription.Physical.EnhancementTotal,
                        Form = typeDescription.Physical.Form,
                        Material = typeDescription.Physical.Material
                    },
                    Type = typeDescription.Type
                };
                damageDescription2.Dice = new DiceFormula(2 * sneakAttackDiceCount, DiceType.D6);
                var bonusSneakAttackDmg = damageDescription2.CreateDamage();
                evt.Add(bonusSneakAttackDmg);
            }
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }
    }

    [HarmonyPatch(typeof(AbilityCustomVitalStrike))]
    public class Patch_AbilityCustomVitalStrike
    {
        [HarmonyPatch(nameof(AbilityCustomVitalStrike.Deliver))]
        [HarmonyPrefix]
        public static void Prefix1(AbilityCustomVitalStrike __instance, AbilityExecutionContext context, TargetWrapper target)
        {
            UnitEntityData maybeCaster = context.MaybeCaster;
            if (maybeCaster != null && maybeCaster.HasFact(BlueprintTool.Get<BlueprintFeature>(EsotericBlade.ConstantEnergyGuid)))
            {
                maybeCaster.AddBuff(BlueprintTool.Get<BlueprintBuff>(EsotericBlade.VitalStrikeKineticBladeBuffGuid), maybeCaster);
            }
        }
    }

    internal class RememberWeaponPart : UnitPart
    {
        [JsonProperty]
        public BlueprintItemWeapon RememberedWeapon { get; set; }

        [JsonProperty]
        public BlueprintItemWeapon RememberedLongSpear { get; set; }
    }

    internal class RememberWeaponShape : AbilityCustomLogic
    {
        public override void Cleanup(AbilityExecutionContext context)
        {
        }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            var caster = context.MaybeCaster;
            if (caster is null)
                yield break;

            var weapon = caster.Body.PrimaryHand.Weapon?.Blueprint;
            if (weapon is not null && weapon.Type.Category != WeaponCategory.KineticBlast && !weapon.IsTwoHanded && !weapon.IsRanged && !weapon.IsNatural)
                caster.Parts.Ensure<RememberWeaponPart>().RememberedWeapon = weapon;
            else if (weapon is not null && weapon.Type.Category == WeaponCategory.Longspear &&
                caster.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.KineticSpearGuid)) != null)
                caster.Parts.Ensure<RememberWeaponPart>().RememberedLongSpear = weapon;
            else
                caster.Parts.Ensure<RememberWeaponPart>().RememberedWeapon = null;

            yield break;
        }
    }

    internal class EnergyBladeCritsBonusComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            var rememberedWeapon = Owner.Parts.Ensure<RememberWeaponPart>().RememberedWeapon;
            if (evt.Weapon != null && evt.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast && evt.Weapon.Blueprint.AttackType == AttackType.Touch && rememberedWeapon != null)
            {
                int bonusCritEdge = 20 - rememberedWeapon.Type.CriticalRollEdge;
                int bonusCritMultiplier = (int)rememberedWeapon.Type.CriticalModifier - 2;
                if (bonusCritEdge > 0)
                    evt.CriticalEdgeBonus += bonusCritEdge;
                if (bonusCritMultiplier > 0)
                    evt.AdditionalCriticalMultiplier.Add(new Modifier(bonusCritMultiplier, Fact, ModifierDescriptor.UntypedStackable));
            }
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}
