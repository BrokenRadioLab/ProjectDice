# SELF_REVIEW_REPORT

Date: 2026-07-03

Task: M10-001_REWARD_RUNTIME_FRAMEWORK

## Review Result

PASS

Reward runtime framework exists as state and data only; reward generation, reward effects, and reward UI remain unimplemented.

## Scope Check

- Added Dice-owned Base Throw Damage to runtime Dice.
- Moved `PROJECT_CORE_PHILOSOPHY.md` under `Docs/Design/`.
- Added Dice Combat Philosophy to the core philosophy document.
- Regenerated M8 task breakdown.
- M8-001 Base Throw Damage Framework remains implemented.
- Implemented M8-002 Guard Gameplay.
- Implemented M8-003 Lightning Gameplay.
- Implemented M8-004 Mend Gameplay.
- Implemented M8-005 Face Presentation Polish.
- Completed M8-006 Validate Starter Face Gameplay.
- Completed TASK_M8_PRESENTATION_READABILITY_FIX.
- Completed TASK_COMBAT_FEEDBACK_ORDER_POLISH.
- Completed STARTER_DICE_BUILD_FIXED_POOL_REMOVAL.
- Completed M9-003 UI Foundation Polish.
- Completed roadmap revision before Reward Selection.
- Inserted M9_STARTER_DICE_BUILD.
- Moved Reward Selection to M10.
- Moved Dice Face Replacement to M11.
- Moved MVP Playtest Polish to M12.
- Documented Starter Dice Build design without implementing it.
- Enlarged expanded Dice Deck UI for readability.
- Implemented M9-001 Starter Dice Build UI.
- Completed M9-002 Validate Starter Dice Build.
- Added `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md`.
- Marked M9_STARTER_DICE_BUILD as DONE after Director approval.
- Marked M10_REWARD_SELECTION as READY.
- Marked M10_REWARD_SELECTION as BLOCKED_BY_UI_ARCHITECTURE_REFACTOR after Director Technical Lock.
- Added `TASK_UI_ARCHITECTURE_REFACTOR` as planned technical debt before large-scale UI expansion.
- Completed `TASK_UI_ARCHITECTURE_REFACTOR`.
- Completed `TASK_SPLIT_BASE_THROW_DAMAGE_AND_FACE_EFFECT_APPLICATION`.
- Completed `M10-001_REWARD_RUNTIME_FRAMEWORK`.
- Added `RewardSelectionState`.
- Added `RewardData`.
- Added `RewardType` categories for Face, Heal, Max HP, and Relic.
- RewardSelectionState can open a runtime selection with assigned rewards.
- RewardSelectionState can select exactly one reward.
- RewardSelectionState clears remaining rewards after selection.
- RewardSelectionState tracks selected reward and consumed state.
- Unity batchmode compile validation completed successfully for M10-001.
- Split enemy HP mutation into Base Throw Damage first and Face damage modifier later.
- Base Throw Damage now applies immediately after enemy impact.
- HUD refresh now happens after Base Throw Damage before Dice Layer appears.
- Dice roll and Face reveal now occur after Base Throw Damage has been applied.
- Attack and Lightning apply only their Face damage modifiers after Face reveal.
- Mend applies healing after Face reveal.
- Guard reduction becomes available after Face reveal for the same battle exchange.
- Face-specific enemy popup now displays only the applied Face damage modifier.
- Base damage, Face damage, player healing, and player damage popups remain target-local.
- Unity batchmode compile validation completed successfully for the sequencing correction.
- Marked M10_REWARD_SELECTION as READY after UI architecture refactor completion.
- Refactored Starter Dice Build, Dice Deck, Run Flow, and combat feedback presenters to prefer serialized scene/prefab UI references.
- Kept runtime fallback creation only as a prototype compatibility path.
- Added a dedicated Combat Feedback Layer for target-local damage/heal popups.
- Removed Dice Animation Layer dependency from combat damage popup creation.
- Confirmed enemy damage popups target the enemy position.
- Confirmed player damage and Mend heal popups target the Hunter position.
- Unity batchmode compile validation completed successfully for `TASK_UI_ARCHITECTURE_REFACTOR`.
- Added active slot runtime support for Wood Dice.
- Added Face Tier to `DiceFace`.
- Removed fixed Starter Face Pool architecture.
- Starter Build now displays permanently unlocked Faces filtered by current Dice Tier.
- Duplicate Face selection no longer requires duplicate source entries.
- The only Starter Build restrictions are Face Tier and Active Face Slot count.
- Unity batchmode compile validation completed successfully for Starter Dice Build revision.
- Starter Build layout now avoids Dice slot clipping/overflow on mobile landscape.
- Dice Deck expanded panel now opens above BottomHUD and does not overlap the Throw button.
- Damage/heal popup timing remains readable.
- Unity batchmode compile validation completed successfully for M9-003.
- Added probability display for selected active Faces.
- Starter Dice now owns Base Throw Damage 3.
- Starter Attack is a 5 damage Face modifier, producing 8 total starter Attack Throw damage.
- Guard can deal Base Throw Damage and reduce the next incoming enemy attack damage by 3.
- Lightning can deal Base Throw Damage and add a deterministic 3 damage Lightning modifier.
- Mend can deal Base Throw Damage and heal the player for up to 5 HP.
- Face Effect presentation now communicates Base Throw Damage plus the resolved Face modifier.
- Throw presentation now shows Base Throw Damage before Dice Layer presentation.
- Face-specific feedback now shows Face modifiers separately from Base Throw Damage.
- Every starter Face now has meaningful gameplay value.
- No starter Face remains No Effect.
- Did not add victory gameplay.
- Did not add restart UI.
- Did not add run summary.
- Did not add battle end presentation or full battle reset flow.
- Did not add stage selection UI.
- Did not add restart flow or new run creation.
- Did not add post-run economy.
- Did not add long-term healing economy.
- Did not add rewards, Dice replacement, inventory, shops, permanent progression, boss systems, or multi-enemy logic.
- Did not implement full Reward Selection gameplay.
- Did not implement reward generation.
- Did not implement reward effects.
- Did not implement reward UI polish.
- Did not implement Dice Face Replacement.
- Did not implement Meta Progression, permanent unlocks, Iron Core, Boss drops, inventory, shops, or new run creation.
- Did not change Base Throw Damage value, Attack modifier, Lightning modifier, Mend heal amount, Guard reduction, enemy attack damage, DiceRoller result logic, or Face selection logic.
- Did not add new Faces beyond renaming starter Spark to Lightning.
- Did not add new Face effects during presentation polish.
- Did not make presentation code decide gameplay values.
- Did not change Base Throw Damage, Attack modifier, Guard reduction, Lightning damage, Mend heal amount, enemy damage, HP mutation order, FaceResolver gameplay logic, Reward Selection, Dice Face Replacement, inventory, meta progression, or enemy AI.
- Did not implement full Reward Selection gameplay, Dice Face Replacement, inventory, shop, branching map, new Face effects, boss mechanics, or multi-enemy gameplay.
- Did not implement inventory, shops, meta progression UI, permanent unlock economy, Dice Tier progression, or new run creation.
- Did not add enemy AI or boss mechanics.
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
- Guard reduction is consumed by BattleController when applying the following enemy attack intent.
- Mend recovery is consumed by BattleController after the Throw damage application.
- `BattleCombatState.HealPlayer(int healing)` owns player HP recovery and clamps healing at Player Max HP.
- `ThrowSequencePresenter` consumes selected Face, FaceEffectData, applied damage, and Base Throw Damage for presentation only.
- `ThrowSequencePresenter` displays Base Throw Damage before Dice Layer presentation.
- M8 remains separate from Reward Selection and Dice Face Replacement.
- M10 Reward Selection remains separate from M11 Dice Face Replacement.
- Long-term progression design defines reward categories, Face rarity, Face unlock progression, Dice Tier unlock progression, and meta unlock direction without implementing them.
- Future production UI should transition to scene/prefab hierarchy, anchored layout, layout groups, TextMeshPro, and data binding.
- `ThrowSequencePresenter` now uses already calculated pending enemy HP damage for the enemy-local damage number.
- Enemy-local damage popup remains presentation-only and does not mutate HP.
- Hit spark and enemy shake remain presentation-only feedback.
- Dice Deck remains Battle Information UI and reads runtime Dice state.
- Dice Deck scale change does not alter battle gameplay.
- Starter Dice Build generates runtime Dice through `BattleDiceState`.
- `DiceRoller` consumes `DiceModel.ActiveFaceSlotCount` so Locked Slots are not rolled.
- `CollapsibleDiceDeckPresenter` consumes `BattleDiceState.CurrentDice` and displays inactive slots as `Locked`.
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
- Starter Dice still contains Attack, Attack, Guard, Guard, Lightning, and Mend.
- Starter Dice Build runtime source is permanently unlocked Faces filtered by current Dice Tier.
- Starter Dice Build creates a Wood Dice with 4 active Face slots and 2 locked slots.
- Locked slots do not enter the roll pool.
- Dice Deck displays active Faces and locked slots from runtime Dice state.
- Unity batchmode import/compile validation completed successfully for M9-002.
- Starter Dice Base Throw Damage is 3.
- Attack Face currently adds 5 damage as a Face modifier.
- Starter Attack Throw total damage is 8 before enemy HP clamping.
- Guard currently reduces the next incoming enemy attack damage by 3 after dealing Base Throw Damage.
- Lightning currently adds a 3 damage Face modifier after Base Throw Damage.
- Mend currently heals up to 5 HP after Base Throw Damage.
- Unity batchmode compile validation completed successfully for M8-006.
- Face Effect detail duration is 1.25 seconds.
- Enemy damage number appears near the enemy body, floats upward, and fades out.
- Enemy damage number shows enemy HP damage only.
- Enemy attack damage popup appears near the Hunter.
- Mend healing popup appears near the Hunter.
- Unity batchmode compile validation completed successfully.
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

M8-002_GUARD_GAMEPLAY is complete.
