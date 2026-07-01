# TASK QUEUE

Selected Milestone: M6_LINEAR_STAGE_RUN

Milestone Status: READY

Source Milestone: `MILESTONE_PLAN.md`

Director Review:

- M2_DICE_CORE is approved and DONE.
- M3_DICE_PRESENTATION is approved and DONE.
- M4_SKILL_RESOLUTION is approved and DONE.
- M5_ENEMY_TURN_AND_BATTLE_LOOP is READY_FOR_DIRECTOR_REVIEW.
- M6_LINEAR_STAGE_RUN task structure is generated and READY.
- The current M3 sequence is the Project Dice Signature Battle Flow.

M4 Locked Principle:

- Face is not a simple skill button.
- The Face is the stopped Dice result.
- The revealed Face naturally causes the combat effect.
- Player understanding should be: "Attack Face appeared, therefore attack happened."
- M4 should center the Face result, not a generic RPG skill activation.

Presentation Reference:

- `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md`

M3 Locked Flow:

- Throw Button.
- Hero Throw.
- White projectile trail.
- Enemy hit flash.
- Dice Animation Layer appears.
- Dice rolls.
- Dice stops.
- Face reveal.
- Face effect.
- Damage number.
- Sequence ends.

M3 Timing Priority:

- Timing and rhythm come before animation quality.
- Keep the sequence short, responsive, and readable.
- First-pass target rhythm:
  - Throw input.
  - Hero Throw beat around 0.05 seconds.
  - Projectile beat around 0.08 seconds.
  - Enemy Flash beat around 0.05 seconds.
  - Dice appearance beat around 0.10 seconds.
  - Rolling beat around 0.45 seconds.
  - Reveal beat around 0.20 seconds.
  - Damage Number beat around 0.15 seconds.
  - Sequence ends.

M4 Scope Guardrails:

- Implement Face Skill Resolution only when a detailed M4 task is selected.
- Do not add enemy turns during M4.
- Do not add rewards, progression, or Dice face replacement during M4.
- Do not redesign Dice result selection.
- Preserve the M3 signature battle flow order.

Post-M4 Roadmap:

- M5: Enemy Turn.
- M6: Battle Complete.
- M7: Reward.
- M8: Dice Face Replacement.
- First complete Run.

## M6 Implementation Tasks

Status: GENERATED

M6 Goal:

Connect individual battles into the fixed five-stage MVP run.

Desired Run Shape:

- Stage 1 normal battle.
- Stage 2 normal battle.
- Stage 3 normal battle.
- Stage 4 elite battle.
- Stage 5 boss battle.
- Defeat ends the run.
- Boss victory completes the run.

M6 Task Rule:

- One gameplay concept per task.
- Keep every task independently verifiable.
- Preserve the existing player Throw, Face Resolution, Dice presentation, enemy turn, HP refresh, and return-to-player loop.
- Do not add rewards, Dice replacement, inventory, shops, branching map, permanent progression, new Face effects, enemy AI, boss mechanics, or multi-enemy logic.
- M6 may introduce battle/run completion state, but it must not implement reward flow.

## M6-001: Battle Outcome State

Status: DONE

Goal:

Add the minimal runtime state needed to represent battle outcome separately from turn ownership.

Requirements:

- Define battle outcome states such as `InProgress`, `Victory`, and `Defeat`.
- Keep outcome state separate from `BattleTurnState`.
- Do not advance stages in this task.
- Do not show victory/defeat presentation in this task.
- Do not add rewards, Dice replacement, progression UI, boss mechanics, or enemy AI.

Done Criteria:

- Battle code can explicitly know whether the current battle is still running, won, or lost.

Validation:

- Outcome state does not calculate damage.
- Outcome state does not trigger presentation.
- Outcome state does not advance stages.
- Existing battle loop still works while outcome is `InProgress`.

## M6-002: Linear Stage Runtime State

Status: DONE

Goal:

Add the runtime state for the fixed five-stage MVP run.

Requirements:

- Represent current stage number.
- Represent stage type:
  - Normal.
  - Elite.
  - Boss.
- Use the fixed order:
  - Stage 1 Normal.
  - Stage 2 Normal.
  - Stage 3 Normal.
  - Stage 4 Elite.
  - Stage 5 Boss.
- Do not create a branching map.
- Do not add rewards or Dice replacement.

Done Criteria:

- Runtime state can report the current stage and whether it is the final boss stage.

Validation:

- Stage order is deterministic.
- No branch selection exists.
- No reward flow exists.

## M6-003: Enemy Defeat Victory Resolution

Status: DONE

Goal:

Resolve enemy defeat into a battle victory outcome.

Requirements:

- Use existing enemy HP state.
- Detect victory after player damage application.
- Mark `BattleOutcomeState` as `Victory`.
- Use `BattleOutcomeState` as the source of truth after victory is set.
- Stop enemy response when enemy is defeated.
- Lock or prevent further Throw input once victory is reached.
- Do not advance to the next stage in this task.
- Do not add rewards or victory screen.

Done Criteria:

- Reducing enemy HP to zero marks the current battle as Victory without advancing stages.

Validation:

- Enemy does not take a turn after being defeated.
- Player input does not start another Throw after victory.
- No reward or stage transition is triggered yet.

## M6-003A: Enemy Group Victory Abstraction

Status: DONE

Goal:

