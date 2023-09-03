using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using System;
using System.Collections.Generic;

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
        internal const string VitalBladeBuffName = "KineticistGeneral.VitalBladeInfusionBuff";
        internal const string VitalBladeBuffGuid = "7B8A66D6-EAFD-4FA7-A241-4CC46374244F";
        internal const string VitalBladeRealBuffName = "KineticistGeneral.VitalBladeInfusionRealBuff";
        internal const string VitalBladeRealBuffGuid = "3B9249F9-226F-4C4F-972D-8A7DBD7B0ABC";

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
        }

        private static BlueprintFeature CreateVitalBlade()
        {
            var buff = BuffConfigurator.New(VitalBladeBuffName, VitalBladeBuffGuid)
                .SetDisplayName(VitalBladeName)
                .SetDescription(VitalBladeDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent(new ReactivateKineticBladeComponent())
                .Configure();

            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 4,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = KineticDuelist.allBlades
            };

            var vital_strike_component = AbilityRefs.VitalStrikeAbility.Reference.Get().GetComponent<AbilityCustomVitalStrike>();

            var realBuff = BuffConfigurator.New(VitalBladeRealBuffName, VitalBladeRealBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(increaseBladeCost)
                .AddNotDispelable()
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(VitalBladeAbilityName, VitalBladeAbilityGuid)
                .SetDisplayName(VitalBladeName)
                .SetDescription(VitalBladeDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDoNotTurnOffOnRest()
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(VitalBladeName, VitalBladeGuid, FeatureGroup.KineticBlastInfusion)
                .SetDisplayName(VitalBladeName)
                .SetDescription(VitalBladeDescription)
                .SetIcon(AbilityRefs.VitalStrikeAbility.Reference.Get().Icon)
                .AddPrerequisiteClassLevel(CharacterClassRefs.KineticistClass.ToString(), 8)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        public static void HandleOtherMods()
        {
            // Update vital blade cost for expanded elements blades
            BlueprintTool.Get<BlueprintBuff>(VitalBladeRealBuffGuid)
                .GetComponent<AddKineticistBurnModifier>().m_AppliableTo = KineticDuelist.allBlades;
        }

        internal class UnitPartVitalBladeLimitAttacks : OldStyleUnitPart
        {
        }

        internal class UnitFactVitalBladeLimitAttacks : UnitFactComponentDelegate
        {

            public override void OnTurnOff()
            {
                base.Owner.Remove<UnitPartVitalBladeLimitAttacks>();
            }

            public override void OnTurnOn()
            {
                base.Owner.Ensure<UnitPartVitalBladeLimitAttacks>();
            }
        }

        [HarmonyPatch(typeof(RuleCalculateAttacksCount))]
        internal class Patch_RuleCalculateAttacksCount
        {
            [HarmonyPatch(nameof(RuleCalculateAttacksCount.OnTrigger))]
            [HarmonyPostfix]
            public static void Postfix1(ref RuleCalculateAttacksCount __instance, RulebookEventContext context)
            {
                var maybeCaster = __instance.Initiator;
                if (maybeCaster == null) return;
                var part = maybeCaster.Get<UnitPartVitalBladeLimitAttacks>();
                if (part == null) return;
                var weapon = maybeCaster.Body.PrimaryHand.MaybeItem?.Blueprint.GetComponent<WeaponKineticBlade>();
                if (weapon == null) return;

                __instance.Result.PrimaryHand.PenalizedAttacks = 0;
                __instance.Result.PrimaryHand.AdditionalAttacks = 1;
                __instance.Result.PrimaryHand.HasteAttacks = 0;
                __instance.Result.SecondaryHand.PenalizedAttacks = 0;
                __instance.Result.SecondaryHand.AdditionalAttacks = 0;
                __instance.Result.SecondaryHand.HasteAttacks = 0;
            }
        }
    }
}
