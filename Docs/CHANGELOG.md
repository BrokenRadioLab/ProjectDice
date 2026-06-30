# CHANGELOG

## 2026-06-30

### Changed

- Started M4_SKILL_RESOLUTION implementation.
- Implemented M4-002 Face Resolver.
- Added `FaceResolver.Resolve(DiceFace selectedFace)` as a pure `DiceFace` to `FaceEffectData` transform.
- Marked M4-002 as DONE and M4-003 Attack Face as NEXT.
- Implemented M4-003 Attack Face.
- Updated `BattleController` so Attack damage is applied from resolved `FaceEffectData` instead of generic selected-Face Throw damage.
- Marked M4-003 as DONE and M4-004 Explicit Undefined Face Handling as NEXT.
- Confirmed Unity batchmode import/compile validation for M4-003 completed successfully with exit code 0.
- Implemented M4-004 Explicit Undefined Face Handling.
- Added explicit no-effect battle log feedback for undefined, null, Guard, Spark, and Mend Face results.
- Marked M4-004 as DONE and M4-005 Face Effect Presentation Beat as NEXT.
- Confirmed Unity batchmode import/compile validation for M4-004 completed successfully with exit code 0.
- Implemented M4-005 Face Effect Presentation Beat.
- Added compact Face Effect Text under `DiceAnimationLayer` after Face Reveal and before Damage Number.
- Damage effects show `Damage`; undefined/no-effect results show `No Effect`.
- Marked M4-005 as DONE and M4-006 Validate M4 Face Skill Resolution as NEXT.
- Confirmed Unity batchmode import/compile validation for M4-005 completed successfully with exit code 0.
- Completed M4-006 Validate M4 Face Skill Resolution.
- Marked M4_SKILL_RESOLUTION as READY_FOR_DIRECTOR_REVIEW.
- Confirmed Unity batchmode import/compile validation for M4-006 completed successfully with exit code 0.
- Implemented M4-001 Face Effect Data Model.
- Added `FaceEffectData` and `FaceEffectType` runtime data for deterministic Face effect results.
- Marked M4-001 as DONE and M4-002 Face Resolver as NEXT.
- Started M3_DICE_PRESENTATION.
- Recorded Director PASS for M3_DICE_PRESENTATION.
- Marked M3_DICE_PRESENTATION as DONE.
- Marked M4_SKILL_RESOLUTION as READY.
- Locked current M3 sequence as Project Dice's Signature Battle Flow.
- Recorded M4 Face causality principle: the revealed Face causes the combat effect and must not feel like a generic skill button.
- Started M3-005 Dice Presentation Validation.
- Completed M3-005 Dice Presentation Validation.
- Marked M3_DICE_PRESENTATION as READY_FOR_DIRECTOR_REVIEW.
- Confirmed Unity batchmode import/compile validation completed successfully with exit code 0.
- Added explicit package dependencies for Unity UI and Input System so the current project scripts compile in batchmode.
- Added Director M3-005 validation focus: feel, readability, natural damage timing, satisfying HP decrease, and responsiveness.
- Implemented M3-002 Rolling Presentation.
- Implemented M3-003 Face Reveal.
- Implemented M3-004 Damage Presentation.
- Added runtime `Damage Number Text` under `DiceAnimationLayer`.
- Added 0.15 second damage number beat after face reveal.
- Kept HP refresh after the full presentation sequence returns.
- Marked M3-004 as DONE and M3-005 Validate M3 Dice Presentation as NEXT.
- Updated `BattleController` to select the Dice result once before battle presentation reveal.
- Updated `ThrowSequencePresenter` so face reveal consumes the already selected `DiceFace`.
- Added runtime `Dice Face Reveal Text` under the rolling Dice placeholder.
- Added 0.20 second face reveal duration.
- Marked M3-003 as DONE and M3-004 Damage Presentation as NEXT.
- Added a runtime `Rolling Dice Placeholder` under `DiceAnimationLayer`.
- Kept `DiceAnimationLayer` visible through the rolling beat before hiding it.
- Added 0.45 second rolling duration with simple frame-like position, color, and orientation changes.
- Marked M3-002 as DONE and M3-003 Face Reveal as NEXT.
- Implemented M3-001 Dice Animation Layer.
- Connected `ThrowSequencePresenter` to the existing scene `DiceAnimationLayer`.
- Updated the throw rhythm so Hero feedback is 0.05 seconds, projectile is 0.08 seconds, enemy flash is 0.05 seconds, and Dice Animation Layer appearance is 0.10 seconds.
- Ensured `DiceAnimationLayer` appears only after enemy hit flash and hides again before damage is applied.
- Marked M3-001 as DONE and M3-002 Rolling Presentation as NEXT.
- Recorded Director final review approval for M2_DICE_CORE.
- Marked M2_DICE_CORE as DONE.
- Marked M3_DICE_PRESENTATION as READY.
- Updated M3 direction so it is no longer treated as a Dice Overlay milestone.
- Recorded that the dice is part of the battle animation sequence, not UI.
- Added M3 task sequence in `TASK_QUEUE.md`: Dice Animation Layer, Rolling presentation, Face reveal, Damage presentation, and Validation.
- Recorded the locked M3 presentation flow: Throw Button, Hero Throw, white projectile trail, Enemy hit flash, Dice Animation Layer appears, Dice rolls, Dice stops, Face reveal, Face effect, Damage number, Sequence ends.
- Confirmed M3 may proceed from Director feedback and `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` while GDD source text remains pending, without inventing GDD content.
- Synchronized documentation before M3 without changing gameplay code or scene files.
- Updated `MILESTONE_PLAN.md` so M0_PROJECT_SETUP and M1_COMBAT_CORE are DONE.
- Earlier synchronized `MILESTONE_PLAN.md` while M2_DICE_CORE was still awaiting Director review.
- Later updated `MILESTONE_PLAN.md` after Director approval so M2_DICE_CORE is DONE and M3_DICE_PRESENTATION is READY.
- Recorded that `Docs/PROJECT_GDD_v1.0.md` is not currently present and will be provided by the Director later.
- Confirmed `Docs/PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0.md` is present and locked.
- Updated `CURRENT_STATE.md` to state that M2 Dice Core implementation was complete and then approved by Director final review.
- Updated `CURRENT_STATE.md` to state that M3 may proceed from Director feedback and the locked Battle Presentation Guide while GDD source text remains pending.
- Marked `TASK_DOCS_SYNC_BEFORE_M3` as DONE in `TASK_QUEUE.md`.
- Updated `DONE_REPORT.md` and `SELF_REVIEW_REPORT.md` for documentation sync validation.