Refactor victory resolution so future 1-3 enemy battles can use a group-level defeated query.

Requirements:

- Add a minimal `EnemyGroupState`.
- Track active enemy slots for the current battle.
- Adapt the existing single enemy HP state into `EnemyGroupState.AreAllEnemiesDefeated`.
- Resolve victory from `EnemyGroupState.AreAllEnemiesDefeated`.
- Keep `BattleOutcomeState` as the source of truth after victory is set.
- Do not implement targeting, multiple enemy HP bars, multi-enemy attacks, rewards, stage advance, run completion, victory presentation, or battle reset.

Done Criteria:

- Current single-enemy battle still reaches Victory through the enemy group query.

Validation:

- Victory uses `EnemyGroupState.AreAllEnemiesDefeated`.
- BattleOutcomeState still owns battle completion.
- No multi-enemy gameplay was added.

## M6-004: Player Defeat Resolution

Status: NEXT

Goal:

Resolve player HP reaching zero into a battle defeat outcome.

Requirements:

- Use existing player HP state.
- Detect defeat after enemy damage application.
- Lock or prevent further Throw input once defeat is reached.
- Do not add restart UI, run summary UI, reward UI, or progression.

Done Criteria:

- Player HP reaching zero marks the current battle as Defeat.

Validation:

- Player cannot continue throwing after defeat.
- Enemy does not repeatedly attack after defeat.
- No reward or stage transition is triggered after defeat.

## M6-005: Advance To Next Stage

Status: PENDING

Goal:

Advance from a won non-boss battle to the next fixed stage.

Requirements:

- Consume a Victory outcome.
- Advance only if the current stage is not the final boss stage.
- Prepare the next battle state after advancing.
- Do not add rewards between stages in M6.
- Do not add stage selection UI.

Done Criteria:

- Winning Stage 1, 2, 3, or 4 advances to the next stage.

Validation:

- Stage number increments exactly once per victory.
- Defeat does not advance the stage.
- Boss victory does not advance to a nonexistent stage.

## M6-006: Complete Linear Run

Status: PENDING

Goal:

Resolve victory on the final stage as completed MVP run state.

Requirements:

- Detect victory on the final boss stage.
- Mark the run as complete.
- Prevent further battle input after run completion.
- Do not add reward, post-run economy, meta progression, or new run setup.

Done Criteria:

- Winning the final fixed stage completes the linear run.

Validation:

- Run complete occurs only after boss victory.
- Non-boss victory does not complete the run.
- Defeat does not complete the run.

## M6-007: Prepare Next Battle

Status: PENDING

Goal:

Prepare the next battle runtime state when advancing to the next stage.

Requirements:

- Reset enemy HP for the new stage.
- Reset battle outcome to `InProgress`.
- Restore turn ownership to `PlayerTurn`.
- Preserve current runtime Dice build.
- Preserve player HP unless Director later specifies stage healing.
- Do not add rewards or Dice replacement.

Done Criteria:

- After a non-boss victory advances the stage, the next battle can start cleanly.

Validation:

- Current Dice build persists across stages.
- Enemy HP is ready for the new battle.
- Player can Throw in the next stage.
- No reward flow is inserted.

## M6-008: Validate Linear Stage Run

Status: PENDING

Goal:

Validate the fixed five-stage run from Stage 1 through run completion or defeat.

Requirements:

- Confirm existing player Throw flow still works.
- Confirm existing enemy turn flow still works.
- Confirm victory stops the enemy response.
- Confirm defeat stops player input.
- Confirm non-boss victory advances stages.
- Confirm boss victory completes the run.
- Confirm runtime Dice build persists across stages.
- Confirm no rewards, Dice replacement, branching map, inventory, shops, permanent progression, new Face effects, enemy AI, boss mechanics, or multi-enemy logic were added.

Done Criteria:

- M6 can be submitted for Director review as the first fixed linear run structure.

Validation:

- No compile errors.
- No `NullReferenceException`.
- Full fixed run path is deterministic.
- Defeat path is deterministic.

## Post-M4 Dice Presentation Scale Fix

Status: DONE

Goal:

Improve Dice Animation Layer readability after APK playtest without adding gameplay.

Completed:

- Increased the Dice placeholder to a readable mobile landscape scale.
- Added a simple runtime backing frame so the Dice result moment feels intentional.
- Enlarged Face Reveal, Face Effect, and Damage Number presentation around the Dice result.
- Kept selected-face validation text and restyled it as `Face S#: FaceName`.
- Replaced generic Hero feedback with provided Hero idle and throw frame animation.
- Spawned the projectile during the forward throw frames and returned the Hero to idle.
- Preserved the current battle flow: Throw, Hero throw animation, projectile, enemy flash, Dice layer, rolling, Face Reveal, Face Effect, Damage Number, damage apply, HP refresh.

Not Added:

- No enemy turn.
- No rewards or progression.
- No new Dice faces.
- No new Face effects.
- No final pixel art assets.
- No multi-enemy logic.

## Post-M4 Dice Presentation Polish

Status: DONE

Goal:

Improve battle presentation readability before starting M5 without adding gameplay.

Completed:

