using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using TabletopTweaks.Core.Utilities;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Controllers.Combat;
using Kingmaker.ElementsSystem;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using Kingmaker.Blueprints.Root;
using Kingmaker.UnitLogic.ActivatableAbilities;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Blueprints.ModReferences;
using Kingmaker.UnitLogic.Class.Kineticist.Actions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Buffs;

namespace KineticArchetypes
{
    internal class KineticDuelist
    {
        internal const string ArchetypeName = "KineticDuelistArchetype";
        internal const string ArchetypeDisplayName = "KineticDuelist.Name";
        internal const string ArchetypeDescription = "KineticDuelist.Description";
        internal const string ArchetypeGuid = "179E8E47-35B8-48EF-84AE-10C6C0A067D3";

        internal const string ProficienciesDisplayName = "KineticDuelist.Proficiencies";
        internal const string ProficienciesDescription = "KineticDuelist.Proficiencies.Description";
        internal const string ProficienciesGuid = "B41F957B-5905-411E-B22F-DD6BED8BFB6E";

        internal const string KDKineticBladeName = "KineticDuelist.KineticBlade";
        internal const string KDKineticBladeGuid = "4C91D695-D84E-4AD5-AED4-0BDD3FD15CEA";
        internal const string KDKineticBladeDescription = "KineticDuelist.KineticBlade.Description";

        internal const string KineticDualBladesFeatureName = "KineticDuelist.KineticDualBladesFeature";
        internal const string KineticDualBladesFeatureGuid = "062A5F9E-493E-473D-B0D5-CA4CEAC0A29A";
        internal const string KineticDualBladesFeatureDescription = "KineticDuelist.KineticDualBladesFeature.Description";

        internal const string KineticDualBladesAbilityName = "KineticDuelist.KineticDualBladesAbility";
        internal const string KineticDualBladesAbilityGuid = "11EAB799-7E33-45B1-A006-5A48A9ADAB5E";
        internal const string KineticDualBladesAbilityDescription = "KineticDuelist.KineticDualBladesAbility.Description";

        internal const string KineticDualBladesBuffName = "KineticDuelist.KineticDualBladesBuff";
        internal const string KineticDualBladesBuffGuid = "93CB0B72-368D-4C07-B4DC-B9509719CD1F";
        internal const string KineticDualBladesBuffDescription = "KineticDuelist.KineticDualBladesBuff.Description";

        internal const string ImprovedKineticDualBladesName = "KineticDuelist.ImprovedKineticDualBlades";
        internal const string ImprovedKineticDualBladesGuid = "7DB4530F-7B21-4301-9F8D-99F405D1048D";
        internal const string ImprovedKineticDualBladesDescription = "KineticDuelist.ImprovedKineticDualBlades.Description";

        internal const string SynchronousChargeFeatureName = "KineticDuelist.SynchronousChargeFeature";
        internal const string SynchronousChargeFeatureGuid = "124E9629-F53D-48F3-BB69-308AF29F80F2";
        internal const string SynchronousChargeFeatureDescription = "KineticDuelist.SynchronousChargeFeature.Description";

        internal const string KineticAssaultFeatureName = "KineticDuelist.KineticAssaultFeature";
        internal const string KineticAssaultFeatureGuid = "74FDA391-BF63-4F36-8666-84DEBE7C70F2";
        internal const string KineticAssaultFeatureDescription = "KineticDuelist.KineticAssaultFeature.Description";

        internal const string GreaterKineticDualBladesName = "KineticDuelist.GreaterKineticDualBlades";
        internal const string GreaterKineticDualBladesGuid = "E9D7BB7A-A4E4-4CE2-BC83-5A63F50CC869";
        internal const string GreaterKineticDualBladesDescription = "KineticDuelist.GreaterKineticDualBlades.Description";

        internal const string DualBlades2ndAttackBuffName = "KineticDuelist.DualBlades2ndAttackBuff";
        internal const string DualBlades2ndAttackBuffGuid = "2DC5E636-E9B7-402D-9D1F-0A821D836E32";
        internal const string DualBlades2ndAttackBuffDescription = "KineticDuelist.DualBlades2ndAttackBuff.Description";

        internal const string DualBlades3rdAttackBuffName = "KineticDuelist.DualBlades3rdAttackBuff";
        internal const string DualBlades3rdAttackBuffGuid = "44BE1F78-BCFD-4C4D-B6BA-874B725F5BDE";
        internal const string DualBlades3rdAttackBuffDescription = "KineticDuelist.DualBlades3rdAttackBuff.Description";