### Documentation Sync Not Changed

- During `TASK_DOCS_SYNC_BEFORE_M3`, no gameplay files, scene files, or combat code were changed.
- No GDD content was invented, redesigned, or reconstructed.

## 2026-06-29

### Changed

- Validated M2 Dice Core end-to-end by static scene/script inspection.
- Confirmed starter Dice exists with six face slots and duplicate slots.
- Confirmed each accepted Throw selects one Dice result slot and stores it in `BattleDiceState`.
- Confirmed result validation text displays selected slot and face.
- Confirmed selected `DiceFace.FixedThrowDamageValue` is the current Throw damage source.
- Confirmed Attack faces deal 5 damage and Guard, Spark, and Mend currently deal 0 damage until skill effects are implemented later.
- Confirmed no Dice overlay animation, face skill activation, enemy turn, rewards, progression, or future systems were added during M2.
- Marked `M2-009_VALIDATE_M2_DICE_CORE` as DONE and `M2_DICE_CORE` as READY_FOR_DIRECTOR_REVIEW in project tracking documents.
- Connected Throw damage to the selected Dice face's fixed throw damage value.
- Replaced `BattleCombatState.ApplyFixedThrowDamageToEnemy` with `BattleCombatState.ApplyDamageToEnemy(int damage)`.
- Removed the scene-level `fixedThrowDamage` source from `BattleCombatState`.
- Updated `BattleController` so the selected Dice face is chosen first, then its value is applied as deterministic damage.
- Marked `M2-008_CONNECT_FIXED_THROW_DAMAGE_SOURCE_TO_DICE_GRADE_MVP_VALUE` as DONE in project tracking documents.
- Added `BattleDiceResultPresenter` for temporary no-overlay validation of the latest selected Dice result.
- Wired `BattleController` to update the validation display after Dice result selection.
- Added a small runtime-created `BattleField` validation text showing selected slot and face name after each accepted Throw.
- Marked `M2-007_SURFACE_LATEST_DICE_RESULT_FOR_DEBUG_FREE_VALIDATION` as DONE in project tracking documents.
- Added `DiceRoller` to select one result slot from the current six-slot runtime Dice.
- Added minimal Dice runtime phase flow for accepted Throws: `Rolling`, `Stopped`, then `Revealed`.
- Updated `BattleDiceState` to store the latest selected Dice slot and expose the latest selected face.
- Updated `BattleController` to coordinate Dice result selection once per accepted Throw while preserving existing fixed damage.
- Connected `BattleController` to the existing Battle scene `BattleDiceState` component.
- Marked `M2-006_SELECT_ONE_DICE_FACE_RESULT_PER_THROW` as DONE in project tracking documents.
- Added `StarterDiceFactory` to create a deterministic six-slot starter Dice runtime instance.
- Added `BattleDiceState` to store the current battle Dice runtime state separately from HP/combat state.
- Added the starter Dice runtime component to `Assets/Scenes/Battle/Battle.unity`.
- Serialized a starter Dice with exactly six face slots, duplicate starter faces, `Ready` phase, and no selected result.
- Marked `M2-005_ADD_STARTER_DICE_RUNTIME_STATE` as DONE in project tracking documents.
- Added runtime-only `DiceFace` data model for minimal Dice face identity, category, and fixed throw damage value reference.
- Added runtime-only `DiceModel` with exactly six face slots, duplicate face support, runtime phase metadata, and latest result slot storage.
- Marked `M2-004_CREATE_DICE_CORE_DATA_MODEL` as DONE in the project tracking documents.
- Added `ThrowSequencePresenter` for the first minimal production-facing Throw presentation sequence.
- Updated `BattleController` so accepted THROW input locks during the presentation sequence, then applies existing fixed damage and refreshes HP.
- Added a temporary thin white projectile trail generated under `BattleField` at runtime.
- Added brief Hero throw feedback and Enemy hit flash using existing UI placeholder primitives.
- Reduced active Hero and standard Enemy placeholders toward small 16-bit sprite-slot scale instead of large debug rectangles.
- Updated `TASK_QUEUE.md`, `CURRENT_STATE.md`, `DONE_REPORT.md`, and `SELF_REVIEW_REPORT.md` for `M2-003_THROW_SEQUENCE_PLACEHOLDER`.
- Aligned `Assets/Scenes/Battle/Battle.unity` with `PROJECT_BATTLE_PRESENTATION_GUIDE_v1.0`.
- Replaced the prototype-style character presentation with `HeroSlot`, `EnemySlotsRoot`, `EnemySlot_01`, `EnemySlot_02`, and `EnemySlot_03`.
- Moved Player HP above the Hero slot and Enemy HP above the active Enemy slot.
- Reserved the center battlefield for future dice presentation, effects, damage numbers, and skill effects.
- Replaced the previous dice overlay presentation with hidden `DiceAnimationLayer` structure only.
- Hid the permanent visible Battle Log while preserving the existing compatibility text reference.
- Added inactive bottom action placeholders for future Skill and Item buttons while keeping THROW as the active primary action.
- Removed the previous rolling overlay placeholder scene objects and `DiceOverlayPresenter` script because the current presentation guide alignment task does not implement dice rolling.
- Restored `BattleController` to the current fixed-damage Throw flow while keeping HP refresh through `BattleHudPresenter`.
- Updated `TASK_QUEUE.md`, `CURRENT_STATE.md`, `DONE_REPORT.md`, and `SELF_REVIEW_REPORT.md` for `M2-002_ALIGN_BATTLE_SCENE_TO_PRESENTATION_GUIDE`.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No dice rolling animation, face reveal, skill activation, enemy turns, multi-enemy targeting, rewards, progression, inventory, item behavior, or future systems were implemented.
- No ScriptableObjects were created for the Dice model.
- Dice face results now provide the deterministic Throw damage value.
- The M2-007 result display is temporary validation presentation only and is not the final Dice Result Overlay.
- No Dice Result Overlay, dice animation overlay, face reveal, skill activation, enemy turns, rewards, progression, inventory, item behavior, or future systems were implemented for M2-008.
- Unity batchmode validation for M2-009 was attempted but blocked because the project was already open in another Unity Editor instance.

