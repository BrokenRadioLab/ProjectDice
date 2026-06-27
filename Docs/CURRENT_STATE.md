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
- `M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION` is DONE as a human-selected presentation foundation task.
- The Battle scene now has a permanent gameplay layout foundation for future pixel art replacement.
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

Completed layout foundation task:

- M2-000: Final Battle Layout Foundation.

Current Battle scene result:

- Battle scene lives at `Assets/Scenes/Battle/Battle.unity`.
- `ProjectSettings/EditorBuildSettings.asset` references the canonical Battle scene path.
- The Battle scene hierarchy now uses:
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
- Battle log and Throw button have been moved into final layout regions.
- `Assets/Scripts/Battle/BattleCombatState.cs` stores known player HP, enemy HP, and fixed throw damage values.
- `Assets/Scripts/Battle/BattleHudPresenter.cs` binds `BattleCombatState` HP values to the Battle scene HP text.
- `Assets/Scripts/Battle/BattleController.cs` handles the current Throw placeholder interaction, applies fixed damage, refreshes HP display, and locks input after enemy defeat.
- Battle log shows simple current feedback such as ready, throw damage, and victory.
- `BattleHudPresenter` remains presentation-only.

## Architecture Boundaries

- `BattleCombatState` stores HP, fixed throw damage, and enemy defeated state, with only simple state mutation allowed.
- `BattleController` coordinates Throw input, fixed damage calls, HP refresh requests, battle log feedback, and input lock.
- `BattleHudPresenter` only presents state in UI.
- Future damage formula, Dice result, and skill calculations should be considered for a separate `BattleDamageResolver` when a later milestone explicitly requires them.

## Next Human Decision

Review the final Battle layout foundation in Unity Play Mode. If accepted, select the next milestone or detailed task from `MILESTONE_PLAN.md`.
