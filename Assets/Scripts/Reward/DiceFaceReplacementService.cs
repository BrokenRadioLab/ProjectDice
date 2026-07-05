using UnityEngine;

public sealed class DiceFaceReplacementService : MonoBehaviour
{
    [SerializeField] private DiceFace lastReplacedFace;
    [SerializeField] private int lastReplacedSlotIndex = -1;

    public DiceFace LastReplacedFace => lastReplacedFace;
    public int LastReplacedSlotIndex => lastReplacedSlotIndex;

    public bool ReplaceSelectedFace(DiceFaceReplacementState replacementState, RewardApplyService rewardApplyService = null)
    {
        if (replacementState == null ||
            !replacementState.IsReplacementActive ||
            !replacementState.HasPendingFaceReward ||
            !replacementState.HasSelectedFaceSlot)
        {
            return false;
        }

        DiceModel runtimeDice = replacementState.CurrentRuntimeDice;
        DiceFace pendingFace = replacementState.PendingFaceReward;
        int selectedSlotIndex = replacementState.SelectedFaceSlotIndex;

        if (runtimeDice == null ||
            pendingFace == null ||
            !replacementState.IsReplacementCandidateSlot(selectedSlotIndex))
        {
            return false;
        }

        runtimeDice.SetFace(selectedSlotIndex, pendingFace);
        lastReplacedSlotIndex = selectedSlotIndex;
        lastReplacedFace = pendingFace.Clone();

        replacementState.ResetReplacement();
        rewardApplyService?.ClearPendingFaceReward();

        return true;
    }
}
