using System;
using UnityEngine;

public enum DiceRuntimePhase
{
    Ready,
    Rolling,
    Stopped,
    Revealed
}

[Serializable]
public sealed class DiceModel
{
    public const int FaceSlotCount = 6;

    [SerializeField] private DiceFace[] faces = new DiceFace[FaceSlotCount];
    [SerializeField, Min(0)] private int baseThrowDamage;
    [SerializeField] private DiceRuntimePhase runtimePhase = DiceRuntimePhase.Ready;
    [SerializeField] private int lastResultSlotIndex = -1;

    public int BaseThrowDamage => baseThrowDamage;
    public DiceRuntimePhase RuntimePhase => runtimePhase;
    public int LastResultSlotIndex => lastResultSlotIndex;

    public int FaceCount => faces != null ? faces.Length : 0;

    public DiceModel()
    {
    }

    public DiceModel(DiceFace[] initialFaces)
        : this(initialFaces, 0)
    {
    }

    public DiceModel(DiceFace[] initialFaces, int baseThrowDamage)
    {
        this.baseThrowDamage = Mathf.Max(0, baseThrowDamage);
        SetFaces(initialFaces);
    }

    public DiceFace GetFace(int slotIndex)
    {
        ValidateSlotIndex(slotIndex);
        return faces[slotIndex];
    }

    public DiceFace[] GetFacesSnapshot()
    {
        DiceFace[] snapshot = new DiceFace[FaceSlotCount];

        for (int i = 0; i < FaceSlotCount; i++)
        {
            snapshot[i] = faces[i]?.Clone();
        }

        return snapshot;
    }

    public void SetFaces(DiceFace[] newFaces)
    {
        if (newFaces == null || newFaces.Length != FaceSlotCount)
        {
            throw new ArgumentException($"Dice must contain exactly {FaceSlotCount} face slots.", nameof(newFaces));
        }

        faces = new DiceFace[FaceSlotCount];

        for (int i = 0; i < FaceSlotCount; i++)
        {
            faces[i] = newFaces[i]?.Clone();
        }

        lastResultSlotIndex = -1;
        runtimePhase = DiceRuntimePhase.Ready;
    }

    public void SetFace(int slotIndex, DiceFace face)
    {
        ValidateSlotIndex(slotIndex);
        faces[slotIndex] = face?.Clone();
    }

    public void BeginRoll()
    {
        lastResultSlotIndex = -1;
        runtimePhase = DiceRuntimePhase.Rolling;
    }

    public void RecordResultSlot(int slotIndex)
    {
        ValidateSlotIndex(slotIndex);
        lastResultSlotIndex = slotIndex;
        runtimePhase = DiceRuntimePhase.Stopped;
    }

    public void RevealResult()
    {
        if (lastResultSlotIndex < 0)
        {
            throw new InvalidOperationException("A Dice result must be stopped before it can be revealed.");
        }

        runtimePhase = DiceRuntimePhase.Revealed;
    }

    public void SetRuntimePhase(DiceRuntimePhase nextPhase)
    {
        runtimePhase = nextPhase;
    }

    private static void ValidateSlotIndex(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= FaceSlotCount)
        {
            throw new ArgumentOutOfRangeException(nameof(slotIndex), slotIndex, $"Slot index must be between 0 and {FaceSlotCount - 1}.");
        }
    }
}
