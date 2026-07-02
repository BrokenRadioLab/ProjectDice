# SELF_REVIEW_REPORT

Date: 2026-07-02

Task: M8-001_BASE_THROW_DAMAGE_FRAMEWORK

## Review Result

PASS

Base Throw Damage Framework follows the locked Dice Combat Philosophy and keeps Face-specific starter gameplay for later M8 tasks.

## Scope Check

- Added Dice-owned Base Throw Damage to runtime Dice.
- Moved `PROJECT_CORE_PHILOSOPHY.md` under `Docs/Design/`.
- Added Dice Combat Philosophy to the core philosophy document.
- Regenerated M8 task breakdown.
- Implemented M8-001 Base Throw Damage Framework.
- Starter Dice now owns Base Throw Damage 5.
- Starter Attack is a 5 damage Face modifier, preserving 10 total starter Attack Throw damage.
- Guard, Spark, and Mend can deal Base Throw Damage before their dedicated Face effects are implemented.
- Did not implement Guard, Spark, or Mend gameplay.
- Did not add victory gameplay.
- Did not add restart UI.
- Did not add run summary.
- Did not add battle end presentation or full battle reset flow.
- Did not add stage selection UI.
- Did not add restart flow or new run creation.
- Did not add post-run economy.
- Did not add healing rules.
- Did not add rewards, Dice replacement, inventory, shops, permanent progression, boss systems, or multi-enemy logic.
- Did not add new Faces, Guard/Spark/Mend gameplay effects, enemy AI, or boss mechanics.
- Did not add multi-enemy targeting, multiple enemy HP mutation, or multi-enemy UI.
- Did not add victory presentation, full battle reset flow, transition UI, rewards, or post-run systems.
- Did not change Dice result selection, FaceResolver logic, EnemyAttackResolver, BattleTurnState, ThrowSequencePresenter, EnemyAttackPresenter, BattleHudPresenter, or Dice Deck logic.
- Did not change post-outcome runtime flow: non-boss Victory can advance the fixed stage position and prepare the next battle, Boss Victory completes the run, and Defeat prevents PlayerTurn from resuming.

## Architecture Check

- `BattleTurnState` owns turn ownership only.
- `BattleOutcomeState` owns battle outcome only.
- `LinearStageRuntimeState` owns current stage position and fixed-order advancement only.
- `LinearRunState` owns fixed linear run completion only.
- `EnemyGroupState` owns the group-level defeated query for current active enemy slots.
- Battle outcome remains separate from turn ownership and combat HP state.
- Stage runtime remains separate from battle outcome, rewards, next battle preparation, transition presentation, and run completion.
- After victory is set, battle completion checks consume `BattleOutcomeState`.
- Player pipeline remains `DiceFace -> FaceResolver -> FaceEffectData -> BattleController -> ThrowSequencePresenter -> BattleCombatState -> BattleHudPresenter`.
- Enemy pipeline remains `EnemyAttackResolver -> EnemyAttackIntent -> EnemyAttackPresenter -> BattleCombatState -> BattleHudPresenter`.
- Dice Deck remains Battle Information UI and reads from `BattleDiceState.CurrentDice`.
- Battle presentation and Dice Deck information UI remain separated.
- Dice Deck remains a runtime build viewer, not battle presentation, reward UI, inventory, or Dice replacement UI.
- `RunFlowPresenter` consumes runtime state but does not own outcome, stage progression, run completion, HP, turn ownership, rewards, or Dice data.
- `DiceModel` owns Base Throw Damage.
- BattleController consumes Dice-owned Base Throw Damage and FaceEffectData to calculate total Throw damage.
- No Hunter-owned permanent Attack stat was introduced.
- Battle Resume is part of the run-flow presentation sequence and remains separate from battle preparation.
- Run Complete is a presentation consumer of `LinearRunState.Completed` and remains separate from run completion ownership.
- Defeat is a presentation consumer of `BattleOutcomeState.Defeat` and remains separate from defeat ownership.

## Validation Review

