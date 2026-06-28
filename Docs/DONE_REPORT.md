# DONE REPORT

Date: 2026-06-29

Selected Task: M2-006_SELECT_ONE_DICE_FACE_RESULT_PER_THROW

Completed Task: Select One Dice Face Result Per Throw

## Summary

Added the first runtime Dice result selection step. Each accepted Throw now starts the current Dice roll state, selects exactly one slot from the six-face runtime Dice, stores that slot in `BattleDiceState`, advances the Dice phase to `Revealed`, and then continues the existing fixed-damage combat flow.

## Completed Work

- Re-read `PROJECT_GDD_v1.0.md`, `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md`, `CURRENT_STATE.md`, `TASK_QUEUE.md`, and `MILESTONE_PLAN.md`.
- Added `Assets/Scripts/Dice/DiceRoller.cs`.
- Added `Assets/Scripts/Dice/DiceRoller.cs.meta`.
- Added `DiceModel.BeginRoll`.
- Added `DiceModel.RevealResult`.
- Kept `DiceModel.RecordResultSlot` as the single stopped-result write point.
- Updated `BattleDiceState` to expose the latest selected result slot and face.
- Added `BattleDiceState.BeginThrowRoll`, `StopAtResultSlot`, and `RevealResult`.
- Updated `BattleController` to coordinate Dice phase flow and select one result per accepted Throw.
- Connected `BattleController` to the existing `BattleDiceState` component in `Assets/Scenes/Battle/Battle.unity`.
- Updated task queue, current state, changelog, and self-review documents.

## Validation

- Each accepted Throw calls Dice result selection once.
- Result selection uses the current six-slot Dice pool.
- Duplicate faces remain represented as duplicate slots in the selection pool.
- Selected result slot is stored in `BattleDiceState`.
- Dice phase flow is `Rolling` to `Stopped` to `Revealed` during the accepted Throw.
- Existing fixed Throw damage still uses `BattleCombatState`.
- HP still refreshes through `BattleHudPresenter`.
- Enemy HP still clamps at 0 through the existing combat state.
- Throw input still locks during the throw sequence and remains locked after enemy defeat.
- `PROJECT_GDD_v1.0.md` was not modified.

## Limitations

- Selected Dice faces do not affect damage yet.
- No Dice Result Overlay was implemented.
- No dice animation art, face reveal, face skills, enemy turn, rewards, progression, or future systems were added.

## Stop Point

Stopped after M2-006 implementation and validation as requested. M2-007 was not started.
