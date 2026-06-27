# TASK QUEUE

Selected Milestone: M1_COMBAT_CORE

Source Milestone: `MILESTONE_PLAN.md`

GDD References:

- Section 17: Throw Damage
- Section 21: Battle System
- Section 22: Victory & Defeat
- Section 33: Battle Screen

## M1-T001: Normalize Battle Scene Location

Status: DONE

Goal:

Make the Battle scene available at the canonical MVP path before adding combat behavior.

Files:

- `Assets/Scenes/Battle/Battle.unity`
- `Assets/Scenes/Battle/Battle.unity.meta`
- `ProjectSettings/EditorBuildSettings.asset`

Requirements:

- Use the existing Battle scene content.
- Preserve the scene meta GUID by moving the `.meta` file with the scene.
- Keep the scene inside the existing `Assets/Scenes/Battle/` folder.
- Update build settings to reference the canonical Battle scene path.
- Do not create scripts.
- Do not implement combat behavior yet.

Validation Checklist:

- `Assets/Scenes/Battle/Battle.unity` exists.
- `Assets/Scenes/Battle/Battle.unity.meta` exists.
- `Assets/Scenes/BattleScene.unity` no longer exists at the old root scene path.
- `ProjectSettings/EditorBuildSettings.asset` references `Assets/Scenes/Battle/Battle.unity`.
- No non-meta script files were created.

Done Criteria:

- Battle scene has a stable canonical path for the remaining M1 work.

## M1-T002: Create Minimal Battle Screen Layout

Status: DONE

Goal:

Create the minimum visible Battle scene layout needed for M1 combat testing.

Files:

- `Assets/Scenes/Battle/Battle.unity`

Requirements:

- Show player side.
- Show enemy side.
- Show player HP text or bar.
- Show enemy HP text or bar.
- Show a Throw button location.
- Show a battle log/result text location.
- Keep layout simple for landscape MVP testing.
- Use placeholder Unity UI only.
- Do not add gameplay logic or combat systems.

Validation Checklist:

- Battle scene opens with visible player and enemy areas.
- HP display locations are visible.
- Throw action location is visible.
- Battle log/result text location is visible.
- No Dice Result Overlay work is included.
- No gameplay scripts are created.

Done Criteria:

- Battle scene has enough visible UI anchors for combat core wiring.

## M1-T003: Add Minimal Combat State

Status: DONE

Goal:

Create the smallest runtime battle state needed to test fixed throw damage.

Files:

- `Assets/Scripts/Battle/`
- `Assets/Scenes/Battle/Battle.unity`

Requirements:

- Track player HP.
- Track enemy HP.
- Track fixed throw damage.
- Exclude Dice face result selection.
- Exclude skill effects.
- Exclude enemy turn behavior.
- Do not wire the Throw button yet.
- Do not apply damage yet.

Validation Checklist:

- Battle starts with known player HP.
- Battle starts with known enemy HP.
- Fixed throw damage is readable from combat state.
- No random damage range exists.
- No Dice face result selection exists.
- No enemy turn behavior exists.

Done Criteria:

- Battle scene can hold deterministic M1 combat state.

## M1-T004: Bind Combat State To HP UI

Status: DONE

Goal:

Display current player and enemy HP from `BattleCombatState` in the existing Battle scene UI placeholders.

Files:

- `Assets/Scripts/Battle/`
- `Assets/Scenes/Battle/Battle.unity`

Requirements:

- Connect existing `BattleCombatState` values to existing HP text placeholders.
- Display player HP from the current stored state values.
- Display enemy HP from the current stored state values.
- Keep this as presentation binding only.
- Do not add Throw button behavior.
- Do not apply damage.
- Do not add Dice result selection.
- Do not add turn logic.
- Do not add victory or defeat logic.

Validation Checklist:

- Player HP text displays `BattleCombatState` player HP values.
- Enemy HP text displays `BattleCombatState` enemy HP values.
- Throw button behavior is not implemented.
- Damage application is not implemented.
- No Dice Result Overlay appears yet.

Done Criteria:

- Battle scene presents current deterministic M1 HP state through the existing UI placeholders.

## M1-T005: Add M1 Victory Stop

Status: DONE

Goal:

Stop the M1 combat test when enemy HP reaches zero.

Files:

- `Assets/Scripts/Battle/`
- `Assets/Scenes/Battle/Battle.unity`

Requirements:

- Use a separate battle controller for input and battle flow.
- Keep `BattleHudPresenter` presentation-only.
- Detect enemy HP at or below zero.
- Disable further Throw input after victory.
- Show simple victory feedback.
- Do not add rewards.
- Do not add stage progression.
- Do not add Dice result selection.
- Do not add turn logic.
- Do not add skills, upgrades, or future systems.

Validation Checklist:

- Enemy HP cannot continue below the intended clamped value.
- Throw action stops after victory.
- Victory state is visible.
- Existing HP display still works through `BattleHudPresenter`.
- `BattleHudPresenter` does not contain damage, input, or battle-flow responsibility.

Done Criteria:

- M1 combat core can demonstrate a simple player-driven victory.

## M1-T006: Validate M1 Combat Core

Status: DONE

Goal:

Confirm M1 exit criteria before requesting human review.

Files:

- `Docs/CURRENT_STATE.md`
- `Docs/DONE_REPORT.md`
- `Docs/SELF_REVIEW_REPORT.md`
- M1 implementation files

Requirements:

- Verify player can perform a throw action.
- Verify fixed throw damage reduces enemy HP.
- Verify no random damage range exists.
- Record any M1 limitations.
- Verify `BattleHudPresenter` remains presentation-only.
- Verify no future systems were introduced.

Validation Checklist:

- M1 exit criteria are checked.
- Human review point is documented.
- No M2 Dice Core behavior is implemented.
- Throw applies fixed damage through `BattleController` and `BattleCombatState`.
- HP UI updates through `BattleHudPresenter`.
- Skills, upgrades, rewards, progression, and future systems are absent.

Done Criteria:

- M1 is ready for human review.