- Enemy defeat marks `BattleOutcomeState` as `Victory`.
- Victory resolution consumes `EnemyGroupState.AreAllEnemiesDefeated`.
- Player defeat resolution consumes `BattleCombatState.IsPlayerDefeated` only to set `BattleOutcomeState.Defeat`.
- Stage advancement consumes `BattleOutcomeState.Victory` and `LinearStageRuntimeState`.
- Run completion consumes Boss-stage `BattleOutcomeState.Victory`.
- Enemy turn does not begin after victory is set.
- Additional Throw input is not accepted after victory is set.
- Player turn does not resume after defeat is set.
- Additional Throw input is not accepted after defeat is set.
- Non-boss Victory advances the fixed runtime stage once.
- Boss-stage Victory does not advance to a nonexistent stage.
- Boss-stage Victory marks the fixed linear run complete.
- Further battle input is blocked after run completion.
- Non-boss stage advance resets enemy HP for the next battle.
- Non-boss stage advance resets battle outcome to `InProgress`.
- Non-boss stage advance restores turn ownership to `PlayerTurn`.
- Current runtime Dice persists across next battle preparation.
- Player HP persists across next battle preparation.
- Dice Deck runtime data continues to read from current runtime Dice.
- Starter Dice still contains Attack, Attack, Guard, Guard, Spark, and Mend.
- Starter Dice Base Throw Damage is 5.
- Attack Face currently adds 5 damage as a Face modifier.
- Starter Attack Throw total damage is 10 before enemy HP clamping.
- Guard, Spark, and Mend currently add no Face effect but still deal Base Throw Damage.
- Run-flow presentation hook does not mutate HP.
- Run-flow presentation hook does not mutate turn ownership.
- Run-flow presentation hook does not mutate stage runtime or run state.
- Boss Victory can be observed after run completion is marked.
- Stage Clear does not appear for Boss Victory.
- Stage Clear does not appear for Defeat.
- Stage Clear does not mutate HP, Dice, battle outcome, stage runtime, run state, or turn ownership.
- Next Stage presentation reads `LinearStageRuntimeState`.
- Next Stage presentation does not advance stages.
- Next Stage presentation does not prepare battles.
- Next Stage presentation does not generate maps, create rewards, replace Dice, or mutate runtime state.
- Battle Resume presentation appears after Next Stage.
- Battle Resume presentation does not mutate HP, turn ownership, battle outcome, stage runtime, run state, or Dice data.
- Battle Resume presentation does not prepare battles, heal the player, add new enemy setup rules, or unlock rewards.
- Run Complete presentation consumes `LinearRunState.Completed`.
- Run Complete presentation does not mutate HP, turn ownership, battle outcome, stage runtime, run state, or Dice data.
- Run Complete presentation does not add rewards, Dice replacement, meta progression, restart flow, new run creation, or post-run economy.
- Defeat presentation consumes `BattleOutcomeState.Defeat`.
- Defeat presentation does not mutate HP, turn ownership, battle outcome, stage runtime, run state, or Dice data.
- Defeat presentation does not add restart UI, run summary, meta progression, rewards, Dice replacement, or inventory.
- M7 static validation confirms `RunFlowPresenter` does not call stage advance, battle reset, HP mutation, Dice mutation, reward, Dice replacement, restart, run summary, new run, or meta progression code.
- Unity batchmode import/compile validation completed successfully with exit code 0.
- Fixed run path is Stage 1 Normal, Stage 2 Normal, Stage 3 Normal, Stage 4 Elite, and Stage 5 Boss.
- Stage advance is not triggered by defeat.
- Reward flow is not triggered by victory.
- Reward flow is not triggered by defeat.
- Unity batchmode import/compile validation completed successfully with exit code 0.

## Residual Risk

- Human Play Mode review is still needed to confirm the existing battle loop still feels unchanged in-device.
- Human Play Mode review is still needed to confirm the exact next battle transition feel on device.
- Human Play Mode review is still needed to confirm the full five-stage run pacing and in-device feel.

## Status

M8-001_BASE_THROW_DAMAGE_FRAMEWORK is complete.