## 2026-06-28

### Changed

- Rebuilt `Assets/Scenes/Battle/Battle.unity` into a permanent Battle layout foundation with `Canvas`, `BattleRoot`, `TopHUD`, `BattleField`, `Battle Log Placeholder`, `BottomHUD`, and `Throw Button Placeholder` structure.
- Replaced oversized hero and enemy debug areas with smaller future sprite placeholder slots.
- Added Player and Enemy status hierarchy with name text and HP text locations.
- Added a centered `Dice Overlay Root` reserved for future overlay presentation and kept it hidden by default.
- Moved the battle log and Throw button into final layout regions while preserving current M1 behavior.
- Removed legacy EventSystem/InputModule scene dependency and kept input compatible with the active New Input System setup.
- Added runtime font fallback for scene UI text visibility in the current Unity setup.
- Updated `TASK_QUEUE.md`, `CURRENT_STATE.md`, `DONE_REPORT.md`, and `SELF_REVIEW_REPORT.md` for `M2-000_FINAL_BATTLE_LAYOUT_FOUNDATION`.
- Validated the M2 Battle layout foundation for `M2-001_EDITOR_LAYOUT_VALIDATION`.
- Confirmed Unity Editor log loaded `Assets/Scenes/Battle/Battle.unity`.
- Confirmed serialized scene hierarchy and references for BattleRoot, TopHUD, BattleField, Battle Log, BottomHUD, Throw input, HP text, and hidden Dice Overlay Root.
- Recorded that Unity batchmode validation was blocked because the project was already open in Unity.
- Added `DiceOverlayPresenter` to present the temporary rolling/unknown Dice Overlay placeholder.
- Wired `BattleController` to show the Dice Overlay before applying existing fixed damage.
- Added a short rolling overlay delay before HP and Battle Log update.
- Added `Rolling Dice Placeholder` and `Rolling State Text` under `Dice Overlay Root`.
- Adjusted the rolling overlay placeholder duration to `1.3` seconds and moved timing to an update-driven unscaled timer.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No dice result selection, face reveal, skill activation, enemy turns, rewards, progression, or future systems were implemented.
- `BattleCombatState`, `BattleController`, and `BattleHudPresenter` responsibilities were not expanded beyond their existing boundaries.

