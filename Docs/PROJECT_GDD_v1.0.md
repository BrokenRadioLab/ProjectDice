

---

# PROJECT GDD v1.0

**Project Codename:** Project Dice

**Document:** PROJECT_GDD_v1.0 (PART 01)

**Version:** 1.0

**Language:** English

**Engine:** Unity 2D

**Platform:** Android / iOS (Landscape)

---

# 1. Game Overview

## Genre

- Dice Deck-Building
    
- Turn-Based Roguelite
    
- RPG
    

## Camera

- 2D Side View Battle
    
- Landscape Mobile
    

## Target Session

- 10~20 minutes per run
    
- 30~60 seconds per battle
    

## Target Audience

Players who enjoy:

- Deck Building
    
- Roguelite progression
    
- Turn-based RPG
    
- Build experimentation
    
- Random strategy
    

---

# 2. Vision

Create a game where the player does not build a card deck.

Instead,

the player builds a magical Dice.

Every Dice Face represents one card.

Every throw is one draw.

Every run becomes a different probability build.

The game should continuously make the player think:

> "What should I replace on my Dice?"

instead of

> "Which card should I add?"

The Dice is the center of the entire game.

---

# 3. Core Identity

This is NOT a traditional RPG.

This is NOT a Dice simulator.

This is NOT a card game.

This game is:

> **A Dice Deck-Building Roguelite RPG.**

The player's primary build is constructed by customizing and evolving a magical Dice instead of collecting cards.

---

# 4. Design Philosophy

## Rule #1

The Dice is always the Hero's primary weapon.

No swords.

No bows.

No staffs.

Everything revolves around the Dice.

---

## Rule #2

Randomness exists ONLY inside the Dice.

The player should never lose because of:

- Random damage
    
- Miss chance
    
- Random accuracy
    

The only randomness should be:

> Which Dice Face lands on top.

---

## Rule #3

Every reward should improve the player's Dice Build.

Rewards should never feel unrelated to the Dice.

---

## Rule #4

The player is not building a character.

The player is building a Dice.

---

## Rule #5

The Dice Result Overlay is the signature experience of the game.

Every battle should naturally build anticipation before the Dice stops rolling.

---

# 5. Player Fantasy

The player fantasy is NOT:

"I am a warrior."

The player fantasy is:

"I am an explorer who possesses one fragment of a legendary divine toy."

As the adventure continues,

the Dice slowly awakens.

New powers emerge.

New faces unlock.

The Dice gradually returns to its original mythical form.

---

# 6. World Setting

Long ago,

there existed a young god.

Unlike the elder gods,

this child-like deity viewed creation as nothing more than a playground.

The Child God possessed only one favorite toy.

A mysterious Dice.

This Dice was no ordinary object.

Whenever it rolled,

reality itself changed.

Fire appeared.

Lightning struck.

Life returned.

Time slowed.

Fortune shifted.

To the Child God,

it was merely a game.

To humanity,

it became catastrophe.

The Elder Gods eventually sealed away the Divine Dice.

However,

its power could not be completely erased.

The Dice was shattered into countless fragments and scattered across the world.

Generations passed.

The truth disappeared.

Only legends remained.

People no longer know the Child God's name.

History remembers only:

> **The Child God**

Likewise,

its favorite toy has no official name.

Ancient records simply call it:

> **The Toy**

---

# 7. Story

Rumors spread across the continent.

Somewhere deep within forgotten ruins lies a fragment of The Toy.

Many hunters have searched.

Few have returned.

The player is an unnamed relic hunter.

Not a chosen hero.

Not royalty.

Not a legendary warrior.

Only someone driven by curiosity.

Their objective is simple.

Find the fragments.

Discover the truth behind The Toy.

Eventually,

recover the complete Divine Dice.

---

# 8. Core Gameplay Loop

```text
Enter Dungeon

↓

Battle

↓

Throw Dice

↓

Base Throw Damage

↓

Dice Result Overlay

↓

Face Skill Activation

↓

Enemy Turn

↓

Victory

↓

Choose Reward

↓

Improve Dice Build

↓

Next Stage

↓

Boss

↓

Next Chapter
```

This gameplay loop should remain unchanged throughout the entire project.

Future content expands this loop,

but never replaces it.

---

# 9. Why Dice Instead of Cards?

Traditional Deck Builders ask:

> "Which card should I draw?"

Project Dice asks:

> "What probability do I want to create?"

Instead of collecting cards,

players modify the six faces of a magical Dice.

Duplicated skills are allowed.

This enables players to create specialized builds such as:

- Defensive Build
    
- Poison Build
    