        internal const string SupressBladeBurnBuffName = "KineticDuelist.SupressBladeBurnBuff";
        internal const string SupressBladeBurnBuffGuid = "5DA2EB59-8D41-4D03-94FD-F7BFD8DD77F1";
        internal const string SupressBladeBurnBuffDescription = "KineticDuelist.SupressBladeBurnBuff.Description";

        internal static readonly BlueprintAbilityReference[] allBlades = {
                // This seems enough to apply to all kinetic blades
                ActivatableAbilityRefs.KineticBladeAirBlastAbility.Cast<BlueprintAbilityReference>().Reference
            };

    internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.KineticDuelist");

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

            // Remove features
            archetype
                .AddToRemoveFeatures(1, FeatureRefs.KineticistProficiencies.ToString())
                .AddToRemoveFeatures(3, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(9, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(11, FeatureRefs.Supercharge.ToString())
                .AddToRemoveFeatures(13, FeatureRefs.MetakinesisQuickenFeature.ToString())
                ;

            // Add features
            var kdprof = ProficienciesFeature();
            var blade0 = KDKineticBladeFeature();
            var blade1 = KineticDualBladesFeature();
            var blade2 = ImprovedKineticDualBladesFeature();
            var synCha = SynchronousChargeFeature();
            var kinAss = KineticAssaultFeature();
            var blade3 = GreaterKineticDualBladesFeature();
            archetype
                .AddToAddFeatures(1, kdprof)
                .AddToAddFeatures(1, blade0)
                .AddToAddFeatures(3, blade1)
                .AddToAddFeatures(9, blade2)
                .AddToAddFeatures(11, synCha)
                .AddToAddFeatures(13, kinAss)
                .AddToAddFeatures(15, blade3)
                .Configure();
            var KineticistClass = CharacterClassRefs.KineticistClass.Reference.Get();
            KineticistClass.Progression.UIGroups = KineticistClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(blade0, blade1, blade2, blade3));

            var atk2nd = DualBlades2ndAttackBuff();
            var atk3rd = DualBlades3rdAttackBuff();

            RestrictFormInfusionSelections();
            SupressBladeBurnOnFullAttack();
        }

        private static BlueprintFeature ProficienciesFeature()
        {
            var kineticistProfAddFacts = new AddFacts();
            var newProfFacts = new BlueprintUnitFactReference[] {
                FeatureRefs.SimpleWeaponProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.LightArmorProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.MediumArmorProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.BucklerProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.RayCalculateFeature.Cast<BlueprintUnitFactReference>().Reference,
            };
            kineticistProfAddFacts.m_Facts = newProfFacts;

            return FeatureConfigurator.New(ProficienciesDisplayName, ProficienciesGuid)
              .SetDisplayName(ProficienciesDisplayName)
              .SetDescription(ProficienciesDescription)
              .SetIcon(FeatureRefs.MediumArmorProficiency.Reference.Get().Icon)
              .SetIsClassFeature(true)
              .AddComponent(kineticistProfAddFacts)
              .AddProficiencies(weaponProficiencies: new WeaponCategory[] { WeaponCategory.KineticBlast })
              .Configure();
        }