## 2026-06-27

### Changed

- Marked `M0_PROJECT_SETUP` as DONE in `MILESTONE_PLAN.md`.
- Began `M1_COMBAT_CORE` and generated detailed M1-only tasks in `TASK_QUEUE.md`.
- Normalized the Battle scene path from `Assets/Scenes/BattleScene.unity` to `Assets/Scenes/Battle/Battle.unity`.
- Updated `ProjectSettings/EditorBuildSettings.asset` to reference the canonical Battle scene path.
- Added scene-only placeholder UI anchors to `Assets/Scenes/Battle/Battle.unity` for hero area, enemy area, player HP, enemy HP, Throw button, and battle log/result text.
- Added `Assets/Scripts/Battle/BattleCombatState.cs` to store known player HP, enemy HP, and fixed throw damage values for M1.
- Added a `Battle Combat State` scene object to `Assets/Scenes/Battle/Battle.unity`.
- Added `Assets/Scripts/Battle/BattleHudPresenter.cs` to present `BattleCombatState` HP values in the existing Battle scene HP placeholders.
- Updated the Battle scene HP placeholder text to display current stored values.
- Added `Assets/Scripts/Battle/BattleController.cs` for M1-only Throw placeholder interaction, fixed damage application, HP refresh, battle log feedback, and victory input lock.
- Added an EventSystem to the Battle scene so the Throw placeholder can receive UI pointer input.
- Extended `BattleCombatState` with deterministic fixed-damage enemy HP reduction and enemy defeated state.
- Validated M1 combat core scope and documented architecture boundaries for state, controller, HUD presentation, and future damage resolution.
- Updated `CURRENT_STATE.md` with M1 progress and the next human decision point.

### Not Changed

- `PROJECT_GDD_v1.0.md` was not modified.
- No gameplay action behavior was implemented.
- No enemy turn behavior was implemented.
- No Dice result selection was implemented.
- No skills, upgrades, rewards, progression, or future milestone systems were implemented.
- No M2 or later milestone systems were introduced during M1 validation.
- No external placeholder assets or prefabs were created.
