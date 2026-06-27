# CURRENT STATE

Last Updated: 2026-06-28

## Source of Truth

`Docs/PROJECT_GDD_v1.0.md` is the source of truth for Project Dice. Planning documents may organize work, but they must not redefine the game against the GDD.

## Current Status

The project is in MVP foundation work.

- M0_PROJECT_SETUP is DONE.
- M1_COMBAT_CORE implementation tasks M1-T001 through M1-T006 are DONE.
- M1_COMBAT_CORE remains ready for human review in `MILESTONE_PLAN.md`.
- `MILESTONE_PLAN.md` remains the only milestone source.
- `M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION` is DONE.
- `M2-001_EDITOR_LAYOUT_VALIDATION` is DONE.
- The Battle scene now has a permanent gameplay layout foundation for future pixel art replacement.
- Unity Editor log confirms `Assets/Scenes/Battle/Battle.unity` was opened and loaded successfully.
- Recent Unity Editor log after Battle scene load contains no `NullReferenceException`, `MissingReferenceException`, old Input/EventSystem errors, or compile errors found by Codex.
- Throw button behavior exists only for the fixed-damage victory-stop test.
- Damage application exists only as fixed throw damage against the current enemy.
- Enemy turn behavior has not been implemented yet.
- Dice result selection has not been implemented yet.
- Dice Overlay Root exists only as a hidden reserved scene root; it does not roll, animate, or select results.
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

Completed layout foundation tasks:

- M2-000: Final Battle Layout Foundation.
- M2-001: Editor Layout Validation.

Current Battle scene result:

- Battle scene lives at `Assets/Scenes/Battle/Battle.unity`.
- `ProjectSettings/EditorBuildSettings.asset` references the canonical Battle scene path.
- The Battle scene hierarchy uses:
  - `Canvas`
  - `BattleRoot`
  - `TopHUD`
  - `Player Status`
  - `Enemy Status`
  - `BattleField`
  - `Hero Sprite Placeholder`
  - `Enemy Sprite Placeholder`
  - `Dice Overlay Root`
  - `Battle Log Placeholder`
  - `BottomHUD`
  - `Throw Button Placeholder`
- Hero and enemy placeholders are sized as future sprite slots instead of oversized debug blocks.
- `Dice Overlay Root` is centered in the battlefield and hidden by default.
- HP status areas include name text and HP text positions suitable for final UI replacement.
- Battle log and Throw button are in final layout regions.
- `Assets/Scripts/Battle/BattleCombatState.cs` stores known player HP, enemy HP, and fixed throw damage values.
- `Assets/Scripts/Battle/BattleHudPresenter.cs` binds `BattleCombatState` HP values to the Battle scene HP text.
- `Assets/Scripts/Battle/BattleController.cs` handles the current Throw placeholder interaction, applies fixed damage, refreshes HP display, and locks input after enemy defeat.
- Battle log shows simple current feedback such as ready, throw damage, and victory.
- `BattleHudPresenter` remains presentation-only.

## Validation Notes

- Codex confirmed the Unity Editor log loaded `Assets/Scenes/Battle/Battle.unity`.
- Codex confirmed the scene file preserves required hierarchy and serialized references.
- Codex confirmed recent editor log lines after the latest Battle scene load do not include target runtime/reference/input errors.
- A separate Unity batchmode run was attempted, but Unity refused because multiple Unity instances already had the project open.
- Human visual review in the active Editor Game view remains the final check for exact feel and scale.

## Architecture Boundaries

- `BattleCombatState` stores HP, fixed throw damage, and enemy defeated state, with only simple state mutation allowed.
- `BattleController` coordinates Throw input, fixed damage calls, HP refresh requests, battle log feedback, and input lock.
- `BattleHudPresenter` only presents state in UI.
- Future damage formula, Dice result, and skill calculations should be considered for a separate `BattleDamageResolver` when a later milestone explicitly requires them.

## Next Human Decision

Review the M2 Battle layout foundation visually in Unity Play Mode. If accepted, select the next milestone or detailed task from `MILESTONE_PLAN.md`.
