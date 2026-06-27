# CHANGELOG

## 2026-06-27

### Changed

- Marked `M0_PROJECT_SETUP` as DONE in `MILESTONE_PLAN.md`.
- Began `M1_COMBAT_CORE` and generated detailed M1-only tasks in `TASK_QUEUE.md`.
- Normalized the Battle scene path from `Assets/Scenes/BattleScene.unity` to `Assets/Scenes/Battle/Battle.unity`.
- Updated `ProjectSettings/EditorBuildSettings.asset` to reference the canonical Battle scene path.
- Added scene-only placeholder UI anchors to `Assets/Scenes/Battle/Battle.unity` for hero area, enemy area, player HP, enemy HP, Throw button, and battle log/result text.
- Added `Assets/Scripts/Battle/BattleCombatState.cs` to store known player HP, enemy HP, and fixed throw damage values for M1.
- Added a `Battle Combat State` scene object to `Assets/Scenes/Battle/Battle.unity`.
- Added `Assets/Scripts/Battle/BattleHudPresenter.cs` to present `BattleCombatState` HP values in the existing Battle scene HP placeholders.
- Updated the Battle scene HP placeholder text to display current stored values.
- Added `Assets/Scripts/Battle/BattleController.cs` for M1-only Throw placeholder interaction, fixed damage application, HP refresh, battle log feedback, and victory input lock.
- Added an EventSystem to the Battle scene so the Throw placeholder can receive UI pointer input.
- Extended `BattleCombatState` with deterministic fixed-damage enemy HP reduction and enemy defeated state.
- Updated `CURRENT_STATE.md` with M1 progress and the next human decision point.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No gameplay action behavior was implemented.
- No enemy turn behavior was implemented.
- No Dice result selection was implemented.
- No skills, upgrades, rewards, progression, or future milestone systems were implemented.
- No external placeholder assets or prefabs were created.
