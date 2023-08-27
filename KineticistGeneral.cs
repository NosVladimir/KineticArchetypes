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
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;

namespace KineticArchetypes
{
    internal class KineticistGeneral
    {
        internal const string ModuleName = "KineticistGeneralAdditions";

        internal const string VitalBladeName = "KineticistGeneral.VitalBladeInfusion";
        internal const string VitalBladeGuid = "992E5EB3-1CF8-4C82-BFC5-1F8C2FC15F6F";
        internal const string VitalBladeDescription = "KineticistGeneral.VitalBladeInfusion.Description";
        internal const string VitalBladeAbilityName = "KineticistGeneral.VitalBladeInfusionAbility";
        internal const string VitalBladeAbilityGuid = "BE23B0AA-73A0-4FC2-AB91-B91387AC3B18";
        internal const string VitalBladeAbilityDescription = "KineticistGeneral.VitalBladeInfusionAbility.Description";
        internal const string VitalBladeBuffName = "KineticistGeneral.VitalBladeInfusionBuff";
        internal const string VitalBladeBuffGuid = "7B8A66D6-EAFD-4FA7-A241-4CC46374244F";
        internal const string VitalBladeRealBuffName = "KineticistGeneral.VitalBladeInfusionRealBuff";
        internal const string VitalBladeRealBuffGuid = "3B9249F9-226F-4C4F-972D-8A7DBD7B0ABC";

        internal const string InfusionFeatureSelection = "58d6f8e9-eea6-3f64-18b1-07ce64f315ea";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.KineticLancer");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure General Additions", e);
            }
        }

        private static void ConfigureEnabled()
        {
            Logger.Info($"Configuring {ModuleName}");

            var vitalBlade = CreateVitalBlade();

            FeatureSelectionConfigurator.For(InfusionFeatureSelection).AddToAllFeatures(vitalBlade).Configure();
        }

        private static BlueprintFeature CreateVitalBlade()
        {
            var buff = BuffConfigurator.New(VitalBladeBuffName, VitalBladeBuffGuid)
                .SetDisplayName(VitalBladeAbilityName)
                .SetDescription(VitalBladeAbilityDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent(new ReactivateKineticBladeComponent())
                .Configure();

            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 2,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var realBuff = BuffConfigurator.New(VitalBladeRealBuffName, VitalBladeRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddComponent(new VitalBladeComponent())
                .AddNotDispelable()
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(VitalBladeAbilityName, VitalBladeAbilityGuid)
                .SetDisplayName(VitalBladeAbilityName)
                .SetDescription(VitalBladeAbilityDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDoNotTurnOffOnRest()
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(VitalBladeName, VitalBladeGuid)
                .SetDisplayName(VitalBladeName)
                .SetDescription(VitalBladeDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .AddPrerequisiteClassLevel(CharacterClassRefs.KineticistClass.ToString(), 8)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        public static void HandleOtherMods()
        {

        }

        internal class VitalBladeComponent : VitalStrikeForKineticBlade
        {

        }
    }
}
