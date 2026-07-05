# TASK QUEUE

Selected Milestone: M11_DICE_FACE_REPLACEMENT

Milestone Status: IN_PROGRESS

Source Milestone: `MILESTONE_PLAN.md`

Director Review:

- M2_DICE_CORE is approved and DONE.
- M3_DICE_PRESENTATION is approved and DONE.
- M4_SKILL_RESOLUTION is approved and DONE.
- M5_ENEMY_TURN_AND_BATTLE_LOOP is DONE.
- M6_LINEAR_STAGE_RUN is DONE.
- M7_RUN_FLOW_PRESENTATION implementation is complete and ready for Director review.
- M8_STARTER_FACE_GAMEPLAY is approved and DONE.
- M9_STARTER_DICE_BUILD is approved and DONE.
- M10_REWARD_SELECTION is DONE.
- M11_DICE_FACE_REPLACEMENT is IN_PROGRESS.
- The current M3 sequence is the Project Dice Signature Battle Flow.
- `Docs/Design/PROJECT_CORE_PHILOSOPHY.md` locks Dice progression and Dice combat philosophy.
- `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md` locks reward categories, Face rarity, Face unlock progression, Dice Tier unlock progression, and meta unlock direction before M10 implementation.

Director Technical Lock:

- `TASK_UI_ARCHITECTURE_REFACTOR` is DONE.
- Core battle UI presenters now prefer scene/prefab-style serialized UI references and named anchored scene objects before using runtime fallback creation.
- Starter Dice Build, Dice Deck, Run Flow, and combat feedback presenters now expose bindable UI references for production scene/prefab hierarchy.
- Dice Deck uses an anchored horizontal layout for expanded slot presentation.
- Damage Popup and Heal Popup now use a dedicated Combat Feedback Layer under the battle field instead of the Dice Animation Layer.
- Damage and heal numbers remain target-local combat feedback and must never be shown on the Dice Layer.
- M10 Reward Selection is DONE and M11 Dice Face Replacement is now the selected implementation milestone.

Recent Presentation Polish:

- `TASK_SPLIT_BASE_THROW_DAMAGE_AND_FACE_EFFECT_APPLICATION` is DONE.
- Base Throw Damage and Face Effect application are now separate battle sequencing steps.
- `BattleCombatState.ApplyDamageToEnemy(baseDamage)` is called immediately after enemy impact and before Dice Animation Layer appears.
- Enemy HP refreshes immediately after Base Throw Damage is applied.
- Base damage popup appears near the enemy before Dice rolling begins.
- Face damage, Mend healing, and Guard reduction are applied only after Dice roll and Face reveal.
- Face damage popup appears near the enemy and shows only the applied Face damage modifier.
- Mend healing popup appears near the Hunter after Face reveal.
- Guard applies its reduction after Face reveal and does not show extra enemy damage popup.
- No combat values, DiceRoller logic, Face selection logic, Reward Selection, or Dice Face Replacement systems were changed.
- `TASK_COMBAT_FEEDBACK_ORDER_POLISH` is DONE.
- Base Throw Damage popup appears near the enemy before Dice Animation Layer appears.
- Dice Layer appears after physical impact/base damage feedback.
- Face reveal and Face effect detail happen after rolling.
- Face-specific enemy damage popup shows only the Face damage modifier near the enemy.
- Mend healing popup appears near the Hunter.
- Enemy attack damage popup appears near the Hunter.
- Unity batchmode compile validation completed successfully.
- Presentation consumes already calculated runtime values and does not decide gameplay.

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
- M7: Run Flow Presentation.
- M8: Starter Face Gameplay.
- M9: Starter Dice Build.
- M10: Reward Selection.
- M11: Dice Face Replacement.
- M12: MVP Playtest Polish.
- First complete Run.

M8 Locked Dice Combat Philosophy:

- Every Throw always deals Base Throw Damage.
- Base Throw Damage belongs to the Dice.
- Base Throw Damage never belongs to the Hunter.
- Face effects modify the Throw.
- Face effects do not replace the Throw.
- Final Throw Result is Base Throw Damage from Dice Tier plus Face Effect.
- Dice Tier determines the base power of every Throw.
- Faces determine how that Throw is modified.
- Reward Selection must wait until every starter Face has meaningful gameplay value.
- Starter Dice Build must happen before Reward Selection.
- Reward Selection must continue a player-authored starting Dice build instead of being the first Dice-building decision.

M9 Starter Dice Build Locked Design:

- Fixed Starter Face Pool has been removed.
- Starter Build displays all permanently unlocked Faces whose Face Tier is less than or equal to the current Dice Tier.
- Wood Dice physically remains D6.
- Wood Dice has 4 Active Face Slots.
- Wood Dice has 2 Locked Slots.
- Locked Slots are not rolled.
- Locked Slots are not Blank, Retry, or failed results.
- Only active Faces enter the roll pool.
- Player chooses 4 Faces from the available unlocked Faces.
- Duplicate Faces are allowed without duplicate source entries.
- The only build restrictions are Face Tier and Active Face Slot count.
- Dice Deck must remain compatible with active and locked slots.

M10 Reward Selection Locked Direction:

- Reward Selection improves an already-existing Dice build.
- Reward Selection must never be the first place where the player creates a Dice.
- Reward categories may include New Face, Recover HP, Run-only Max HP increase, and future Relic.
- New Face rewards should be selected in M10 and inserted into the Dice later in M11.
- Dice Face Replacement remains separate from Reward Selection.
- Meta progression unlocks choices, not raw Hunter power.

M11 Dice Face Replacement Locked Direction:

- Reward Selection gives the player a new Face.
- Dice Face Replacement asks the player: "Which Face should I replace?"
- This is the first true Dice-building decision during a Run.
- Reward Selection and Dice Face Replacement remain separate systems.
- Locked Slots are never replacement targets.
- Current Wood Dice has 4 Active Slots and 2 Locked Slots.
- Replacement must operate on the current runtime Dice.
- Duplicate Faces remain legal.
- No permanent unlocks, Dice Shards, Meta Progression, Shop, Inventory, Collection, Face Upgrade, or Face Merge belongs to M11-001.

## M9-003: UI Foundation Polish

Status: DONE

Goal:

Fix Starter Build and Dice Deck readability issues before M10 Reward Selection.

Completed:

- Starter Build panel width was increased for mobile landscape readability.
- Starter Build active and locked Dice slots now fit inside the panel without clipping.
- Starter Build Face Pool buttons now wrap across rows as more unlocked Faces become available.
- Dice Deck expanded panel now opens above the bottom HUD instead of across the Throw button line.
- Dice Deck expanded panel no longer overlaps the Throw button.
- Damage/heal popup readability was rechecked after combat feedback polish.
- Unity batchmode compile validation completed successfully for M9-003.
- No Reward Selection, Dice Face Replacement, Meta Progression, or new Face effects were added.

## M9 Implementation Tasks

Status: DONE

M9 Goal:

Allow the player to build the starting Dice before a run begins.

M9 Scope Guardrails:

