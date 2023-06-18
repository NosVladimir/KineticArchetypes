# KineticArchetypes
A mod for Pathfinder: Wrath of the Righteous, focused on making more kineticist archetypes and other class archetypes that uses kinetic blasts.

# How to install

Download the zip file and drag it into ModFinder or UnityModManager.

# Cross-mod compatibility

Should be compatible with DarkCodex and KineticistElementsExpanded.

For DarkCodex, there is a minor known issue - see below for more details. 

# Contents
### Fixes
Fixed the problem where taking burn from a kinetic blade would deactivate the blade after the first attack due to insufficient remaining burn.
- Implemented by checking the blade availability using your burn at the start of round and not considering any burn taken during the round, and thus has a side effect that if you spend some burn before doing your first attack, the attacks may not happen when there is indeed no burn left. E.g. you are at 4/5 burn at start of round and your blade would cost 1 burn - you use Shroud of Water to increase AC and accept 1 burn - you're now at 5/5 burn - blade still shows available but you cannot accept the burn to attack. In turn based this should be a minor issue, but in real time it could lock you out of doing anything. If this ever happens, manually deactivate the blade.

### [Kinetic Duelist](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/kinetic-duelist/)

A kineticist archetype that allows legit two-weapon fighting with kinetic blades. Heavily homebrewed since gaining dual blades at 17th level is not very appealing.
- Cannot choose form infusions that are >1st level and do not list kinetic blade as prerequisite
- No infusions at 3rd and 9th levels, no supercharge, no metakinesis quicken
- 0 burn kinetic blade at 1st level, can AoO
- Off-hand kinetic blade at 3rd level, more attacks with it at 9th and 15th level
- Recovers 1 burn in a burn-accepted round on first hit in a full-attack with kinetic blades from 11th level
- Kinetic assault special charge. 3 burn, attacks with both blades and auto maximise

# Known issues
Conflict with DarkCodex's Patch_KineticistAllowOpportunityAttack (referered to as the patch in below):
- If the patch is enabled, anything else other than a kinetic blade will allow AoOs. E.g. if you're dual-wielding a kinetic blade with a dagger, the dagger can AoO. This patch also enables kinetic whip to re-enable AoOs for kinetic blades. However, its code is written to a lower level than mine, causing the patch to disable AoOs for a kinetic duelist with the 1st level feature that is meant be enabling AoOs for kinetic blades.
- If the patch is not enabled, the base game rule follows, so as long as a kinetic blade is equipped, AoOs are prohibited. E.g. if you are dual-wielding a kinetic blade with a dagger, neither can perform AoO. This also makes kinetic whip to lose its functionality of re-enabling AoO for a kinetic blade. However, for a kinetic duelist with the 1st level feature, AoOs are properly re-enabled as the feature describes.
- **TLDR:** If you are not playing kinetic duelist, enable the patch. If you are playing kinetic duelist, disable the patch. If you have both a kinetic duelist and another kineticist in your party, you have to choose who can AoO with kinetic blades.

# Planned contents
### [Cinder Adept](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/paizo-llc-kineticist-archetypes/cinder-adept-kineticist-archetype/)
Fire-only kineticist with a horse.

### Homebrew fighter archetype using kinetic blades
(Rough idea) A fighter archetype that trades some bonus feats for elemental focus and kinetic blades, trades weapon training for special weapon training for kinetic blades, and trades advanced weapon training for maybe kinetic dual blades. Maybe also trades armor training with something but I feel like that would be too much and won't make a huge distinction from a kinetic duelist.

### [Kinetic Lancer](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/kinetic-lancer/)
Using kinetic lance instead of blade, leaping around the battlefield to full-attack foes.

## Technically challenging planned contents
### [Elemental Annihilator](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/paizo-llc-kineticist-archetypes/elemental-annihilator-kineticist-archetype/)
Features Devastating Infusion that can full-attack foes at 120 feet, but only deals a fixed 1d8+ConModifier dmg. Trades all wild talents for extra combat feats. Has a very powerful capstone at 20th level that's a composite infusion at 4 burn dealing 5*(10d6+10) bludgeoning+cold+electricity+fire+force dmg. I suppose it can also be used together with form infusions.

### [Onslaught Blaster](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/onslaught-blaster/)
Convert every single dice increase of kinetic blasts into an additional blast fired. Maybe also works with AOE form infusions but it will be a disaster to any pc.

### [Dread Soul](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/dread-soul/)
Evil only. Forces others to take burn. Drains creatures souls to create soulstones.

### [Nihilicist](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/nihilicist/)
Null element - empty blast and zero blast, dealing direct non-lethal damage. Can convert into lethal damage with a standard action, plus some conditions to the target.

# Acknowledgements
- WittleWolfie for BPCore and Vek17 for TTT-Core. I wouldn't have started modding myself without the amazing modding environments thanks to them
- WittleWolfie (again) for CharacterOptionsPlus and Trunito for DarkCodex. Their code provided the best examples for me to learn and gave me tons of inspirations of how things could be done
- All people in the discord modding community!
