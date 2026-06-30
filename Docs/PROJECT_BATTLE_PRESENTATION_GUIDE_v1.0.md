

Version: 1.0
Status: LOCKED
Project: Project Dice

---

Purpose

This document defines the permanent presentation structure of the battle scene.

It specifies layout, composition, animation flow, and screen hierarchy.

This document is the visual source of truth for every future Battle Scene implementation.

Do not redesign this structure unless explicitly approved by the Project Director.

---

Design Philosophy

Project Dice is a 16-bit SNES-style JRPG.

The battle scene should resemble classic console RPG battles while introducing one unique mechanic:

The Hero attacks by throwing dice.

The dice are both:

- The attack projectile.
- The combat result.

The presentation should remain simple, readable, and faithful to classic pixel RPG limitations.

Do not introduce modern cinematic effects.

---

Screen Composition

The screen is divided into three functional areas.

Top

Battle Field

Bottom Action Area

There is no permanent battle log.

---

Character Layout

Hero

Position:

Left side

Approximately 25~30% of screen width.

Facing right.

---

Enemy

Position:

Right side

Approximately 70~75% of screen width.

Facing left.

Supported enemy count:

- 1
- 2
- 3

Enemies are vertically stacked.

Example:

Enemy A

Enemy B

Enemy C

Bosses may occupy larger space.

---

HP Display

HP is NOT displayed in a fixed HUD.

HP belongs to each character.

Display:

HP Bar

HP Value

Above the character's head.

Each HP display follows its character.

---

Battle Background

Simple SNES pixel background.

Purpose:

Support gameplay.

Never distract.

No animated scenery.

No excessive decorations.

---

Dice Stage

The center of the battlefield is reserved for dice presentation.

Normally:

Empty.

No permanent UI.

No permanent dice.

The player should focus on characters until a throw occurs.

---

Throw Sequence

This sequence is LOCKED.

Throw Button

↓

Hero Throw Animation

(2~3 frames)

↓

White projectile trail

(Simple pixel line)

↓

Enemy Hit Flash

(1~2 frames)

↓

Dice Animation Layer appears

↓

Dice rolls briefly

↓

Dice stops

↓

Top face is revealed

↓

Face Skill activates

↓

Damage Number appears

↓

Dice disappears

Sequence ends.

---

Hero Throw Animation

Very small animation.

Suggested:

Idle

↓

Throw

↓

Idle

Maximum:

3 frames.

---

Projectile

Represented only by a white pixel trail.

No visible flying dice is required.

Players understand the throw through:

Hero animation

+ 

Projectile line

+ 

Enemy hit reaction.

---

Enemy Hit

Simple hit flash.

No long knockback.

No dramatic animation.

Classic SNES style.

---

Dice Animation Layer

Appears ONLY after enemy impact.

This is NOT a permanent UI.

This is NOT a menu.

It is a temporary battle presentation layer.

The dice appears as though it has fallen near the enemy after impact.

Suggested flow:

Appear

↓

Small bounce

↓

Roll

↓

Stop

↓

Reveal

↓

Disappear

---

Dice Animation

Simple.

Recommended:

4~6 frames.

No complex physics.

No 3D rotation.

Classic pixel animation only.

---

Face Reveal

The dice finishes with the result facing the player.

The top face must be immediately readable.

Result presentation should be short.

---

Damage

Damage numbers appear only after the dice result is revealed.

Damage never appears before the dice stops.

---

Battle Log

No permanent battle log.

Future status messages, if needed, should be brief floating text.

---

Bottom Action Area

Bottom area is reserved for battle commands.

Current:

Throw button only.

Future:

Skill

Throw

Item

Throw remains the primary action.

---

Camera

Fixed camera.

No camera shake larger than a few pixels.

No zoom.

No cinematic movement.

---

Sprite Scale

Hero

48×48

Standard Enemy

48×48

Large Enemy

64×64

Boss

96×96 (or larger when necessary)

Sprites should remain readable.

---

Animation Philosophy

Classic SNES limitations.

Prefer:

Short

Responsive

Readable

Avoid:

Long cinematic sequences

Complex transitions

Modern animation styles

---

Future Rule

Every future battle feature must respect this presentation flow.

Dice presentation is part of battle animation, not UI.

Characters remain the focus of the battle.

The dice becomes the focus only during the throw sequence.

This document is LOCKED.