- Increased the Dice result placeholder to 288x288.
- Moved the Dice result slightly below center so it reads like a landed result after enemy impact.
- Kept the primary reveal text as only the selected Face name, such as `Spark`.
- Kept Face Effect as smaller secondary text, such as `Damage` or `No Effect`.
- Kept selected-slot validation text as small corner `RESULT S#: FaceName` debug text.
- Preserved Hero idle and throw animation assets from `Assets/Art`.
- Preserved projectile timing after the fifth throw frame.
- Preserved current battle flow: Throw, Hero throw animation, projectile, enemy flash, Dice layer, rolling, Face Reveal, Face Effect, Damage Number, damage apply, HP refresh.

Not Added:

- No enemy turn.
- No rewards or progression.
- No new Dice faces.
- No Guard, Spark, or Mend real effects.
- No multi-enemy logic.
- No inventory or stage system.

## M5 Implementation Tasks

Status: IN_PROGRESS

M5 Goal:

Complete the first full battle turn cycle.

Desired Loop:

- Player Throw.
- Face Resolution.
- Battle Presentation.
- Damage.
- Enemy Turn.
- Player HP Update.
- Return to Idle.
- Player can Throw again.

M5 Task Rule:

- One gameplay concept per task.
- Keep every task independently verifiable.
- Preserve the existing player Throw, Dice result, Face resolution, Dice presentation, damage apply, and HP refresh flow.
- Enemy behavior should be fixed and deterministic for MVP.
- Do not add enemy AI complexity.
- Do not add battle completion systems during M5.
- Do not add rewards, stage progression, Dice replacement, inventory, shops, permanent progression, new Face types, boss systems, or multi-enemy logic.

## M5-001: Enemy Runtime Turn State

Status: DONE - APPROVED

Goal:

Add the minimum runtime state needed for battle-level turn ownership after player resolution.

Requirements:

- Define battle-level turn ownership, not enemy-local state only.
- Include clear ownership states:
  - `PlayerTurn`
  - `Transition`
  - `EnemyTurn`
- Represent whether an enemy turn should occur after the player Throw resolves.
- Keep enemy turn state separate from Dice result selection and Face resolution.
- Do not apply player HP damage in this task.
- Do not trigger enemy presentation in this task.
- Do not add enemy AI, target selection, random actions, rewards, progression, or battle completion.
- Keep existing player Throw, Dice presentation, Face Effect, damage apply, and HP refresh behavior working.

Done Criteria:

- Battle flow can determine that player input belongs to `PlayerTurn`.
- Battle flow can enter `Transition` during accepted player action.
- Battle flow can mark `EnemyTurn`/pending enemy response as a future handoff point without resolving enemy damage yet.

Validation:

- Enemy turn state does not call `DiceRoller`.
- Enemy turn state does not resolve Face effects.
- Enemy turn state does not mutate player HP.
- Enemy turn state does not trigger presentation.
- Existing player Throw flow remains unchanged.

Completed:

- Added `BattleTurnOwner` with `PlayerTurn`, `Transition`, and `EnemyTurn`.
- Added `BattleTurnState` as the battle-level turn ownership runtime holder.
- `BattleTurnState` starts in `PlayerTurn`.
- `BattleController` checks `BattleTurnState.CanAcceptPlayerAction` before accepting Throw.
- Accepted Throw moves turn ownership into `Transition`.
- After the current player resolution finishes, the system can mark `EnemyTurn` as pending and currently returns to `PlayerTurn` immediately to preserve existing Throw behavior until later M5 tasks implement enemy resolution/presentation/damage.
- No enemy AI, enemy damage, player damage, enemy presentation, rewards, progression, battle end, new Dice faces, or Guard/Spark/Mend effects were added.

## M5-002: Enemy Attack Resolution

Status: DONE - APPROVED

Goal:

Resolve the enemy's fixed MVP attack into pending player damage.

Requirements:

- Consume enemy runtime turn state from M5-001.
- Produce a deterministic pending enemy damage value.
- Use a fixed damage amount for MVP.
- Do not apply player HP damage in this task.
- Do not own presentation.
- Do not add random damage ranges, enemy AI, action lists, buffs, debuffs, or multiple enemy actions.

Done Criteria:

- Enemy turn can produce one deterministic pending damage result without mutating HP.

Validation:

- Pending enemy damage is deterministic.
- No player HP mutation occurs during resolution.
- No presentation code decides the damage amount.
- No new Face effects or Dice logic are added.

Completed:

- Added `EnemyAttackIntent` as the deterministic pending enemy attack data.
- Added `EnemyAttackIntentType` with `None` and `Damage`.
- Added `EnemyAttackResolver.Resolve(BattleTurnState battleTurnState)`.
- Enemy attack resolution returns fixed MVP damage 5 only while battle ownership is `EnemyTurn` and enemy turn is pending.
- `BattleController` stores the resolved pending enemy attack intent after player resolution reaches the enemy handoff point.
- Existing M5-001 temporary return to `PlayerTurn` is preserved until later tasks implement enemy presentation, player damage, and turn transition.
- No enemy animation, player HP reduction, enemy presentation, battle end, rewards, progression, enemy AI, random action logic, or new Face effects were added.

## M5-003: Enemy Attack Presentation

Status: DONE

Goal:

Show a short, readable enemy attack beat after player damage resolution.

Requirements:

- Presentation occurs only after the player's Throw, Face Reveal, Face Effect, Damage Number, damage apply, and HP refresh flow.
- Use simple SNES-style feedback only.
- Keep the beat short and readable.
- Presentation may use a simple enemy flash, movement nudge, white trail, or Hero hit flash if needed.
- Do not apply player HP damage in this task.
- Do not decide gameplay damage in presentation.
- Do not add cinematic camera, particle-heavy effects, knockback systems, or long animations.

Done Criteria:

- Enemy response is visually understandable before player HP changes.

Validation:

- Enemy attack presentation happens after player resolution.
- Presentation does not mutate HP.
- Presentation does not call enemy attack resolution logic.
- Presentation does not change Dice or Face state.

Completed:

- Added `EnemyAttackPresenter` as a presentation-only consumer of `EnemyAttackIntent`.
- Enemy attack presentation runs after player Throw, Face Reveal, Face Effect, Damage Number, enemy HP damage application, and HP refresh.
- Presentation uses short SNES-style feedback:
  - enemy windup flash
  - simple white strike trail from enemy to Hero
  - brief Hero hit flash
- `BattleController` keeps battle ownership in `EnemyTurn` while enemy attack presentation plays, then returns to `PlayerTurn`.
- `EnemyAttackPresenter` does not apply player HP damage.
- `EnemyAttackPresenter` does not decide damage.
- `EnemyAttackPresenter` does not call `EnemyAttackResolver`, `BattleCombatState`, or `BattleHudPresenter`.
- No enemy AI, player HP reduction, battle end, rewards, progression, random enemy behavior, new Face effects, or complex VFX were added.

## M5-004: Player Damage Application

Status: DONE - APPROVED

Goal:

Apply resolved enemy attack damage to player HP after enemy attack presentation.

Requirements:

- Consume the pending enemy damage value from M5-002.
- Apply damage only after M5-003 enemy attack presentation completes.
- Keep `BattleCombatState` as the player HP mutation owner.
- Refresh HP presentation after damage application.
- Do not add defeat flow, run end, rewards, progression, or battle completion.

Done Criteria:

- Player HP decreases from the fixed enemy attack after the enemy presentation beat.

Validation:

- Player HP does not change before enemy attack presentation.
- Player HP clamps according to existing combat state rules.
- `BattleHudPresenter.Refresh()` updates visible HP after damage.
- Existing enemy HP damage from player Attack still works.

Completed:

- Added `BattleCombatState.ApplyDamageToPlayer(int damage)` as the only player HP mutation path for enemy attack damage.
- `BattleController` now applies resolved `EnemyAttackIntent` damage only after `EnemyAttackPresenter.Play(...)` completes.
- `BattleHudPresenter.Refresh()` runs after player damage application.
- Pending enemy attack intent is consumed and cleared after player damage application.
- Existing player Attack damage against enemy HP still works through `BattleCombatState.ApplyDamageToEnemy`.
- No defeat flow, battle end, rewards, progression, enemy AI, random behavior, or new Face effects were added.

## M5-005: Turn Transition

Status: DONE - APPROVED

Goal:

Return the battle to player-ready idle state after enemy turn resolution.

Requirements:

- Keep player input locked through player Throw, player presentation, enemy presentation, and player HP update.
- Unlock player input only after the enemy turn completes.
- Return Hero presentation to idle.
- Allow the player to Throw again after the loop completes.
- Skip enemy action if the enemy is already defeated by the player action.
- Do not implement victory UI, defeat UI, rewards, progression, or stage transitions.

Done Criteria:

- A player can perform Throw, receive enemy response, and then Throw again.

Validation:

- Input remains locked during the full turn sequence.
- Input unlocks after the enemy turn completes.
- Enemy does not act after being defeated.
- Repeated player turns work without duplicate enemy responses.

Completed:

- Added an explicit `BattleTurnState.BeginTransition()` state handoff.
- Added explicit `BattleTurnState.BeginEnemyTurn()` ownership entry point.
- Existing `PrepareEnemyTurn()` now routes through `BeginEnemyTurn()` for compatibility.
- Battle flow now visibly follows `PlayerTurn -> Transition -> EnemyTurn -> Transition -> PlayerTurn`.
- `BattleController` keeps input locked through player Throw, player presentation, enemy attack presentation, player damage application, HP refresh, and turn transition.
- Player input unlocks only after ownership returns to `PlayerTurn`.
- Enemy action is skipped if the enemy is defeated by the player action.
- No battle end, defeat handling, rewards, progression, enemy AI improvements, or Dice Deck implementation was added.

## M5-006: Collapsible Dice Deck

Status: DONE - APPROVED

Goal:

Add a collapsed-by-default Dice Deck battle information UI that lets the player inspect the current runtime six-face Dice build during battle.

Purpose:

- This is the Dice Deck.
- This is a Battle Information UI.
- This is the Current Runtime Dice Viewer.
- This is not battle presentation.
- This is not a reward system.
- This is not a Dice replacement system.
- This is not inventory, Face editing, progression, or stage flow.

Requirements:

- Dice Deck is collapsed by default.
- Dice Deck expands when tapped.
- Dice Deck collapses when tapped again.
- Dice Deck displays the current six Dice face slots.
- Dice Deck reads from current runtime Dice state.
- Do not hardcode six static entries.
- Do not show Starter Dice as a static UI source.
- Future Dice replacement should automatically update Dice Deck because it reads from runtime Dice state.
- Dice Deck must not permanently occupy battle space.
- Dice Deck must not interfere with Hero, Enemy, Dice Animation Layer, or Throw button.
- Each slot should eventually display:
  - Face icon.
  - Face name.
  - Short effect description if available.
