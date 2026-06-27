# TASK QUEUE

Selected Task: M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION

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

Requirements:

- Create the final Battle scene hierarchy foundation:
  - Canvas
  - BattleRoot
  - TopHUD
  - Player Status
  - Enemy Status
  - BattleField
  - Hero Sprite Placeholder
  - Enemy Sprite Placeholder
  - Dice Overlay Root
  - Battle Log
  - BottomHUD
  - Throw Button
- Keep existing HP presentation functional.
- Keep existing Throw button behavior functional.
- Keep existing battle log functionality.
- Reserve a centered Dice Overlay Root hidden by default.
- Avoid oversized debug rectangles.
- Do not add dice rolling, dice result selection, skills, enemy turns, rewards, progression, or future systems.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Existing Throw behavior still works.
- Existing HP updates still work through `BattleHudPresenter`.
- Existing battle log still works through `BattleController`.
- `Dice Overlay Root` exists in the Battle scene.
- `Dice Overlay Root` is hidden by default.
- Hero placeholder is sized and positioned as a future sprite slot rather than a large debug area.
- Enemy placeholder is sized and positioned as a future sprite slot rather than a large debug area.
- Layout resembles the intended final mobile landscape battle screen foundation.

Done Criteria:

- Battle scene uses the final layout hierarchy foundation while preserving current M1 combat behavior and avoiding new gameplay systems.
