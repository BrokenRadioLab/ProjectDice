# MILESTONE PLAN

`PROJECT_GDD_v1.0.md` is the source of truth. This plan is the only milestone source and must not redefine the game against the GDD.

Current source-of-truth status:

- `Docs/PROJECT_GDD_v1.0.md` is not currently present and will be provided by the Director later.
- `Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present and locked.
- M3 is approved by Director review. Do not invent GDD content while implementing later milestones.

Detailed implementation tasks belong in `TASK_QUEUE.md` only after a milestone is selected for implementation.

## MVP Direction

The MVP must validate the core Dice Deck-Building Roguelite loop before expanding into branching maps, events, shops, meta progression, multiple chapters, or advanced economy.

Required MVP pillars:

- Dice throw combat loop.
- Fixed throw damage.
- Dice battle presentation.
- Dice face skill activation.
- Enemy turn.
- Victory and defeat.
- Five-stage linear progression.
- Basic reward selection.
- Dice face replacement.

## Milestone Status

1. M0_PROJECT_SETUP - DONE
2. M1_COMBAT_CORE - DONE
3. M2_DICE_CORE - DONE
4. M3_DICE_PRESENTATION - DONE
5. M4_SKILL_RESOLUTION - READY_FOR_DIRECTOR_REVIEW
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

Status: DONE

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

Status: DONE

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

Approved by Director final review on 2026-06-30.

## M3_DICE_PRESENTATION

Status: DONE

Goal:

Deliver the signature battle presentation for each player throw.

Scope:

- Dice Animation Layer appears after enemy impact.
- Dice rolls briefly as part of the battle animation sequence.
- Dice stops and reveals the selected face.
- Damage number appears only after the dice result is revealed.
- Input lockout while the presentation sequence is active.

Exit Criteria:

- Every player throw shows the dice as part of the battle animation sequence.
- The dice becomes visible only after enemy impact.
- The revealed face matches the selected Dice result.
- Damage is presented only after the face reveal.
- The sequence remains short, responsive, readable, and SNES-style.

Human Review Point:

Confirm the Hero throw, impact, dice roll, face reveal, and damage presentation establish the intended visual identity before skill resolution is layered in.

Validation Status:

- M3-001 through M3-005 are complete.
- Static validation confirms the complete throw presentation order.
- Unity batchmode import/compile validation completed successfully on 2026-06-30.
- Director review passed on 2026-06-30.
- This sequence is now the Project Dice Signature Battle Flow.

Precondition:

- Follow Director M2 final review feedback and `Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md`.
- Do not treat the dice as UI or a menu overlay.
- Do not invent GDD content while GDD source text is pending.

## M4_SKILL_RESOLUTION

Status: READY_FOR_DIRECTOR_REVIEW

Goal:

Resolve the landed Dice face into deterministic MVP skill effects.

Scope:

- Small MVP face effect set.
- Fixed, readable effects only.
- Activation after Dice presentation and face reveal.
- No random damage ranges or unrelated character upgrades.
- Preserve the cause-and-effect chain: the revealed Face causes the combat effect.
- Treat Face as the result of the stopped Dice, not as a skill button.

Exit Criteria:

- Landed faces trigger their matching effects.
- Skill resolution is deterministic.
- Effects are visible or understandable to the player.

Human Review Point:

Confirm the initial face effects are enough to validate Dice build decisions.

Director-Locked Principle:

- Face is not a simple skill button.
- Player understanding should be "Attack Face appeared, therefore attack happened."
- M4 should center the Face result, not a generic RPG skill activation.

Detailed Task Status:

- M4 detailed implementation tasks have been generated and Director-refined in `Docs/TASK_QUEUE.md`.
- M4 implementation has started.
- M4-001 Face Effect Data Model is DONE.
- M4-002 Face Resolver is DONE.
- M4-003 Attack Face is DONE.
- M4-004 Explicit Undefined Face Handling is DONE.
- M4-005 Face Effect Presentation Beat is DONE.
- M4-006 Validate M4 Face Skill Resolution is DONE.
- Unity batchmode import/compile validation completed successfully on 2026-06-30.
- M4 is ready for Director review.
- `Attack` is the only currently defined damage Face behavior.
- `Guard`, `Spark`, and `Mend` must not receive invented effects until Director or GDD source text defines them.
- M4 task architecture is `DiceFace`, FaceResolver, Gameplay Effect, Presentation, `BattleCombatState`, `BattleHudPresenter`.

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
