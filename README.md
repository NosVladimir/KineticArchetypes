# KineticArchetypes
A mod for Pathfinder: Wrath of the Righteous, focused on making more kineticist archetypes and other class archetypes that use kinetic blasts.

# How to install

This mod is available in ModFinder. Alternatively, you can download the zip file in the [releases page](https://github.com/NosVladimir/KineticArchetypes/releases/) and drag it into ModFinder or UnityModManager.

# Cross-mod compatibility

Should be compatible with DarkCodex and KineticistElementsExpanded.

# Contents
### Fixes
Fixed the problem where taking burn from a kinetic blade would deactivate the blade after the first attack due to insufficient remaining burn. Kinetic blades now only deactivate if there is indeed no burn left for an attack, either at the start of a round, or during a round immediately after an attack command is issued.

### General
Added an ability to allow for remembering a currently held weapon, so that when kinetic blades are formed, the remembered weapon's shape is used instead of a never-changing scimitar. This works the same way as the base game transmog system with the golem, but any visual effect coming from the remembered weapon's enchants sometimes appears sometimes not. I have no idea how to fix that.

Added infusion [Vital Blade](https://libraryofmetzofitz.fandom.com/wiki/Kinetic_Blade#VITAL_BLADE) that allows kinetic blades to work with vital strike and improved vital strike.

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
A homebrew fighter archetype that has access to an energy kinetic blade, can make AoOs with it and use it for Cleaving Finishes and Vital Strikes.
- Trades 1st level combat feat for a full-progression simple energy kinetic blade, and Str modifier for KB damage
- Can multiclass with proper kineticist, adds up kinetic blast damage progression but reverts KB damage modifier to conform kineticist main stat (Con/Int/Wis/Cha)
- Trades 4th level combat feat for KB AoO and KB combat feats including all Cleaving Finishes and all Vital Strikes
- Trades weapon training 1/2/3/4 for weapon training (kinetic blade). Retains additional weapon type choices and advanced weapon training options. Works with anything dependent on weapon training, e.g. golves of dueling, all advanced weapon training features and Sohei's flurry
- Trades 10th and 20th level combat feats for changing KB critical range and multiplier to be the same as remembered weapon's base type stats (see above for remembering a weapon). This doesn't account for weapon crit enchants

### [Kinetic Lancer](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/kinetic-lancer/)
Using kinetic spear, leaping around the battlefield to full-attack foes.
- Trades 1st level infusion for a 0 burn kinetic blade and the ability to [jump](https://www.d20pfsrd.com/skills/Acrobatics/#jumping-and-falling)
- Signature ability: Dragoon Dive, leaping to a target and making a kinetic blade attack, with gather power during jumping. Trades some wilds talents and infusions to enhance Dragoon Dive, including lower jump DC, jumping full-attack, higher gather power burn reduction, extra attack, and bonus damage  
- Trades 5th level infusion for Kinetic Spear, a longspear-shaped kinetic blade with reach and crit range 19-20
- Trades 9th level infusion for impaling the target of Dragoon Dive, denying their standard actions

### [Onslaught Blaster](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/onslaught-blaster/)
Convert every single dice increase of kinetic blasts into an additional blast fired. Also works with AOE form infusions. **WARNING:** be careful when firing a lot of AOE form infusions, as it will likely cause frame rate drops for a short moment due to too much FX.
- For every 1d6 damage increase of a normal blast (or every 2d6 for composites), convert it into one more blast fired at 1d6 (or 2d6), except for kinetic blades and wall/cloud/deadly ground
- Has an automatic targeting mode for distributing the number of blasts among targets, based on their HP
- Trades all metakinesis for increasing the number of blasts
- Archetype exclusive feat [Kinetic Railgun](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/onslaught-blaster#Kinetic_Railgun) that further adds 1 blast and reduces burn cost when hasted
- In base game, can fire at most 34 blasts in a single cast with greater kinetic diadem and lesser kineticist rod equipped

# Known issues
**If you encounter any bug, feel free to fire an issue!** English and Chinese are fine.

Taking levels first in an esoteric blade then a proper kineticist would give you associated composite blasts right away. Not a big deal so not going to fix.

Using dragoon dive to initiate a battle would sometimes cause you to lose your attacks - that's because the enemy was moving too fast and by the end of the fixed-point jump it's out of reach. This can be partially mitigated by increasing the speed of the character, so jumping time is reduced. It would be too complicated to factor in moving targets in real time, so this is not going to be fixed.

The reach granted by kinetic spear also applies to natural attacks.

Onslaught blaster's targeted strike sometimes counts fewer blasts than actually fired.

Levelling up during the casting animation of an onslaught blast may cause you to lose one dice permenantly. **Wait for some time until you see your equipments unlock, then level up!**

# Planned contents
Currently no guaranteed future contents. Below is a list of archetypes I'm interested in and might try to implement, but given how bizzarre they are I cannot promise anything. Maybe also some more base game fixes around blade whirlwind.

### [Elemental Annihilator](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/paizo-llc-kineticist-archetypes/elemental-annihilator-kineticist-archetype/)
Features Devastating Infusion that can full-attack foes at 120 feet, but only deals a fixed 1d8+ConModifier dmg. Trades all wild talents for extra combat feats. Has a very powerful capstone at 20th level that's a composite infusion at 4 burn dealing 5*(10d6+10) bludgeoning+cold+electricity+fire+force dmg. I suppose it can also be used together with form infusions.

### [Dread Soul](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/dread-soul/)
Evil only. Forces others to take burn. Drains creatures souls to create soulstones.

### [Nihilicist](https://www.d20pfsrd.com/alternative-rule-systems/occult-adventures/occult-classes/kineticist/archetypes/legendary-games-kineticist-archetypes/nihilicist/)
Null element - empty blast and zero blast, dealing direct non-lethal damage. Can convert into lethal damage with a standard action, plus some conditions to the target.

# Acknowledgements
- [WittleWolfie](https://github.com/WittleWolfie) for [BPCore](https://wittlewolfie.github.io/WW-Blueprint-Core/articles/intro.html) and [Vek17](https://github.com/Vek17) for [TTT-Core](https://github.com/Vek17/TabletopTweaks-Core). I wouldn't have started modding myself without the amazing modding environments thanks to them
- WittleWolfie (again) for [CharacterOptionsPlus](https://github.com/WittleWolfie/CharacterOptionsPlus) and [Truinto](https://github.com/Truinto) for [DarkCodex](https://github.com/Truinto/DarkCodex). Their code provided the best examples for me to learn and gave me tons of inspirations of how things could be done
- Special thanks to: Freyja, who was the first user of this mod and provided lots of valuable feedback; [pheonix99](https://github.com/pheonix99) for providing some starting code for the jump and helpful discussions around rules; [deng-tianhan](https://github.com/deng-tianhan) for actively playing with this mod and helped testing kinetic lancer to eliminate bugs; [Spencer](https://github.com/SpencerMycek) for [KineticistElementsExpanded](https://github.com/SpencerMycek/KineticistExpandedElements) and contributing to the development of vital blade and kinetic railgun
- All people in the discord modding community!
