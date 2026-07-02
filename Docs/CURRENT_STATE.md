# CURRENT STATE

Last Updated: 2026-07-02

## Source of Truth

`Docs/PROJECT_GDD_v1.0.md` is the source of truth for Project Dice. It is not currently present and will be provided by the Director later.

`Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present, locked, and is the current visual/presentation guide for the Battle scene layout.

`Docs/Design/PROJECT_CORE_PHILOSOPHY.md` is present, locked, and defines Dice progression, active slots, Base Throw Damage, and Dice combat philosophy.

`Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md` is present, locked, and defines long-term Reward Selection and progression direction before M10.

Planning and presentation documents may organize work, but they must not redefine the game against the GDD.

M3 is approved by Director review. GDD content must not be invented while the GDD source file is pending.

## Current Status

The project is in MVP foundation work.

- M0_PROJECT_SETUP is DONE.
- M1_COMBAT_CORE implementation tasks M1-T001 through M1-T006 are DONE.
- M1_COMBAT_CORE is DONE in `MILESTONE_PLAN.md`.
- `MILESTONE_PLAN.md` remains the only milestone source.
- `M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION` is DONE.
- `M2-001_EDITOR_LAYOUT_VALIDATION` is DONE.
- `M2-002_ALIGN_BATTLE_SCENE_TO_PRESENTATION_GUIDE` is DONE.
- `M2-003_THROW_SEQUENCE_PLACEHOLDER` is DONE.
- `M2-004_CREATE_DICE_CORE_DATA_MODEL` is DONE.
- `M2-005_ADD_STARTER_DICE_RUNTIME_STATE` is DONE.
- `M2-006_SELECT_ONE_DICE_FACE_RESULT_PER_THROW` is DONE.
- `M2-007_SURFACE_LATEST_DICE_RESULT_FOR_DEBUG_FREE_VALIDATION` is DONE.
- `M2-008_CONNECT_FIXED_THROW_DAMAGE_SOURCE_TO_DICE_GRADE_MVP_VALUE` is DONE.
- `M2-009_VALIDATE_M2_DICE_CORE` is DONE.
- M2_DICE_CORE is DONE and approved by Director final review.
- M3_DICE_PRESENTATION is DONE and approved by Director review.
- M4_SKILL_RESOLUTION is DONE and approved by Director review.
- M5_ENEMY_TURN_AND_BATTLE_LOOP is DONE.
- M6_LINEAR_STAGE_RUN is DONE.
- M7_RUN_FLOW_PRESENTATION is READY_FOR_DIRECTOR_REVIEW.
- M8_STARTER_FACE_GAMEPLAY is DONE.
- M9_STARTER_DICE_BUILD is DONE.
- M10_REWARD_SELECTION is READY.
- M11_DICE_FACE_REPLACEMENT is PENDING.
- M12_MVP_PLAYTEST_POLISH is PENDING.
- `M8-001_BASE_THROW_DAMAGE_FRAMEWORK` is DONE.
- `M8-002_GUARD_GAMEPLAY` is DONE.
- `M8-003_LIGHTNING_GAMEPLAY` is DONE.
- `M8-004_MEND_GAMEPLAY` is DONE.
- `M8-005_FACE_PRESENTATION_POLISH` is DONE.
- `M8-006_VALIDATE_STARTER_FACE_GAMEPLAY` is DONE.
- `M4-001_FACE_EFFECT_DATA_MODEL` is DONE.
- `M4-002_FACE_RESOLVER` is DONE.
- `M4-003_ATTACK_FACE` is DONE.
- `M4-004_EXPLICIT_UNDEFINED_FACE_HANDLING` is DONE.
- `M4-005_FACE_EFFECT_PRESENTATION_BEAT` is DONE.
- `M4-006_VALIDATE_M4_FACE_SKILL_RESOLUTION` is DONE.
- `M5-001_ENEMY_RUNTIME_TURN_STATE` is DONE.
- `M5-002_ENEMY_ATTACK_RESOLUTION` is DONE.
- `M5-003_ENEMY_ATTACK_PRESENTATION` is DONE.
- `M5-004_PLAYER_DAMAGE_APPLICATION` is DONE.
- `M5-005_TURN_TRANSITION` is DONE.
- `M5-006_COLLAPSIBLE_DICE_DECK` is DONE.
- `M5-007_VALIDATE_M5_BATTLE_LOOP` is DONE.
- `M6-001_BATTLE_OUTCOME_STATE` is DONE.
- `M6-002_LINEAR_STAGE_RUNTIME_STATE` is DONE.
- `M6-003_ENEMY_DEFEAT_VICTORY_RESOLUTION` is DONE.
- `M6-003A_ENEMY_GROUP_VICTORY_ABSTRACTION` is DONE.
- `M6-004_PLAYER_DEFEAT_RESOLUTION` is DONE.
- `M6-005_ADVANCE_TO_NEXT_STAGE` is DONE.
- `M6-006_COMPLETE_LINEAR_RUN` is DONE.
- `M6-007_PREPARE_NEXT_BATTLE` is DONE.
- `M6-008_VALIDATE_LINEAR_STAGE_RUN` is DONE.
- `M7-001_RUN_FLOW_PRESENTATION_ENTRY_POINT` is DONE.
- `M7-002_STAGE_CLEAR_PRESENTATION` is DONE.
- `M7-003_NEXT_STAGE_PRESENTATION` is DONE.
- `M7-004_BATTLE_RESUME_PRESENTATION` is DONE.
- `M7-005_RUN_COMPLETE_PRESENTATION` is DONE.
- `M7-006_DEFEAT_PRESENTATION` is DONE.
- `M7-007_VALIDATE_RUN_FLOW_PRESENTATION` is DONE.
- `M3-001_DICE_ANIMATION_LAYER` is DONE.
- `M3-002_ROLLING_PRESENTATION` is DONE.
- `M3-003_FACE_REVEAL` is DONE.
- `M3-004_DAMAGE_PRESENTATION` is DONE.
- `M3-005_VALIDATE_M3_DICE_PRESENTATION` is DONE.
- M3 must not be treated as a Dice Overlay. The Dice is part of the battle animation sequence, not UI.
- Battle Scene now follows `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0`.
- Damage application now uses the selected Dice face's fixed throw damage value against the current enemy.
- Throw now has a minimal presentation sequence before fixed damage is applied.
- Enemy attack intent resolution, enemy attack presentation, player HP damage application, and explicit turn transition now exist.
- Collapsible Dice Deck now exists as a collapsed-by-default Battle Information UI.
- Dice Deck reads from current runtime Dice state and is not hardcoded to Starter Dice.
- Dice Deck button interaction has been fixed by ensuring runtime UI event handling exists for its Button clicks/taps.
- Dice Deck is positioned on the Bottom HUD left edge, aligned with the Throw button row.
- Dice Deck expands horizontally into six square runtime face slots.
- Dice Deck probability display is intentionally not implemented in M5.
- M5 battle loop validation is complete for static sequence order and Unity import/compile.
- Dice result selection now records exactly one selected Dice face slot per accepted Throw.
- The latest selected Dice slot and face can be seen through a temporary non-final validation display.
- Dice rolling presentation has been implemented.
- Visual dice face reveal has been implemented.
- Damage number presentation has been implemented.
- M3 Dice Presentation validation is complete for static sequence order and Unity import/compile.
- Director review passed M3 and locked the current sequence as Project Dice's Signature Battle Flow.
- M4 is complete and approved; M5 is now focused on completing the first player/enemy/player battle loop.
- Face effect data now exists as a runtime data model only.
- FaceResolver now maps selected DiceFace data to FaceEffectData without executing gameplay.
- Attack Face damage now flows through `FaceResolver` and `FaceEffectData` before applying enemy HP damage.
- Undefined or no-effect Faces now report explicit no-effect feedback without changing HP.
- Face effect results now receive a short presentation beat after Face Reveal and before Damage Number.
- M4 Face Skill Resolution validation is complete for static sequence order and Unity import/compile.
- Post-M4 presentation polish has increased the Dice result to 288x288, moved it slightly below center, and kept the selected Face name as the primary result text.
- Temporary selected-slot validation text remains available as small corner `RESULT S#: FaceName` debug text until final Dice face art exists.
- Generic Hero color feedback has been replaced by the provided Hero idle and throw frame animation from `Assets/Art`.
- Hero Hit 2-frame animation now plays as the primary feedback during enemy attack presentation.
- Red Goblin Idle 4-frame animation now loops in the existing enemy slot.
- Battle-level turn ownership now exists through `BattleTurnState` with `PlayerTurn`, `Transition`, and `EnemyTurn`.
- Battle outcome runtime state now exists through `BattleOutcomeState` with `InProgress`, `Victory`, and `Defeat`.
- `BattleOutcomeState` is independent from `BattleTurnState` and `BattleCombatState` and does not calculate damage, inspect HP, trigger presentation, advance stages, unlock rewards, restart battles, or own battle flow.
- Enemy defeat now marks `BattleOutcomeState` as `Victory`.
- Enemy defeat victory resolution now goes through `EnemyGroupState.AreAllEnemiesDefeated` so future 1-3 enemy battles can share the same group-level query.
- `EnemyGroupState` slot 2 and slot 3 must remain inactive until real HP state exists for those slots; if they are enabled early, the group intentionally does not report all enemies defeated.
- After `BattleOutcomeState` is `Victory`, enemy turn does not begin and additional Throw input is not accepted.
- Battle completion flow now consumes `BattleOutcomeState` as the source of truth after victory is set.
- Player defeat now marks `BattleOutcomeState` as `Defeat` after enemy damage application and HP refresh.
- After `BattleOutcomeState` is `Defeat`, player turn does not resume and additional Throw input is not accepted.
- Non-boss Victory now advances `LinearStageRuntimeState` to the next fixed stage.
- Boss-stage Victory does not advance to a nonexistent stage.
- M6-005 advances runtime stage position only; rewards, Dice replacement, run completion, battle presentation, victory UI, and next battle preparation remain unimplemented.
- Boss-stage Victory now marks `LinearRunState` as completed.
- `LinearRunState` owns only whether the fixed linear run is still in progress or completed.
- Run completion blocks further battle input without adding rewards, Dice replacement, meta progression, restart flow, victory presentation, new run creation, or battle reset.
- Non-boss stage advance now prepares the next battle runtime by resetting enemy HP, resetting `BattleOutcomeState` to `InProgress`, restoring `BattleTurnState` to `PlayerTurn`, and refreshing HP.
- Prepare Next Battle preserves current runtime Dice and player HP.
- Prepare Next Battle does not add rewards, Dice replacement, inventory, meta progression, battle presentation, new run creation, or healing rules.
- M6 Linear Stage Run validation is complete for static runtime flow and Unity import/compile.
- The fixed runtime path now supports Stage 1 Normal, Stage 2 Normal, Stage 3 Normal, Stage 4 Elite, and Stage 5 Boss progression.
- Non-boss Victory advances and prepares the next battle; Boss Victory marks `LinearRunState` as completed.
- Defeat stops player input and does not advance stages, complete the run, or trigger rewards.
- Player HP, current runtime Dice, and Dice Deck runtime data persist across next battle preparation.
- M6 remains free of rewards, Dice replacement, inventory, meta progression, branching map, enemy AI, boss mechanics, and multi-enemy gameplay.
- Run flow presentation now has a presentation-only entry point through `RunFlowPresenter`.
- `RunFlowPresenter` consumes battle outcome, current stage, and run completion context without mutating HP, turn ownership, stage runtime, or run state.
- Stage Clear presentation now appears as a short `Stage Cleared` beat after non-boss Victory.
- Stage Clear does not appear for Boss Victory, Defeat, or InProgress outcomes.
- Next Stage presentation now shows the current runtime stage number and stage type after M6 stage advancement.
- Next Stage presentation reads `LinearStageRuntimeState` and does not advance stages, prepare battles, generate maps, create rewards, replace Dice, or mutate runtime state.
- Battle Resume presentation now shows a short `Battle Start` beat after Next Stage and before player input resumes.
- Battle Resume presentation does not mutate HP, turn ownership, battle outcome, stage runtime, run state, Dice state, rewards, or battle preparation.
- Run Complete presentation now shows a short `Run Complete` beat after Boss Victory completes `LinearRunState`.
- Run Complete presentation does not add rewards, Dice replacement, meta progression, restart flow, new run creation, post-run economy, or runtime mutation.
- Defeat presentation now shows a short `Defeat` beat when `BattleOutcomeState` reaches Defeat.
- Defeat presentation does not add restart UI, run summary, meta progression, rewards, Dice replacement, inventory, or runtime mutation.
- M7 Run Flow Presentation validation is complete for static presentation sequence, runtime ownership, scope guardrails, and Unity import/compile.
- M7 is ready for Director review.
- M7 has not added rewards, Dice replacement, inventory, meta progression, enemy AI, boss mechanics, or new Face effects yet.
- Dice-owned Base Throw Damage now exists on `DiceModel`.
- Starter Dice currently has Base Throw Damage 3.
- Starter Attack currently modifies the Throw by 5 damage, producing 8 total starter Attack Throw damage.
- Guard, Lightning, and Mend currently deal Base Throw Damage through the Throw.
- BattleController now applies total Throw damage as Dice Base Throw Damage plus Face damage modifier.
- No Hunter permanent Attack stat was introduced for M8-001.
- Guard now has a deterministic defensive Face effect.
- Guard Throw still deals Base Throw Damage, then reduces the next incoming enemy attack damage by 3.
- Lightning now has a deterministic single-target MVP damage modifier.
- Lightning currently deals Base Throw Damage 3 plus Lightning modifier damage 3, for 6 total damage before enemy HP clamping.
- Lightning remains the future chaining / area-damage Face direction, but no chaining, area targeting, enemy selection, or multi-enemy gameplay has been implemented.
- Mend now has a deterministic recovery Face effect.
- Mend Throw still deals Base Throw Damage 3, then heals the player for up to 5 HP without exceeding Max HP.
- Mend does not add long-term healing economy, rewards, progression, inventory, or Dice replacement.
- Face Effect presentation now communicates Dice Base Throw Damage plus the resolved Face modifier.
- Damage Number remains the applied enemy damage value after Face Effect presentation.
- Presentation consumes Base Throw Damage and FaceEffectData but does not decide gameplay.
- M8 Starter Face Gameplay validation is complete.
- Every starter Face now has gameplay value: Attack offense, Guard defense, Lightning secondary offense, and Mend recovery.
- No starter Face remains No Effect.
- Reward Selection, Dice Face Replacement, inventory, meta progression, branching map, enemy AI, boss mechanics, complex status systems, new Face pools, and multi-enemy gameplay remain unimplemented.
- Post-M8 presentation readability fix is complete.
- Face Effect detail now remains visible long enough to read and uses a multi-line result format.
- Enemy damage number now appears near the enemy body, floats upward, fades out, and represents actual enemy HP damage only.
- Enemy hit feedback now includes stronger flash, a short shake, and a small hit spark.
- Presentation still consumes already calculated runtime data and does not decide gameplay values.
- Linear stage runtime state now exists through `LinearStageRuntimeState` with fixed current-stage lookup for Stage 1 Normal, Stage 2 Normal, Stage 3 Normal, Stage 4 Elite, and Stage 5 Boss.
- `LinearStageRuntimeState` owns current stage index, current stage type, boss-stage check, and fixed-order advancement; it does not know battle outcome, rewards, next battle preparation, transition presentation, or run completion.
- Accepted player Throw now moves turn ownership into `Transition`; current M5 flow then enters `EnemyTurn`, resolves a pending enemy attack intent, plays enemy attack presentation, applies player damage, refreshes HP, moves through `Transition`, and returns to `PlayerTurn`.
- Enemy attack resolution now produces a deterministic pending attack intent: fixed 5 Damage when the battle is in pending `EnemyTurn`.
- The pending enemy attack intent is presented during M5-003 and applied to player HP during M5-004.
- This post-M4 presentation scale fix did not add gameplay, enemy turns, rewards, progression, new Dice faces, new Face effects, or Dice result logic.
- Skills, upgrades, rewards, progression, and future milestone systems have not been implemented.
- GDD content has not been redesigned or invented.

