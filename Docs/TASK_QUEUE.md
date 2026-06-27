# TASK QUEUE

Selected Task: M2-001_EDITOR_LAYOUT_VALIDATION

Source Milestone: `MILESTONE_PLAN.md`

GDD References:

- Section 19: Dice Result Overlay
- Section 32: User Interface
- Section 33: Battle Screen
- Section 34: Dice Result Overlay
- Section 37: Art Direction

## M2-000: Final Battle Layout Foundation

Status: DONE

Goal:

Rebuild the Battle scene presentation into the permanent gameplay layout foundation that future pixel art assets can replace.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scripts/Battle/BattleHudPresenter.cs`

Done Criteria:

- Battle scene uses the final layout hierarchy foundation while preserving current M1 combat behavior and avoiding new gameplay systems.

## M2-001: Editor Layout Validation

Status: DONE

Goal:

Validate the completed M2-000 Battle layout in the current Unity Editor context without adding gameplay systems.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scripts/Battle/BattleHudPresenter.cs`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Confirm the Battle scene opens in Unity Editor.
- Confirm the final layout hierarchy exists.
- Confirm Hero and Enemy placeholders are production-sized sprite slots, not debug boxes.
- Confirm `Dice Overlay Root` exists and is hidden by default.
- Confirm Throw input remains connected to the Throw button hit area.
- Confirm fixed damage, HP refresh, battle log, victory, and input lock remain wired.
- Confirm no `NullReferenceException`, `MissingReferenceException`, or UI/Input console errors are present in the recent Unity Editor log.
- Do not add dice logic, enemy turns, skills, rewards, progression, or future systems.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- `Assets/Scenes/Battle/Battle.unity` was loaded by the Unity Editor.
- `BattleRoot`, `TopHUD`, `BattleField`, `Battle Log Placeholder`, and `BottomHUD` exist.
- `Player Status` and `Enemy Status` exist.
- `Hero Sprite Placeholder` and `Enemy Sprite Placeholder` exist.
- `Dice Overlay Root` exists and has `m_IsActive: 0`.
- `BattleController` still references `BattleCombatState`, `BattleHudPresenter`, `battleLogText`, and `throwButtonHitArea`.
- `BattleHudPresenter` still references player and enemy HP text objects.
- Recent Unity Editor log after Battle scene load contains no `NullReferenceException`, `MissingReferenceException`, old Input/EventSystem errors, or compile errors.
- No future gameplay systems were added.

Done Criteria:

- M2-000 layout foundation is validated as structurally ready for human visual review in Play Mode, with no layout/reference/input issues found by Codex.
