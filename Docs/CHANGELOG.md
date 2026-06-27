# CHANGELOG

## 2026-06-28

### Changed

- Rebuilt `Assets/Scenes/Battle/Battle.unity` into a permanent Battle layout foundation with `Canvas`, `BattleRoot`, `TopHUD`, `BattleField`, `Battle Log Placeholder`, `BottomHUD`, and `Throw Button Placeholder` structure.
- Replaced oversized hero and enemy debug areas with smaller future sprite placeholder slots.
- Added Player and Enemy status hierarchy with name text and HP text locations.
- Added a centered `Dice Overlay Root` reserved for future overlay presentation and kept it hidden by default.
- Moved the battle log and Throw button into final layout regions while preserving current M1 behavior.
- Removed legacy EventSystem/InputModule scene dependency and kept input compatible with the active New Input System setup.
- Added runtime font fallback for scene UI text visibility in the current Unity setup.
- Updated `TASK_QUEUE.md`, `CURRENT_STATE.md`, `DONE_REPORT.md`, and `SELF_REVIEW_REPORT.md` for `M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION`.
- Validated the M2 Battle layout foundation for `M2-001_EDITOR_LAYOUT_VALIDATION`.
- Confirmed Unity Editor log loaded `Assets/Scenes/Battle/Battle.unity`.
- Confirmed serialized scene hierarchy and references for BattleRoot, TopHUD, BattleField, Battle Log, BottomHUD, Throw input, HP text, and hidden Dice Overlay Root.
- Recorded that Unity batchmode validation was blocked because the project was already open in Unity.
- Added `DiceOverlayPresenter` to present the temporary rolling/unknown Dice Overlay placeholder.
- Wired `BattleController` to show the Dice Overlay before applying existing fixed damage.
- Added a short rolling overlay delay before HP and Battle Log update.
- Added `Rolling Dice Placeholder` and `Rolling State Text` under `Dice Overlay Root`.
- Adjusted the rolling overlay placeholder duration to `1.3` seconds and moved timing to an update-driven unscaled timer.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No dice result selection, face reveal, skill activation, enemy turns, rewards, progression, or future systems were implemented.
- `BattleCombatState`, `BattleController`, and `BattleHudPresenter` responsibilities were not expanded beyond their existing boundaries.

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
- Validated M1 combat core scope and documented architecture boundaries for state, controller, HUD presentation, and future damage resolution.
- Updated `CURRENT_STATE.md` with M1 progress and the next human decision point.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No gameplay action behavior was implemented.
- No enemy turn behavior was implemented.
- No Dice result selection was implemented.
- No skills, upgrades, rewards, progression, or future milestone systems were implemented.
- No M2 or later milestone systems were introduced during M1 validation.
- No external placeholder assets or prefabs were created.
