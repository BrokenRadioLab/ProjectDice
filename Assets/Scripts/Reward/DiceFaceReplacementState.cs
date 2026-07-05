using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DiceFaceReplacementState : MonoBehaviour
{
    [SerializeField] private DiceModel currentRuntimeDice;
    [SerializeField] private DiceFace pendingFaceReward;
    [SerializeField] private bool replacementActive;
    [SerializeField] private int selectedFaceSlotIndex = -1;

    public DiceModel CurrentRuntimeDice => currentRuntimeDice;
    public DiceFace PendingFaceReward => pendingFaceReward;
    public bool IsReplacementActive => replacementActive;
    public bool HasPendingFaceReward => pendingFaceReward != null;
    public bool HasSelectedFaceSlot => selectedFaceSlotIndex >= 0;
    public int SelectedFaceSlotIndex => selectedFaceSlotIndex;

    public bool BeginReplacement(BattleDiceState diceState, DiceFace faceReward)
    {
        return BeginReplacement(diceState != null ? diceState.CurrentDice : null, faceReward);
    }

    public bool BeginReplacement(DiceModel runtimeDice, DiceFace faceReward)
    {
        if (runtimeDice == null || faceReward == null)
        {
            ResetReplacement();
            return false;
        }

        currentRuntimeDice = runtimeDice;
        pendingFaceReward = faceReward.Clone();
        selectedFaceSlotIndex = -1;
        replacementActive = HasReplacementCandidate();

        return replacementActive;
    }

    public bool TrySelectReplacementSlot(int slotIndex)
    {
        if (!replacementActive || !IsReplacementCandidateSlot(slotIndex))
        {
            return false;
        }

        selectedFaceSlotIndex = slotIndex;
        return true;
    }

    public int[] GetReplacementCandidateSlots()
    {
        if (currentRuntimeDice == null)
        {
            return Array.Empty<int>();
        }

        List<int> candidateSlots = new List<int>(currentRuntimeDice.ActiveFaceSlotCount);
        for (int i = 0; i < currentRuntimeDice.ActiveFaceSlotCount; i++)
        {
            if (IsReplacementCandidateSlot(i))
            {
                candidateSlots.Add(i);
            }
        }

        return candidateSlots.ToArray();
    }

    public bool IsReplacementCandidateSlot(int slotIndex)
    {
        if (currentRuntimeDice == null)
        {
            return false;
        }

        if (slotIndex < 0 || slotIndex >= currentRuntimeDice.ActiveFaceSlotCount)
        {
            return false;
        }

        return currentRuntimeDice.GetFace(slotIndex) != null;
    }

    public void ResetReplacement()
    {
        currentRuntimeDice = null;
        pendingFaceReward = null;
        replacementActive = false;
        selectedFaceSlotIndex = -1;
    }

    private bool HasReplacementCandidate()
    {
        return GetReplacementCandidateSlots().Length > 0;
    }
}
