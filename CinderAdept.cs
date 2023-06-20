using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using static Kingmaker.Blueprints.Classes.BlueprintProgression;

namespace KineticArchetypes
{
    internal class CinderAdept
    {
        internal const string ArchetypeName = "CinderAdeptArchetype";
        internal const string ArchetypeDisplayName = "CinderAdept.Name";
        internal const string ArchetypeDescription = "CinderAdept.Description";
        internal const string ArchetypeGuid = "FFF86252-CD22-47A1-BB53-58F6C39A7EE2";

        internal const string FireFocusName = "CinderAdept.FireFocus";
        internal const string FireFocusGuid = "C00E9B6C-BAE6-4989-BC5D-3B4E311BB2E9";
        internal const string FireFocusDescription = "CinderAdept.FireFocus.Description";

        internal const string GallopingSiphonName = "CinderAdept.GallopingSiphon";
        internal const string GallopingSiphonGuid = "0B8452A6-EAD5-45D6-B478-4D3A90789802";
        internal const string GallopingSiphonDescription = "CinderAdept.GallopingSiphon.Description";
        internal const string GallopingSiphonBuffName = "CinderAdept.GallopingSiphonBuff";
        internal const string GallopingSiphonBuffGuid = "80CC7EA2-23E0-47BE-B801-61A0D86D07C8";
        internal const string GallopingSiphonBuffDescription = "CinderAdept.GallopingSiphonBuff.Description";