- Fire Build
    
- Lightning Build
    
- Critical Build
    

The strategy comes from shaping probability,

not drawing cards.

This is the primary gameplay identity of Project Dice.

---

# PROJECT_GDD_v1.0_PART_02.md

---

# 10. Dice System

## Design Goal

The Dice is the center of every gameplay system.

It simultaneously functions as:

- Primary Weapon
    
- Build System
    
- Progression System
    
- Randomness System
    
- Reward Target
    

Every major gameplay mechanic should improve, modify or evolve the Dice.

---

# 11. Dice Structure

Every Dice contains six independent faces.

Each face may contain either:

- Weapon Face
    
- Skill Face
    

Players may freely customize every unlocked face.

Example

```text
① Attack

② Fire

③ Shield

④ Heal

⑤ Poison

⑥ Critical
```

Duplicate faces are fully supported.

Example

```text
① Fire

② Fire

③ Fire

④ Shield

⑤ Heal

⑥ Fire
```

There is no uniqueness restriction unless a specific skill explicitly defines one.

---

# 12. Dice Build Philosophy

Unlike traditional Deck Builders,

players are not collecting cards.

Instead,

they are constructing probability.

Example

Balanced Build

```text
Attack
Shield
Fire
Heal
Poison
Lightning
```

Aggressive Build

```text
Attack
Attack
Fire
Fire
Critical
Critical
```

Defensive Build

```text
Shield
Shield
Shield
Heal
Attack
Heal
```

Poison Build

```text
Poison
Poison
Poison
Shield
Heal
Attack
```

The player is encouraged to specialize.

---

# 13. Dice Grades

There are five Dice grades.

|Grade|Korean|
|---|---|
|Common|일반|
|Uncommon|고급|
|Rare|희귀|
|Heroic|영웅|
|Legendary|전설|

Higher grades represent larger fragments of the Divine Toy.

Progression is not merely stronger equipment.

It represents recovering more of the original mythical power.

---

# 14. Dice Grade Progression

Higher Dice grades provide:

- Higher Throw Damage
    
- More usable Dice Faces
    
- Higher equipment grade limit
    
- Additional Throw Effects (Rare+)
    

Example progression

|Grade|Faces|Throw Damage|
|---|--:|--:|
|Common|4|Low|
|Uncommon|5|Medium|
|Rare|6|High|
|Heroic|6|Very High|
|Legendary|6|Maximum|

Exact numerical values are balancing data and are intentionally excluded from the GDD.

---

# 15. Dice Faces

Every unlocked face contains exactly one item.

A face may contain:

- Attack
    
- Defensive Skill
    
- Healing Skill
    
- Magic Skill
    
- Passive Skill
    
- Special Skill
    

Only one item may occupy a face.

Replacing a face permanently removes the previous content.

---

# 16. Equipment Grade Rules

A Dice may only equip items of the same or lower grade.

Example

Rare Dice

Can equip

- Common
    
- Uncommon
    
- Rare
    

Cannot equip

- Heroic
    
- Legendary
    

This rule guarantees meaningful Dice progression.

Obtaining a stronger Dice expands future build possibilities.

---

# 17. Throw Damage

Every player attack begins with throwing the Dice.

Throw Damage is:

- Guaranteed
    
- Fixed
    
- Never random
    

Throw Damage increases with Dice grade.

Random damage ranges are intentionally excluded.

Reason:

Randomness already exists through Dice Face outcomes.

Adding additional damage variance weakens strategic planning.

---

# 18. Dice Throw Effects

Rare Dice and above gain unique Throw Effects.

These effects occur immediately after the Dice hits the target,

before the Dice Result Overlay begins.

Examples (placeholder)

- Piercing
    
- Explosion
    
- Chain Impact
    
- Multiple Bounce
    

Specific effects will be finalized during balancing.

---

# 19. Dice Result Overlay

The Dice Result Overlay is the signature presentation of the game.

Battle pauses.

The background remains visible beneath a dark transparent layer.

The Dice appears at screen center.

Sequence

1. Dice enlarges
    
2. Dice spins rapidly
    
3. Faces become visible while rotating
    
4. Dice stops
    
5. Top face is revealed
    
6. Overlay closes
    
7. Selected skill activates
    

This moment should create anticipation every turn.

---

# 20. Dice Upgrade Philosophy

Players should always feel they are improving their Dice,

not replacing disposable equipment.

Every upgrade should produce one of the following feelings:

- More damage
    
- Better probability
    
- More strategic choices
    
- Higher build freedom
    
- Stronger Divine Fragment
    

The emotional reward is:

> "My Dice has evolved."

