using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Class.Kineticist.ActivatableAbility;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Parts;
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var weaponTrainingKB = CreateWeaponTrainingKineticBlade();

            archetype
                .AddToRemoveFeatures(1, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(10, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(20, FeatureSelectionRefs.FighterFeatSelection.ToString())
                .AddToRemoveFeatures(5, FeatureSelectionRefs.WeaponTrainingSelection.ToString())
                .AddToRemoveFeatures(9, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())
                .AddToRemoveFeatures(13, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())
                .AddToRemoveFeatures(17, FeatureSelectionRefs.WeaponTrainingRankUpSelection.ToString())

                .AddToAddFeatures(1, ProgressionRefs.KineticBlastProgression.ToString())
                .AddToAddFeatures(1, kineticBlade)
                .AddToAddFeatures(5, weaponTrainingKB)
                .AddToAddFeatures(9, weaponTrainingKB)
                .AddToAddFeatures(13, weaponTrainingKB)
                .AddToAddFeatures(17, weaponTrainingKB)
                .Configure();

            var FighterClass = CharacterClassRefs.FighterClass.Reference.Get();
            UIGroup uiGroup0 = new();
            //uiGroup0.Features.Add(kineticBlade);
            uiGroup0.Features.Add(weaponTrainingKB);
            
            var oldUIGroup = FighterClass.Progression.UIGroups;
            int num = oldUIGroup.Length;
            var newUIGroup = new UIGroup[num + 1];
            if (num > 0)
                Array.Copy(oldUIGroup, newUIGroup, num);
            newUIGroup[num] = uiGroup0;
            FighterClass.Progression.UIGroups = newUIGroup;


            ProgressionConfigurator.For(ProgressionRefs.KineticBlastProgression.ToString())
                .AddToArchetypes(ArchetypeGuid)
                .Configure();
        }

        private static BlueprintFeature CreateKineticBlade()
        {
            var reduceBladeCost = new AddKineticistBurnModifier
            {
                Value = -5,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = new BlueprintAbilityReference[]
                {
                    AbilityRefs.KineticBladeAirBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeColdBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeEarthBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeElectricBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeFireBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                    AbilityRefs.KineticBladeWaterBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference
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
                //ProgressionRefs.AirBlastProgression.Reference.Get(),
                ProgressionRefs.ElectricBlastProgression.Reference.Get(),
                //ProgressionRefs.EarthBlastProgression.Reference.Get(),
                ProgressionRefs.FireBlastProgression.Reference.Get(),
                //ProgressionRefs.WaterBlastProgression.Reference.Get(),
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
                .Configure();
        }

        private static void AddModBladeToFeatures()
        {
            var modBladeBurnGuids = new string[]
            {
                "5d81270056d24a2e88df79dfb983cbcd", // Telekinetic
                "4471efdc8c1440faba7110675ddb31af", // Negative
                "1ec348ed9dcb437eb601f20b98f25181", // Gravity
                "e7c2a4e7dcae40b09dda30a879123483", // Wood
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
                //"6ce72cb2bf0244b0bd0e5e0a552a6a4a", // Telekinetic
                "21b063289b4f4c7783a24b179a0ea3c0", // Negative
                //"da0d241e1c63441e8d9ee50f61de8c1f", // Gravity
                //"736473267be3455bad091a5138423175", // Wood
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
}
