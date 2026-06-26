# CURRENT STATE

Last Updated: 2026-06-27

## Source of Truth

`Docs/PROJECT_GDD_v1.0.md` is the source of truth for Project Dice. Planning documents may organize work, but they must not redefine the game against the GDD.

## Current Status

The project is in planning and setup state.

- M0_PROJECT_SETUP is DONE.
- M1_COMBAT_CORE is IN_PROGRESS.
- M1-T001 is DONE.
- M1-T002 is DONE.
- M1-T003 is DONE.
- Gameplay actions have not been implemented yet.
- Throw button behavior has not been implemented yet.
- Combat systems beyond minimal state storage have not been created.
- Placeholder assets have not been created for M1-T001, M1-T002, or M1-T003.
- GDD has not been intentionally modified.
- `MILESTONE_PLAN.md` is the only milestone source.
- `TASK_QUEUE.md` contains detailed tasks only for the selected M1_COMBAT_CORE milestone.

## Workflow Rule

Codex should not generate detailed implementation tasks until a human selects a milestone for implementation.

When a milestone is selected:

- Read the selected milestone from `MILESTONE_PLAN.md`.
- Re-check the relevant GDD sections.
- Generate small READY tasks for that milestone only.
- Keep the implementation inside the selected milestone scope.
- Write those detailed tasks into `TASK_QUEUE.md`.
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

Selected milestone: M1_COMBAT_CORE

Completed task:

- M1-T001: Normalize Battle Scene Location.
- M1-T002: Create Minimal Battle Screen Layout.
- M1-T003: Add Minimal Combat State.

Current result:

- Battle scene now lives at `Assets/Scenes/Battle/Battle.unity`.
- The matching scene meta file now lives at `Assets/Scenes/Battle/Battle.unity.meta`.
- The old root scene path `Assets/Scenes/BattleScene.unity` is no longer used.
- `ProjectSettings/EditorBuildSettings.asset` now points to the canonical Battle scene path.
- Battle scene now includes visible placeholder UI anchors for hero area, enemy area, player HP, enemy HP, Throw button, and battle log/result text.
- `Assets/Scripts/Battle/BattleCombatState.cs` stores known player HP, enemy HP, and fixed throw damage values.
- `Assets/Scenes/Battle/Battle.unity` contains a `Battle Combat State` scene object using that state component.

## Next Human Decision

Review M1-T003. If approved, continue with the next READY task in `TASK_QUEUE.md`: M1-T004 Wire Throw Action to Fixed Damage.