not

> "I found another weapon."

---

# PROJECT_GDD_v1.0_PART_03.md

---

# 21. Battle System

## Battle Structure

Combat is fully turn-based.

One battle consists of repeated player and enemy turns until one side reaches zero HP.

```text
Player Turn

↓

Throw Dice

↓

Throw Damage

↓

Dice Result Overlay

↓

Face Skill Activation

↓

Enemy Turn

↓

Repeat
```

---

# 22. Victory & Defeat

## Victory

A battle ends immediately when the enemy HP reaches zero.

The player receives rewards depending on battle type.

---

## Defeat

The run immediately ends when the player's HP reaches zero.

All temporary Dice Build progress is lost.

Only permanent progression (if implemented later) is preserved.

---

# 23. Battle Types

There are three battle categories.

## Normal Battle

Purpose

- Resource acquisition
    
- Stable progression
    

Reward

- Gold
    

---

## Elite Battle

Purpose

- High Risk
    
- Build progression
    

Reward

Choose ONE of three random rewards.

Possible rewards

- New Skill
    
- New Weapon Face
    
- Higher Grade Dice
    

---

## Boss Battle

Purpose

Chapter completion.

Bosses should significantly change player builds.

Reward

Choose ONE of three premium rewards.

Higher Dice grades have increased appearance probability.

---

# 24. Reward System

The reward screen is one of the most important moments in the game.

Every reward should improve the player's Dice Build.

Reward categories

```text
Weapon

Skill

Dice
```

The player chooses exactly one.

---

# 25. Reward Rules

## Weapon

Replaces one Dice Face.

The player selects which face to overwrite.

---

## Skill

Replaces one Dice Face.

The player selects which face to overwrite.

---

## Dice

Replaces the current Dice.

The player immediately gains

- Higher Throw Damage
    
- Higher Grade Limit
    
- Additional Face Slots (when applicable)
    
- Higher future reward quality
    

---

# 26. Economy

## Primary Currency

Gold

Gold is obtained from:

- Normal Battles
    
- Events
    
- Treasure
    

Gold spending locations are intentionally left flexible for future balancing.

Current candidates include

- Shop
    
- Forge
    
- Shrine
    
- Healing
    

Final implementation will be determined after MVP validation.

---

# 27. Stage Structure

Final version adopts a branching node progression.

Inspired by node-based roguelites.

Example

```text
          Elite
         /
Start ─ ○ ─ ○ ─ Boss
         \     /
        Shop ○
            \
           Event
```

Node types

- Battle
    
- Elite
    
- Shop
    
- Event
    
- Boss
    

The map itself should remain visually simple.

Gameplay depth comes from decisions,

not map complexity.

---

# 28. MVP Stage Structure

MVP intentionally ignores branching.

Structure

```text
Stage 1

↓

Stage 2

↓

Stage 3

↓

Elite

↓

Boss
```

Purpose

Validate

- Battle Flow
    
- Dice Build
    
- Reward Loop
    

without introducing route complexity.

---

# 29. Chapter Structure

The final game consists of multiple chapters.

Example progression

Chapter 1

Forgotten Ruins

↓

Chapter 2

Underground Mine

↓

Chapter 3

Ancient Library

↓

Chapter 4

Abyss

↓

Chapter 5

The Divine Sanctuary

Chapter names are placeholders.

Each chapter introduces

- New enemies
    
- New visual themes
    
- New rewards
    
- New mechanics
    

without changing the core Dice gameplay.

---

# 30. Enemy Design Philosophy

Enemies should encourage different Dice Builds.

Examples

Heavy Armor

Encourages

Poison

Magic

Armor Break

---

Glass Cannon

Encourages

Burst Damage

---

Healer

Encourages

Continuous Pressure

---

Counter Enemy

Encourages

Defensive Builds

Enemy variety should promote experimentation,

not stat inflation.

---

# 31. Boss Philosophy

Bosses should act as build checks.

Bosses should never require a single mandatory strategy.

Instead,

different Dice Builds should solve the encounter in different ways.

A successful boss should make the player think

> "My current Dice Build worked."

rather than

> "My stats were high enough."

---

# PROJECT_GDD_v1.0_PART_04.md

---

# 32. User Interface

## Design Goal

The UI should remain clean and readable.

The player's attention should always stay on:

- The Hero
    
- The Monster
    
- The Dice
    

No unnecessary interface elements should compete for attention.

---

# 33. Battle Screen

The battle screen consists of:

```text
----------------------------------------------------

Hero                         Monster

HP Bar                       HP Bar

------------------------------

Throw Button

Current Dice

Gold

----------------------------------------------------
```

