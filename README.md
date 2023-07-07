# KineticArchetypes
A mod for Pathfinder: Wrath of the Righteous, focused on making more kineticist archetypes and other class archetypes that use kinetic blasts.

# How to install

This mod is available in ModFinder. Alternatively, you can download the zip file in the [releases page](https://github.com/NosVladimir/KineticArchetypes/releases/) and drag it into ModFinder or UnityModManager.

# Cross-mod compatibility

Should be compatible with DarkCodex and KineticistElementsExpanded.

# Contents
### Fixes
Fixed the problem where taking burn from a kinetic blade would deactivate the blade after the first attack due to insufficient remaining burn.
- Implemented by checking the blade availability using your burn at the start of round and not considering any burn taken during the round, and thus has a side effect that if you spend some burn before doing your first attack, the attacks may not happen when there is indeed no burn left. E.g. you are at 4/5 burn at start of round and your blade would cost 1 burn - you use Shroud of Water to increase AC and accept 1 burn - you're now at 5/5 burn - blade still shows available but you cannot accept the burn to attack. In turn based this should be a minor issue, but in real time it could lock you out of doing anything. If this ever happens, manually deactivate the blade.

### General
Added an ability to allow for remembering a currently held weapon, so that when kinetic blades are formed, the remembered weapon's shape is used instead of a never-changing scimitar. This works the same way as the base game transmog system with the golem, but any visual effect coming from the remembered weapon's enchants sometimes appears sometimes not. I have no idea how to fix that.

### [Kinetic Duelist](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/kinetic-duelist/)

A kineticist archetype that allows legit two-weapon fighting with kinetic blades. Heavily homebrewed since gaining dual blades at 17th level is not very appealing.
- Cannot choose form infusions that are >1st level and do not list kinetic blade as prerequisite
- No infusions at 3rd and 9th levels, no supercharge, no metakinesis quicken
- 0 burn kinetic blade at 1st level, can AoO
- Off-hand kinetic blade at 3rd level, more attacks with it at 9th and 15th level
- Recovers 1 burn in a burn-accepted round on first hit in a full-attack with kinetic blades from 11th level
- Kinetic assault special charge. 3 burn, attacks with both blades and auto maximise

### [Cinder Adept](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/paizo-llc-kineticist-archetypes/cinder-adept-kineticist-archetype/)

Fire-focused kineticist with a horse.
- Must choose fire as primary and secondary element. Can still choose a different third element (in base game there is no benefit of choosing the same element except for the composite blast, so the third element is not restricted)
- Trades 1st level infusion for Mounted Combat bonus feat 
- Trades 4th, 8th, 12th wild talents for a horse animal companion at level -3

### Esoteric Blade
A fighter archetype that has access to an energy kinetic blade, can make AoOs with it and use it for Cleaving Finishes and Vital Strikes.
- Trades 1st level combat feat for a full-progression simple energy kinetic blade, and Str modifier for KB damage
- Can multiclass with proper kineticist, adds up kinetic blast damage progression but reverts KB damage modifier to conform kineticist main stat (Con/Int/Wis/Cha)
- Trades 4th level combat feat for KB AoO and KB combat feats including all Cleaving Finishes and all Vital Strikes
- Trades weapon training 1/2/3/4 for weapon training (kinetic blade). Retains additional weapon type choices and advanced weapon training options. Works with anything dependent on weapon training, e.g. golves of dueling, all advanced weapon training features and Sohei's flurry
- Trades 10th and 20th level combat feats for changing KB critical range and multiplier to be the same as remembered weapon's base type stats (see above for remembering a weapon). This doesn't account for weapon crit enchants

# Known issues
**If you encounter any bug, feel free to fire an issue!**

Taking levels first in an esoteric blade then a proper kineticist would give you associated composite blasts right away. Not a big deal so not going to fix.

# Planned contents
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
