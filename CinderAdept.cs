using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticArchetypes
{
    internal class CinderAdept
    {
        internal const string ArchetypeName = "CinderAdeptArchetype";
        internal const string ArchetypeDisplayName = "CinderAdept.Name";
        internal const string ArchetypeDescription = "CinderAdept.Description";
        internal const string ArchetypeGuid = "FFF86252-CD22-47A1-BB53-58F6C39A7EE2";

        internal const string CinderAdeptClassSkillName = "CinderAdept.ClassSkill";
        internal const string CinderAdeptClassSkillGuid = "C00E9B6C-BAE6-4989-BC5D-3B4E311BB2E9";
        internal const string CinderAdeptClassSkillDescription = "CinderAdept.ClassSkill.Description";


        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.CinderAdept");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure kinetic duelist", e);
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
                //.RemoveFromClassSkills(new StatType[] { StatType.SkillUseMagicDevice })
                //.AddToClassSkills(StatType.SkillAthletics)
                .SetClassSkills(new StatType[] {StatType.SkillAthletics, StatType.SkillMobility, StatType.SkillStealth, StatType.SkillPerception, StatType.SkillPersuasion})
                .AddToRemoveFeatures(4, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(8, FeatureSelectionRefs.WildTalentSelection.ToString())
                .AddToRemoveFeatures(12, FeatureSelectionRefs.WildTalentSelection.ToString())
                .Configure();

            RestrictElementalSelection();
        }

        private static void RestrictElementalSelection()
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
    }
}
