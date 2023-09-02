using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.UnitSettings;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace KineticArchetypes
{
    // TODO : I want to add Kinetic Railgun feat
    internal class OnslaughtBlaster
    {
        internal const string ArchetypeName = "OnslaughtBlasterArchetype";
        internal const string ArchetypeDisplayName = "OnslaughtBlaster.Name";
        internal const string ArchetypeDescription = "OnslaughtBlaster.Description";
        internal const string ArchetypeGuid = "081863E0-A59A-47D7-AED9-643A5EE6F356";

        internal const string OnslaughtBlastName = "OnslaughtBlaster.OnslaughtBlast";
        internal const string OnslaughtBlastGuid = "5707ADF5-0264-4D6B-90AE-64CF1DE96CC8";
        internal const string OnslaughtBlastDescription = "OnslaughtBlaster.OnslaughtBlast.Description";
        internal const string OnslaughtBlastAbilityName = "OnslaughtBlaster.OnslaughtBlastAbility";
        internal const string OnslaughtBlastAbilityGuid = "3F8C1884-4598-4AE5-964E-558E2E49A8BE";
        internal const string OnslaughtBlastBuffName = "OnslaughtBlaster.OnslaughtBlastBuff";
        internal const string OnslaughtBlastBuffGuid = "DDC76673-059B-4865-A23D-8AE7A1E78B7C";
        internal const string AutoTargetAbilityName = "OnslaughtBlaster.AutoTargetAbility";
        internal const string AutoTargetAbilityGuid = "981FE767-F917-4E8E-8DA2-03DBBE38CEC4";
        internal const string AutoTargetAbilityDescription = "OnslaughtBlaster.AutoTargetAbility.Description";
        internal const string AutoTargetBuffName = "OnslaughtBlaster.AutoTargetBuff";
        internal const string AutoTargetBuffGuid = "5308F79F-F786-4543-A3B9-409BC84F0FD7";
        internal const string AutoTargetBuffDescription = "OnslaughtBlaster.AutoTargetBuff.Description";

        internal const string FocusedBlasterName = "OnslaughtBlaster.FocusedBlaster";
        internal const string FocusedBlasterGuid = "074B60CD-4708-42C6-866E-C5E755A4F0CB";
        internal const string FocusedBlasterDescription = "OnslaughtBlaster.FocusedBlaster.Description";

        internal const string BurnedBlasterName = "OnslaughtBlaster.BurnedBlaster";
        internal const string BurnedBlasterGuid = "C9315F87-5F97-4B76-AA59-F791F0994E2A";
        internal const string BurnedBlasterDescription = "OnslaughtBlaster.BurnedBlaster.Description";

        internal const string ExcessiveBlasterName = "OnslaughtBlaster.ExcessiveBlaster";
        internal const string ExcessiveBlasterGuid = "3F4EFC7B-7D27-43B2-916C-9CEA68682E43";
        internal const string ExcessiveBlasterDescription = "OnslaughtBlaster.ExcessiveBlaster.Description";
        internal const string ExcessiveBlasterAbilityName = "OnslaughtBlaster.ExcessiveBlasterAbility";
        internal const string ExcessiveBlasterAbilityGuid = "7FB1ABD3-3BA1-4CBC-825B-0EC889BC7BC9";
        internal const string ExcessiveBlasterAbilityDescription = "OnslaughtBlaster.ExcessiveBlasterAbility.Description";
        internal const string ExcessiveBlasterBuffName = "OnslaughtBlaster.ExcessiveBlasterBuff";
        internal const string ExcessiveBlasterBuffGuid = "98B9AD77-DD65-4647-B480-8B3C21AC635D";

        internal const string TargetedStrikeName = "OnslaughtBlaster.TargetedStrike";
        internal const string TargetedStrikeGuid = "97ABCEDF-6339-43CA-A6C8-DF45DB8AF383";
        internal const string TargetedStrikeDescription = "OnslaughtBlaster.TargetedStrike.Description";
        internal const string TargetedStrikeDebuffName = "OnslaughtBlaster.TargetedStrikeDebuff";
        internal const string TargetedStrikeDebuffGuid = "41E91BC0-9C19-4DD2-B1D5-DAC2A85D7027";
        internal const string TargetedStrikeDebuffDescription = "OnslaughtBlaster.TargetedStrikeDebuff.Description";

        internal const string ExtremeBlasterName = "OnslaughtBlaster.ExtremeBlaster";
        internal const string ExtremeBlasterGuid = "1C27F829-3A7C-474D-9E8A-DA3EA38BD644";
        internal const string ExtremeBlasterDescription = "OnslaughtBlaster.ExtremeBlaster.Description";
        internal const string ExtremeBlasterAbilityName = "OnslaughtBlaster.ExtremeBlasterAbility";
        internal const string ExtremeBlasterAbilityGuid = "FB5521B8-8DE3-431E-AF57-3DF6D3F72B67";
        internal const string ExtremeBlasterAbilityDescription = "OnslaughtBlaster.ExtremeBlasterAbility.Description";
        internal const string ExtremeBlasterBuffName = "OnslaughtBlaster.ExtremeBlasterBuff";
        internal const string ExtremeBlasterBuffGuid = "2A64427A-A5B8-4121-8748-60A9468976F4";

        internal const string OmniBlasterName = "OnslaughtBlaster.OmniBlaster";
        internal const string OmniBlasterGuid = "AD496984-3A4C-499C-A94C-B1E579AAC181";
        internal const string OmniBlasterDescription = "OnslaughtBlaster.OmniBlaster.Description";
        internal const string OmniBlasterAbilityName = "OnslaughtBlaster.OmniBlasterAbility";
        internal const string OmniBlasterAbilityGuid = "FB25ADD7-1A58-4169-8EA8-92C0A3E79B01";
        internal const string OmniBlasterBuffName = "OnslaughtBlaster.OmniBlasterBuff";
        internal const string OmniBlasterBuffGuid = "D08956F9-DB1D-4331-BBF2-853910B6DCED";

        internal const string KineticRailgunName = "OnslaughtBlaster.KineticRailgun";
        internal const string KineticRailgunGuid = "72C23F97-9092-405F-8EB0-6D4665024BF3";
        internal const string KineticRailgunDescription = "OnslaughtBlaster.KineticRailgun.Description";

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.OnslaughtBlaster");

        internal static List<BlueprintAbilityReference> Blasts =
            FeatureRefs.BurnFeature.Reference.Get().GetComponent<AddKineticistPart>().m_Blasts.ToList();

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure Onslaught Blaster", e);
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
                .AddToRemoveFeatures(5, FeatureRefs.MetakinesisEmpowerFeature.ToString())
                .AddToRemoveFeatures(9, FeatureRefs.MetakinesisMaximizedFeature.ToString())
                .AddToRemoveFeatures(13, FeatureRefs.MetakinesisQuickenFeature.ToString())
                .AddToRemoveFeatures(11, FeatureRefs.Supercharge.ToString())
                .AddToRemoveFeatures(19, FeatureSelectionRefs.MetakinesisMaster.ToString())

                .AddToAddFeatures(1, CreateOnslaughtBlast())
                .AddToAddFeatures(1, CreateFocusedBlaster())
                .AddToAddFeatures(3, CreateBurnedBlaster())
                .AddToAddFeatures(5, CreateExcessiveBlaster())
                .AddToAddFeatures(9, CreateTargetedStrike())
                .AddToAddFeatures(13, CreateExtremeBlaster())
                .AddToAddFeatures(17, CreateOmniBlaster())
                .Configure();

            var kinetic_railgun_feat = FeatureConfigurator.New(KineticRailgunName, KineticRailgunGuid, FeatureGroup.Feat)
                .SetDisplayName(KineticRailgunName)
                .SetDescription(KineticRailgunDescription)
                .SetIcon(AbilityRefs.ClashingRocks.Reference.Get().Icon)
                .AddComponent(new KineticRailgunBurnReduction())
                .AddPrerequisiteFeature(OnslaughtBlaster.ExcessiveBlasterGuid)
                .Configure();
        }

        private static BlueprintFeature CreateOnslaughtBlast()
        {
            var autoTargetBuff = BuffConfigurator.New(AutoTargetBuffName, AutoTargetBuffGuid)
                .SetDisplayName(AutoTargetBuffName)
                .SetDescription(AutoTargetBuffDescription)
                .SetIcon(AbilityRefs.HolySmite.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();

            var autoTargetAbility = ActivatableAbilityConfigurator.New(AutoTargetAbilityName, AutoTargetAbilityGuid)
                .SetDisplayName(AutoTargetAbilityName)
                .SetDescription(AutoTargetAbilityDescription)
                .SetIcon(AbilityRefs.HolySmite.Reference.Get().Icon)
                .SetBuff(autoTargetBuff)
                .AddComponent(new ShowNumberOfBlasts())
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            BuffConfigurator.New(OnslaughtBlastBuffName, OnslaughtBlastBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent(new OnslaughtBlastBuffComponent())
                .AddNotDispelable()
                .Configure();

            var ability = AbilityConfigurator.New(OnslaughtBlastAbilityName, OnslaughtBlastAbilityGuid)
                .SetRange(AbilityRange.Personal)
                .SetActionType(UnitCommand.CommandType.Free)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .AddComponent(new AbilityOnslaughtBlast())
                .SetType(AbilityType.Physical)
                .SetDisableLog()
                .SetHidden()
                .Configure();

            return FeatureConfigurator.New(OnslaughtBlastName, OnslaughtBlastGuid)
                .SetDisplayName(OnslaughtBlastName)
                .SetDescription(OnslaughtBlastDescription)
                .SetIcon(AbilityRefs.HolySmite.Reference.Get().Icon)
                .AddComponent(new InitiateOnslaughtBlast())
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability, autoTargetAbility })
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateFocusedBlaster()
        {
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.GatherPowerModeMedium)
                .AddUniqueComponent(new OnslaughtBlasterGatherPowerRestriction(), ComponentMerge.Fail, null).Configure();
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.GatherPowerModeHigh)
                .AddUniqueComponent(new OnslaughtBlasterGatherPowerRestriction(), ComponentMerge.Fail, null).Configure();

            return FeatureConfigurator.New(FocusedBlasterName, FocusedBlasterGuid)
                .SetDisplayName(FocusedBlasterName)
                .SetDescription(FocusedBlasterDescription)
                .SetIcon(ActivatableAbilityRefs.GatherPowerModeLow.Reference.Get().Icon)
                .AddComponent(new FocusedBlasterBurnReduction())
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateBurnedBlaster()
        {
            return FeatureConfigurator.New(BurnedBlasterName, BurnedBlasterGuid)
                .SetDisplayName(BurnedBlasterName)
                .SetDescription(BurnedBlasterDescription)
                .SetIcon(AbilityRefs.ThunderingDrums.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateExcessiveBlaster()
        {
            var icon = AbilityRefs.CallLightning.Reference.Get().Icon;

            var buff = BuffConfigurator.New(ExcessiveBlasterBuffName, ExcessiveBlasterBuffGuid)
                .SetDisplayName(ExcessiveBlasterName)
                .SetDescription(ExcessiveBlasterAbilityDescription)
                .SetIcon(icon)
                .AddNotDispelable()
                .AddComponent(new ExBlastBurnIncrease() { Increase = 1 })
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(ExcessiveBlasterAbilityName, ExcessiveBlasterAbilityGuid)
                .SetDisplayName(ExcessiveBlasterName)
                .SetDescription(ExcessiveBlasterAbilityDescription)
                .SetIcon(icon)
                .SetBuff(buff)
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            return FeatureConfigurator.New(ExcessiveBlasterName, ExcessiveBlasterGuid)
                .SetDisplayName(ExcessiveBlasterName)
                .SetDescription(ExcessiveBlasterDescription)
                .SetIcon(icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateTargetedStrike()
        {
            BuffConfigurator.New(TargetedStrikeDebuffName, TargetedStrikeDebuffGuid)
                .SetDisplayName(TargetedStrikeDebuffName)
                .SetDescription(TargetedStrikeDebuffDescription)
                .SetIcon(FeatureRefs.CripplingStrike.Reference.Get().Icon)
                .AddNotDispelable()
                .AddStatBonus(ModifierDescriptor.UntypedStackable, stat: StatType.AC, value: -1)
                .SetStacking(StackingType.Stack)
                .Configure();

            return FeatureConfigurator.New(TargetedStrikeName, TargetedStrikeGuid)
                .SetDisplayName(TargetedStrikeName)
                .SetDescription(TargetedStrikeDescription)
                .SetIcon(FeatureRefs.CripplingStrike.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateExtremeBlaster()
        {
            var icon = AbilityRefs.CallLightningStorm.Reference.Get().Icon;

            var buff = BuffConfigurator.New(ExtremeBlasterBuffName, ExtremeBlasterBuffGuid)
                .SetDisplayName(ExtremeBlasterName)
                .SetDescription(ExtremeBlasterAbilityDescription)
                .SetIcon(icon)
                .AddNotDispelable()
                .AddComponent(new ExBlastBurnIncrease() { Increase = 3 })
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(ExtremeBlasterAbilityName, ExtremeBlasterAbilityGuid)
                .SetDisplayName(ExtremeBlasterName)
                .SetDescription(ExtremeBlasterAbilityDescription)
                .SetIcon(icon)
                .SetBuff(buff)
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            return FeatureConfigurator.New(ExtremeBlasterName, ExtremeBlasterGuid)
                .SetDisplayName(ExtremeBlasterName)
                .SetDescription(ExtremeBlasterDescription)
                .SetIcon(icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateOmniBlaster()
        {
            var icon = AbilityRefs.Stormbolts.Reference.Get().Icon;

            var buff = BuffConfigurator.New(OmniBlasterBuffName, OmniBlasterBuffGuid)
                .SetDisplayName(OmniBlasterName)
                .SetDescription(OmniBlasterDescription)
                .SetIcon(icon)
                .AddNotDispelable()
                .AddComponent(new ExBlastBurnIncrease() { Increase = 4 })
                .Configure();

            var ability = ActivatableAbilityConfigurator.New(OmniBlasterAbilityName, OmniBlasterAbilityGuid)
                .SetDisplayName(OmniBlasterName)
                .SetDescription(OmniBlasterDescription)
                .SetIcon(icon)
                .SetBuff(buff)
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            return FeatureConfigurator.New(OmniBlasterName, OmniBlasterGuid)
                .SetDisplayName(OmniBlasterName)
                .SetDescription(OmniBlasterDescription)
                .SetIcon(icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .SetIsClassFeature()
                .Configure();
        }

        public static bool InappropriateBlast(BlueprintAbility ability)
        {
            return //ability.ToString().Equals("FoeThrowInfusionThrowAbility") ||
                ability.GetComponent<AbilityKineticist>() == null ||
                ability.GetComponent<AbilityDeliveredByWeapon>() != null ||
                ability.GetComponent<AbilityEffectRunAction>()?.Actions.Actions[0] is ContextActionSpawnAreaEffect ||
                !(Blasts.Contains(ability.ToReference<BlueprintAbilityReference>()) || 
                    (ability.m_Parent != null && Blasts.Contains(ability.m_Parent)));
        }

        internal static void HandleOtherMods()
        {
            Blasts = FeatureRefs.BurnFeature.Reference.Get().GetComponent<AddKineticistPart>().m_Blasts.ToList();
        }
    }

    internal class OnslaughtBlasterGatherPowerRestriction : ActivatableAbilityRestriction
    {
        public override bool IsAvailable()
        {
            return Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(OnslaughtBlaster.FocusedBlasterGuid)) == null;
        }
    }

    internal class OnslaughtBlastPart : UnitPart
    {
        public bool Repeating = false;
        public AbilityData Blast = null;
        public TargetWrapper Target = null;

        [SerializeField]
        public int BlastRank = 0;

        [SerializeField]
        public int RodRank = 0;

        public void ClearAttributes()
        {
            Repeating = false;
            BlastRank = 0;
            Blast = null;
            Target = null;
            RodRank = 0;
        }

        public int CalculateNumberOfBlasts()
        {
            int number = BlastRank != 0 ? BlastRank : Owner.GetFeature(FeatureRefs.KineticBlastFeature.Reference).Rank;
            int level = Owner.Descriptor.Progression.GetClassLevel(CharacterClassRefs.KineticistClass.Reference);
            if (Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(OnslaughtBlaster.BurnedBlasterGuid)) != null)
            {
                int burn = Owner.Parts.Get<UnitPartKineticist>().AcceptedBurn;
                if (level > 2 && burn > 0)
                    number++;
                if (level > 8 && burn > 1)
                    number++;
                if (level > 14 && burn > 2)
                    number++;
            }

            bool excessive = false, extreme = false, omni = false, haste = false;
            foreach (var buff in Owner.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(OnslaughtBlaster.ExcessiveBlasterBuffName))
                    excessive = true;
                else if (buff.Blueprint.ToString().Equals(OnslaughtBlaster.ExtremeBlasterBuffName))
                    extreme = true;
                else if (buff.Blueprint.ToString().Equals(OnslaughtBlaster.OmniBlasterBuffName))
                    omni = true;
                else if (buff.Blueprint.ToString().Equals(BuffRefs.HasteBuff.Reference.GetBlueprint().ToString()) && Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(OnslaughtBlaster.KineticRailgunGuid)) != null)
                    haste = true;
            }

            if (haste)
                number++;

            if (omni)
                number *= 2;
            else
            {
                if (excessive)
                    number += Math.Max(level - 3, 0) / 4 + 1;
                if (extreme)
                    number += Math.Max(level - 13, 0) / 2 + 7;
            }

            return number;
        }
    }

    internal class ShowNumberOfBlasts : BlueprintComponent
    {
        public int GetNumber(UnitEntityData Unit)
        {
            return Unit.Parts.Get<OnslaughtBlastPart>()?.CalculateNumberOfBlasts() ?? 0;
        }
    }

    [HarmonyPatch(typeof(MechanicActionBarSlotActivableAbility))]
    public class Patch_MechanicActionBarSlotActivableAbility
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MechanicActionBarSlotActivableAbility.GetResource))]
        public static bool Prefix1(MechanicActionBarSlotActivableAbility __instance, ref int __result)
        {
            var component = __instance.ActivatableAbility.Blueprint.GetComponent<ShowNumberOfBlasts>();
            if (component == null)
                return true;
            __result = component.GetNumber(__instance.Unit);
            return false;
        }
    }

    internal class InitiateOnslaughtBlast : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, ISubscriber, IGlobalRulebookSubscriber
    {
        public override void OnTurnOn()
        {
            Owner.Ensure<OnslaughtBlastPart>();
        }

        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            var part = Owner.Parts.Get<OnslaughtBlastPart>();
            if (evt.IsDuplicateSpellApplied || evt.Spell?.Blueprint == null ||
                OnslaughtBlaster.InappropriateBlast(evt.Spell.Blueprint) || part == null || part.Repeating)
                return;

            foreach (var buff in Owner.Buffs)
                if (buff.Blueprint.ToString().Equals(OnslaughtBlaster.OnslaughtBlastBuffName))
                {
                    Owner.Facts.Remove(buff);
                    break;
                }

            foreach (var buff in Owner.Buffs)
                if (buff.Blueprint == BuffRefs.MetamagicRodLesserKineticBuff.Reference.Get())
                    part.RodRank = 1;

            part.BlastRank = Owner.GetFeature(FeatureRefs.KineticBlastFeature.Reference).Rank;
            part.Blast = evt.Spell;
            part.Target = evt.SpellTarget;

            Owner.AddBuff(BlueprintTool.Get<BlueprintBuff>(OnslaughtBlaster.OnslaughtBlastBuffGuid), Owner, duration: 5.Seconds());
            Owner.GetFeature(FeatureRefs.KineticBlastFeature.Reference).Rank = 1;
        }

        public void OnEventDidTrigger(RuleCastSpell evt)
        {
            var part = Owner.Parts.Get<OnslaughtBlastPart>();
            if (evt.IsDuplicateSpellApplied || evt.Spell?.Blueprint == null ||
                OnslaughtBlaster.InappropriateBlast(evt.Spell.Blueprint) || part == null || part.Repeating)
                return;

            part.Repeating = true;

            if (evt.Spell.Blueprint.ToString().Equals("FoeThrowInfusionThrowAbility"))
            {
                part.Repeating = false;
                return;
            }
            UnitUseAbility unitUseAbility = new(UnitCommand.CommandType.Free, Owner.Descriptor.Abilities.GetAbility(BlueprintTool.Get<BlueprintAbility>(OnslaughtBlaster.OnslaughtBlastAbilityGuid)).Data, new TargetWrapper(Owner));
            unitUseAbility.IgnoreCooldown();
            unitUseAbility.DisableLog = true;
            unitUseAbility.Init(Owner);

            Owner.Commands.AddToQueueOrRun(unitUseAbility, false);
        }
    }

    internal class OnslaughtBlastBuffComponent : UnitFactComponentDelegate, ISubscriber, IGlobalSubscriber, IRulebookHandler<RuleDealDamage>, IInitiatorRulebookHandler<RuleDealDamage>
    {
        [SerializeField]
        public OnslaughtBlastPart Part = null;

        private List<UnitEntityData> _targets = new();
        private List<int> _hits = new();
        private TimeSpan _lastFrameTime = TimeSpan.Zero;

        public void OnEventAboutToTrigger(RuleDealDamage evt) { }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Reason?.Ability?.Blueprint != Part.Blast.Blueprint)
                return;

            if (Owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(OnslaughtBlaster.TargetedStrikeGuid)) != null)
            {
                TimeSpan gameTime = Game.Instance.TimeController.GameTime;
                if (_lastFrameTime != gameTime)
                {
                    int index = _targets.IndexOf(evt.Target);
                    if (index != -1)
                    {
                        _hits[index]++;
                        OnslaughtBlaster.Logger.Info($"{_hits[index]} Hit, {_hits[index] % 5 == 0}");
                        if (_hits[index] % 5 == 0)
                            evt.Target.AddBuff(BlueprintTool.Get<BlueprintBuff>(OnslaughtBlaster.TargetedStrikeDebuffGuid), Owner, 6.Seconds());
                    }
                    else
                    {
                        _targets.Add(evt.Target);
                        _hits.Add(1);
                        OnslaughtBlaster.Logger.Info("First hit");
                    }
                    _lastFrameTime = gameTime;
                }
            }

            if (!Part.Repeating)
                Owner.Buffs.RemoveFact(Fact.Blueprint);
        }

        public override void OnActivate()
        {
            Owner.Body.AllSlots.ForEach(slot => { slot.Lock.Retain(); });
            Part = Owner.Parts.Get<OnslaughtBlastPart>();
        }

        public override void OnDeactivate()
        {
            Owner.Body.AllSlots.ForEach(slot => { slot.Lock.Release(); });
            if (Part == null || Part.Blast == null || Part.BlastRank == 0)
                return;

            int rodRank = 0;
            foreach (var buff in Owner.Buffs)
                if (buff.Blueprint == BuffRefs.MetamagicRodLesserKineticBuff.Reference.Get())
                    rodRank = 1;
            var blastFeature = FeatureRefs.KineticBlastFeature.Reference;
            if (Owner.GetFeature(blastFeature) == null)
                Owner.AddFact(blastFeature);
            Owner.GetFeature(blastFeature).Rank = Part.BlastRank - Part.RodRank + rodRank;
            Part.ClearAttributes();
            _targets.Clear();
            _hits.Clear();
        }
    }

    internal class AbilityOnslaughtBlast : AbilityCustomLogic
    {
        [SerializeField]
        private List<TargetWrapper> _targeted = new();

        public override void Cleanup(AbilityExecutionContext context)
        {
            var part = context.Caster.Parts.Get<OnslaughtBlastPart>();
            if (part == null)
                return;
            part.Repeating = false;
            _targeted.Clear();
        }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            var part = context.Caster.Parts.Get<OnslaughtBlastPart>();
            if (part == null)
                yield break;

            var repetitions = new List<int>() { part.CalculateNumberOfBlasts() - 1 };
            var targets = new List<TargetWrapper> { part.Target };
            _targeted.Add(part.Target);

            foreach (var buff in context.Caster.Buffs)
                if (buff.Blueprint.ToString().Equals(OnslaughtBlaster.AutoTargetBuffName))
                {
                    foreach (var enemy in context.Caster.Memory.Enemies)
                    {
                        UnitEntityData unit = enemy.Unit;
                        if (!(unit == null || unit.View == null || unit == part.Target || unit.HPLeft <= 0 ||
                            !part.Blast.CanTarget(unit) || context.Caster.DistanceTo(unit) > part.Blast.GetApproachDistance(unit)))
                        {
                            repetitions[0]--;
                            repetitions.Add(1);
                            targets.Add(unit);
                            if (repetitions[0] == 0)
                                break;
                        }
                    }
                    break;
                }

            var totalHealth = targets.Select(t => t.Unit?.HPLeft ?? 0).Sum();
            if (targets.Count > 1 && totalHealth > 0)
            {
                targets.Sort(delegate (TargetWrapper a, TargetWrapper b)
                {
                    if (a.Unit is null) return 1;
                    else if (b.Unit is null) return -1;
                    else return a.Unit.HPLeft < b.Unit.HPLeft ? 1 : -1;
                });

                int leftRepeats = repetitions[0];
                int leftTotal = leftRepeats;
                int initialTargetIndex = targets.IndexOf(part.Target);
                repetitions[initialTargetIndex] = 0;
                repetitions[0] = 1;

                for (int i = 0; i < targets.Count; i++)
                {
                    int addRepeat = (targets[i].Unit?.HPLeft ?? 0) * leftTotal / totalHealth;
                    repetitions[i] += addRepeat;
                    leftRepeats -= addRepeat;
                }

                for (int i = 0; i < leftRepeats; i++)
                    repetitions[i % repetitions.Count] += 1;
            }

            var routines = repetitions.Zip(targets, Tuple.Create).Select(t => RepeatBlast(t.Item1, part.Blast, t.Item2)).ToArray();
            var casting = true;
            while (casting)
            {
                casting = false;
                foreach (var r in routines)
                    casting |= r.MoveNext();
                yield return null;
            }
        }

        private IEnumerator RepeatBlast(int repetition, AbilityData blast, TargetWrapper target)
        {
            if (repetition == 0)
                yield break;
            float timeSinceStart = 0f;
            float interval = 2f / (repetition + 2) + 1f / 45f;
            while (repetition > 0)
            {
                if (_targeted.Contains(target))
                {
                    while (timeSinceStart < interval)
                    {
                        timeSinceStart += Game.Instance.TimeController.GameDeltaTime;
                        yield return null;
                    }
                    timeSinceStart = 0f;
                }
                else
                {
                    _targeted.Add(target);
                    while (timeSinceStart < UnityEngine.Random.Range(0.05f, 0.2f))
                    {
                        timeSinceStart += Game.Instance.TimeController.GameDeltaTime;
                        yield return null;
                    }
                    timeSinceStart = 0f;
                }

                RuleCastSpell ruleCastSpell = new(blast, target)
                {
                    IsDuplicateSpellApplied = true,
                };
                Rulebook.Trigger(ruleCastSpell);

                repetition--;
            }
        }
    }

    internal class FocusedBlasterBurnReduction : UnitFactComponentDelegate, IKineticistCalculateAbilityCostHandler, IGlobalSubscriber, ISubscriber, IUnitSubscriber
    {
        public void HandleKineticistCalculateAbilityCost(UnitDescriptor caster, BlueprintAbility abilityBlueprint, ref KineticistAbilityBurnCost cost)
        {
            if (!caster.Equals(Owner.Descriptor) || Owner.Parts.Get<OnslaughtBlastPart>() == null ||
                OnslaughtBlaster.InappropriateBlast(abilityBlueprint))
                return;

            int level = caster.Progression.GetClassLevel(CharacterClassRefs.KineticistClass.Reference);
            if (level > 10)
                cost.GatherPower++;
            if (level > 18)
                cost.GatherPower++;
        }
    }

    internal class KineticRailgunBurnReduction : UnitFactComponentDelegate, IKineticistCalculateAbilityCostHandler, IGlobalSubscriber, ISubscriber, IUnitSubscriber
    {
        public void HandleKineticistCalculateAbilityCost(UnitDescriptor caster, BlueprintAbility abilityBlueprint, ref KineticistAbilityBurnCost cost)
        {
            if (!caster.Equals(Owner.Descriptor) || Owner.Parts.Get<OnslaughtBlastPart>() == null ||
                OnslaughtBlaster.InappropriateBlast(abilityBlueprint))
                return;

            bool isHasted = caster.Buffs.HasFact(BuffRefs.HasteBuff.Reference.Get());
            if (isHasted)
                cost.Decrease(1, KineticistBurnType.Blast);
        }
    }

    internal class ExBlastBurnIncrease : UnitFactComponentDelegate, IKineticistCalculateAbilityCostHandler, IGlobalSubscriber, ISubscriber, IUnitSubscriber
    {
        [SerializeField]
        public int Increase { get; set; }

        public void HandleKineticistCalculateAbilityCost(UnitDescriptor caster, BlueprintAbility abilityBlueprint, ref KineticistAbilityBurnCost cost)
        {
            if (!caster.Equals(Owner.Descriptor) || Owner.Parts.Get<OnslaughtBlastPart>() == null ||
                OnslaughtBlaster.InappropriateBlast(abilityBlueprint))
                return;

            cost.Increase(Increase, KineticistBurnType.Metakinesis);
        }

        public override void OnActivate()
        {
            var excessive = BlueprintTool.Get<BlueprintActivatableAbility>(OnslaughtBlaster.ExcessiveBlasterAbilityGuid);
            var extreme = BlueprintTool.Get<BlueprintActivatableAbility>(OnslaughtBlaster.ExtremeBlasterAbilityGuid);
            var omni = BlueprintTool.Get<BlueprintActivatableAbility>(OnslaughtBlaster.OmniBlasterAbilityGuid);
            foreach (var a in Owner.Descriptor.ActivatableAbilities.RawFacts)
            {
                if (!a.IsOn)
                    continue;
                if (((Increase == 1 || Increase == 3) && a.Blueprint == omni) ||
                    (Increase == 4 && (a.Blueprint == excessive || a.Blueprint == extreme)))
                {
                    a.SetIsOn(value: false, null);
                    a.Stop(forceRemovedBuff: true);
                }
            }
        }
    }
}