The battle interface should be playable using one hand.

All important interactions should be reachable by thumb.

---

# 34. Dice Result Overlay

This is the signature presentation of the game.

Sequence

```text
Throw Dice

↓

Hit Enemy

↓

Dark Overlay

↓

Center Dice

↓

Rolling

↓

Result

↓

Overlay Close

↓

Skill Activation
```

Requirements

- Background remains visible.
    
- Dice is always centered.
    
- Dice is the only moving object.
    
- Result must remain visible briefly before disappearing.
    

The Overlay should never feel rushed.

It should create anticipation.

---

# 35. Dice Build Screen

Players may edit the Dice outside battle.

Display

```text
      [ ① ]

 [⑥]       [②]

 [⑤]       [③]

      [④]
```

Selecting a face opens available Skills or Weapons.

Replacing a face immediately updates the Dice Build.

Duplicate faces are allowed.

---

# 36. Reward Screen

After Elite and Boss battles:

Three rewards are displayed.

```text
+----------------+

Reward A

+----------------+

Reward B

+----------------+

Reward C

+----------------+
```

Player selects exactly one.

No confirmation window.

Selection should feel immediate.

---

# 37. Art Direction

Visual Style

- 16-bit / 32-bit Pixel Art
    
- SNES-inspired
    
- Low saturation fantasy palette
    
- 32×32 character sprites
    
- 1px black outline
    
- Four-direction lighting
    

Overall atmosphere

Warm fantasy

Adventure

Ancient ruins

Forgotten relics

Never dark horror.

Never realistic.

---

# 38. Character Design

Hero

- Anonymous relic hunter
    
- Small readable silhouette
    
- Simple equipment
    
- No oversized armor
    
- Adventure-focused appearance
    

The Hero should feel like

someone searching for legends,

not saving the world.

---

# 39. Dice Design

The Dice is the visual icon of the game.

It should immediately stand out.

As grades increase,

the Dice gradually regains its divine appearance.

Examples

Common

- Plain
    
- Small
    
- Worn
    

Rare

- Rune engravings
    
- Glowing edges
    

Heroic

- Floating fragments
    
- Light effects
    

Legendary

- Divine aura
    
- Golden runes
    
- Animated glow
    

The Dice should visually communicate progression.

---

# 40. Environment

Dungeon environments should support exploration.

Examples

Chapter 1

Forgotten Ruins

Chapter 2

Underground Mine

Chapter 3

Ancient Library

Chapter 4

Abyss

Chapter 5

Divine Sanctuary

Every chapter should introduce

new colors,

new enemies,

and new atmosphere,

while preserving gameplay consistency.

---

# 41. Audio Direction

Music

Fantasy

Adventure

Mystery

Hope

Combat music should remain energetic,

without becoming stressful.

---

Sound Effects

Most important sounds

1. Dice Throw
    
2. Enemy Hit
    
3. Dice Rolling
    
4. Dice Stop
    
5. Skill Activation
    

The sound of the Dice stopping should become

the most memorable sound in the game.

---

# 42. MVP Scope

The MVP exists only to validate gameplay.

Included

- Chapter 1
    
- Five stages
    
- Linear progression
    
- Three normal enemies
    
- One Elite
    
- One Boss
    
- One Dice grade progression
    
- Reward selection
    
- Dice Build editing
    
- Dice Result Overlay
    

Excluded

- Branching map
    
- Events
    
- Multiple chapters
    
- Advanced economy
    
- Meta progression
    
- Large enemy variety
    

---

# 43. Future Expansion

Planned systems

- Branching node map
    
- Additional chapters
    
- New Dice grades
    
- More skills
    
- More enemies
    
- Events
    
- Shops
    
- Relics
    
- Permanent progression
    
- Challenge modes
    
- Endless mode
    

Future content must expand the Dice Build system,

never replace it.

---

# 44. Non-Negotiable Design Rules

These rules must never be violated.

The Dice is always the Hero's primary weapon.

The Dice is always the player's build.

Randomness comes only from Dice results.

Throw Damage is fixed.

Duplicate Dice Faces are allowed.

Higher Dice grades always feel meaningful.

The Dice Result Overlay is the signature experience.

Every reward should improve the player's Dice Build.

The player should feel they are rebuilding the Divine Toy,

not merely collecting stronger equipment.

This is not a traditional RPG.

This is a Dice Deck-Building Roguelite where the player's strategy comes from designing probability through a customizable magical Dice.

---

# End of PROJECT_GDD_v1.0

**Document Status:** Complete (v1.0 Draft)

**Ready for Codex Implementation**