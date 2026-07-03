# MILESTONE PLAN

`PROJECT_GDD_v1.0.md` is the source of truth. This plan is the only milestone source and must not redefine the game against the GDD.

Current source-of-truth status:

- `Docs/PROJECT_GDD_v1.0.md` is present.
- `Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present and locked.
- `Docs/Design/PROJECT_CORE_PHILOSOPHY.md` is present and locks Dice progression and Dice combat philosophy.
- `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md` is present and locks long-term reward and progression direction before M10.
- M3 is approved by Director review. Do not invent GDD content while implementing later milestones.

Detailed implementation tasks belong in `TASK_QUEUE.md` only after a milestone is selected for implementation.

## MVP Direction

The MVP must validate the core Dice Deck-Building Roguelite loop before expanding into branching maps, events, shops, meta progression, multiple chapters, or advanced economy.

Required MVP pillars:

- Dice throw combat loop.
- Dice Tier Base Throw Damage.
- Dice battle presentation.
- Starter Face gameplay identity.
- Enemy turn.
- Victory and defeat.
- Five-stage linear progression.
- Basic reward selection.
- Dice face replacement.
- Starter Dice build before reward selection.

## Milestone Status

1. M0_PROJECT_SETUP - DONE
2. M1_COMBAT_CORE - DONE
3. M2_DICE_CORE - DONE
4. M3_DICE_PRESENTATION - DONE
5. M4_SKILL_RESOLUTION - DONE
6. M5_ENEMY_TURN_AND_BATTLE_LOOP - DONE
7. M6_LINEAR_STAGE_RUN - DONE
8. M7_RUN_FLOW_PRESENTATION - READY_FOR_DIRECTOR_REVIEW
9. M8_STARTER_FACE_GAMEPLAY - DONE
10. M9_STARTER_DICE_BUILD - DONE
11. M10_REWARD_SELECTION - BLOCKED_BY_UI_ARCHITECTURE_REFACTOR
12. M11_DICE_FACE_REPLACEMENT - PENDING
13. M12_MVP_PLAYTEST_POLISH - PENDING

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

Status: DONE

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
- M4 is DONE and approved by Director review.
- Post-M4 Dice presentation scale/readability fix is complete and did not change M4 gameplay scope.
- Post-M4 Hero feedback replacement is complete: provided Hero idle/throw frames now carry the throw feedback.
- Post-M4 Dice presentation polish is complete and keeps validation text secondary.
- `Attack` is the only currently defined damage Face behavior.
- `Guard`, `Lightning`, and `Mend` must not receive invented effects until Director or GDD source text defines them.
- M4 task architecture is `DiceFace`, FaceResolver, Gameplay Effect, Presentation, `BattleCombatState`, `BattleHudPresenter`.

## M5_ENEMY_TURN_AND_BATTLE_LOOP

Status: DONE

Goal:

Complete the first full battle turn loop: player action, enemy response, and return to player input.

Scope:

- Enemy action after player resolution.
- Fixed enemy behavior for MVP.
- Player HP update after enemy action.
- Turn transition back to idle/player input.
- Clean input blocking while enemy turn is resolving.
- No enemy AI complexity.
- No battle completion, rewards, progression, or stage flow.

Exit Criteria:

- Player Throw resolves through the existing Face and presentation flow.
- Enemy performs one fixed response after player resolution if still able to act.
- Player HP updates after enemy presentation.
- Control returns to the player after the enemy turn.
- The player can Throw again after the loop returns to idle.
- Enemy does not act after it has already been defeated.

Human Review Point:

Confirm the first full turn cycle feels natural before adding battle completion and stage systems.

Detailed Task Status:

- M5 detailed implementation tasks have been generated in `Docs/TASK_QUEUE.md`.
- M5 implementation has started.
- M5-001 Enemy Runtime Turn State is DONE and approved.
- M5-002 Enemy Attack Resolution is DONE and approved.
- M5-003 Enemy Attack Presentation is DONE.
- M5-004 Player Damage Application is DONE and approved.
- M5-005 Turn Transition is DONE and approved.
- M5-006 Collapsible Dice Deck is DONE and approved.
- M5-007 Validate M5 Battle Loop is DONE.
- M5 is DONE.
- M5 must preserve the existing player Throw, Face Resolution, Dice presentation, damage apply, and HP refresh flow before adding the enemy response.
- M5 includes battle-level turn ownership with `PlayerTurn`, `Transition`, and `EnemyTurn`.
- Enemy attack resolution currently produces a deterministic pending fixed 5 Damage intent.
- Enemy attack presentation now presents that intent with a short enemy attack beat.
- Player damage application now consumes the resolved enemy attack intent after presentation and refreshes HP.
- Turn transition now explicitly follows `PlayerTurn`, `Transition`, `EnemyTurn`, `Transition`, `PlayerTurn`.
- Dice Deck is a collapsed-by-default Battle Information UI and Current Runtime Dice Viewer for inspecting the current six-face build; it is not battle presentation, rewards, Dice replacement, inventory, progression, or stage flow.
- Dice Deck must always read current runtime Dice state and must never be hardcoded to Starter Dice.
- Dice Deck probability display is reserved for a future milestone and is not part of M5.
- Victory, defeat, rewards, stage progression, inventory, shops, permanent progression, new Face types, boss systems, and multi-enemy logic are out of scope for M5.

## M6_LINEAR_STAGE_RUN

Status: DONE

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

Detailed Task Status:

- M6 detailed implementation tasks have been generated in `Docs/TASK_QUEUE.md`.
- M6 implementation has started.
- M6-001 Battle Outcome State is DONE.
- M6-002 Linear Stage Runtime State is DONE.
- M6-003 Enemy Defeat Victory Resolution is DONE.
- M6-003A Enemy Group Victory Abstraction is DONE.
- M6-004 Player Defeat Resolution is DONE.
- M6-005 Advance To Next Stage is DONE.
- M6-006 Complete Linear Run is DONE.
- M6-007 Prepare Next Battle is DONE.
- M6-008 Validate Linear Stage Run is DONE.
- M6 is DONE as the first fixed five-stage linear run structure.
- M6 must preserve the current player/enemy/player battle loop.
- M6 must not add rewards, Dice replacement, inventory, shops, branching map, permanent progression, new Face effects, enemy AI, boss mechanics, or multi-enemy logic.

## M7_RUN_FLOW_PRESENTATION

Status: DONE

Goal:

Make the completed runtime stage progression visible to the player before introducing rewards or Dice replacement.

Scope:

- Stage Clear presentation after non-boss Victory.
- Next Stage presentation before the next battle resumes.
- Run Complete presentation after boss Victory.
- Defeat presentation if needed for readability.
- No rewards.
- No Dice replacement.
- No inventory, shops, meta progression, branching map, enemy AI, or new Face effects.

Exit Criteria:

- Player can visually understand Stage Clear, Next Stage, and battle resume flow.
- Runtime stage progression remains owned by M6 systems.
- Presentation consumes existing runtime state and does not decide gameplay.

Human Review Point:

Confirm the run flow feels readable before adding reward selection.

Detailed Task Status:

- M7 detailed implementation tasks have been generated in `Docs/TASK_QUEUE.md`.
- M7 implementation has started.
- M7-001 Run Flow Presentation Entry Point is DONE.
- M7-002 Stage Clear Presentation is DONE.
- M7-003 Next Stage Presentation is DONE.
- M7-004 Battle Resume Presentation is DONE.
- M7-005 Run Complete Presentation is DONE.
- M7-006 Defeat Presentation is DONE.
- M7-007 Validate Run Flow Presentation is DONE.
- M7 is ready for Director review as the visible run-flow layer over M6 runtime progression.
- M7 must consume existing M6 runtime state and must not decide gameplay.
- M7 must not add rewards, Dice replacement, inventory, shops, meta progression, branching map, enemy AI, boss mechanics, new Face effects, or multi-enemy gameplay.

## M8_STARTER_FACE_GAMEPLAY

Status: DONE

Goal:

Give every starter Face a simple, meaningful gameplay identity before Reward Selection and Dice Face Replacement begin.

Scope:

- Establish the new Throw formula: Dice Tier Base Throw Damage plus Face Effect.
- Base Throw Damage belongs to the Dice, never the Hunter.
- Face effects modify the Throw and do not replace the Throw.
- Attack remains the primary damage Face.
- Guard becomes a simple defensive Face.
- Lightning becomes a simple secondary damage or utility Face.
- Mend becomes a simple recovery Face.
- Keep effects deterministic, readable, and minimal.
- Do not add reward selection, Dice face replacement, inventory, shops, meta progression, branching map, enemy AI, boss mechanics, or complex status systems.

Exit Criteria:

- Every starter Face has meaningful gameplay value.
- Every Throw deals Dice Tier Base Throw Damage.
- Face effects are resolved as modifiers on top of the base Throw.
- The player can begin making build decisions using only the starter Dice.

Human Review Point:

Confirm starter Faces create meaningful decisions before adding reward selection.

Detailed Task Status:

- M8 detailed implementation tasks have been generated in `Docs/TASK_QUEUE.md`.
- M8 implementation is complete and ready for Director review.
- M8-001 Base Throw Damage Framework is DONE.
- M8-002 Guard Gameplay is DONE.
- M8-003 Lightning Gameplay is DONE.
- M8-004 Mend Gameplay is DONE.
- M8-005 Face Presentation Polish is DONE.
- M8-006 Validate Starter Face Gameplay is DONE.
- M8 is approved and DONE.
- M8 must follow `Docs/Design/PROJECT_CORE_PHILOSOPHY.md`.

## M9_STARTER_DICE_BUILD

Status: DONE

Goal:

Allow the player to build the starting Dice before a run begins.

Scope:

- Display the unlocked Starter Face Pool.
- Starter Build displays all permanently unlocked Faces whose Face Tier is less than or equal to the current Dice Tier.
- Wood Dice remains a physical D6.
- Wood Dice has 4 Active Face Slots and 2 Locked Slots.
- Locked Slots are inactive and never rolled.
- Locked Slots are not Blank, Retry, or failed results.
- Player chooses 4 active Faces from the available starter pool.
- Duplicate Faces are allowed without duplicate source entries.
- The only build restrictions are Face Tier and Active Face Slot count.
- Build the runtime starting Dice from the selected active Faces.
- Preserve D6 battle presentation.
- Keep Dice Deck compatible with active and locked slots.

Exit Criteria:

- Player can choose the 4 active Faces for the starting Wood Dice.
- Only active Faces enter the roll pool.
- Dice Deck communicates the active build and locked slots clearly.
- Reward Selection has not been implemented.

Human Review Point:

Confirm the player understands starting Dice composition before rewards modify that build.

Detailed Task Status:

- M9-001 Starter Dice Build UI is DONE.
- M9-002 Validate Starter Dice Build is DONE.
- M9-003 UI Foundation Polish is DONE.
- M9 is approved by Director review.

## M10_REWARD_SELECTION

Status: BLOCKED_BY_UI_ARCHITECTURE_REFACTOR

Goal:

Introduce reward choice after eligible run-flow points without implementing Dice face replacement yet.

Precondition:

- Complete `TASK_UI_ARCHITECTURE_REFACTOR` before Reward UI expands further.
- Transition temporary runtime-created UI to production-ready scene/prefab UI where applicable.

Scope:

- Basic reward selection after eligible battles.
- Three reward options.
- Reward options may include New Face, Recover HP, Run-only Max HP increase, or future Relic.
- Keep all growth temporary within the Run unless explicitly defined as permanent meta progression.
- Choose exactly one reward.
- Follow `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md`.
- Reward Selection improves an already-existing runtime Dice build.
- Reward Selection must not create the player's Dice.
- No Dice face replacement implementation in this milestone unless explicitly promoted by Director.
- No inventory, shops, meta progression, branching map, enemy AI, or boss mechanics.

Exit Criteria:

- Player can select one reward.
- Reward choice can be handed to a later Dice replacement step.

Human Review Point:

Confirm reward choices are understandable before adding face replacement.

## M11_DICE_FACE_REPLACEMENT

Status: PENDING

Goal:

Validate the core build decision: replacing one current runtime Dice face with a selected reward.

Scope:

- Choose which current runtime Dice face to replace.
- Apply the selected reward to the current runtime Dice.
- Duplicate faces remain legal.
- Dice Deck automatically reflects the updated runtime Dice.
- No inventory, shops, meta progression, branching map, enemy AI, or boss mechanics.

Exit Criteria:

- Player can choose which Dice face to replace.
- Duplicate faces remain legal.
- Updated Dice build affects later throws.

Human Review Point:

Confirm reward choices create the intended question: "What should I replace on my Dice?"

## M12_MVP_PLAYTEST_POLISH

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
