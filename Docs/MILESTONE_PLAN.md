# MILESTONE PLAN

`PROJECT_GDD_v1.0.md` is the source of truth. This plan is the only milestone source and must not redefine the game against the GDD.

Detailed implementation tasks belong in `TASK_QUEUE.md` only after a milestone is selected for implementation.

## MVP Direction

The MVP must validate the core Dice Deck-Building Roguelite loop before expanding into branching maps, events, shops, meta progression, multiple chapters, or advanced economy.

Required MVP pillars:

- Dice throw combat loop.
- Fixed throw damage.
- Dice Result Overlay.
- Dice face skill activation.
- Enemy turn.
- Victory and defeat.
- Five-stage linear progression.
- Basic reward selection.
- Dice face replacement.

## Milestone Status

1. M0_PROJECT_SETUP - DONE
2. M1_COMBAT_CORE - IN_PROGRESS
3. M2_DICE_CORE - PENDING
4. M3_DICE_RESULT_OVERLAY - PENDING
5. M4_SKILL_RESOLUTION - PENDING
6. M5_ENEMY_TURN_AND_BATTLE_END - PENDING
7. M6_LINEAR_STAGE_RUN - PENDING
8. M7_REWARD_AND_FACE_REPLACEMENT - PENDING
9. M8_MVP_PLAYTEST_POLISH - PENDING

## M0_PROJECT_SETUP

Status: DONE

Goal:

Establish the Unity project baseline and planning workflow without implementing gameplay.

Scope:

- Preserve the GDD as the source of truth.
- Keep planning documents aligned with MVP-first development.
- Confirm required project folders and MVP scene folders exist.
- Avoid scripts, placeholder assets, and gameplay implementation.

Exit Criteria:

- Project structure is ready for MVP work.
- Planning docs describe milestone workflow clearly.
- GDD is unchanged.

Human Review Point:

Confirmed complete by human request before starting M1.

## M1_COMBAT_CORE

Status: IN_PROGRESS

Goal:

Create the minimum playable player action loop for combat.

Scope:

- Battle scene entry for MVP testing.
- Player-facing Throw action.
- Fixed throw damage against one enemy.
- Basic battle state needed to test repeated turns.

Exit Criteria:

- Player can perform a throw action in battle.
- Enemy HP can be reduced by fixed throw damage.
- Throw damage has no random range.

Human Review Point:

Confirm the base combat rhythm feels clear before Dice randomness and overlay presentation are added.

## M2_DICE_CORE

Status: PENDING

Goal:

Represent the Dice as the player's build and select one face result per throw.

Scope:

- Starter Dice structure.
- Six Dice face slots for MVP.
- Duplicate face support.
- Dice face result selection.
- Dice grade or MVP fixed throw damage source.

Exit Criteria:

- Dice faces can define the result pool.
- A throw produces exactly one face result.
- Duplicate faces naturally affect probability.
- Combat randomness is limited to the Dice face result.

Human Review Point:

Confirm the Dice model supports the intended probability-building fantasy before adding face effects.

## M3_DICE_RESULT_OVERLAY

Status: PENDING

Goal:

Deliver the signature Dice Result Overlay moment for each player throw.

Scope:

- Battle-visible dark overlay.
- Centered Dice presentation.
- Rolling or face-cycling anticipation.
- Clear final result reveal.
- Input lockout while overlay is active.

Exit Criteria:

- Every player throw shows the overlay.
- The revealed face matches the selected Dice result.
- The result remains visible long enough to read.

Human Review Point:

Confirm the overlay has the intended anticipation and readability before skill resolution is layered in.

## M4_SKILL_RESOLUTION

Status: PENDING

Goal:

Resolve the landed Dice face into deterministic MVP skill effects.

Scope:

- Small MVP face effect set.
- Fixed, readable effects only.
- Activation after Dice Result Overlay.
- No random damage ranges or unrelated character upgrades.

Exit Criteria:

- Landed faces trigger their matching effects.
- Skill resolution is deterministic.
- Effects are visible or understandable to the player.

Human Review Point:

Confirm the initial face effects are enough to validate Dice build decisions.

## M5_ENEMY_TURN_AND_BATTLE_END

Status: PENDING

Goal:

Complete the battle loop with enemy turns, victory, and defeat.

Scope:

- Enemy action after player resolution.
- Fixed enemy behavior for MVP.
- Victory when enemy HP reaches zero.
- Defeat when player HP reaches zero.
- Clean input blocking after battle end.

Exit Criteria:

- Battles can end in victory.
- Battles can end in defeat.
- Enemy does not act after it is defeated.
- Run-ending defeat is clearly communicated.

Human Review Point:

Confirm battle outcomes and turn pacing before connecting multiple stages.

## M6_LINEAR_STAGE_RUN

Status: PENDING

Goal:

Connect battles into the five-stage MVP run.

Scope:

- Stage 1 normal battle.
- Stage 2 normal battle.
- Stage 3 normal battle.
- Stage 4 elite battle.
- Stage 5 boss battle.
- No branching map.

Exit Criteria:

- Victory advances through the fixed stage order.
- Defeat ends the run.
- Boss victory completes the MVP run.

Human Review Point:

Confirm the five-stage run length and pacing before adding rewards.

## M7_REWARD_AND_FACE_REPLACEMENT

Status: PENDING

Goal:

Validate the core build decision: choosing rewards that alter the Dice.

Scope:

- Basic reward selection after eligible battles.
- Three reward options.
- Choose exactly one reward.
- Weapon and Skill rewards replace one Dice face.
- Minimal Dice upgrade reward support if needed for MVP validation.

Exit Criteria:

- Player can select one reward.
- Player can choose which Dice face to replace.
- Duplicate faces remain legal.
- Updated Dice build affects later throws.

Human Review Point:

Confirm reward choices create the intended question: "What should I replace on my Dice?"

## M8_MVP_PLAYTEST_POLISH

Status: PENDING

Goal:

Make the MVP readable and stable enough for repeated design playtests.

Scope:

- Basic UI readability.
- Battle and reward feedback clarity.
- Mobile landscape layout sanity pass.
- Bug fixing only within MVP scope.
- Document known limitations and post-MVP decisions.

Exit Criteria:

- A full MVP run can be completed.
- A run can also fail through defeat.
- Core Dice build loop is testable repeatedly.
- Known limitations are recorded in `CURRENT_STATE.md`.

Human Review Point:

Decide whether to iterate on MVP feel or begin post-MVP expansion planning.