- Probability display is a future M8-or-later addition and is not part of M5.

Done Criteria:

- Player can inspect the current runtime Dice build during battle without disrupting battle presentation or input layout.

Validation:

- Default state is collapsed.
- Tap toggles expanded/collapsed state.
- Expanded state shows six runtime Dice slots.
- Dice Deck reads from `BattleDiceState.CurrentDice`.
- No reward, Dice replacement, inventory, Face editing, progression, or stage system is added.
- No probability display is added during M5.

Completed:

- Added `CollapsibleDiceDeckPresenter` as the Dice Deck battle information UI.
- Dice Deck is collapsed by default.
- Tapping `Dice Deck` expands the viewer; tapping `Close Deck` collapses it again.
- Expanded Dice Deck displays six runtime Dice slots.
- Dice Deck reads from `BattleDiceState.CurrentDice` and refreshes from runtime data while expanded.
- Dice Deck is not hardcoded to Starter Dice.
- Dice Deck does not use reward, replacement, inventory, progression, or stage systems.
- Probability and duplicate-face aggregation were not implemented in M5.

Bugfix Completed:

- Fixed Dice Deck tap/click handling by ensuring a runtime `EventSystem` exists for UI Button events.
- Preserved collapsed-by-default state, runtime Dice source, expanded six-slot view, and Close Deck collapse behavior.
- No reward, Dice replacement, inventory, progression, new Face effects, or enemy turn changes were added.

## M5-007: Validate M5 Battle Loop

Status: DONE

Goal:

Validate the first complete battle turn loop before moving to battle completion work.

Requirements:

- Confirm player Throw still works.
- Confirm Face resolution still drives player effect meaning.
- Confirm player presentation order is preserved.
- Confirm enemy attack resolution is deterministic.
- Confirm enemy attack presentation happens before player HP damage.
- Confirm player HP updates after enemy attack presentation.
- Confirm input returns to player after the enemy turn.
- Confirm repeated loops work.
- Confirm no rewards, stage progression, Dice replacement, inventory, shops, permanent progression, new Face types, boss systems, or multi-enemy logic were added.

Done Criteria:

- M5 can be submitted for Director review as the first complete battle turn cycle.

Validation:

- No compile errors.
- No `NullReferenceException`.
- Player can Throw again after enemy turn.
- Damage and HP still work for both sides.
- Enemy turn does not occur after enemy defeat.

Completed:

- Confirmed player Throw still resolves through Dice result selection and Face resolution.
- Confirmed player battle presentation still runs before enemy HP damage application.
- Confirmed enemy attack resolution remains deterministic fixed 5 Damage.
- Confirmed enemy attack presentation runs before player HP damage application.
- Confirmed player HP updates after enemy attack presentation.
- Confirmed turn ownership returns to `PlayerTurn` after enemy presentation, player damage, HP refresh, and transition.
- Confirmed Dice Deck remains separate Battle Information UI and reads from `BattleDiceState.CurrentDice`.
- Confirmed no rewards, stage progression, Dice replacement, inventory, shops, permanent progression, new Face types, boss systems, or multi-enemy logic were added.
- Unity batchmode import/compile validation completed successfully with exit code 0.

## M4 Implementation Tasks

Status: GENERATED

M4 Task Rule:

- One gameplay concept per task.
- Keep each task independently verifiable.
- Do not add enemy turns, rewards, progression, Dice face replacement, or new Dice result logic.
- Do not invent undefined Face effects for `Guard`, `Spark`, or `Mend`.
- Preserve the current M3 Signature Battle Flow.
- Maintain architecture order: `DiceFace`, FaceResolver, Gameplay Effect, Presentation, `BattleCombatState`, `BattleHudPresenter`.
- Presentation code must not decide gameplay.
- Gameplay code must not own presentation.

## M4-001: Face Effect Data Model

Status: DONE

Goal:

Create the minimal runtime data concept that represents a deterministic Face effect result.

Requirements:

- Use the already selected `DiceFace` from M2.
- Do not call `DiceRoller`.
- Do not select another result.
- Do not apply damage inside the model itself.
- Do not modify `BattleDiceState` ownership.
- Keep the result deterministic and data-light.
- Effect data should be able to express at least:
  - source Face id or display name.
  - effect type or equivalent classification.
  - resolved damage amount.
  - whether the Face had an implemented MVP effect.

Done Criteria:

- Face effect data can describe what should happen without mutating HP.

Validation:

- The data model does not call Dice selection.
- The data model does not directly update `BattleCombatState`.
- The data model does not reference presentation objects.
- No enemy turn, rewards, progression, Dice face replacement, or new Dice result logic is added.

Completed:

- Added `FaceEffectType` with `None` and `Damage`.
- Added `FaceEffectData` to describe source Face id/name, effect type, damage amount, and implemented state.
- Added factory helpers for no-effect and damage effect data.
- The model stores data only and does not mutate combat state or presentation.

## M4-002: Face Resolver

Status: DONE

Goal:

Determine which gameplay effect should execute from the selected `DiceFace`.

Requirements:

- Consume the already selected `DiceFace` from M2.
- Do not call `DiceRoller`.
- Do not select another result.
- Decide what should happen, but do not apply HP changes.
- Do not own presentation.
- Do not modify `BattleDiceState` ownership.
- Keep the result deterministic and data-light.
- `Attack` may resolve to a damage effect using its existing `FixedThrowDamageValue`.
- `Guard`, `Spark`, and `Mend` must resolve to explicit undefined/no-effect data until Director or GDD source text defines them.

Done Criteria:

- A selected `DiceFace` can be resolved into Face effect data without mutating HP or running presentation.

Validation:

- Resolver consumes the existing selected Face only.
- Resolver does not roll again.
- Resolver does not directly update `BattleCombatState`.
- Resolver does not call presentation code.
- No enemy turn, rewards, progression, Dice face replacement, or new Dice result logic is added.

Completed:

- Added `FaceResolver.Resolve(DiceFace selectedFace)`.
- `starter_attack` resolves to `FaceEffectData.Damage` using the selected Face's existing `FixedThrowDamageValue`.
- Null, `Guard`, `Spark`, `Mend`, and any undefined Face resolve to explicit `FaceEffectData.None`.
- Resolver does not mutate HP, trigger presentation, access UI, or call Dice result selection.

## M4-003: Attack Face

Status: DONE

Goal:

Implement `Attack` Face as the first MVP gameplay effect using the Face Resolver output.

Requirements:

- `Attack` Face resolves to deterministic damage using its current `FixedThrowDamageValue`.
- Preserve the current 5 damage value for starter `Attack`.
- Applying damage belongs to this current Attack gameplay task because Attack is the only implemented gameplay effect.
- Enemy HP damage must come from the resolved Attack Face effect, not from a generic Throw damage path.
- The player-facing cause must remain: Attack Face appeared, therefore attack happened.
- Do not add random damage ranges.
- Do not add critical hits, modifiers, combo logic, or target selection.
- Do not implement Guard, Spark, or Mend effects in this task.
- Damage is still applied only after Face Reveal and Face effect presentation timing.
- `BattleCombatState` receives only the final resolved damage value.
- `BattleCombatState` must not inspect Dice or Face data.
- HP refresh still occurs after damage application.
- Preserve victory input lock behavior.

Done Criteria:

- When `Attack` is the selected Face, the resolved Attack effect applies 5 damage through `BattleCombatState` after presentation timing.

Validation:

- Starter `Attack` Face still resolves to 5 damage.
- Duplicate `Attack` faces remain separate Dice slots and do not become a unique-face probability table.
- `BattleCombatState.ApplyDamageToEnemy` is still the only HP mutation path for enemy damage.
- Damage application still occurs after presentation returns.
- HP refresh still occurs after damage application.
- No other starter Face gains new gameplay behavior in this task.
- No enemy turn, rewards, progression, Dice face replacement, or new Dice result logic is added.

Completed:

- `BattleController` now resolves the selected `DiceFace` through `FaceResolver`.
- Pending damage presentation now reads from resolved `FaceEffectData`.
- Enemy HP damage now applies from `FaceEffectData.DamageAmount` only when the resolved effect type is `Damage`.
- The previous generic selected-Face throw damage helper was removed.
- Undefined/no-effect Faces produce zero pending damage and do not apply enemy damage.

## M4-004: Explicit Undefined Face Handling

Status: DONE

Goal:

Handle starter Faces without Director-defined MVP effects explicitly and safely.

Requirements:

- `Guard`, `Spark`, and `Mend` must not receive invented effects.
- Undefined starter Faces should resolve as implemented=false or equivalent no-effect result.
- Undefined starter Faces should not damage the enemy.
- Undefined starter Faces should not heal, shield, spark, stun, draw, reroll, or alter future turns.
- The result should remain visible or understandable enough for validation.

Done Criteria:

- Non-`Attack` starter Faces resolve without causing hidden gameplay changes.

Validation:

- `Guard`, `Spark`, and `Mend` do not change enemy HP.
- `Guard`, `Spark`, and `Mend` do not change player HP.
- No shield, enemy turn, reward, progression, Dice face replacement, or future system is added.
- Documentation or validation output makes clear that their effects are intentionally pending Director/GDD definition.

Completed:

- Undefined/no-effect Face results now produce explicit battle log feedback.
- Null or unknown Face results are reported as having no effect yet.
- Guard, Spark, Mend, and other undefined Faces still resolve through `FaceResolver` as `FaceEffectData.None`.
- Undefined/no-effect Faces still do not apply enemy damage or player healing.
- No Guard, Spark, Mend, shield, stun, heal, reroll, enemy turn, reward, progression, or Dice face replacement behavior was added.

## M4-005: Face Effect Presentation Beat

Status: DONE

Goal:

Add a short presentation beat that communicates the resolved Face effect after Face Reveal and before Damage Number.

Requirements:

- Keep the Dice as part of the battle animation sequence.
- The effect beat must follow Face Reveal.
- Damage Number must still appear after the Face effect beat.
- Use short, readable, SNES-style presentation.
- Avoid camera movement, complex particles, long anticipation, or modern VFX.
- Do not turn the Face effect into a menu, button, or UI overlay.
- Undefined Faces may show a minimal pending/no-effect validation cue only.

Done Criteria:

- The player can understand that the revealed Face caused the following effect.

Validation:

