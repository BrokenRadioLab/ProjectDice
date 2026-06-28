# TASK QUEUE

Selected Task: M2-002_ALIGN_BATTLE_SCENE_TO_PRESENTATION_GUIDE

Source Milestone: `MILESTONE_PLAN.md`

Presentation Reference:

- `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md`

GDD References:

- Section 4: Design Philosophy
- Section 17: Throw Damage
- Section 21: Battle System
- Section 32: User Interface

## M2-002: Align Battle Scene To Presentation Guide

Status: DONE

Goal:

Align `Assets/Scenes/Battle/Battle.unity` with `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0` so the Battle scene resembles the intended final production battle layout instead of a prototype/debug layout.

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

- Reposition Hero to the left side at the final sprite slot location.
- Reposition Enemy to the right side at the final sprite slot location.
- Prepare `EnemySlotsRoot` with `EnemySlot_01`, `EnemySlot_02`, and `EnemySlot_03`.
- Keep only `EnemySlot_01` currently active/used.
- Move Player HP above Hero.
- Move Enemy HP above EnemySlot_01.
- Keep the center battlefield empty for future dice presentation, effects, damage numbers, and skills.
- Replace the previous Dice Overlay structure with `DiceAnimationLayer`.
- Keep `DiceAnimationLayer` hidden by default and inside `BattleField`.
- Hide the permanent visible Battle Log while preserving the existing text reference for compatibility.
- Keep THROW as the primary action button.
- Add inactive Skill and Item placeholders in the bottom action area.
- Preserve fixed Throw damage, HP refresh, and defeat input lock.
- Do not implement dice rolling, dice result selection, face reveal, skills, enemy turns, rewards, progression, inventory, or item systems.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- `HeroSlot` exists under `BattleField`.
- `EnemySlotsRoot` exists under `BattleField`.
- `EnemySlot_01`, `EnemySlot_02`, and `EnemySlot_03` exist.
- Player HP is parented to the Hero slot.
- Enemy HP is parented to EnemySlot_01.
- Center battlefield has no permanent visible object.
- `DiceAnimationLayer` exists inside `BattleField`.
- `DiceAnimationLayer` is hidden by default.
- `Battle Log Placeholder` is hidden by default.
- `Skill Button Placeholder` and `Item Button Placeholder` exist and are inactive.
- Throw remains the active primary button.
- `BattleController` still applies fixed damage immediately through `BattleCombatState`.
- `BattleHudPresenter` still owns HP UI refresh.
- No `DiceOverlayPresenter` reference remains.
- No rolling placeholder, dice result, face reveal, skill, enemy turn, reward, or progression logic was added.

Done Criteria:

- Battle Scene now follows `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0` for hierarchy, character placement, HP placement, center space reservation, DiceAnimationLayer structure, hidden battle log, and bottom action layout while preserving current fixed-damage Throw behavior.