- Implement Starter Dice Build only when a detailed M9 task is selected.
- Do not add Reward Selection during M9-001.
- Do not add Dice Face Replacement during M9-001.
- Do not add inventory, shops, meta progression, branching map, new Face effects, enemy AI, boss mechanics, or multi-enemy gameplay.
- Preserve physical D6 Dice presentation.
- Locked Slots must never enter the roll pool.

## M9-001: Starter Dice Build UI

Status: DONE

Goal:

Create the Run Start Dice Build screen before Battle begins.

Requirements:

- Display a Main Menu style Start Run step.
- Display the unlocked Starter Face Pool.
- Display the current Wood Dice with 4 Active Slots and 2 Locked Slots.
- Allow the player to choose which 4 Faces become active.
- Update the Dice preview in real time.
- Display current Face probabilities.
- Prevent invalid builds.
- Keep duplicate Faces legal without requiring duplicate pool entries.
- Generate the runtime Starter Dice from the selected active Faces.
- Do not implement Reward Selection.
- Do not implement Dice Face Replacement.

Completed:

- Added `StarterDiceBuildPresenter` as a Run Start / Starter Dice Build UI.
- Battle starts only after Starter Dice Build is completed.
- Fixed Starter Face Pool has been removed.
- Starter Build displays all permanently unlocked Faces filtered by current Dice Tier.
- Player chooses 4 active Faces from the available unlocked Faces.
- Duplicate Faces are legal without duplicate source entries.
- Wood Dice runtime build uses 4 Active Face Slots and 2 Locked Slots.
- Locked Slots are represented as null inactive slots and do not enter the roll pool.
- `DiceModel` now tracks `ActiveFaceSlotCount`.
- `DiceFace` now tracks `FaceTier`.
- `DiceRoller` now selects only within active face slots.
- Unity batchmode compile validation completed successfully for Starter Dice Build revision.
- Dice preview updates as Faces are picked or removed.
- Probability display updates from the selected active Faces.
- Existing Battle scene consumes the generated runtime Dice through `BattleDiceState.SetCurrentDice`.
- No Reward Selection, Dice Face Replacement, inventory, shops, meta progression UI, new Face gameplay, Dice Tier progression, Hunter progression, enemy AI, boss mechanics, or multi-enemy gameplay was added.

## M9-002: Validate Starter Dice Build

Status: DONE

Goal:

Validate that Starter Dice Build correctly creates the runtime Wood Dice before battle begins.

Validation:

- Confirm Starter Dice Build generates the selected runtime Dice through `BattleDiceState`.
- Confirm Locked Slots never enter the roll pool.
- Confirm probability display matches the selected active Faces.
- Confirm Battle consumes the generated runtime Dice.
- Confirm Dice Deck displays the same active Faces and locked slots during battle.
- Confirm Reward Selection has not been added.
- Confirm Dice Face Replacement has not been added.
- Confirm Unity compile passes.

Completed:

- Verified `StarterDiceBuildPresenter` fills 4 active Face slots and leaves 2 inactive locked slots.
- Verified `DiceModel.ActiveFaceSlotCount` is set to 4 for the Wood Dice runtime build.
- Verified `DiceRoller` selects only from active Face slots and never rolls locked slots.
- Verified `BattleDiceState.SetCurrentDice` preserves the generated runtime Dice and active slot count.
- Verified `CollapsibleDiceDeckPresenter` reads `BattleDiceState.CurrentDice` and displays inactive slots as `Locked`.
- Verified probability text is generated from the selected active Faces.
- Unity batchmode import/compile validation completed successfully.
- Verified no Reward Selection, Dice Face Replacement, inventory, shops, meta progression, new Face gameplay, enemy AI, boss mechanics, or multi-enemy gameplay was added.

M9 Director Result:

- M9_STARTER_DICE_BUILD is approved and DONE.
- Starter Dice Build is a permanent core gameplay loop step.
- The player's first meaningful run choice is which Dice to bring into the run.

## M10 Implementation Tasks

Status: DONE

M10 Goal:

Introduce Reward Selection after eligible reward-bearing nodes.

M10 Pre-Implementation Requirement:

- Long-term progression design is documented in `Docs/Design/PROJECT_LONG_TERM_PROGRESSION_DESIGN.md`.

M10 Scope Guardrails:

- Implement Reward Selection only when a detailed M10 task is selected.
- Do not implement Dice Face Replacement during M10 unless explicitly instructed.
- Do not add inventory, shops, meta progression UI, permanent unlock economy, Dice Tier progression, new run creation, boss mechanics, or multi-enemy gameplay.
- Reward Selection must improve an existing runtime Dice build, not create the Dice.
- Battle nodes grant no Reward Selection.
- Reward Selection belongs to Elite, Treasure, and Boss reward flow only.
- Permanent Face Drops and Dice Shards remain separate from Reward Selection.

## M10-001: Reward Runtime Framework

Status: DONE

Goal:

Create the runtime state holder and data structure for Reward Selection without implementing reward generation, reward effects, or reward UI polish.

Completed:

- Added `RewardSelectionState`.
- Added `RewardData`.
- Added `RewardType` values:
  - Face
  - Heal
  - MaxHp
  - Relic
- `RewardSelectionState` owns active/inactive selection state.
- `RewardSelectionState` owns the current reward list.
- `RewardSelectionState` owns the selected reward.
- `RewardSelectionState` owns whether a reward has been consumed.
- Selecting one reward closes the runtime selection state and clears remaining current rewards.
- Runtime structure can represent Battle Victory -> Reward Selection opens -> reward data assigned -> one reward selected -> selection closes -> continue run.
- No reward generation logic was added.
- No reward effects were added.
- No reward UI polish was added.
- No Dice Face Replacement, Meta Progression, permanent unlocks, Iron Core, or Boss drops were added.
- Unity batchmode compile validation completed successfully for M10-001.

## M10-002: Face Rarity and Definition Schema Lock

Status: DONE

Goal:

Define the shared Face rarity model and lifetime Face definition schema before Reward Pool and Reward Generator implementation.

Scope:

- Add the runtime structure for Face rarity.
- Define approved rarity values for M10.
- Recommended values:
  - Common
  - Rare
  - Epic
  - Legendary
- Define the long-term `FaceDefinition` shape.
- `FaceDefinition` should be the shared source used by Reward Pool, Reward Generator, Permanent Face Drops, Collection, Encyclopedia, Save Data, and future synergy systems.
- Lock the expected FaceDefinition fields:
  - Face ID
  - Display Name
  - Category
  - Rarity
  - Tier
  - Effect Type
  - Effect Parameters
  - Short Description
  - Flavor Text
  - Icon
  - IsStarterFace
  - IsUnlockedByDefault
- Effect Parameters should be a structured value set rather than a single integer.
- Expected Effect Parameters fields:
  - Primary Value
  - Secondary Value
  - Duration
  - Chance
- Early starter Faces mostly use Primary Value.
- Future Faces can use additional fields without changing the FaceDefinition schema.
- Example parameter usage:
  - Attack: Primary Value = Damage.
  - Guard: Primary Value = Reduction.
  - Mend: Primary Value = Heal.
  - Poison: Primary Value = Damage, Duration = turns.
  - Burn: Primary Value = Damage, Duration = turns.
  - Freeze: Primary Value = SkipTurns.
  - Rage: Primary Value = BonusDamage, Duration = turns.
