using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using static Kingmaker.Armies.TacticalCombat.Grid.TacticalCombatGrid;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

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
            var buff = BuffConfigurator.New(OnslaughtBlastBuffName, OnslaughtBlastBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddNewRoundTrigger(newRoundActions: ActionsBuilder.New().RemoveSelf())
                .Configure();
            
            var ability = AbilityConfigurator.New(OnslaughtBlastAbilityName, OnslaughtBlastAbilityGuid)
                .SetRange(AbilityRange.Personal)
                .SetActionType(CommandType.Free)
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
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        private static BlueprintFeature CreateFocusedBlaster()
        {
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

    internal class OnslaughtBlastPart : UnitPart
    {
        public bool Started = false;
        public bool Repeating = false;
        public int Repetitions = 0;
        public int BlastRank = 0;
        public AbilityData Blast = null;
        public TargetWrapper Target = null;

        public void ClearAttributes()
        {
            Started = false;
            Repeating = false;
            Repetitions = 0;
            BlastRank = 0;
            Blast = null;
            Target = null;
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
            if (evt.IsDuplicateSpellApplied || evt.Spell.Blueprint.GetComponent<AbilityKineticist>() == null || 
                evt.Spell.Blueprint.GetComponent<AbilityDeliveredByWeapon>() != null || part == null || part.Started)
                return;
            part.Started = true;
            part.BlastRank = Owner.GetFeature(FeatureRefs.KineticBlastFeature.Reference).Rank;
            part.Repetitions = part.BlastRank;
            part.Blast = evt.Spell;
            part.Target = evt.SpellTarget;
            Owner.GetFeature(FeatureRefs.KineticBlastFeature.Reference).Rank = 1;
        }

        public void OnEventDidTrigger(RuleCastSpell evt)
        {
            var part = Owner.Parts.Get<OnslaughtBlastPart>();
            if (evt.IsDuplicateSpellApplied || !evt.Success || 
                evt.Spell.Blueprint.GetComponent<AbilityKineticist>() == null ||
                evt.Spell.Blueprint.GetComponent<AbilityDeliveredByWeapon>() != null || part == null || part.Repeating)
                return;

            part.Repeating = true;
            UnitUseAbility unitUseAbility = new(CommandType.Free, Owner.Descriptor.Abilities.GetAbility(BlueprintTool.Get<BlueprintAbility>(OnslaughtBlaster.OnslaughtBlastAbilityGuid)).Data, new TargetWrapper(Owner));
            unitUseAbility.IgnoreCooldown();
            unitUseAbility.DisableLog = true;
            unitUseAbility.Init(Owner);
            Owner.Commands.AddToQueueOrRun(unitUseAbility, false);
        }
    }

    internal class OnslaughtBlastBuffComponent : UnitFactComponentDelegate, ISubscriber, IGlobalSubscriber, IRulebookHandler<RuleDealDamage>, IInitiatorRulebookHandler<RuleDealDamage>
    {
        private OnslaughtBlastPart _part = null;

        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Reason?.Ability?.Blueprint != _part.Blast.Blueprint || _part.Repetitions != 0)
                return;

            Owner.Buffs.RemoveFact(Fact.Blueprint);
        }

        public override void OnActivate()
        {
            _part = Owner.Parts.Get<OnslaughtBlastPart>();
            if (_part = null)
                Owner.Buffs.RemoveFact(Fact.Blueprint);
        }

        public override void OnDeactivate()
        {
            if (_part = null)
                return;
            var blastFeature = FeatureRefs.KineticBlastFeature.Reference;
            if (Owner.GetFeature(blastFeature) == null)
                Owner.AddFact(blastFeature);
            Owner.GetFeature(blastFeature).Rank = _part.BlastRank;
            _part.ClearAttributes();
            Owner.Body.AllSlots.ForEach(slot => { slot.Lock.Release(); });
        }
    }

    internal class AbilityOnslaughtBlast : AbilityCustomLogic
    {
        public override void Cleanup(AbilityExecutionContext context) { }

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            // OnslaughtBlaster.Logger.Info("Onslaught blast ability");
            context.Caster.Body.AllSlots.ForEach(slot => { slot.Lock.Retain(); });

            var part = context.Caster.Parts.Get<OnslaughtBlastPart>();
            if (part == null) 
                yield break;
            // OnslaughtBlaster.Logger.Info("Onslaught blast repeat");
            IEnumerator routine = RepeatBlast(context, part);
            while (routine.MoveNext())
            {
                yield return null;
            }
            OnslaughtBlaster.Logger.Info("Onslaught blast complete");
        }

        private IEnumerator RepeatBlast(AbilityExecutionContext context, OnslaughtBlastPart part)
        {
            float timeSinceStart = 0f;
            float interval = (1f / part.Repetitions) + 1f / 32f;
            while (part.Repetitions > 0)
            {
                // OnslaughtBlaster.Logger.Info($"{part.Blast.Blueprint} repetitions count: {part.BlastRank - part.Repetitions + 1}");
                part.Repetitions--;
                if (part.Repetitions <= 0)
                    yield break;
                while (timeSinceStart < interval)
                {
                    timeSinceStart += Game.Instance.TimeController.GameDeltaTime;
                    yield return null;
                }
                timeSinceStart = 0f;

                // OnslaughtBlaster.Logger.Info($"Now Repeat Cast {part.Blast} to {part.Target}");
                RuleCastSpell ruleCastSpell = new(part.Blast, part.Target)
                {
                    IsDuplicateSpellApplied = true,
                };
                ruleCastSpell.SetSuccess(true);
                Rulebook.Trigger(ruleCastSpell);
                /*if (evt.Result != null && ruleCastSpell.Result != null)
                {
                    ruleCastSpell.Result.Context.AttackRoll = evt.Result.Context.AttackRoll;
                }*/
            }
            yield return null;
        }
    }
}