        private static BlueprintFeature KDKineticBladeFeature()
        {
            // Give kinetic blade as bonus infusion
            var addFact = new AddFacts();
            addFact.m_Facts = new BlueprintUnitFactReference[] { FeatureRefs.KineticBladeInfusion.Cast<BlueprintUnitFactReference>().Reference };

            // Reduce kinetic blade cost by 1
            var reduceBladeCost = new AddKineticistBurnModifier();
            reduceBladeCost.Value = -1;
            reduceBladeCost.BurnType = KineticistBurnType.Infusion;
            reduceBladeCost.m_AppliableTo = allBlades;

            return FeatureConfigurator.New(KDKineticBladeName, KDKineticBladeGuid)
                .SetDisplayName(KDKineticBladeName)
                .SetDescription(KDKineticBladeDescription)
                .SetIcon(AbilityRefs.BlessWeapon.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .AddComponent(addFact)
                .AddComponent(reduceBladeCost)
                .Configure();
        }

        private static BlueprintFeature KineticDualBladesFeature()
        {
            // Dual blade buff
            var increaseBladeCost = new AddKineticistBurnModifier();
            increaseBladeCost.Value = 1;
            increaseBladeCost.BurnType = KineticistBurnType.Infusion;
            increaseBladeCost.m_AppliableTo = allBlades;

            var buff = BuffConfigurator.New(KineticDualBladesBuffName, KineticDualBladesBuffGuid)
                .SetDisplayName(KineticDualBladesBuffName)
                .SetDescription(KineticDualBladesBuffDescription)
                .SetIcon(AbilityRefs.DivineFavor.Reference.Get().Icon)
                .AddComponent(increaseBladeCost)
                .Configure();

            // Dual blade ability
            var ability = AbilityConfigurator.New(KineticDualBladesAbilityName, KineticDualBladesAbilityGuid)
                .SetDisplayName(KineticDualBladesAbilityName)
                .SetDescription(KineticDualBladesAbilityDescription)
                .SetIcon(AbilityRefs.DivineFavor.Reference.Get().Icon)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(buff, isNotDispelable: true))
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.Personal)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            var addFact = new AddFacts();
            addFact.m_Facts = new BlueprintUnitFactReference[] {
                FeatureRefs.TwoWeaponFighting.Cast<BlueprintUnitFactReference>().Reference,
                ability.ToReference<BlueprintUnitFactReference>()
            } ;

            return FeatureConfigurator.New(KineticDualBladesFeatureName, KineticDualBladesFeatureGuid)
                .SetDisplayName(KineticDualBladesFeatureName)
                .SetDescription(KineticDualBladesFeatureDescription)
                .SetIcon(FeatureRefs.TwoWeaponFighting.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .AddComponent(addFact)
                .Configure();
        }