## Workflow Rule

Codex should not generate detailed implementation tasks until a human selects a milestone or task for implementation.

When a milestone or task is selected:

- Read the selected milestone from `MILESTONE_PLAN.md`.
- Re-check the relevant GDD sections.
- Keep the implementation inside the selected scope.
- Write selected detailed tasks into `TASK_QUEUE.md`.
- Update `CURRENT_STATE.md` after meaningful progress.

## Milestone Source

Milestones are defined only in `Docs/MILESTONE_PLAN.md`:

1. M0_PROJECT_SETUP
2. M1_COMBAT_CORE
3. M2_DICE_CORE
4. M3_DICE_PRESENTATION
5. M4_SKILL_RESOLUTION
6. M5_ENEMY_TURN_AND_BATTLE_LOOP
7. M6_LINEAR_STAGE_RUN
8. M7_RUN_FLOW_PRESENTATION
9. M8_STARTER_FACE_GAMEPLAY
10. M9_STARTER_DICE_BUILD
11. M10_REWARD_SELECTION
12. M11_DICE_FACE_REPLACEMENT
13. M12_MVP_PLAYTEST_POLISH

## MVP Must Include

- Dice throw combat loop.
- Dice Tier Base Throw Damage.
- Dice battle presentation.
- Starter Face gameplay identity.
- Enemy turn.
- Victory and defeat.
- Five-stage linear progression.
- Basic reward selection.
- Dice face replacement.
- Starter Dice Build before Reward Selection.