        internal const string MountName = "CinderAdept.Mount";
        internal const string MountGuid = "DB9D88FC-503A-4F30-A4FC-877077ABB24C";
        internal const string MountDescription = "CinderAdept.Mount.Description";
        internal const string MountProgressionName = "CinderAdept.MountProgression";
        internal const string MountProgressionGuid = "DAC4E751-3B10-4160-AFCF-211F3CD0C634";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.CinderAdept");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure Cinder adept", e);
            }
        }

        private static void ConfigureEnabled()
        {
            Logger.Info($"Configuring {ArchetypeName}");

            var archetype =
                ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.KineticistClass)
                    .SetLocalizedName(ArchetypeDisplayName)
                    .SetLocalizedDescription(ArchetypeDescription);

            archetype
                .SetReplaceClassSkills(true)
                .SetClassSkills(new StatType[] {StatType.SkillAthletics, StatType.SkillMobility, StatType.SkillStealth, StatType.SkillPerception, StatType.SkillPersuasion})
                .AddToRemoveFeatures(1, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(4, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(8, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(12, FeatureSelectionRefs.WildTalentSelection.ToString())
                
                .AddToAddFeatures(1, CreateFireFocus())
                .AddToAddFeatures(1, CreateGallopingSiphon())
                .AddToAddFeatures(4, CreateMount())
                .Configure();

            RestrictElementSelection();
        }

        private static BlueprintFeature CreateFireFocus()
        {
            return FeatureConfigurator.New(FireFocusName, FireFocusGuid)
                .SetDisplayName(FireFocusName)
                .SetDescription(FireFocusDescription)
                .SetIcon(AbilityRefs.SummonElementalHugeFire.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature CreateGallopingSiphon()
        {
            var feature = FeatureConfigurator.New(GallopingSiphonName, GallopingSiphonGuid)
                .SetReapplyOnLevelUp(true)
                .SetDisplayName(GallopingSiphonName)
                .SetDescription(GallopingSiphonDescription)
                .SetIcon(FeatureRefs.MountedCombat.Reference.Get().Icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { FeatureRefs.MountedCombat.Cast<BlueprintUnitFactReference>() })
                .AddContextRankConfig(ContextRankConfigs
                    .ClassLevel(new string[] { CharacterClassRefs.KineticistClass.ToString() })
                    .WithStartPlusDivStepProgression(divisor: 4, start: 0))
                .AddConcentrationBonus(
                    checkFact: true,
                    value: ContextValues.Rank(),
                    requiredFact: BuffRefs.MountedBuff.Cast<BlueprintUnitFactReference>())
                .SetIsClassFeature(true)
                .Configure();

            var buff = BuffConfigurator.New(GallopingSiphonBuffName, GallopingSiphonBuffGuid)
                .SetDisplayName(GallopingSiphonBuffName)
                .SetDescription(GallopingSiphonBuffDescription)
                .SetIcon(AbilityRefs.ResistFire.Reference.Get().Icon)
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Prolong)
                .AddContextRankConfig(ContextRankConfigs
                    .ClassLevel(new string[] { CharacterClassRefs.KineticistClass.ToString() })
                    .WithCustomProgression((10, 5), (20, 10)))
                .AddResistEnergy(value: ContextValues.Rank())
                .Configure();

            var gatherPowerRunActions = AbilityRefs.GatherPower.Reference.Get().GetComponent<AbilityEffectRunAction>();
            var contexActionApplyBuff = new ContextActionApplyBuff()
            {
                m_Buff = buff.ToReference<BlueprintBuffReference>(),
                UseDurationSeconds = true,
                DurationSeconds = 6,
                IsFromSpell = false,
                IsNotDispelable = true,
                AsChild = false
            };
            var applyBuffAction = new Conditional()
            {
                ConditionsChecker = new ConditionsChecker()
                {
                    Operation = Operation.And,
                    Conditions = new Condition[]
                    {
                        new ContextConditionHasBuff() { m_Buff = BuffRefs.MountedBuff.Cast<BlueprintBuffReference>().Reference },
                        new ContextConditionHasFact() { m_Fact = feature.ToReference<BlueprintUnitFactReference>() }
                    }
                },
                IfTrue = new ActionList() { Actions = new GameAction[]
                { 
                    contexActionApplyBuff,
                    new ContextActionsOnPet()
                    {
                        PetType = PetType.AnimalCompanion,
                        Actions = new ActionList() { Actions = new GameAction[] { contexActionApplyBuff} }
                    }
                } }
            };
            var oldActions = gatherPowerRunActions.Actions.Actions;
            int num = oldActions.Length;
            var newActions = new GameAction[num + 1];
            if (num > 0)
                Array.Copy(oldActions, newActions, num);
            newActions[num] = applyBuffAction;
            gatherPowerRunActions.Actions.Actions = newActions;
            return feature;
        }

        private static BlueprintFeature CreateMount()
        {
            var animalRank = FeatureRefs.AnimalCompanionRank.Cast<BlueprintFeatureBaseReference>().Reference;
            var levelEntries = new LevelEntry[] 
            { 
                new LevelEntry { Level = 5, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 6, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 7, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 8, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 9, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 10, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 11, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 12, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 13, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 14, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 15, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 16, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 17, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 18, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 19, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } },
                new LevelEntry { Level = 20, m_Features = new List<BlueprintFeatureBaseReference> { animalRank } }
            };

            var animalProgression = ProgressionConfigurator.New(MountProgressionName, MountProgressionGuid)
                .SetDisplayName(MountName)
                .SetDescription(MountDescription)
                .AddToLevelEntries(levelEntries)
                .SetClasses(new ClassWithLevel[] { new ClassWithLevel{
                    m_Class = CharacterClassRefs.KineticistClass.Cast<BlueprintCharacterClassReference>().Reference }})
                .SetIsClassFeature(true)
                .Configure();

            return FeatureSelectionConfigurator.New(MountName, MountGuid)
                .SetDisplayName(MountName)
                .SetDescription(MountDescription)
                .SetIcon(FeatureRefs.DruidNatureBond.Reference.Get().Icon)
                .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[]
                {
                    FeatureRefs.AnimalCompanionEmptyCompanion.Reference.Get(),
                    FeatureRefs.AnimalCompanionFeatureHorse.Reference.Get(),
                    FeatureRefs.AnimalCompanionFeatureHorse_PreorderBonus.Reference.Get()
                })
                .AddComponent(new AddFeatureOnApply() { m_Feature = animalProgression.ToReference<BlueprintFeatureReference>() })
                .AddComponent(new AddFeatureOnApply() { m_Feature = 
                    FeatureRefs.AnimalCompanionRank.Cast<BlueprintFeatureReference>().Reference })
                .AddComponent(new AddFeatureOnApply() { m_Feature = 
                    FeatureRefs.MountTargetFeature.Cast<BlueprintFeatureReference>().Reference })
                .AddComponent(new AddFeatureOnApply() { m_Feature =
                    FeatureSelectionRefs.AnimalCompanionArchetypeSelection.Cast<BlueprintFeatureReference>().Reference })
                .SetIsClassFeature(true)
                .Configure();
        }

        private static void RestrictElementSelection()
        {
            var elements = new BlueprintProgression[]
            {
                ProgressionRefs.ElementalFocusAir_0.Reference.Get(),
                ProgressionRefs.ElementalFocusEarth_0.Reference.Get(),
                ProgressionRefs.ElementalFocusWater_0.Reference.Get(),
                ProgressionRefs.SecondaryElementAir.Reference.Get(),
                ProgressionRefs.SecondaryElementEarth.Reference.Get(),
                ProgressionRefs.SecondaryElementWater.Reference.Get()
            };
            
            for (int i = 0; i < elements.Length; i++) 
            {
                ProgressionConfigurator.For(elements[i])
                    .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                    .Configure();
            }
        }

        public static void HandleOtherMods()
        {

            return;
        }
    }

    [HarmonyPatch(typeof(UnitPartRider))]
    public class Patch_UnitPartRider
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(UnitPartRider.Dismount), new Type[] { typeof(bool) })]
        public static void Postfix(UnitPartRider __instance)
        {
            var owner = __instance.Owner;
            foreach (var buff in owner.Buffs)
                if (buff.Blueprint.ToString().Equals(CinderAdept.GallopingSiphonBuffName))
                    buff.SetDuration(TimeSpan.FromSeconds(0));
                
            for (int i = 0; i < owner.Pets.Count; i++)
            {
                var pet = owner.Pets[i].Entity;
                if (pet != null && (pet.Get<UnitPartPet>()?.Type == PetType.AnimalCompanion))
                    foreach (var buff in pet.Buffs)
                        if (buff.Blueprint.ToString().Equals(CinderAdept.GallopingSiphonBuffName))
                            buff.SetDuration(TimeSpan.FromSeconds(0));
            }
        }
    }
}
