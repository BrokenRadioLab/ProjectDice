using System;
using UnityEngine;

public static class DiceRoller
{
    public static int SelectResultSlot(DiceModel dice)
    {
        if (dice == null)
        {
            throw new ArgumentNullException(nameof(dice));
        }

        if (dice.FaceCount != DiceModel.FaceSlotCount)
        {
            throw new InvalidOperationException($"Dice must contain exactly {DiceModel.FaceSlotCount} face slots.");
        }

        return UnityEngine.Random.Range(0, DiceModel.FaceSlotCount);
    }
}
