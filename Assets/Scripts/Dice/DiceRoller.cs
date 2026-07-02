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

        int activeFaceSlotCount = dice.ActiveFaceSlotCount;

        for (int i = 0; i < activeFaceSlotCount; i++)
        {
            if (dice.GetFace(i) == null)
            {
                throw new InvalidOperationException("Active Dice face slots must not be locked.");
            }
        }

        return UnityEngine.Random.Range(0, activeFaceSlotCount);
    }
}
