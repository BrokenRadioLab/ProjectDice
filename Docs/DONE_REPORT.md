# DONE REPORT

Date: 2026-06-29

Selected Task: M2-008_CONNECT_FIXED_THROW_DAMAGE_SOURCE_TO_DICE_GRADE_MVP_VALUE

Completed Task: Connect Throw Damage To Selected Dice Face Value

## Summary

Throw damage now comes from the selected Dice face's deterministic damage value instead of a scene-level hardcoded fixed throw value. The Battle flow still selects one Dice face, shows the temporary validation result, applies damage, refreshes HP, and locks input after enemy defeat.

## Completed Work

- Updated `BattleCombatState` to apply an incoming damage value with `ApplyDamageToEnemy(int damage)`.
- Removed the serialized `fixedThrowDamage` field from `BattleCombatState`.
- Removed the old `fixedThrowDamage` value from `Assets/Scenes/Battle/Battle.unity`.
- Updated `BattleController` to keep the selected Dice face from result selection.
- Updated `BattleController` to apply `selectedFace.FixedThrowDamageValue` as deterministic Throw damage.
- Kept `BattleDiceResultPresenter` as validation-only presentation.
- Updated task queue, current state, changelog, and self-review documents.

## Validation

- Throw still selects one Dice face through the existing M2 Dice selection flow.
- Selected slot and face name remain visible through the temporary validation text.
- Damage is now read from the selected Dice face value.
- Enemy HP still updates through `BattleCombatState.ApplyDamageToEnemy`.
- Enemy HP still clamps at 0.
- HP still refreshes through `BattleHudPresenter`.
- Enemy defeat input lock remains in `BattleController`.
- `PROJECT_GDD_v1.0.md` was not modified.

## Limitations

- This task does not add Dice grade progression tables.
- This task does not add face skills.
- This task does not add enemy turns.
- This task does not add rewards, progression, dice animation overlay, or multi-enemy logic.
- Non-damaging starter faces currently apply 0 damage because their face value is 0.

## Stop Point

Stopped after M2-008 implementation and validation as requested. M2-009 was not started.