## Deferred Until After MVP

- Branching node map.
- Events.
- Shops.
- Advanced economy.
- Multiple chapters.
- Large enemy variety.
- Meta progression.
- Challenge modes.
- Endless mode.

## Current Implementation Progress

Completed M1 combat tasks:

- M1-T001: Normalize Battle Scene Location.
- M1-T002: Create Minimal Battle Screen Layout.
- M1-T003: Add Minimal Combat State.
- M1-T004: Bind Combat State To HP UI.
- M1-T005: Add M1 Victory Stop.
- M1-T006: Validate M1 Combat Core.

Completed layout and presentation foundation tasks:

- M2-000: Final Battle Layout Foundation.
- M2-001: Editor Layout Validation.
- M2-002: Align Battle Scene To Presentation Guide.
- M2-003: Throw Sequence Placeholder.
- M2-004: Create Dice Core Data Model.
- M2-005: Add Starter Dice Runtime State.
- M2-006: Select One Dice Face Result Per Throw.
- M2-007: Surface Latest Dice Result For Debug-Free Validation.
- M2-008: Connect Fixed Throw Damage Source To Dice Grade MVP Value.
- M2-009: Validate M2 Dice Core.

Director-approved M3 direction:

- Throw Button.
- Hero Throw.
- White projectile trail.
- Enemy hit flash.
- Dice Animation Layer appears.
- Dice rolls.
- Dice stops.
- Face reveal.
- Face effect.
- Damage number.
- Sequence ends.

