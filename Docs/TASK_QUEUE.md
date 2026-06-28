# TASK QUEUE

Selected Milestone: M2_DICE_CORE

Milestone Validation Status: READY_FOR_DIRECTOR_REVIEW

Source Milestone: `MILESTONE_PLAN.md`

GDD References:

- Section 10: Dice System
- Section 11: Dice Structure
- Section 13: Dice Grades
- Section 14: Dice Grade Progression
- Section 15: Dice Faces
- Section 17: Throw Damage
- Section 19: Dice Result Overlay
- Section 21: Battle System
- Section 32: User Interface

Presentation Reference:

- `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md`

## Completed M2 Foundation Tasks

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

## M2-002: Align Battle Scene To Presentation Guide

Status: DONE

Goal:

Align `Assets/Scenes/Battle/Battle.unity` with `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0` so the Battle scene resembles the intended final production battle layout instead of a prototype/debug layout.

Done Criteria:

- Battle Scene follows `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0` for hierarchy, character placement, HP placement, center space reservation, DiceAnimationLayer structure, hidden battle log, and bottom action layout while preserving current fixed-damage Throw behavior.

## Remaining M2 Tasks

## M2-003: Throw Sequence Placeholder

Status: DONE

Goal:

Implement the first minimal Throw presentation sequence according to `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0`.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scripts/Battle/ThrowSequencePresenter.cs`
- `Assets/Scripts/Battle/ThrowSequencePresenter.cs.meta`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Keep `PROJECT_GDD_v1.0.md` unchanged.
- Keep `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` unchanged.
- Lock THROW input when the sequence starts.
- Show simple Hero throw feedback.
- Show a thin white projectile trail from `HeroSlot` toward `EnemySlot_01`.
- Show a brief Enemy hit flash.
- Apply existing fixed damage after the feedback.
- Refresh HP through `BattleHudPresenter`.
- Hide projectile and hit placeholders after the sequence.
- Unlock THROW if the enemy is still alive.
- Keep placeholder visuals small, production-positioned, and 16-bit battle presentation friendly.
- Keep Hero and standard Enemy placeholder scale aligned with the guide's 48x48 sprite intent.
- Do not implement dice rolling, dice result, face reveal, face skills, enemy turn, multi-enemy targeting, rewards, or progression.

Validation Checklist:

- THROW triggers the sequence.
- Damage no longer feels completely instant.
- Projectile trail appears briefly.
- Enemy hit feedback appears briefly.
- HP updates after feedback.
- Hero placeholder remains small and production-positioned.
- Enemy placeholder remains small and production-positioned.
- HP still refreshes through `BattleHudPresenter`.
- Enemy HP still clamps at 0.
- Throw locks after enemy defeat.
- No dice rolling, dice result, face reveal, skill, enemy turn, reward, progression, or multi-enemy targeting logic was added.

Done Criteria:

- THROW now plays a minimal Hero feedback, projectile trail, and Enemy hit flash before applying existing fixed damage, without adding dice result logic or future combat systems.

## M2-004: Create Dice Core Data Model

Status: DONE

Goal:

Create the minimal runtime Dice model needed for MVP throws: one Dice with six face slots and duplicate face support.

Files:

- `Assets/Scripts/Dice/DiceFace.cs`
- `Assets/Scripts/Dice/DiceFace.cs.meta`
- `Assets/Scripts/Dice/DiceModel.cs`
- `Assets/Scripts/Dice/DiceModel.cs.meta`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Keep the model runtime-only for now.
- Represent exactly six Dice face slots.
- Allow duplicate faces.
- Keep face data minimal enough for M2.
- Include only data needed for result selection and fixed throw damage source.
- Do not implement face skills.
- Do not implement rewards or face replacement.
- Do not create ScriptableObjects unless this task is explicitly revised.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- `DiceModel` can hold six face entries.
- Duplicate face entries are legal.
- The model does not include skill resolution behavior.
- The model does not include reward or progression behavior.
- Code compiles without Unity console errors.

Done Criteria:

- A minimal Dice runtime data model exists and can represent the player's six-face Dice build.

## M2-005: Add Starter Dice Runtime State

Status: DONE

Goal:

Add a starter Dice state to the Battle scene so MVP combat has a concrete Dice build to throw.

Files:

- `Assets/Scripts/Dice/StarterDiceFactory.cs`
- `Assets/Scripts/Dice/StarterDiceFactory.cs.meta`
- `Assets/Scripts/Battle/BattleDiceState.cs`
- `Assets/Scripts/Battle/BattleDiceState.cs.meta`
- `Assets/Scripts/Dice/DiceModel.cs`
- `Assets/Scenes/Battle/Battle.unity`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Create one starter Dice with six face slots.
- Duplicate faces must be allowed in the starter Dice.
- Keep starter values deterministic and simple.
- Keep Dice state separate from `BattleCombatState`.
- Do not move HP state into Dice state.
- Do not implement dice result overlay.
- Do not implement face skills.
- Do not implement reward replacement.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Battle scene has a serialized/runtime Dice state source.
- Starter Dice contains six faces.
- Duplicate starter faces are supported.
- Existing Throw fixed damage still works.
- HP UI still refreshes through `BattleHudPresenter`.
- No overlay, skill, reward, or progression behavior is added.

Done Criteria:

- Battle scene has a simple starter Dice build available for later result selection.

## M2-006: Select One Dice Face Result Per Throw

Status: DONE

Goal:

Add the first Dice result selection behavior so each accepted Throw selects exactly one face from the current six-face Dice.

Files:

- `Assets/Scripts/Dice/DiceRoller.cs`
- `Assets/Scripts/Dice/DiceRoller.cs.meta`
- `Assets/Scripts/Dice/DiceModel.cs`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scripts/Battle/BattleDiceState.cs`
- `Assets/Scenes/Battle/Battle.unity`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Each accepted Throw produces exactly one Dice face result.
- Selection must use the six face slots as the result pool.
- Duplicate faces must naturally affect probability.
- Keep fixed throw damage behavior intact.
- Store or expose the latest selected face for validation/debug visibility if needed.
- Do not implement Dice Result Overlay.
- Do not implement face reveal UI.
- Do not implement face skills.
- Do not implement enemy turns.
- Do not implement rewards, progression, or face replacement.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Accepted Throw invokes Dice face selection once.
- One and only one face result is selected per Throw.
- Duplicate faces are represented as duplicate entries in the selection pool.
- Enemy HP still decreases by fixed damage.
- Enemy HP still clamps to zero.
- Throw still locks after enemy defeat.
- No overlay or skill logic is added.