- Define Face Category values:
  - Attack
  - Defense
  - Recovery
  - Utility
  - Status
  - Summon (Future)
  - Special (Future)
- Category represents the player-facing role of a Face.
- Effect Type represents the gameplay effect that executes.
- Short Description is used by Reward UI, Dice Deck, and Collection.
- Flavor Text is used by Encyclopedia or Collection flavor surfaces only.
- Example role/effect split:
  - Attack Face: Category Attack, Effect Type Damage.
  - Lightning Face: Category Attack, Effect Type Damage.
  - Guard Face: Category Defense, Effect Type Guard.
  - Mend Face: Category Recovery, Effect Type Heal.
  - Poison Face: Category Status, Effect Type Poison.
- Assign rarity to current starter Faces.
- Define rarity philosophy:
  - Common: basic attack, healing, and defense.
  - Rare: stronger or more specialized effects.
  - Epic: build-defining Faces.
  - Legendary: Faces that can change the direction of a run.
- Define how Reward Generator should consume rarity.
- Define how Permanent Face Drops should consume rarity.
- Ensure Face rarity can be reused by:
  - Reward Pool
  - Reward Generator
  - Permanent Face Drops
  - Future Face Collection
  - Future synergy and balance systems

Do Not:

- Do not implement reward generation.
- Do not implement permanent Face Drops.
- Do not rebalance existing Faces.
- Do not implement Dice Face Replacement.
- Do not implement Meta Progression.
- Do not implement Collection.
- Do not implement Encyclopedia.
- Do not implement Save Data.
- Do not implement future synergy logic.

Validation:

- Face rarity structure exists for runtime use.
- FaceDefinition-compatible DiceFace structure is implemented and locked for runtime use.
- Effect Parameters are implemented as structured data rather than a single integer.
- Face Category is implemented separately from Effect Type.
- Short Description and Flavor Text are implemented separately.
- Starter Face rarity is assigned in runtime data.
- Rarity philosophy is documented.
- Reward Generator rarity consumption rule is documented.
- Permanent Drop rarity consumption rule is documented.
- Existing starter Faces remain valid.
- No reward gameplay is added.
- Unity compile passes.

Completed:

- Added `FaceRarity` enum:
  - Common
  - Rare
  - Epic
  - Legendary
- Added `FaceCategory` enum:
  - Attack
  - Defense
  - Recovery
  - Utility
  - Status
  - Summon
  - Special
- Added `FaceEffectParameters` structured value data:
  - PrimaryValue
  - SecondaryValue
  - Duration
  - Chance
- Extended `DiceFace` to carry FaceDefinition-compatible runtime fields:
  - Face ID
  - Display Name
  - Category
  - Rarity
  - Tier
  - Effect Type
  - Effect Parameters
  - Short Description
  - Flavor Text
  - Icon
  - IsStarterFace
  - IsUnlockedByDefault
- Starter Face runtime defaults now include Common rarity, category, Tier 1, effect type, effect parameters, starter flag, and default unlock flag.
- Guard and Mend now resolve their gameplay values from `EffectParameters.PrimaryValue`.
- Existing Base Throw Damage, Attack, Guard, Lightning, and Mend values were preserved.
- No Reward Pool, Reward Generator, Reward UI, Reward Apply, Dice Face Replacement, Permanent Face Drops, or Dice Shards were added.

## M10-003: Reward Pool

Status: DONE

Goal:

Define what rewards can exist in the current M10 reward pool.

Scope:

- Create reward pool data for run-scoped rewards.
- Include only the smallest set needed to prove reward selection.
- Reward candidates may include:
  - Face rewards
  - Heal rewards
  - Max HP run-only rewards
  - Relic placeholders if low risk
- Face reward candidates may prepare entries such as:
  - Attack
  - Lightning
  - Guard
  - Mend
  - Poison
  - Burn
  - Freeze
  - Crit
  - Rage
- Faces without implemented gameplay may be data-only or excluded from active generation until safe.

Do Not:

- Do not implement reward random selection yet.
- Do not implement reward effects yet.
- Do not implement Reward UI yet.
- Do not implement Dice Face Replacement.
- Do not implement permanent Face Drops.
- Do not implement Shard drops.
- Do not implement Gold or Shop economy.

Validation:

- Reward pool can provide structured reward candidates.
- Reward pool distinguishes run-only rewards from meta progression drops.
- Battle nodes are not configured to provide rewards.
- Unity compile passes.

Completed:

- Added `RewardPool` as the runtime source for run-scoped reward candidates.
- Face reward candidates reference existing `DiceFace` FaceDefinition-compatible runtime data.
- RewardPool does not redefine Attack, Guard, Lightning, or Mend Face metadata.
- RewardPool includes currently safe runtime Face rewards from unlocked Tier-valid Faces.
- RewardPool includes simple run-only Heal and Max HP reward entries.
- RewardPool includes a Relic placeholder entry only as runtime category data.
- RewardPool does not decide drop probability, node behavior, reward generation, reward selection, or reward application.
- No Reward Generator, Reward Selection UI, Reward Apply, Dice Face Replacement, Permanent Face Drops, Dice Shards, Shop, Gold, Inventory, Collection UI, or Meta Progression was added.

## M10-004: Reward Generator

Status: DONE

Goal:

Generate reward options from the Reward Pool based on reward-bearing node type.

Scope:

- Add node-aware reward generation for eligible node types.
- Generate a small set of reward options, likely 3.
- Supported reward-bearing nodes:
  - Elite
  - Treasure
  - Boss
- Battle nodes must generate no rewards.
- Rest nodes must not use Reward Selection.
- Suggested early generation direction:
  - Elite: Face / Relic / Heal mix.
  - Treasure: Face / Relic / Max HP mix.
  - Boss: Reward Selection plus separate future Shard and permanent Face Drop hooks.

Do Not:

- Do not implement node map.
- Do not implement permanent Face Drops.
- Do not implement Shard drops.
- Do not implement pity systems.
- Do not implement reward application.
- Do not implement Reward UI polish.
- Do not implement Dice Face Replacement.

Validation:

- Elite, Treasure, and Boss can request reward options.
- Battle returns no Reward Selection options.
- Generator consumes Reward Pool data instead of hardcoding UI choices.
- Unity compile passes.

Completed:

- Added `RewardNodeType`:
  - Battle
  - Elite
  - Treasure
  - Rest
  - Boss
- Added `RewardGenerator`.
- `RewardGenerator` consumes `RewardPool` candidates.
- Default generation count is 3 reward options.
- Elite generates 3 options from Face, Relic, and Heal category preference.
- Treasure generates 3 options from Face, Relic, and Max HP category preference.
- Boss generates 3 options from Face, Heal, Max HP, and Relic category preference.
- Battle returns no reward options.
- Rest returns no reward options.
- Exact duplicate reward IDs are avoided inside one generated option set where possible.
- Generated options are cloned structured `RewardData` only.
- No Reward Selection UI, Reward Apply, Dice Face Replacement, Permanent Face Drops, Dice Shards, Pity system, Shop, Gold, Inventory, Collection UI, Meta Progression, Node Map, New Run flow, or Boss mechanics were added.

## M10-005: Reward Selection UI