        private static BlueprintFeature ImprovedKineticDualBladesFeature()
        {
            return FeatureConfigurator.New(ImprovedKineticDualBladesName, ImprovedKineticDualBladesGuid)
                .SetDisplayName(ImprovedKineticDualBladesName)
                .SetDescription(ImprovedKineticDualBladesDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingImproved.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature SynchronousChargeFeature()
        {
            return FeatureConfigurator.New(SynchronousChargeFeatureName, SynchronousChargeFeatureGuid)
                .SetDisplayName(SynchronousChargeFeatureName)
                .SetDescription(SynchronousChargeFeatureDescription)
                .SetIcon(AbilityRefs.JoyfulRapture.Reference.Get().Icon)
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().Add(new Heal1BurnAcceptedThisTurn()),
                    triggerBeforeAttack: false,
                    onlyHit: true,
                    onlyOnFirstHit: true,
                    onlyOnFullAttack: true,
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature KineticAssaultFeature()
        {
            return FeatureConfigurator.New(KineticAssaultFeatureName, KineticAssaultFeatureGuid)
                .SetDisplayName(KineticAssaultFeatureName)
                .SetDescription(KineticAssaultFeatureDescription)
                .SetIcon(BuffRefs.FreedomOfMovementBuff.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature GreaterKineticDualBladesFeature()
        {
            return FeatureConfigurator.New(GreaterKineticDualBladesName, GreaterKineticDualBladesGuid)
                .SetDisplayName(GreaterKineticDualBladesName)
                .SetDescription(GreaterKineticDualBladesDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingGreater.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }
    
        private static BlueprintBuff DualBlades2ndAttackBuff()
        {
            return BuffConfigurator.New(DualBlades2ndAttackBuffName, DualBlades2ndAttackBuffGuid)
                .SetDisplayName(DualBlades2ndAttackBuffName)
                .SetDescription(DualBlades2ndAttackBuffDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingImproved.Reference.Get().Icon)
                .SetFlags(new BlueprintBuff.Flags[] { BlueprintBuff.Flags.HiddenInUi })
                .AddComponent(new BuffExtraOffhandBladeAttack())
                .AddNotDispelable()
                .Configure();
        }

        private static BlueprintBuff DualBlades3rdAttackBuff()
        {
            return BuffConfigurator.New(DualBlades3rdAttackBuffName, DualBlades3rdAttackBuffGuid)
                .SetDisplayName(DualBlades3rdAttackBuffName)
                .SetDescription(DualBlades3rdAttackBuffDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingGreater.Reference.Get().Icon)
                .SetFlags(new BlueprintBuff.Flags[] { BlueprintBuff.Flags.HiddenInUi })
                .AddComponent(new BuffExtraOffhandBladeAttack())
                .AddNotDispelable()
                .Configure();
        }

        private static void RestrictFormInfusionSelections()
        {
            var formInfusions = new Blueprint<BlueprintReference<BlueprintFeature>>[]
            {
                FeatureRefs.CloudInfusion,
                FeatureRefs.EruptionInfusion,
                FeatureRefs.SprayInfusion,
                FeatureRefs.FragmentationInfusion,
                FeatureRefs.DeadlyEarthInfusion,
                FeatureRefs.CycloneInfusion,
                FeatureRefs.DetonationInfusion,
                FeatureRefs.WallInfusion,
                FeatureRefs.SpindleInfusion
            };
            foreach (var infusion in formInfusions) 
            {
                FeatureConfigurator.For(infusion)
                    .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                    .Configure();
            }
        }

        private static void SupressBladeBurnOnFullAttack()
        {
            // Temporarily supress kinetic blade burn cost
            // so it won't deactivate itself after 1st attack due to no burn left
            var supressBlastBurn = new AddKineticistBurnModifier();
            supressBlastBurn.Value = -100;
            supressBlastBurn.BurnType = KineticistBurnType.Blast;
            supressBlastBurn.m_AppliableTo = allBlades;

            var supressInfusionBurn = new AddKineticistBurnModifier();
            supressInfusionBurn.Value = -100;
            supressInfusionBurn.BurnType = KineticistBurnType.Infusion;
            supressInfusionBurn.m_AppliableTo = allBlades;

            var supressMetakinesisBurn = new AddKineticistBurnModifier();
            supressMetakinesisBurn.Value = -100;
            supressMetakinesisBurn.BurnType = KineticistBurnType.Metakinesis;
            supressMetakinesisBurn.m_AppliableTo = allBlades;

            var buff = BuffConfigurator.New(SupressBladeBurnBuffName, SupressBladeBurnBuffGuid)
                .SetDisplayName(SupressBladeBurnBuffName)
                .SetDescription(SupressBladeBurnBuffDescription)
                .SetFlags(new BlueprintBuff.Flags[] { BlueprintBuff.Flags.HiddenInUi })
                // IMMEDIATELY REMOVES ITSELF, but still works like a charm
                .AddRemoveBuffOnTurnOn()
                .AddNotDispelable()
                .AddComponent(supressBlastBurn)
                .AddComponent(supressInfusionBurn)
                .AddComponent(supressMetakinesisBurn)
                .Configure();
            
            FeatureConfigurator.For(FeatureRefs.KineticBladeInfusion)
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().Add(new SupressBladeBurn(buff: buff)),
                    triggerBeforeAttack: true,
                    onlyHit: false)
                .Configure();
        }
    }

    public class BuffExtraOffhandBladeAttack : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttacksCount>, IRulebookHandler<RuleCalculateAttacksCount>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAttacksCount evt)
        {
            var offhand = Owner.Body.SecondaryHand;
            if (offhand.HasWeapon && offhand.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                evt.AddExtraAttacks(1, false, true, offhand.Weapon);
        }

        public void OnEventDidTrigger(RuleCalculateAttacksCount evt)
        {
        }
    }

    public class SupressBladeBurn : ContextAction
    {
        public BlueprintBuff buff;

        public SupressBladeBurn(BlueprintBuff buff)
        {
            this.buff = buff;
        }

        public override string GetCaption()
        {
            return "Kineticist supressing blade burn";
        }

        public override void RunAction()
        {
            KineticDuelist.Logger.Info("Supressing blade burn");
            UnitPartKineticist unitPartKineticist = Context.MaybeCaster?.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
            {
                KineticDuelist.Logger.Error("Caster is not kineticist");
            }
            else
            {
                //unitPartKineticist.HealBurn(1);
                if (buff != null)
                    unitPartKineticist.Owner.AddBuff(buff, unitPartKineticist.Owner);
            }
        }
    }

    public class Heal1BurnAcceptedThisTurn : ContextAction
    {
        public override string GetCaption()
        {
            return "Kineticist heal 1 burn accepted this turn";
        }

        public override void RunAction()
        {
            KineticDuelist.Logger.Info("Healing 1 burn");
            UnitPartKineticist unitPartKineticist = Context.MaybeCaster?.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
            {
                KineticDuelist.Logger.Error("Caster is not kineticist");
            }
            else
            {
                if (unitPartKineticist.AcceptedBurnThisRound > 0)
                    unitPartKineticist.HealBurn(1);
            }
        }
    }

    [HarmonyPatch(typeof(AddKineticistBlade))]
    public class Patch_AddKineticistBlade
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.AddKineticistBlade");

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnActivate))]
        public static void Postfix1(AddKineticistBlade __instance)
        {
            Kingmaker.EntitySystem.Entities.UnitEntityData owner = __instance.Owner;

            // Allow AoO if having KD blade feature
            if (owner.GetFeature(BlueprintTools.GetBlueprint<BlueprintFeature>(KineticDuelist.KDKineticBladeGuid)) != null)
                owner.State.RemoveCondition(UnitCondition.DisableAttacksOfOpportunity);

            // Spawn off-hand blade if dual blade activated
            bool dualbuff = false;
            foreach (var buff in owner.Buffs)
                if (buff.Blueprint.ToString().Equals(KineticDuelist.KineticDualBladesBuffName))
                    dualbuff = true;
            if (dualbuff)
            {
                Logger.Info("Try to insert kinetic blade to off hand");
                var bladeOffHand = (ResourcesLibrary.TryGetBlueprint(__instance.m_Blade.Guid) as BlueprintItemWeapon).CreateEntity<ItemEntityWeapon>();
                bladeOffHand.MakeNotLootable();
                if (owner.Body.SecondaryHand.HasItem || !owner.Body.SecondaryHand.CanInsertItem(bladeOffHand))
                {
                    Logger.Info("Can't insert kineticist blade to off hand");
                }
                else
                {
                    owner.Body.SecondaryHand.InsertItem(bladeOffHand);
                    Logger.Info("Inserted kinetic blade to off hand");
                }

                // Add extra offhand attacks if having improved/greater dual blade features and no TWF features
                var TWFRank = owner.GetFeature(FeatureRefs.TwoWeaponFightingBasicMechanics.Reference.Get()).GetRank();
                if (owner.GetFeature(BlueprintTools.GetBlueprint<BlueprintFeature>(KineticDuelist.ImprovedKineticDualBladesGuid)) != null &&  TWFRank < 3)
                {
                    var buff1 = owner.AddBuff(BlueprintTools.GetBlueprint<BlueprintBuff>(KineticDuelist.DualBlades2ndAttackBuffGuid), owner);
                }
                if (owner.GetFeature(BlueprintTools.GetBlueprint<BlueprintFeature>(KineticDuelist.GreaterKineticDualBladesGuid)) != null && TWFRank < 4)
                {
                    var buff2 = owner.AddBuff(BlueprintTools.GetBlueprint<BlueprintBuff>(KineticDuelist.DualBlades3rdAttackBuffGuid), owner);
                }
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnDeactivate))]
        public static void Postfix2(AddKineticistBlade __instance)
        {
            var owner = __instance.Owner;
            var handsSets = owner.Body.HandsEquipmentSets;
            foreach (var handsSet in handsSets)
            {
                var offhand = handsSet.SecondaryHand;
                if (offhand.HasWeapon)
                {
                    // This variable is ESSENTIAL!!!
                    var weapon = offhand.Weapon;
                    if (weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    {
                        Logger.Info("Removing kinetic blade from off hand");
                        weapon.HoldingSlot.Lock.ReleaseAll();
                        weapon.HoldingSlot?.RemoveItem();
                        using (ContextData<ItemsCollection.SuppressEvents>.Request())
                            weapon.Collection?.Remove(offhand.Weapon);
                        Logger.Info("Removed kinetic blade from off hand");

                        Logger.Info($"Dispose garbage entity {offhand.Weapon}");
                        weapon.Dispose();
                    }
                }
            }

            // Remove dual blade buff and extra attack buffs
            foreach (var buff in owner.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(KineticDuelist.KineticDualBladesBuffName)   ||
                    buff.Blueprint.ToString().Equals(KineticDuelist.DualBlades2ndAttackBuffName) || 
                    buff.Blueprint.ToString().Equals(KineticDuelist.DualBlades3rdAttackBuffName))
                {
                    buff.SetDuration(TimeSpan.FromSeconds(0));
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnTurnOn))]
        public static void Postfix3(AddKineticistBlade __instance)
        {
            var handsSets = __instance.Owner.Body.HandsEquipmentSets;
            foreach (var handsSet in handsSets)
            {
                var offhand = handsSet.SecondaryHand;
                if (offhand.HasWeapon)
                {
                    if (offhand.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    {
                        offhand.Weapon.HoldingSlot.Lock.Retain();
                    }
                }
            }
        }
    }
}
