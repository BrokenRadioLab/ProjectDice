# TASK QUEUE

Selected Task: M2-002_DICE_ROLLING_OVERLAY_PLACEHOLDER

Source Milestone: `MILESTONE_PLAN.md`

GDD References:

- Section 4: Design Philosophy
- Section 17: Throw Damage
- Section 19: Dice Result Overlay
- Section 21: Battle System
- Section 32: User Interface
- Section 34: Dice Result Overlay

## M2-000: Final Battle Layout Foundation

Status: DONE

Goal:

Rebuild the Battle scene presentation into the permanent gameplay layout foundation that future pixel art assets can replace.

Done Criteria:

- Battle scene uses the final layout hierarchy foundation while preserving current M1 combat behavior and avoiding new gameplay systems.

## M2-001: Editor Layout Validation

Status: DONE

Goal:

Validate the completed M2-000 Battle layout in the current Unity Editor context without adding gameplay systems.

Done Criteria:

- M2-000 layout foundation is validated as structurally ready for human visual review in Play Mode, with no layout/reference/input issues found by Codex.

## M2-002: Dice Rolling Overlay Placeholder

Status: DONE

Goal:

Show a production-positioned rolling/unknown Dice Overlay placeholder immediately after Throw input, before existing fixed damage is applied.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scripts/Battle/DiceOverlayPresenter.cs`
- `Assets/Scripts/Battle/DiceOverlayPresenter.cs.meta`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Throw input is accepted through the existing Throw button hit area.
- Throw input locks immediately.
- `Dice Overlay Root` becomes active.
- Overlay displays an unknown rolling placeholder using UI primitives.
- Existing fixed damage happens after a `1.3` second overlay delay.
- HP UI updates after damage through `BattleHudPresenter`.
- Battle Log updates after damage.
- Overlay hides after the placeholder sequence.
- Throw input unlocks only if the enemy is still alive.
- Enemy defeated state still locks further Throw input.
- Do not implement dice face selection.
- Do not implement random results.
- Do not implement skills.
- Do not implement enemy turns.
- Do not implement rewards or progression.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- `Dice Overlay Root` is hidden at scene start.
- `DiceOverlayPresenter` is assigned on `Dice Overlay Root`.
- `BattleController` references `DiceOverlayPresenter`.
- Rolling placeholder image and `ROLLING...` text exist under `Dice Overlay Root`.
- Throw sequence calls `ShowRolling` before fixed damage.
- Throw sequence waits `1.3` seconds before applying fixed damage.
- Fixed damage still uses `BattleCombatState.ApplyFixedThrowDamageToEnemy`.
- HP UI still refreshes through `BattleHudPresenter`.
- Battle Log still updates through `BattleController`.
- Throw input remains locked during the overlay sequence.
- Enemy defeat still keeps Throw input locked.
- Recent Unity Editor log after script reload contains no target compile, reference, or input errors found by Codex.
- No dice result selection or skill logic was added.

Done Criteria:

- Throw now feels staged: click, rolling overlay placeholder, fixed damage, HP/log update, overlay hide.
