# PROJECT_LONG_TERM_PROGRESSION_DESIGN

Date: 2026-07-02

Status: DESIGN_LOCK_BEFORE_M10

Purpose:

Define the long-term progression structure that Reward Selection must respect before M10 implementation begins.

This document is design-only.

Do not treat it as implementation scope.

------------------------------------------------------------------------

# Core Principle

Project Dice is a Dice Deck-building Roguelite.

The player does not permanently become stronger.

The Dice becomes more customizable.

Permanent progression unlocks choices, not permanent raw power.

Run progression should create temporary decisions inside a run.

Meta progression should expand what future runs can choose from.

------------------------------------------------------------------------

# Official Gameplay Flow

The approved long-term gameplay flow is:

Main Menu

↓

Starter Dice Build

↓

Runtime Dice

↓

Battle

↓

Reward Selection

↓

Dice Face Replacement

↓

Next Battle

↓

Run Complete

Starter Dice Build is the first strategic decision of every run.

Reward Selection improves an already-existing Dice build.

Reward Selection must never feel like the place where the player first creates a Dice.

------------------------------------------------------------------------

# Reward Categories

Reward Selection may eventually offer more than Face rewards.

Reward categories should create a choice between immediate survival and long-term run improvement.

Approved reward category directions:

- New Face reward
- Recover HP
- Increase Max HP for the current run only
- Future Relic

Do not implement all categories at once.

M10 should start with the smallest reward set that proves the selection flow.

------------------------------------------------------------------------

# New Face Rewards

New Face rewards are the primary Dice-building reward.

They should offer a Face that can later be inserted into the current runtime Dice through Dice Face Replacement.

New Face rewards must not directly replace a Face during Reward Selection.

Reward Selection answers:

"What reward do I want?"

Dice Face Replacement answers:

"Where does this Face go in my Dice?"

Keep these responsibilities separate.

------------------------------------------------------------------------

# Survival Rewards

Survival rewards should support the run without becoming permanent player power.

Approved survival reward directions:

- Recover a fixed amount of HP.
- Increase Max HP for the current run only.

Survival rewards should compete with Dice improvement.

The player should consider:

"Do I need to survive now?"

or

"Can I improve my Dice for later?"

------------------------------------------------------------------------

# Relic Rewards

Relics are future run-only modifiers.

Relics are not required for the first M10 implementation.

Relics may eventually modify:

- Throw behavior
- Face effects
- Reward options
- Battle conditions
- Dice replacement choices

Relics should remain run-scoped unless a later Director design explicitly defines permanent relic unlocks.

------------------------------------------------------------------------

# Face Rarity

Face rarity should control reward availability and long-term unlock pacing.

Recommended rarity structure:

- Common
- Rare
- Epic

Do not add rarity-driven balance complexity before the basic reward flow works.

Starter Faces should remain simple and readable.

New reward Faces may use rarity later to communicate power, complexity, or specialization.

------------------------------------------------------------------------

# Face Unlock Progression

Permanent Face progression unlocks access to more Face choices.

It must not directly increase the Hunter's combat stats.

Approved direction:

- New Face types can be unlocked permanently.
- Unlocked Faces can enter future reward pools.
- Some unlocked Faces may enter future Starter Face pools.
- Locked Faces do not appear before they are unlocked.

Permanent Face unlocks expand the player's strategic vocabulary.

They do not make every Throw stronger by themselves.

------------------------------------------------------------------------

# Starter Face Pool Progression

Starter Face pools may expand through meta progression.

Starter Pool progression should let future runs begin with different strategic identities.

Examples:

- More offensive starter pool
- More defensive starter pool
- Recovery-focused starter pool
- Utility-focused starter pool

Starter Pool progression must preserve the M9 rule:

The player chooses active Faces before the run begins.

------------------------------------------------------------------------

# Dice Tier Unlock Progression

The physical Dice always remains D6.

Dice Tier changes:

- Active Face Slot count
- Base Throw Damage

Dice Tier does not change the physical Dice shape.

Locked slots are inactive.

Locked slots are not Blank.

Locked slots are not Retry.

Locked slots never enter the roll pool.

Example tier direction:

Wood Dice:

- 4 Active Face Slots
- 2 Locked Slots
- Base Throw Damage 3

Iron Dice:

- 5 Active Face Slots
- 1 Locked Slot
- Higher Base Throw Damage

Hunter Dice:

- 6 Active Face Slots
- 0 Locked Slots
- Higher Base Throw Damage

Exact values beyond Wood Dice are not locked here.

------------------------------------------------------------------------

# Meta Unlock Conditions

Meta unlock conditions should reward play variety and milestone completion.

Recommended unlock condition directions:

- Complete a run.
- Reach a specific stage.
- Defeat a boss.
- Use a Face a certain number of times.
- Win with a specific Dice build pattern.
- Discover a Face through Reward Selection.

Avoid unlock conditions based purely on permanent stat accumulation.

Meta progression should open more possible builds, not simply increase numeric power.

------------------------------------------------------------------------

# M10 Reward Selection Boundary

M10 should implement Reward Selection only after this design direction is understood.

M10 must not implement:

- Dice Face Replacement
- Inventory
- Shops
- Meta progression UI
- Permanent unlock economy
- Dice Tier progression
- New run creation

M10 should create the reward choice moment.

M11 should decide how a selected Face changes the Dice.

------------------------------------------------------------------------

# Locked Sentences

The player does not permanently become stronger.

The Dice becomes more customizable.

Starter Dice Build creates the initial Dice.

Reward Selection improves the current run.

Dice Face Replacement changes the runtime Dice.

Meta progression unlocks choices, not raw power.