- Sequence remains: Throw, Hero feedback, projectile, enemy flash, Dice layer, rolling, face reveal, Face effect, damage number, damage apply, HP refresh.
- The Face effect beat does not select a new result.
- The Face effect beat does not apply damage directly.
- No enemy turn, rewards, progression, Dice face replacement, or new Dice result logic is added.

Completed:

- `ThrowSequencePresenter.Play` now receives resolved `FaceEffectData`.
- Added a short Face Effect Text presentation beat after Face Reveal and before Damage Number.
- Damage effects show compact `Damage` feedback.
- Undefined/no-effect results show compact `No Effect` feedback.
- Face effect presentation does not apply damage, mutate HP, select dice, or execute gameplay.

## M4-006: Validate M4 Face Skill Resolution

Status: DONE

Goal:

Validate the complete M4 Face Skill Resolution flow before moving to enemy turn work.

Requirements:

- Confirm selected Face is resolved exactly once.
- Confirm `FaceResolver` decides the gameplay effect without presentation or HP mutation.
- Confirm `Attack` Face causes deterministic damage through the resolved Face effect.
- Confirm undefined starter Faces do not gain invented gameplay effects.
- Confirm HP changes only after the Face effect and damage presentation timing.
- Confirm `BattleCombatState` still owns only HP and damage application.
- Confirm `BattleDiceState` still owns Dice runtime state only.
- Confirm M3 Signature Battle Flow remains intact.
- Confirm no enemy turn, rewards, progression, Dice face replacement, or future systems were added.

Done Criteria:

- M4_SKILL_RESOLUTION is DONE and approved by Director review.

Validation:

- Static inspection confirms one selected Face resolution per accepted Throw.
- Static inspection confirms no reroll or second random result occurs.
- Static inspection confirms presentation code does not decide gameplay.
- Static inspection confirms damage source is the resolved Face effect.
- Unity import/compile validation passes.
- Director Play Mode review remains required for live causality and readability.

Completed:

- Confirmed `FaceResolver.Resolve` is called once per accepted Throw after Dice result selection.
- Confirmed no second Dice roll or random result occurs during Face resolution.
- Confirmed `Attack` resolves to a Damage effect through `FaceResolver`.
- Confirmed Guard, Spark, Mend, null, and unknown Faces resolve to `FaceEffectData.None`.
- Confirmed Face Reveal occurs before Face Effect presentation.
- Confirmed Face Effect presentation occurs before Damage Number presentation.
- Confirmed damage application occurs only after `ThrowSequencePresenter.Play` returns.
- Confirmed `BattleCombatState.ApplyDamageToEnemy` is called only for resolved `FaceEffectType.Damage`.
- Confirmed `ThrowSequencePresenter` does not decide gameplay or mutate HP.
- Unity batchmode import/compile validation completed successfully with exit code 0.
- M4_SKILL_RESOLUTION is DONE and approved by Director review.

## M3 Implementation Tasks

## M3-001: Dice Animation Layer

Status: DONE

Goal:

Prepare the existing hidden `DiceAnimationLayer` as the temporary battle presentation layer that appears only after enemy impact.

Requirements:

- Keep the dice as battle animation, not UI or menu overlay.
- Use the existing Battle scene center reservation and `DiceAnimationLayer`.
- Keep layer hidden outside the throw sequence.
- Appear only after the current Hero throw, projectile trail, and enemy hit flash.
- Preserve selected Dice result logic from M2.
- Do not implement rolling animation, face reveal, face effects, damage number presentation, enemy turn, rewards, progression, or layout redesign.

Done Criteria:

- `DiceAnimationLayer` can be shown and hidden during the Throw sequence at the correct point after enemy impact.

Validation:

- `ThrowSequencePresenter` references the existing scene `DiceAnimationLayer`.
- `DiceAnimationLayer` remains hidden by default.
- `DiceAnimationLayer` is shown only after Hero feedback, projectile trail, and enemy hit flash.
- `DiceAnimationLayer` hides again before damage is applied.
- Rolling animation, face reveal, face effects, damage number presentation, enemy turn, rewards, progression, and layout redesign were not implemented.

## M3-002: Rolling Presentation

Status: DONE

Goal:

Add a short, readable, SNES-style dice rolling presentation after the Dice Animation Layer appears.

Requirements:

- Keep animation short and responsive.
- Use simple 4 to 6 frame style or equivalent placeholder presentation.
- No 3D rotation.
- No complex physics.
- No cinematic camera movement.
- Do not implement final face reveal, face effects, damage number presentation, enemy turn, rewards, or progression.

Done Criteria:

- Each accepted Throw shows a brief dice roll before stopping.

Validation:

- `DiceAnimationLayer` remains visible through the rolling presentation.
- A runtime `Rolling Dice Placeholder` is created under `DiceAnimationLayer`.
- Rolling lasts 0.45 seconds with simple frame-like position, color, and 90-degree orientation changes.
- No visual face reveal was implemented.
- No damage number presentation was implemented.
- No face skill activation, enemy turn, rewards, progression, or Dice result logic redesign was implemented.

## M3-003: Face Reveal

Status: DONE

Goal:

Reveal the selected Dice face clearly after the rolling presentation stops.

Requirements:

- Revealed face must match the selected `BattleDiceState` result.
- Top face must be immediately readable.
- Result presentation must be short.
- Reveal must consume the already selected Dice result only.
- Do not call `DiceRoller` from the presentation layer.
- Do not generate another random result.
- Do not modify `BattleDiceState`.
- Do not resolve face skills yet.
- Do not implement rewards, progression, or enemy turn.

