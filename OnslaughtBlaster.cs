using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
namespace KineticArchetypes
{
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

        internal const string TargetedStrikeName = "OnslaughtBlaster.TargetedStrike";
        internal const string TargetedStrikeGuid = "97ABCEDF-6339-43CA-A6C8-DF45DB8AF383";
        internal const string TargetedStrikeDescription = "OnslaughtBlaster.TargetedStrike.Description";

        internal const string ExtremeBlasterName = "OnslaughtBlaster.ExtremeBlaster";
        internal const string ExtremeBlasterGuid = "1C27F829-3A7C-474D-9E8A-DA3EA38BD644";
        internal const string ExtremeBlasterDescription = "OnslaughtBlaster.ExtremeBlaster.Description";

        internal const string OmniBlasterName = "OnslaughtBlaster.OmniBlaster";
        internal const string OmniBlasterGuid = "AD496984-3A4C-499C-A94C-B1E579AAC181";
        internal const string OmniBlasterDescription = "OnslaughtBlaster.OmniBlaster.Description";


        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.OnslaughtBlaster");

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
            // holy smite, thundering drums, call lightning, word of chaos, call lightning storm, storm bolts 
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
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            var buff = BuffConfigurator.New(OnslaughtBlastBuffName, OnslaughtBlastBuffGuid)
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
                .Configure();
        }

        private static BlueprintFeature CreateBurnedBlaster()
        {
            return FeatureConfigurator.New(BurnedBlasterName, BurnedBlasterGuid)
                .SetDisplayName(BurnedBlasterName)
                .SetDescription(BurnedBlasterDescription)
                .SetIcon(AbilityRefs.ThunderingDrums.Reference.Get().Icon)
                .Configure();
        }

        private static BlueprintFeature CreateExcessiveBlaster()
        {
            return FeatureConfigurator.New(ExcessiveBlasterName, ExcessiveBlasterGuid)
                .SetDisplayName(ExcessiveBlasterName)
                .SetDescription(ExcessiveBlasterDescription)
                .SetIcon(AbilityRefs.CallLightning.Reference.Get().Icon)
                .Configure();
        }

        private static BlueprintFeature CreateTargetedStrike()
        {
            return FeatureConfigurator.New(TargetedStrikeName, TargetedStrikeGuid)
                .SetDisplayName(TargetedStrikeName)
                .SetDescription(TargetedStrikeDescription)
                .SetIcon(FeatureRefs.CripplingStrike.Reference.Get().Icon)
                .Configure();
        }

        private static BlueprintFeature CreateExtremeBlaster()
        {
            return FeatureConfigurator.New(ExtremeBlasterName, ExtremeBlasterGuid)
                .SetDisplayName(ExtremeBlasterName)
                .SetDescription(ExtremeBlasterDescription)
                .SetIcon(AbilityRefs.CallLightningStorm.Reference.Get().Icon)
                .Configure();
        }

        private static BlueprintFeature CreateOmniBlaster()
        {
            return FeatureConfigurator.New(OmniBlasterName, OmniBlasterGuid)
                .SetDisplayName(OmniBlasterName)
                .SetDescription(OmniBlasterDescription)
                .SetIcon(AbilityRefs.Stormbolts.Reference.Get().Icon)
                .Configure();
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
    }

    internal class InitiateOnslaughtBlast : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, ISubscriber, IGlobalRulebookSubscriber
    {
        public override void OnTurnOn()
        {
            Owner.Ensure<OnslaughtBlastPart>();
        }

        private bool Restricted([CanBeNull]OnslaughtBlastPart part, RuleCastSpell evt)
        {
            return evt.IsDuplicateSpellApplied || evt.Spell.Blueprint.GetComponent<AbilityKineticist>() == null ||
                evt.Spell.Blueprint.GetComponent<AbilityDeliveredByWeapon>() != null ||
                evt.Spell.Blueprint.GetComponent<AbilityEffectRunAction>()?.Actions.Actions[0] is ContextActionSpawnAreaEffect ||
                part == null || part.Repeating;
        }

        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            var part = Owner.Parts.Get<OnslaughtBlastPart>();
            if (Restricted(part, evt))
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
            if (Restricted(part, evt))
                return;

            part.Repeating = true;

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

        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {

        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Reason?.Ability?.Blueprint != Part.Blast.Blueprint || Part.Repeating)
                return;

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
            // OnslaughtBlaster.Logger.Info("Onslaught blast ability");
            var part = context.Caster.Parts.Get<OnslaughtBlastPart>();
            if (part == null) 
                yield break;
            // OnslaughtBlaster.Logger.Info("Onslaught blast repeat");

            var repetitions = new List<int>() { part.BlastRank - 1 };
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
                targets.Sort(delegate(TargetWrapper a, TargetWrapper b)
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

            var routines = repetitions.Zip(targets, Tuple.Create).Select(t => RepeatBlast(context, t.Item1, part.Blast, t.Item2)).ToArray();
            var casting = true;
            while (casting)
            {
                casting = false;
                foreach (var r in routines)
                    casting |= r.MoveNext();
                yield return null;
            }
            /*while (routines.Where(r => r.MoveNext()).Any())
                yield return null;*/
            // OnslaughtBlaster.Logger.Info("Onslaught blast complete");
        }

        private IEnumerator RepeatBlast(AbilityExecutionContext context, int repetition, AbilityData blast, TargetWrapper target)
        {
            if (repetition == 0)
                yield break;
            float timeSinceStart = 0f;
            float interval = 2f / (repetition + 2) + 1f / 45f;
            while (repetition > 0)
            {
                // OnslaughtBlaster.Logger.Info($"{blast.Blueprint} repetition count: {repetition}");
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

                // OnslaughtBlaster.Logger.Info($"Now Repeat Cast {blast} to {target}");
                RuleCastSpell ruleCastSpell = new(blast, target)
                {
                    IsDuplicateSpellApplied = true,
                };
                Rulebook.Trigger(ruleCastSpell);

                repetition--;
            }
        }
    }
}