Status: DONE

Goal:

Create the first playable reward choice screen.

Desired presentation:

- Victory or reward-bearing node result.
- "Choose One".
- Three clear reward options.
- Player selects exactly one.
- Remaining options disappear.

Scope:

- Scene/prefab-style anchored UI following the UI architecture refactor.
- Bind UI to `RewardSelectionState`.
- Display reward type, name, and short readable description.
- Keep the UI readable on mobile landscape.
- Keep Reward Selection separate from Dice Face Replacement.

Do Not:

- Do not implement Dice Face Replacement UI.
- Do not implement inventory.
- Do not implement Shop.
- Do not implement Meta Progression UI.
- Do not implement node map.
- Do not implement Reward UI as a long-term runtime-created debug hierarchy.

Validation:

- Reward UI opens only when RewardSelectionState is active.
- Player can choose exactly one reward.
- Selection closes the runtime reward state.
- Existing Battle flow still works.
- Unity compile passes.

Completed:

- Added `RewardSelectionPresenter`.
- RewardSelectionPresenter binds to `RewardSelectionState`.
- RewardSelectionPresenter displays Reward Selection title, Choose One text, and up to 3 reward options.
- Reward option buttons display reward name, reward type, and short description.
- Selecting one option calls `RewardSelectionState.TrySelectReward(...)`.
- Selected reward remains stored in `RewardSelectionState`.
- Reward UI hides after selection and remaining options disappear through `RewardSelectionState`.
- Reward UI uses scene/prefab-style references when available and runtime-created UI only as fallback.
- `BattleController` opens Reward Selection only for eligible current linear stages:
  - Elite
  - Boss
- Normal Battle stages do not open Reward Selection.
- Current linear run has no Treasure stage, so no Treasure flow was invented.
- Selected rewards are not applied in this task.
- No Reward Apply, Dice Face Replacement, Permanent Face Drops, Dice Shards, Pity system, Shop, Gold, Inventory, Collection UI, Meta Progression, Node Map, Treasure node flow, New Run flow, or Boss mechanics were added.

## M10-006: Reward Apply

Status: DONE

Goal:

Apply the selected run-scoped reward.

Scope:

- Apply Heal reward.
- Apply run-only Max HP reward if included.
- Store selected Face reward for later Dice Face Replacement if Face rewards are included.
- Apply simple Relic placeholder only if a prior task explicitly included relic data.

Do Not:

- Do not directly replace Dice Faces during Reward Apply unless Director promotes that scope.
- Do not permanently unlock Faces.
- Do not apply Dice Shards.
- Do not implement permanent account progression.
- Do not implement Shop or Gold.

Validation:

- Selected reward applies once.
- Remaining rewards do not apply.
- Run-only rewards remain run-scoped.
- Face reward handoff remains separate from M11 Dice Face Replacement.
- Unity compile passes.

Completed:

- Added `RewardApplyService`.
- RewardApplyService reads the selected reward from `RewardSelectionState`.
- Heal rewards apply immediately through `BattleCombatState.HealPlayer(...)`.
- Heal rewards respect Player Max HP.
- Run-only Max HP rewards apply through `BattleCombatState.IncreasePlayerMaxHpForRun(...)`.
- Max HP rewards increase Player Max HP and Current HP by the same amount.
- Face rewards are stored as pending runtime Face rewards for M11 handoff only.
- Face rewards do not modify the current Dice.
- Relic rewards are stored as placeholder runtime ownership only.
- Relic rewards have no gameplay effect.
- RewardSelectionState is reset after reward application.
- `BattleController` applies the selected reward after Reward Selection UI closes.
- HUD refreshes after reward application.
- No Dice Face Replacement, Permanent Face Drops, Dice Shards, Meta Progression, Shop, Gold, Inventory, Collection UI, Node Map, New Run Flow, Treasure stage, Boss reward extras, or Relic gameplay was added.

## M10-007: Validate Reward Selection

Status: DONE

Goal:

Validate the complete first Reward Selection loop.

Validation:

- Battle nodes produce no rewards.
- Elite/Treasure/Boss reward flow can produce Reward Selection.
- Player can select exactly one reward.
- Reward is applied or handed off according to type.
- Remaining rewards disappear.
- Reward Selection improves the current Run only.
- Permanent Face Drops are not part of Reward Selection.
- Dice Shards are not part of Reward Selection.
- Dice Face Replacement remains separate unless explicitly promoted.
- No Meta Progression, Shop, Gold economy, Boss drop system, or node map is added.
- Unity compile passes.

Completed:

- Validated RewardGenerator output rules:
  - Elite generates 3 reward options.
  - Treasure generates 3 reward options from generator support.
  - Boss generates 3 reward options.
  - Battle returns no reward options.
  - Rest returns no reward options.
- Confirmed RewardGenerator consumes RewardPool data and does not redefine reward definitions.
- Confirmed RewardSelectionPresenter binds to RewardSelectionState.
- Confirmed Reward Selection UI can display up to 3 reward options with reward name, reward type, and short description.
- Confirmed player selection records exactly one selected reward in RewardSelectionState.
- Confirmed remaining active reward options disappear after selection.
- Confirmed Reward UI closes after selection.
- Confirmed RewardApplyService consumes selected reward and resets RewardSelectionState.
- Confirmed Heal rewards use `BattleCombatState.HealPlayer(...)` and do not exceed Max HP.
- Confirmed Max HP rewards use `BattleCombatState.IncreasePlayerMaxHpForRun(...)` and increase Max HP and Current HP by the same amount.
- Confirmed Face rewards become pending runtime Face rewards only.
- Confirmed current Dice is unchanged after Face reward selection.
- Confirmed Relic rewards remain placeholder runtime ownership only and have no gameplay effect.
- Confirmed RewardGenerator remains stateless after generation.
- Confirmed pending Face reward remains available for M11.
- Confirmed Reward UI does not reopen unexpectedly in the static flow.
- Confirmed Normal Battle stages do not show Reward Selection.
- Confirmed Elite and Boss reward flow can open Reward Selection in the current linear run.
- Confirmed Treasure stage flow was not invented.
- Confirmed Dice Face Replacement, Permanent Face Drops, Dice Shards, Meta Progression, Shop, Gold, Inventory, Collection UI, Node Map, Treasure stage, New Run Flow, Boss reward extras, and Relic gameplay remain unimplemented.
- Unity batchmode compile validation passed.

## M11 Implementation Tasks

Status: IN_PROGRESS

M11 Goal:

Validate the core build decision of replacing one current runtime Dice Face with a selected Face reward.

M11 Scope Guardrails:

- Implement Dice Face Replacement only when a detailed M11 task is selected.
- Keep Reward Selection and Dice Face Replacement as separate systems.
- Do not modify Dice during M11-001.
- Do not add permanent Face unlocks, Dice Shards, Meta Progression, Shop, Inventory, Collection, new Dice Tier, Face Upgrade, or Face Merge.
- Locked Slots must never be selectable as replacement targets.

## M11-001: Dice Face Replacement Runtime Framework

Status: DONE

Goal:

Create the runtime replacement state required after a Face reward is selected, without modifying Dice and without adding UI.

Completed:

- Added `DiceFaceReplacementState`.
- Replacement state holds the current runtime Dice reference.
- Replacement state holds the pending Face reward cloned from M10 reward apply handoff.
- Replacement state tracks whether replacement is active.
- Replacement state tracks a nullable selected replacement slot through `SelectedFaceSlotIndex`.
- Replacement state exposes replacement candidate slots for future UI.
- `BattleController` starts replacement state only after a Face reward is selected and applied.
- Locked Slots are excluded from replacement candidates by checking `DiceModel.ActiveFaceSlotCount`.
- Current Wood Dice replacement candidates are limited to active slots 0-3.
- No Dice mutation occurs.
- No replacement UI, Face insertion, Face removal, probability update, Dice Deck update, permanent Face unlock, Dice Shards, Meta Progression, Shop, Inventory, Collection, new Dice Tier, Face Upgrade, or Face Merge was added.
- Existing Battle, Starter Dice Build, Reward Pool, Reward Generator, Reward Selection, Reward Apply, Dice Roll, Face Resolution, and Dice Deck behavior is preserved.
- Unity batchmode compile validation completed successfully for M11-001.

## M11-002: Dice Face Replacement UI

Status: READY

Goal:

Present the active replacement state and allow the player to choose which active runtime Dice Face slot should be replaced.

Scope:

- Display the pending Face reward.
- Display only active current runtime Dice slots as replacement targets.
- Hide or mark Locked Slots as unavailable without allowing selection.
- Store the selected replacement slot in `DiceFaceReplacementState`.
- Do not mutate Dice yet unless explicitly promoted by Director.

## M7 Implementation Tasks

Status: GENERATED

M7 Goal:

Make the existing M6 runtime run flow visible to the player before rewards and Dice replacement are introduced.

Desired Presentation Shape:

- Non-boss Victory.
- Stage Clear.
- Next Stage.
- Battle resumes.
- Boss Victory.
- Run Complete.
- Player Defeat.
- Defeat feedback.

M7 Task Rule:

- One presentation concept per task.
- Keep every task independently verifiable.
- Presentation consumes existing runtime state only.
- Runtime state remains owned by M6 systems.
- Do not add rewards, Dice replacement, inventory, shops, meta progression, branching map, enemy AI, boss mechanics, new Face effects, or multi-enemy gameplay.
- Do not make run flow presentation decide battle outcome, stage advance, next battle preparation, or run completion.

## M7-001: Run Flow Presentation Entry Point

Status: DONE

Goal:

Add the minimal presentation-only entry point needed for run flow beats after battle outcome resolution.

Requirements:

- Introduce a presentation-only component or hook for run flow presentation.
- Consume existing `BattleOutcomeState`, `LinearStageRuntimeState`, and `LinearRunState` data.
- Do not calculate Victory or Defeat.
- Do not advance stages.
- Do not prepare the next battle.
- Do not reset battle state.
- Do not add UI for rewards or Dice replacement.

Done Criteria:

- Battle flow has a clear place where run flow presentation can play after battle outcome changes.

Validation:

- Existing battle loop still works.
- Presentation hook does not mutate HP.
- Presentation hook does not mutate turn ownership.
- Presentation hook does not mutate stage or run state.
- No rewards, Dice replacement, inventory, meta progression, branching map, enemy AI, boss mechanics, or new Face effects are added.

Completed:

- Added `RunFlowPresenter` as a presentation-only run-flow entry point.
- `RunFlowPresenter` consumes `BattleOutcomeState`, `LinearStageRuntimeState`, and `LinearRunState` context.
- `BattleController` now calls the run-flow presentation hook after Victory and Defeat outcomes.
- Boss Victory can be observed again after M6 run completion is marked.
- The hook does not calculate outcome, advance stages, prepare battles, reset battle state, mutate HP, mutate turn ownership, or add reward/Dice replacement UI.

## M7-002: Stage Clear Presentation

Status: DONE

Goal:

Show a short Stage Clear presentation after non-boss Victory.

Requirements:

- Stage Clear appears only after `BattleOutcomeState` is Victory.
- Stage Clear appears only for non-boss stages.
- Presentation should be short, readable, and 16-bit RPG appropriate.
- Do not advance stages inside the presentation.
- Do not prepare the next battle inside the presentation.
- Do not show rewards.

Done Criteria:

- Winning Stage 1, 2, 3, or 4 produces a visible Stage Clear beat before the next run-flow beat.

Validation:

- Stage Clear does not appear on Defeat.
- Stage Clear does not appear after Boss Victory.
- Stage Clear does not mutate runtime state.
- Existing player Throw, enemy turn, damage, HP refresh, and battle outcome flow still work.

Completed:

- `RunFlowPresenter` now shows a short `Stage Cleared` presentation beat for non-boss Victory.
- Stage Clear is skipped for Boss Victory, Defeat, and InProgress outcomes.
- Stage Clear reads `BattleOutcomeState` and `LinearStageRuntimeState` only.
- Stage Clear does not advance stages, prepare the next battle, reset battle state, modify HP, modify Dice, unlock rewards, or add reward UI.

## M7-003: Next Stage Presentation

Status: DONE

Goal:

Show the next stage identity before the next battle resumes.

Requirements:

- Present the current runtime stage after non-boss stage advancement.
- Use existing `LinearStageRuntimeState` values.
- Show stage number and stage type in a simple temporary presentation.
- Do not create a map.
- Do not create branching stage selection.
- Do not add rewards between stages.

Done Criteria:

- After Stage Clear, the player can see the next Stage number/type before battle resumes.

Validation:

- Next Stage presentation reads runtime stage state.
- No hardcoded fake stage progression is added.
- No branching map, reward, Dice replacement, inventory, or shop UI is added.

Completed:

- `RunFlowPresenter` now shows the current runtime stage after M6 stage advancement.
- Next Stage presentation displays `Stage N` and the current `StageType`.
- Next Stage presentation is triggered only after the previous non-boss Victory Stage Clear beat.
- Next Stage reads `LinearStageRuntimeState` and does not advance stages, prepare battles, generate maps, create rewards, replace Dice, or mutate runtime state.

## M7-004: Battle Resume Presentation

Status: DONE

Goal:

Make the transition from Next Stage back to active battle readable.

Requirements:

- Add a short battle resume beat after the next stage is presented.
- Player input should resume only after the run-flow presentation sequence ends.
- Preserve M6 next battle preparation behavior.
- Preserve player HP and current runtime Dice persistence.
- Do not add healing rules.
- Do not add new enemy setup rules beyond existing M6 preparation.

Done Criteria:

- After non-boss Victory, the player sees Stage Clear, Next Stage, and then returns to a ready battle state.

Validation:

- Throw input is not accepted during run-flow presentation.
- Throw input is accepted after battle resumes.
- Player HP persists.
- Current runtime Dice persists.
- Dice Deck still reads from current runtime Dice.

Completed:

- Added a short `Battle Start` beat after the Next Stage presentation.
- Battle Resume is presented by `RunFlowPresenter` and does not mutate runtime state.
- Input remains locked until the run-flow presentation sequence completes.
- M6 next battle preparation behavior remains unchanged.

## M7-005: Run Complete Presentation

Status: DONE

Goal:

