# CURRENT STATE

Last Updated: 2026-07-01

## Source of Truth

`Docs/PROJECT_GDD_v1.0.md` is the source of truth for Project Dice. It is not currently present and will be provided by the Director later.

`Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present, locked, and is the current visual/presentation guide for the Battle scene layout.

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
- M5_ENEMY_TURN_AND_BATTLE_LOOP is READY_FOR_DIRECTOR_REVIEW.
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
- Battle-level turn ownership now exists through `BattleTurnState` with `PlayerTurn`, `Transition`, and `EnemyTurn`.
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
6. M5_ENEMY_TURN_AND_BATTLE_END
7. M6_LINEAR_STAGE_RUN
8. M7_REWARD_AND_FACE_REPLACEMENT
9. M8_MVP_PLAYTEST_POLISH

## MVP Must Include

- Dice throw combat loop.
- Fixed throw damage.
- Dice battle presentation.
- Dice face skill activation.
- Enemy turn.
- Victory and defeat.
- Five-stage linear progression.
- Basic reward selection.
- Dice face replacement.

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
- M7: Reward.
- M8: Dice Face Replacement.
- First complete Run.

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
- Codex confirmed starter Dice has six serialized face slots: Attack, Attack, Guard, Guard, Spark, and Mend.
- Codex confirmed duplicate face slots are represented as duplicate entries in the selection pool.
- Codex confirmed Attack faces have `fixedThrowDamageValue` 5.
- Codex confirmed Guard, Spark, and Mend currently have `fixedThrowDamageValue` 0 because skill effects are not implemented yet.
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