M3 focuses on the signature battle presentation, not final dice gameplay or skill resolution.

Director-locked M3 timing principle:

- Timing and rhythm are higher priority than animation quality.
- M3 should establish responsive 16-bit JRPG combat feel before adding richer visuals.
- Initial target rhythm: Throw input, short Hero Throw beat, quick projectile, brief enemy flash, Dice appearance, rolling beat, reveal beat, damage number, sequence end.

Post-M3 roadmap:

- M4: Face Skill Resolution. READY.
- M5: Enemy Turn and Battle Loop. IN_PROGRESS.
- M6: Battle Complete.
- M7: Run Flow Presentation.
- M8: Starter Face Gameplay.
- M9: Starter Dice Build.
- M10: Reward Selection.
- M11: Dice Face Replacement.
- M12: MVP Playtest Polish.
- First complete Run.

M8 locked design:

- Every Throw always deals Base Throw Damage.
- Base Throw Damage belongs to the Dice.
- Base Throw Damage never belongs to the Hunter.
- Face effects modify the Throw.
- Face effects do not replace the Throw.
- Final Throw Result is Dice Tier Base Throw Damage plus Face Effect.
- Dice Tier controls Active Face Slot count and Base Throw Damage.
- Every starter Face must have meaningful gameplay value before Reward Selection begins.
- Starter Dice Build must happen before Reward Selection.
- M9_STARTER_DICE_BUILD will allow the player to choose 4 active Faces from Attack x2, Guard x1, Mend x1, and Lightning x1.
- Wood Dice remains physical D6 with 4 Active Face Slots and 2 Locked Slots.
- Locked Slots are inactive, not rolled, and are not Blank or Retry.
- Only active Faces enter the roll pool.
- M9-001 Starter Dice Build UI is DONE.
- M9-002 Validate Starter Dice Build is DONE.
- M9 is approved by Director review.
- Battle now begins with a Run Start / Starter Dice Build UI in the existing Battle scene.
- Starter Face Pool is Attack x2, Guard x1, Mend x1, and Lightning x1.
- Player chooses 4 active Faces before battle input unlocks.
- `DiceModel` now tracks active Face slot count.
- `DiceRoller` only rolls active Face slots.
- Starter Dice Build displays a live Dice preview and probability summary.
- Existing Battle scene consumes the generated runtime Dice through `BattleDiceState`.
- Locked Slots are preserved as inactive runtime slots and never enter the roll pool.
- Dice Deck displays the current active Faces and locked slots from runtime Dice state.

