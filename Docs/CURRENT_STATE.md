# CURRENT STATE

Last Updated: 2026-06-29

## Source of Truth

`Docs/PROJECT_GDD_v1.0.md` is the source of truth for Project Dice. Planning and presentation documents may organize work, but they must not redefine the game against the GDD.

`PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is the current visual/presentation guide for the Battle scene layout.

## Current Status

The project is in MVP foundation work.

- M0_PROJECT_SETUP is DONE.
- M1_COMBAT_CORE implementation tasks M1-T001 through M1-T006 are DONE.
- M1_COMBAT_CORE remains ready for human review in `MILESTONE_PLAN.md`.
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
- Battle Scene now follows `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0`.
- Damage application now uses the selected Dice face's fixed throw damage value against the current enemy.
- Throw now has a minimal presentation sequence before fixed damage is applied.
- Enemy turn behavior has not been implemented yet.
- Dice result selection now records exactly one selected Dice face slot per accepted Throw.
- The latest selected Dice slot and face can be seen through a temporary non-final validation display.
- Dice rolling, dice face reveal, and skill activation have not been implemented.
- Skills, upgrades, rewards, progression, and future milestone systems have not been implemented.
- GDD has not been intentionally modified.

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
4. M3_DICE_RESULT_OVERLAY
5. M4_SKILL_RESOLUTION
6. M5_ENEMY_TURN_AND_BATTLE_END
7. M6_LINEAR_STAGE_RUN
8. M7_REWARD_AND_FACE_REPLACEMENT
9. M8_MVP_PLAYTEST_POLISH

## MVP Must Include

- Dice throw combat loop.
- Fixed throw damage.
- Dice Result Overlay.
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
- `ThrowSequencePresenter` presents the temporary Hero feedback, white projectile trail, and Enemy hit flash before fixed damage is applied.
- `BattleHudPresenter` still binds `BattleCombatState` HP values to Battle scene HP text.
- `DiceFace` defines minimal runtime face data for result selection and fixed throw damage value reference.
- `DiceModel` represents exactly six Dice face slots, supports duplicate faces as separate slots, and stores lightweight runtime phase/result metadata for result selection and future presentation expansion.
- `StarterDiceFactory` creates a deterministic six-slot starter Dice with duplicate starter faces.
- `BattleDiceState` stores the current runtime Dice separately from `BattleCombatState`.
- `DiceRoller` selects one result slot from the current six-slot Dice pool.
- Each accepted Throw moves Dice runtime phase through `Ready` or previous `Revealed` state into `Rolling`, then `Stopped`, then `Revealed`.
- The selected Dice face is stored in `BattleDiceState` without changing the current fixed-damage combat outcome.
- `BattleDiceResultPresenter` creates a small runtime-only validation text under `BattleField`.
- The validation text shows the latest selected slot and face after each accepted Throw.
- The validation text does not use `DiceAnimationLayer` and is not the final Dice Result Overlay.
- Throw damage is now derived from `BattleDiceState.LastSelectedFace.FixedThrowDamageValue`.
- The previous scene-level `BattleCombatState.fixedThrowDamage` source was removed.

## Validation Notes

- Codex confirmed the scene file includes `HeroSlot`, `EnemySlotsRoot`, `EnemySlot_01`, `EnemySlot_02`, `EnemySlot_03`, and hidden `DiceAnimationLayer`.
- Codex confirmed previous `DiceOverlayPresenter` script and scene references were removed.
- Codex confirmed rolling placeholder text/object references were removed.
- Codex confirmed `BattleController` calls `BattleCombatState.ApplyDamageToEnemy` with the selected Dice face damage value.
- Codex confirmed HP refresh still flows through `BattleHudPresenter.Refresh`.
- Codex confirmed Throw sequence presentation is isolated in `ThrowSequencePresenter`.
- Codex confirmed `DiceModel.FaceSlotCount` is exactly 6.
- Codex confirmed `DiceModel.SetFaces` rejects non-six-face arrays.
- Codex confirmed duplicate faces are legal because each slot stores its own `DiceFace` data.
- Codex confirmed `BattleDiceState` is present in `Battle.unity` with a serialized six-face starter Dice.
- Codex confirmed starter Dice begins in `Ready` phase with `lastResultSlotIndex` set to `-1`.
- Codex confirmed `BattleController` references `BattleDiceState` and calls Dice result selection once per accepted Throw.
- Codex confirmed `DiceRoller.SelectResultSlot` selects from exactly six Dice face slots.
- Codex confirmed `BattleController` references `BattleDiceResultPresenter` and updates the validation display after result selection.
- Codex confirmed fixed damage now uses `BattleDiceState.LastSelectedFace.FixedThrowDamageValue` after the Throw presentation sequence.
- Static validation found no `DiceOverlayPresenter`, `diceOverlayPresenter`, `rollingOverlayDuration`, `Rolling Dice Placeholder`, or `Rolling State Text` references.
- Human Play Mode review remains the final check for visual feel and clickable Throw behavior inside the active Editor.

## Architecture Boundaries

- `BattleCombatState` stores HP and enemy defeated state, with only simple state mutation allowed.
- `BattleController` coordinates Throw input, Throw presentation sequence timing, selected Dice face damage calls, HP refresh requests, battle log compatibility text, and input lock.
- `ThrowSequencePresenter` only presents minimal throw feedback and does not calculate damage.
- `BattleHudPresenter` only presents state in UI.
- `DiceFace` and `DiceModel` are runtime-only data classes and do not resolve skills, rewards, progression, or UI.
- `StarterDiceFactory` creates starter Dice data only and does not select results.
- `DiceRoller` selects a Dice slot only and does not resolve damage, skills, rewards, or presentation.
- `BattleDiceState` owns the current Dice runtime state and latest selected result, and does not own HP/combat state.
- `BattleDiceResultPresenter` presents the latest selected Dice result for validation only and does not select results or affect combat.
- Future damage formula and skill calculations should be considered for a separate `BattleDamageResolver` when a later milestone explicitly requires them.

## Next Human Decision

Review the Battle scene presentation in Unity Play Mode. If accepted, select the next milestone or detailed task from `MILESTONE_PLAN.md`.