Done Criteria:

- M2 combat randomness exists only as a selected Dice face result, without changing the current fixed-damage combat outcome.

## M2-007: Surface Latest Dice Result For Debug-Free Validation

Status: DONE

Goal:

Expose the latest selected Dice face in a minimal, non-permanent validation-friendly way without adding the final Dice Result Overlay.

Files:

- `Assets/Scripts/Battle/BattleDiceResultPresenter.cs`
- `Assets/Scripts/Battle/BattleDiceResultPresenter.cs.meta`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scenes/Battle/Battle.unity`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Keep this as temporary validation presentation only.
- Do not create the final Dice Result Overlay.
- Do not animate rolling.
- Do not reveal a top face as final overlay presentation.
- Do not permanently occupy the center battle space.
- Keep `BattleHudPresenter` presentation-only for HP.
- Do not put Dice selection logic in the presenter.
- Do not implement face skills or enemy turns.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Latest selected Dice face can be verified in Play Mode.
- The display is not a permanent battle log replacement.
- The display does not conflict with the reserved center DiceAnimationLayer flow.
- Throw fixed damage and HP refresh still work.
- No M3 final overlay behavior is introduced.

Done Criteria:

- Developers can verify Dice result selection without implementing the final Dice Result Overlay milestone.

## M2-008: Connect Fixed Throw Damage Source To Dice Grade MVP Value

Status: DONE

Goal:

Prepare fixed throw damage to come from the current MVP Dice grade/value source while keeping damage deterministic and non-random.

Files:

- `Assets/Scripts/Battle/BattleCombatState.cs`
- `Assets/Scripts/Battle/BattleController.cs`
- `Assets/Scenes/Battle/Battle.unity`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Keep Throw damage fixed, deterministic, and non-random.
- Add only the minimum Dice grade/value source needed for MVP.
- Preserve the current visible damage amount unless the task explicitly documents a new MVP value.
- Do not implement Dice grade rewards.
- Do not implement Dice replacement.
- Do not implement random damage ranges.
- Do not implement skill effects.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Throw damage still has no random range.
- Throw damage can be traced to the current Dice grade/value source.
- Enemy HP still decreases correctly.
- Enemy HP still clamps to zero.
- Throw still locks after enemy defeat.
- Dice face result selection remains separate from fixed throw damage.

Done Criteria:

- M2 has a clean MVP path for fixed throw damage to come from the current Dice without introducing non-M2 systems.

## M2-009: Validate M2 Dice Core

Status: DONE

Goal:

Validate the completed M2 Dice Core end-to-end before moving into the Dice Result Overlay milestone.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scripts/Battle/`
- `Assets/Scripts/Dice/`
- `Docs/TASK_QUEUE.md`
- `Docs/CURRENT_STATE.md`
- `Docs/CHANGELOG.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`

Requirements:

- Open or validate `Battle.unity` in Unity Editor if possible.
- Confirm no compile errors.
- Confirm Throw still accepts input.
- Confirm fixed damage still applies.
- Confirm one Dice face result is selected per Throw.
- Confirm duplicate faces affect result probability by existing as duplicate pool entries.
- Confirm Dice face selection does not apply skills.
- Confirm no Dice Result Overlay, enemy turn, reward, progression, inventory, or item behavior has been introduced.
- Do not modify `PROJECT_GDD_v1.0.md`.

Validation Checklist:

- Battle scene loads without missing script references.
- Throw button remains clickable.
- Enemy HP decreases by fixed deterministic damage.
- Latest Dice face result changes according to the six-face pool.
- Duplicate faces remain legal.
- Enemy defeated state still locks Throw.
- No `NullReferenceException`.
- No `MissingReferenceException`.
- No compile errors.
- No future milestone systems are present.

Done Criteria:

- M2_DICE_CORE is ready for human review and can hand off cleanly to M3_DICE_RESULT_OVERLAY.