Show a clear Run Complete presentation after Boss Victory.

Requirements:

- Run Complete appears only when `LinearRunState` is completed.
- Consume Boss Victory/run completion state from existing runtime systems.
- Do not start a new run.
- Do not show rewards.
- Do not show meta progression.
- Do not add restart flow.

Done Criteria:

- Winning Stage 5 Boss produces a visible Run Complete beat and does not resume battle input.

Validation:

- Run Complete does not appear after non-boss Victory.
- Run Complete does not appear after Defeat.
- Further Throw input remains blocked after run completion.
- No reward, Dice replacement, meta progression, restart, or new run system is added.

Completed:

- Added a short `Run Complete` beat when `LinearRunState` is completed after Boss Victory.
- Run Complete is presented by `RunFlowPresenter` and does not mutate runtime state.
- Further battle input remains blocked by existing run completion ownership.
- No reward, Dice replacement, meta progression, restart flow, or new run creation was added.

## M7-006: Defeat Presentation

Status: DONE

Goal:

Show a clear Defeat presentation when the player loses the battle.

Requirements:

- Defeat presentation appears only after `BattleOutcomeState` is Defeat.
- Consume existing defeat state.
- Do not add restart UI.
- Do not add run summary.
- Do not add meta progression.
- Do not add rewards.

Done Criteria:

- Player HP reaching 0 leads to visible Defeat feedback and battle input remains blocked.

Validation:

- Defeat presentation does not appear after Victory.
- Stage does not advance after Defeat.
- Run does not complete after Defeat.
- No restart, reward, Dice replacement, inventory, or meta progression system is added.

Completed:

- Added a short `Defeat` beat when `BattleOutcomeState` reaches Defeat.
- Defeat is presented by `RunFlowPresenter` and does not mutate runtime state.
- Battle input remains blocked by existing Defeat ownership.
- No restart UI, run summary, meta progression, rewards, Dice replacement, or inventory was added.

## M7-007: Validate Run Flow Presentation

Status: DONE

Goal:

Validate the full run flow presentation sequence before starting reward selection.

Requirements:

- Confirm non-boss Victory shows Stage Clear.
- Confirm next stage identity is shown before battle resumes.
- Confirm battle resumes after non-boss run-flow presentation.
- Confirm Boss Victory shows Run Complete.
- Confirm Defeat shows Defeat presentation.
- Confirm runtime state is still owned by M6 systems.
- Confirm no rewards or Dice replacement were added.

Done Criteria:

- M7 can be submitted for Director review as the visible run-flow layer over M6 runtime progression.

Validation:

- PASS: Static validation confirms non-boss Victory can show Stage Clear, Next Stage, and Battle Start through `RunFlowPresenter`.
- PASS: Static validation confirms Boss Victory can show Run Complete by consuming `LinearRunState.Completed`.
- PASS: Static validation confirms Defeat can show Defeat by consuming `BattleOutcomeState.Defeat`.
- PASS: `RunFlowPresenter` does not call stage advance, battle reset, HP mutation, Dice mutation, reward, Dice replacement, restart, run summary, new run, or meta progression code.
- PASS: Unity batchmode compile completed successfully.
- PASS: No compile errors, `NullReferenceException`, or `MissingReferenceException` were found in the validation log.
- PASS: M7 remains presentation-only over M6 runtime ownership.
- PASS: No rewards, Dice replacement, inventory, shops, meta progression, branching map, enemy AI, boss mechanics, new Face effects, or multi-enemy gameplay were added.

Completed:

- Validated the full M7 run-flow presentation scope.
- Marked M7_RUN_FLOW_PRESENTATION as ready for Director review.

## M8 Implementation Tasks

Status: GENERATED

M8 Goal:

Implement the minimum gameplay identity required for every starter Face using the newly locked Dice Combat Philosophy.

Core Formula:

Final Throw Result = Dice Tier Base Throw Damage + Face Effect

M8 Task Rule:

- One gameplay concept per task.
- Keep every task independently verifiable.
- Every Throw must deal Base Throw Damage from the Dice.
- Base Throw Damage belongs to the Dice, never the Hunter.
- Face effects modify the Throw and do not replace the Throw.
- Keep all starter Face effects deterministic, readable, and simple.
- Do not add Reward Selection.
- Do not add Dice Face Replacement.
- Do not add inventory, shops, meta progression, branching map, enemy AI, boss mechanics, complex status systems, new Face pools, or multi-enemy gameplay.
- Follow `Docs/Design/PROJECT_CORE_PHILOSOPHY.md`.

## M8-001: Base Throw Damage Framework

Status: DONE

Goal:

Establish the runtime framework where every accepted Throw deals Dice Tier Base Throw Damage before Face-specific modifiers are applied.

Requirements:

- Introduce or route Base Throw Damage as Dice-owned runtime data.
- Do not store Base Throw Damage as Hunter-owned power.
- Preserve the current physical D6 Dice model.
- Preserve current runtime Dice Face selection.
- Preserve current battle presentation order.
- Attack should no longer be the only source of all damage meaning.
- Guard, Lightning, and Mend should still be allowed to produce base damage through the Throw even before their own effects are implemented.
- Do not implement Guard, Lightning, or Mend effects in this task.
- Do not implement Dice Tier progression UI or active slot progression in this task.

Done Criteria:

- Every Throw can produce Dice-owned Base Throw Damage.
- Face effects can be added on top of Base Throw Damage in later tasks.
- Existing Throw, Face Reveal, Face Effect, Damage Number, Damage Apply, Enemy Turn, and Run Flow still work.

Validation:

- Attack Throw deals Base Throw Damage plus its Face effect if currently implemented.
- Guard, Lightning, and Mend Throws still deal Base Throw Damage.
- No Hunter permanent Attack stat is introduced.
- No Reward Selection or Dice Face Replacement is added.

Completed:

- Added Dice-owned Base Throw Damage to the runtime Dice model.
- Starter Dice now owns Base Throw Damage 3.
- Starter Attack now acts as a 5 damage Face modifier, making starter Attack Throw total damage 8.
- BattleController now calculates Dice Base Throw Damage and Face damage modifier separately. Current sequencing applies them at separate timing points.
- Guard, Lightning, and Mend can now deal Base Throw Damage even before their own Face effects are implemented.
- No Hunter permanent Attack stat, Reward Selection, or Dice Face Replacement was added.

## M8-002: Guard Gameplay

Status: DONE

Goal:

Give Guard a simple defensive identity while preserving Base Throw Damage.

Requirements:

- Guard modifies the Throw with a deterministic defensive effect.
- Guard does not replace the Throw.
- Guard still allows Base Throw Damage to occur.
- Keep the effect simple and MVP-readable.
- Do not add complex shield stacks, block status systems, enemy AI changes, or reward systems.

Done Criteria:

- Guard has meaningful gameplay value.
- Guard is no longer a No Effect Face.
- Guard remains compatible with the existing enemy turn and player damage flow.

Validation:

- Guard deals Base Throw Damage.
- Guard applies its defensive modifier.
- Guard does not trigger rewards, Dice replacement, inventory, or progression.

Completed:

- Guard now modifies the Throw with a deterministic defensive effect.
- Guard Throw still deals Dice Base Throw Damage.
- Guard reduces the next incoming enemy attack damage by 3 during the same battle exchange.
- Guard presentation now shows `Guard` as the Face Effect beat.
- No block stacks, shield duration system, status system, enemy AI change, Reward Selection, or Dice Face Replacement was added.

## M8-003: Lightning Gameplay

Status: DONE

Goal:

Give Lightning a simple secondary damage or utility identity while preserving Base Throw Damage.

Requirements:

- Lightning modifies the Throw with a deterministic secondary damage or utility effect.
- Lightning does not replace the Throw.
- Lightning still allows Base Throw Damage to occur.
- Keep the effect simple and MVP-readable.
- Do not add complicated status systems, random chains, multi-enemy targeting, or reward systems.
- Future Lightning direction is chaining / area damage.
- Current MVP single-enemy Lightning deals Base Throw Damage plus 3 Lightning modifier damage.

Done Criteria:

- Lightning has meaningful gameplay value.
- Lightning is no longer a No Effect Face.
- Lightning remains compatible with the existing single-enemy MVP battle flow.

Validation:

- Lightning deals Base Throw Damage.
- Lightning applies its simple modifier.
- Lightning does not trigger rewards, Dice replacement, inventory, or progression.

Completed:

- Renamed the starter `Spark` Face identity to `Lightning`.
- Starter Dice now uses `starter_lightning` / `Lightning` as the runtime Face identity.
- Starter Dice Base Throw Damage was tuned from 5 to 3.
- Starter Attack remains a 5 damage Face modifier, making starter Attack Throw total damage 8.
- Lightning now resolves through `FaceResolver` as a deterministic 3 damage Face modifier.
- Current MVP Lightning Throw deals Base Throw Damage 3 plus Lightning modifier damage 3, for 6 total damage before enemy HP clamping.
- No multi-enemy chaining, area targeting, enemy selection, new enemy slots, reward selection, Dice replacement, inventory, meta progression, or Dice Tier UI was added.

## M8-004: Mend Gameplay

Status: DONE

Goal:

Give Mend a simple recovery identity while preserving Base Throw Damage.

Requirements:

- Mend modifies the Throw with a deterministic recovery effect.
- Mend does not replace the Throw.
- Mend still allows Base Throw Damage to occur.
- Keep the effect simple and MVP-readable.
- Do not add long-term healing economy, meta progression, reward systems, or inventory.
- Current MVP Mend heals the player for 5 HP after the Throw.
- Healing must not exceed Player Max HP.

Done Criteria:

- Mend has meaningful gameplay value.
- Mend is no longer a No Effect Face.
- Mend remains compatible with player HP and enemy turn flow.

Validation:

- Mend deals Base Throw Damage.
- Mend applies its recovery modifier.
- Mend does not trigger rewards, Dice replacement, inventory, or progression.

Completed:

- Added `Mend` as a implemented `FaceEffectType`.
- Starter `Mend` now resolves through `FaceResolver` to a deterministic 5 HP recovery effect.
- Added `BattleCombatState.HealPlayer(int healing)` as the single player HP recovery mutation path.
- Mend Throw still deals Dice Base Throw Damage before applying recovery.
- Mend healing is clamped by Player Max HP.
- Mend presentation can now show Base Throw Damage plus Mend HP recovery as the Face Effect beat.
- No long-term healing economy, reward selection, Dice replacement, inventory, meta progression, enemy AI, boss mechanics, or multi-enemy gameplay was added.

## M8-005: Face Presentation Polish

Status: DONE

Goal:

Update battle presentation so Base Throw Damage and Face Effect are readable as one Throw result.

Requirements:

- Presentation should communicate that the Dice hit always causes Base Throw Damage.
- Presentation should communicate that the revealed Face modifies that Throw.
- Preserve the existing battle rhythm:
  - Throw
  - Hero Throw
  - Projectile
  - Enemy Hit
  - Dice Layer
  - Rolling
  - Face Reveal
  - Face Effect
  - Damage Number
  - Damage Apply
  - HP Refresh
- Do not add new gameplay.
- Do not add reward, replacement, inventory, progression, or new UI systems.

Done Criteria:

- The player can understand both base damage and Face effect from the battle presentation.
- The presentation remains short, readable, and SNES-style.

Validation:

- Attack, Guard, Lightning, and Mend presentation remains readable.
- Damage number and HP update still happen after presentation.
- No gameplay is triggered directly from presentation code.

Completed:

- `ThrowSequencePresenter` now receives Dice Base Throw Damage for presentation.
- Face Effect beat now displays Base Throw Damage plus the resolved Face modifier.
- Attack and Lightning presentation can show Base Damage plus damage modifier.
- Guard presentation can show Base Damage plus incoming damage reduction.
- Mend presentation can show Base Damage plus HP recovery.
- Damage Number remains the applied enemy damage value.
- No gameplay values are decided by presentation code.
- No reward selection, Dice replacement, inventory, progression, new Face effects, or new UI systems were added.

## M8-006: Validate Starter Face Gameplay

Status: DONE

Goal:

Validate that every starter Face has meaningful gameplay value before Reward Selection begins.

Requirements:

- Confirm every Throw deals Dice-owned Base Throw Damage.
- Confirm Attack remains the primary damage Face.
- Confirm Guard has defensive value.
- Confirm Lightning has secondary damage or utility value.
- Confirm Mend has recovery value.
- Confirm no starter Face remains No Effect.
- Confirm Full Reward Selection gameplay has not been implemented; only M10-001 runtime framework exists.
- Confirm Dice Face Replacement has not been implemented.

Done Criteria:

- M8 can be submitted for Director review.
- Starter Dice creates meaningful build decisions before rewards exist.

Validation:

- Unity compile passes.
- No `NullReferenceException`.
- Player Throw flow still works.
- Enemy turn still works.
- Battle outcome still works.
- Stage runtime still works.
- Run flow presentation still works.
- No rewards, Dice replacement, inventory, shops, meta progression, branching map, enemy AI, boss mechanics, complex status systems, new Face pools, or multi-enemy gameplay are added.

Completed:

- Confirmed starter Dice Base Throw Damage is 3.
- Confirmed starter Dice contains Attack, Attack, Guard, Guard, Lightning, and Mend.
- Confirmed Attack remains the primary offense Face: Base 3 plus Attack modifier 5, for 8 total damage before enemy HP clamping.
- Confirmed Guard has defensive value: Base 3 plus same-exchange enemy attack reduction 3.
- Confirmed Lightning has secondary offensive value: Base 3 plus Lightning modifier 3, for 6 total damage before enemy HP clamping.
- Confirmed Mend has recovery value: Base 3 plus up to 5 HP recovery, clamped by Player Max HP.
- Confirmed no starter Face remains No Effect.
- Confirmed Reward Selection and Dice Face Replacement were not implemented.
- Confirmed Unity batchmode compile passes with no `NullReferenceException` or `MissingReferenceException`.
- M8_STARTER_FACE_GAMEPLAY is ready for Director review.

## TASK_M8_PRESENTATION_READABILITY_FIX

Status: DONE

Goal:

Improve battle feedback readability after M8 Starter Face Gameplay without changing gameplay values.