Done Criteria:

- The dice reveal visibly matches the selected Dice face result.

Validation:

- `BattleController` selects the Dice result once before the presentation reveal.
- `ThrowSequencePresenter.Play` receives the selected `DiceFace` and displays that face name after rolling.
- `ThrowSequencePresenter` does not call `DiceRoller`.
- No additional random result is generated for presentation.
- No `BattleDiceState` structure or ownership change was made.
- No damage number presentation, face skill activation, enemy turn, rewards, progression, or Dice result logic redesign was implemented.

## M3-004: Damage Presentation

Status: DONE

Goal:

Show damage number presentation only after the Dice face reveal.

Requirements:

- Damage number must not appear before the dice stops and reveals.
- Preserve existing deterministic damage source from the selected `DiceFace`.
- Keep damage presentation short, readable, and classic SNES-style.
- Do not implement skill effects beyond the existing damage value.
- Do not implement enemy turn, rewards, or progression.

Done Criteria:

- Damage is visually presented after face reveal and before the sequence ends.

Validation:

- `ThrowSequencePresenter` shows damage number presentation after `PlayFaceReveal`.
- Damage number lasts 0.15 seconds.
- `BattleController` applies damage and refreshes HP only after the presentation sequence returns.
- Damage number uses the selected Dice face's deterministic damage value, clamped to current enemy HP for display.
- No face skill activation, enemy turn, rewards, progression, new Dice result logic, or multi-enemy logic was implemented.

## M3-005: Validate M3 Dice Presentation

Status: DONE

Goal:

Validate the complete M3 battle presentation sequence before moving to skill resolution.

Requirements:

- Confirm Throw input locks during the sequence.
- Confirm dice appears only after enemy impact.
- Confirm rolling presentation occurs.
- Confirm face reveal matches the selected Dice result.
- Confirm damage number appears only after face reveal.
- Confirm sequence remains short, readable, and SNES-style.
- Evaluate feel, not only functional correctness:
  - Face reveal is readable.
  - Damage number appears naturally after reveal.
  - HP decreases at a satisfying moment.
  - Full sequence remains responsive rather than slow.
- Confirm no enemy turn, rewards, progression, or future systems were added.

Done Criteria:

- M3_DICE_PRESENTATION is ready for Director review.

Validation:

- Static inspection confirmed Throw input remains locked during the presentation coroutine.
- Static inspection confirmed `DiceAnimationLayer` appears after enemy impact.
- Static inspection confirmed rolling presentation occurs before face reveal.
- Static inspection confirmed face reveal consumes the already selected `DiceFace`.
- Static inspection confirmed damage number appears only after face reveal.
- Static inspection confirmed damage is applied and HP refreshes only after the presentation sequence returns.
- Unity batchmode import/compile validation completed successfully with exit code 0.
- No enemy turn, rewards, progression, face skill activation, new Dice result logic, or future systems were added.
- Director Play Mode review remains the final authority for live feel, timing, readability, and responsiveness.

## Completed Documentation Sync Task

## TASK_DOCS_SYNC_BEFORE_M3

Status: DONE

Goal:

Synchronize project documentation before starting M3 without implementing gameplay, modifying scene layout, or modifying combat code.

Completed:

- Updated `MILESTONE_PLAN.md` so M0 and M1 were DONE, M2_DICE_CORE was ready for Director review, and M3_DICE_PRESENTATION was pending before Director final approval.
- Recorded that `Docs/PROJECT_GDD_v1.0.md` is not currently present and will be provided by the Director later.
- Confirmed `Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present and locked.
- Updated `CURRENT_STATE.md` to state that M2 Dice Core implementation was complete and ready for Director approval before final review.
- Recorded the then-current GDD source text gap before Director final approval allowed M3 to proceed from review feedback and the locked Battle Presentation Guide.
- Updated `CHANGELOG.md`, `DONE_REPORT.md`, and `SELF_REVIEW_REPORT.md`.

Validation:

- No gameplay files changed.
- No scene files changed.
- No GDD redesign occurred.
- MILESTONE_PLAN.md matches CURRENT_STATE.md for M0, M1, M2, and M3 state.
- Missing GDD source text is clearly reported.

M3 implementation tasks were not created.

## Completed M2 Status

Selected Milestone: M2_DICE_CORE

Milestone Status: DONE

GDD References:

- Section 10: Dice System
- Section 11: Dice Structure
- Section 13: Dice Grades
- Section 14: Dice Grade Progression
- Section 15: Dice Faces
- Section 17: Throw Damage
- Section 19: Dice Presentation
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
- Do not implement final Dice battle presentation.
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

Expose the latest selected Dice face in a minimal, non-permanent validation-friendly way without adding the final Dice battle presentation.

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
- Do not create the final Dice battle presentation.
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

- Developers can verify Dice result selection without implementing the final Dice battle presentation milestone.

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

Validate the completed M2 Dice Core end-to-end before moving into the Dice presentation milestone.

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
- Confirm no final Dice presentation, enemy turn, reward, progression, inventory, or item behavior has been introduced.
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

- M2_DICE_CORE is approved by Director final review and can hand off to M3_DICE_PRESENTATION.