M10 locked design direction:

- Reward Selection improves the Dice the player already built.
- Reward Selection does not create the Dice.
- Reward categories may include New Face, Recover HP, Run-only Max HP increase, and future Relic.
- New Face selection and Dice Face Replacement remain separate responsibilities.
- Face rarity, Face unlock progression, Dice Tier unlock progression, and meta unlock conditions are documented in `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md`.

Director-locked M4 principle:

- Face is not a simple skill button.
- The Face is the result of the stopped Dice.
- The Face result should naturally lead into the combat effect.
- The player should understand "Attack Face appeared, therefore attack happened," not just "a skill activated."

Current Battle scene result:

- Battle scene lives at `Assets/Scenes/Battle/Battle.unity`.
- The Battle scene hierarchy uses `Canvas`, `BattleRoot`, `BattleField`, hidden compatibility `TopHUD`, hidden compatibility `Battle Log Placeholder`, and `BottomHUD`.
- `BattleField` contains `HeroSlot`, `EnemySlotsRoot`, and hidden `DiceAnimationLayer`.
- `EnemySlotsRoot` contains `EnemySlot_01`, `EnemySlot_02`, and `EnemySlot_03`.
- `EnemySlot_01` is the only currently active enemy slot.
- `EnemySlot_02` and `EnemySlot_03` exist as inactive placeholders only.
- Player HP is positioned above `HeroSlot`.
- Enemy HP is positioned above `EnemySlot_01`.
- The center of the battlefield is left empty for future dice presentation, effects, damage numbers, and skills.
- `DiceAnimationLayer` is hidden by default and reserved only as future dice presentation structure.
- Permanent visible Battle Log presentation is hidden; the existing text reference remains for compatibility with current `BattleController`.
- Bottom action area reserves future `Skill | THROW | Item` structure.
- THROW remains the active primary action.
- Skill and Item placeholders are inactive and have no behavior.
- `BattleCombatState` still stores known player HP and enemy HP values.
- `BattleCombatState` applies incoming deterministic damage values to enemy HP and clamps enemy HP at 0.
- `BattleController` still coordinates Throw input, selected Dice face damage, battle log compatibility text, and input lock after victory.
- `BattleTurnState` owns battle-level turn ownership and starts in `PlayerTurn`.
- `BattleTurnState` exposes explicit `BeginTransition()`, `BeginEnemyTurn()`, and `BeginPlayerTurn()` ownership handoffs.
- `CollapsibleDiceDeckPresenter` owns Dice Deck information UI only.
- Dice Deck reads from `BattleDiceState.CurrentDice` and displays the current runtime six-face build.
- `BattleController` accepts Throw only while turn ownership allows player action.
- `EnemyAttackResolver` converts pending `EnemyTurn` ownership into a deterministic `EnemyAttackIntent`.
- `EnemyAttackIntent` currently supports only `None` and fixed `Damage`.
- `EnemyAttackPresenter` presents resolved enemy attack intent only and does not mutate HP.
- `BattleCombatState.ApplyDamageToPlayer` is the player HP mutation path for enemy attack damage.
- `ThrowSequencePresenter` presents the Hero throw animation, white projectile trail, and Enemy hit flash before fixed damage is applied.
- `ThrowSequencePresenter` now shows the existing `DiceAnimationLayer` briefly after enemy hit flash and hides it again before damage is applied.
- `ThrowSequencePresenter` now keeps `DiceAnimationLayer` visible through a 0.45 second rolling placeholder before hiding it.
- `BattleHudPresenter` still binds `BattleCombatState` HP values to Battle scene HP text.
- `DiceFace` defines minimal runtime face data for result selection and fixed throw damage value reference.
- `DiceModel` represents exactly six Dice face slots, supports duplicate faces as separate slots, and stores lightweight runtime phase/result metadata for result selection and future presentation expansion.
- `StarterDiceFactory` creates a deterministic six-slot starter Dice with duplicate starter faces.
- `BattleDiceState` stores the current runtime Dice separately from `BattleCombatState`.
- `DiceRoller` selects one result slot from the current six-slot Dice pool.
- Each accepted Throw moves Dice runtime phase through `Ready` or previous `Revealed` state into `Rolling`, then `Stopped`, then `Revealed`.
- The selected Dice face is stored in `BattleDiceState` without changing the current fixed-damage combat outcome.
- `BattleDiceResultPresenter` creates small corner runtime validation text under `BattleField`.
- `BattleTurnState` does not roll Dice, resolve Faces, apply player HP damage, or trigger presentation.
- `DiceAnimationLayer` is now connected to the Throw sequence as the M3-001 presentation layer entry point.
- `DiceAnimationLayer` now contains a runtime-created `Rolling Dice Placeholder` during the M3-002 rolling beat.
- `DiceAnimationLayer` now reveals the already selected Dice face name after rolling.
- `BattleController` now selects the Dice result before `ThrowSequencePresenter.Play` so presentation can consume the existing result.
- `ThrowSequencePresenter` now shows a runtime damage number after face reveal.
- `BattleController` still applies damage and refreshes HP only after the presentation sequence returns.
- Attack damage is now derived from resolved `FaceEffectData` produced by `FaceResolver`.
- `ThrowSequencePresenter` now uses a 288x288 Dice placeholder with a simple runtime backing frame for the Dice result moment.
- `ThrowSequencePresenter` now loops Hero idle frames, plays six Hero throw frames on accepted Throw, spawns the projectile during the forward throw frames, and returns the Hero to idle.
- Temporary selected-slot validation text remains secondary; the primary Dice result is read through Face Reveal and Face Effect presentation.
- The previous scene-level `BattleCombatState.fixedThrowDamage` source was removed.