Requirements:

- Keep existing gameplay values unchanged.
- Keep existing battle flow order.
- Make Face Effect detail readable long enough for mobile landscape play.
- Show Face name, Base Throw Damage, Face modifier, and total enemy damage or recovery context.
- Show actual enemy HP damage near the enemy body.
- Make enemy damage number float upward and fade out.
- Strengthen enemy hit feedback with small SNES-style effects.
- Presentation must consume already calculated runtime data and must not decide gameplay.

Completed:

- Increased Face Effect detail duration to 1.25 seconds.
- Reformatted Face Effect detail into multi-line MVP-readable result text.
- Attack can show `Attack`, `Base 3 + 5`, `= 8 Damage`.
- Guard can show `Guard`, `Base 3 = 3 Damage`, `Enemy Damage -3`.
- Lightning can show `Lightning`, `Base 3 + 3`, `= 6 Damage`.
- Mend can show `Mend`, `Base 3 = 3 Damage`, `Heal +5 HP`.
- Enemy damage number now appears near the enemy body.
- Enemy damage number uses actual pending enemy HP damage only.
- Enemy damage number floats upward and fades out.
- Enemy hit feedback now includes stronger flash, a short enemy shake, and a small hit spark.
- No Base Throw Damage, Face modifier, Guard reduction, Lightning damage, Mend healing, enemy damage, HP mutation order, FaceResolver gameplay logic, Reward Selection, Dice Face Replacement, inventory, meta progression, or enemy AI was changed.

## M6 Implementation Tasks

Status: DONE

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
- Keep enemy slot 2 and slot 3 inactive until real HP state is added for those slots.
- Do not implement targeting, multiple enemy HP bars, multi-enemy attacks, rewards, stage advance, run completion, victory presentation, or battle reset.

Done Criteria:

- Current single-enemy battle still reaches Victory through the enemy group query.

Validation:

- Victory uses `EnemyGroupState.AreAllEnemiesDefeated`.
- BattleOutcomeState still owns battle completion.
- No multi-enemy gameplay was added.

## M6-004: Player Defeat Resolution

Status: DONE

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

Status: DONE

Goal:

Advance from a won non-boss battle to the next fixed stage.

Requirements:

- Consume a Victory outcome.
- Advance only if the current stage is not the final boss stage.
- Advance runtime stage position only.
- Leave next battle preparation for M6-007.
- Do not add rewards between stages in M6.
- Do not add stage selection UI.

Done Criteria:

- Winning Stage 1, 2, 3, or 4 advances to the next stage.

Validation:

- Stage number increments exactly once per victory.
- Defeat does not advance the stage.
- Boss victory does not advance to a nonexistent stage.

## M6-006: Complete Linear Run

Status: DONE

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

Status: DONE

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

Status: DONE

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
- Static validation confirmed Stage 1 through Stage 5 fixed progression.
- Static validation confirmed boss Victory marks the linear run completed.
- Static validation confirmed player HP, current runtime Dice, and Dice Deck runtime data persist across next battle preparation.
- Static validation confirmed rewards, Dice replacement, inventory, meta progression, branching map, enemy AI, boss mechanics, and multi-enemy gameplay remain unimplemented.
- Unity batchmode import/compile validation completed successfully with exit code 0.

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
- Kept the primary reveal text as only the selected Face name, such as `Lightning`.
- Kept Face Effect as smaller secondary text, such as `Damage` or `No Effect`.
- Kept selected-slot validation text as small corner `RESULT S#: FaceName` debug text.
- Preserved Hero idle and throw animation assets from `Assets/Art`.
- Preserved projectile timing after the fifth throw frame.
- Preserved current battle flow: Throw, Hero throw animation, projectile, enemy flash, Dice layer, rolling, Face Reveal, Face Effect, Damage Number, damage apply, HP refresh.

Not Added:

- No enemy turn.
- No rewards or progression.
- No new Dice faces.
- No Guard, Lightning, or Mend real effects.
- No multi-enemy logic.
- No inventory or stage system.

## M5 Implementation Tasks

Status: DONE

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
- No enemy AI, enemy damage, player damage, enemy presentation, rewards, progression, battle end, new Dice faces, or Guard/Lightning/Mend effects were added.

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
- Do not invent undefined Face effects for `Guard`, `Lightning`, or `Mend`.
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
- `Guard`, `Lightning`, and `Mend` must resolve to explicit undefined/no-effect data until Director or GDD source text defines them.

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
- Null, `Guard`, `Lightning`, `Mend`, and any undefined Face resolve to explicit `FaceEffectData.None`.
- Resolver does not mutate HP, trigger presentation, access UI, or call Dice result selection.

## M4-003: Attack Face

Status: DONE

Goal:

Implement `Attack` Face as the first MVP gameplay effect using the Face Resolver output.

Requirements:

- `Attack` Face resolves to deterministic damage using its current `FixedThrowDamageValue`.
- Starter `Attack` total damage has been tuned to 8 after M6 validation.
- Applying damage belongs to this current Attack gameplay task because Attack is the only implemented gameplay effect.
- Enemy HP damage must come from the resolved Attack Face effect, not from a generic Throw damage path.
- The player-facing cause must remain: Attack Face appeared, therefore attack happened.
- Do not add random damage ranges.
- Do not add critical hits, modifiers, combo logic, or target selection.
- Do not implement Guard, Lightning, or Mend effects in this task.
- Damage is still applied only after Face Reveal and Face effect presentation timing.
- `BattleCombatState` receives only the final resolved damage value.
- `BattleCombatState` must not inspect Dice or Face data.
- HP refresh still occurs after damage application.
- Preserve victory input lock behavior.

Done Criteria:

- When `Attack` is the selected Face, the resolved Attack effect applies 8 total damage through `BattleCombatState` after presentation timing.

Validation:

- Starter `Attack` Face now resolves to 8 total damage.
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

- `Guard`, `Lightning`, and `Mend` must not receive invented effects.
- Undefined starter Faces should resolve as implemented=false or equivalent no-effect result.
- Undefined starter Faces should not damage the enemy.
- Undefined starter Faces should not heal, shield, lightning, stun, draw, reroll, or alter future turns.
- The result should remain visible or understandable enough for validation.

Done Criteria:

- Non-`Attack` starter Faces resolve without causing hidden gameplay changes.

Validation:

- `Guard`, `Lightning`, and `Mend` do not change enemy HP.
- `Guard`, `Lightning`, and `Mend` do not change player HP.
- No shield, enemy turn, reward, progression, Dice face replacement, or future system is added.
- Documentation or validation output makes clear that their effects are intentionally pending Director/GDD definition.

Completed:

- Undefined/no-effect Face results now produce explicit battle log feedback.
- Null or unknown Face results are reported as having no effect yet.
- Guard, Lightning, Mend, and other undefined Faces still resolve through `FaceResolver` as `FaceEffectData.None`.
- Undefined/no-effect Faces still do not apply enemy damage or player healing.
- No Guard, Lightning, Mend, shield, stun, heal, reroll, enemy turn, reward, progression, or Dice face replacement behavior was added.

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
- Confirmed Guard, Lightning, Mend, null, and unknown Faces resolve to `FaceEffectData.None`.
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