## Validation Notes

- Codex confirmed the scene file includes `HeroSlot`, `EnemySlotsRoot`, `EnemySlot_01`, `EnemySlot_02`, `EnemySlot_03`, and hidden `DiceAnimationLayer`.
- Codex confirmed previous `DiceOverlayPresenter` script and scene references were removed.
- Codex confirmed rolling placeholder text/object references were removed.
- Codex confirmed `BattleController` calls `BattleCombatState.ApplyDamageToEnemy` only for resolved `FaceEffectType.Damage`.
- Codex confirmed HP refresh still flows through `BattleHudPresenter.Refresh`.
- Codex confirmed Throw sequence presentation is isolated in `ThrowSequencePresenter`.
- Codex confirmed `DiceModel.FaceSlotCount` is exactly 6.
- Codex confirmed `DiceModel.SetFaces` rejects non-six-face arrays.
- Codex confirmed duplicate faces are legal because each slot stores its own `DiceFace` data.
- Codex confirmed `BattleDiceState` is present in `Battle.unity` with a serialized six-face starter Dice.
- Codex confirmed starter Dice begins in `Ready` phase with `lastResultSlotIndex` set to `-1`.
- Codex confirmed `BattleController` references `BattleDiceState` and calls Dice result selection once per accepted Throw.
- Codex confirmed `DiceRoller.SelectResultSlot` selects from exactly six Dice face slots.
- Codex confirmed `BattleController` updates `BattleDiceResultPresenter` after result presentation for debug validation only.
- Codex confirmed `ThrowSequencePresenter` references the existing scene `DiceAnimationLayer`.
- Codex confirmed `DiceAnimationLayer` is still inactive by default in `Battle.unity`.
- Codex confirmed `DiceAnimationLayer` is shown after enemy hit flash and hidden before selected face damage is applied.
- Codex confirmed the rolling placeholder is presentation-only and does not change Dice result selection.
- Codex confirmed the complete M3 sequence order: Hero feedback, projectile, enemy flash, Dice layer, rolling, face reveal, damage number, damage apply, HP refresh.
- Codex confirmed `ThrowSequencePresenter` displays the already selected `DiceFace` and does not call `DiceRoller`.
- Codex confirmed `BattleController` applies damage and refreshes HP after `ThrowSequencePresenter.Play` returns.
- Unity batchmode import/compile validation completed successfully with exit code 0 using `/tmp/projectdice_m3_005_unity_display_2.log`.
- Director review passed M3 and approved the current battle sequence as the signature flow.
- Codex confirmed no visual face reveal, damage number presentation, face skill activation, enemy turn, reward, progression, or Dice result logic redesign was added for M3-002.
- Codex confirmed `DiceRoller.SelectResultSlot` is still called only once per accepted Throw through `BattleController.SelectDiceResult`.
- Codex confirmed `ThrowSequencePresenter` consumes the selected `DiceFace` and does not call `DiceRoller`.
- Codex confirmed no additional random result is generated for M3-003 presentation.
- Codex confirmed no damage number presentation, face skill activation, enemy turn, reward, progression, or Dice result logic redesign was added for M3-003.
- Codex confirmed damage number presentation occurs after face reveal and before HP refresh.
- Codex confirmed no face skill activation, enemy turn, reward, progression, new Dice result logic, or multi-enemy logic was added for M3-004.
- Codex confirmed Attack damage now uses resolved `FaceEffectData.DamageAmount` after the Throw presentation sequence.
- Codex confirmed `BattleController` no longer directly reads `DiceFace.FixedThrowDamageValue` for damage application.
- Codex confirmed starter Dice has six serialized face slots: Attack, Attack, Guard, Guard, Lightning, and Mend.
- Codex confirmed duplicate face slots are represented as duplicate entries in the selection pool.
- Codex confirmed Attack faces have `fixedThrowDamageValue` 5.
- Codex confirmed Lightning has `fixedThrowDamageValue` 3.
- Codex confirmed Guard and Mend currently have `fixedThrowDamageValue` 0 because their remaining skill effects are not implemented yet.
- Codex confirmed selected-slot validation text displays as small corner `RESULT S#: FaceName` through `BattleDiceResultPresenter`.
- Codex confirmed `BattleCombatState` only applies received deterministic damage and does not own Dice logic.
- Static validation found no `DiceOverlayPresenter`, `diceOverlayPresenter`, `rollingOverlayDuration`, or `Rolling State Text` references.
- Static validation confirms `Rolling Dice Placeholder` now exists only as an M3-002 runtime-created child under `DiceAnimationLayer`.
- Unity batchmode validation for M2-009 was attempted but blocked because the project was already open in another Unity Editor instance.
- Human Play Mode review remains the final check for live Throw input, visual feel, and clickable behavior inside the active Editor.

## Architecture Boundaries

- `BattleCombatState` stores HP and enemy defeated state, with only simple state mutation allowed.
- `BattleController` coordinates Throw input, Throw presentation sequence timing, selected Dice face damage calls, HP refresh requests, battle log compatibility text, and input lock.
- `ThrowSequencePresenter` only presents minimal throw feedback and does not calculate damage.
- `BattleHudPresenter` only presents state in UI.
- `DiceFace` and `DiceModel` are runtime-only data classes and do not resolve skills, rewards, progression, or UI.
- `StarterDiceFactory` creates starter Dice data only and does not select results.
- `DiceRoller` selects a Dice slot only and does not resolve damage, skills, rewards, or presentation.
- `BattleDiceState` owns the current Dice runtime state and latest selected result, and does not own HP/combat state.
- `BattleDiceResultPresenter` remains validation-only and does not affect Dice selection, gameplay, or presentation timing.
- `BattleTurnState` owns `PlayerTurn`, `Transition`, and `EnemyTurn` only; it does not resolve attacks, apply damage, or trigger presentation.
- `EnemyAttackResolver` owns enemy attack intent resolution only and does not mutate HP or trigger presentation.
- `EnemyAttackPresenter` owns enemy attack presentation only and does not decide damage or mutate HP.
- Future damage formula and skill calculations should be considered for a separate `BattleDamageResolver` when a later milestone explicitly requires them.

## Next Human Decision

Director review M5_ENEMY_TURN_AND_BATTLE_LOOP. Automated/static validation confirms battle loop order, Dice Deck runtime source, and scope; human Play Mode review remains required for live feel.
