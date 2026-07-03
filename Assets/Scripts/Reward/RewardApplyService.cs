using UnityEngine;

public sealed class RewardApplyService : MonoBehaviour
{
    [SerializeField] private DiceFace pendingFaceReward;
    [SerializeField] private RewardData placeholderRelicReward;
    [SerializeField] private RewardData lastAppliedReward;
    [SerializeField, Min(0)] private int lastAppliedHealAmount;
    [SerializeField, Min(0)] private int lastAppliedMaxHpAmount;

    public DiceFace PendingFaceReward => pendingFaceReward;
    public RewardData PlaceholderRelicReward => placeholderRelicReward;
    public RewardData LastAppliedReward => lastAppliedReward;
    public int LastAppliedHealAmount => lastAppliedHealAmount;
    public int LastAppliedMaxHpAmount => lastAppliedMaxHpAmount;
    public bool HasPendingFaceReward => pendingFaceReward != null;
    public bool HasPlaceholderRelicReward => placeholderRelicReward != null;

    public bool ApplySelectedReward(RewardSelectionState rewardSelectionState, BattleCombatState combatState)
    {
        if (rewardSelectionState == null || !rewardSelectionState.HasSelectedReward)
        {
            return false;
        }

        RewardData selectedReward = rewardSelectionState.SelectedReward.Clone();
        ResetLastAppliedValues();
        lastAppliedReward = selectedReward.Clone();

        switch (selectedReward.RewardType)
        {
            case RewardType.Heal:
                ApplyHealReward(selectedReward, combatState);
                break;
            case RewardType.MaxHp:
                ApplyMaxHpReward(selectedReward, combatState);
                break;
            case RewardType.Face:
                StorePendingFaceReward(selectedReward);
                break;
            case RewardType.Relic:
                StorePlaceholderRelicReward(selectedReward);
                break;
        }

        rewardSelectionState.ResetSelection();
        return true;
    }

    private void ApplyHealReward(RewardData reward, BattleCombatState combatState)
    {
        if (combatState == null || reward == null)
        {
            return;
        }

        lastAppliedHealAmount = combatState.HealPlayer(reward.Value);
    }

    private void ApplyMaxHpReward(RewardData reward, BattleCombatState combatState)
    {
        if (combatState == null || reward == null)
        {
            return;
        }

        lastAppliedMaxHpAmount = combatState.IncreasePlayerMaxHpForRun(reward.Value);
    }

    private void StorePendingFaceReward(RewardData reward)
    {
        pendingFaceReward = reward != null && reward.Face != null ? reward.Face.Clone() : null;
    }

    private void StorePlaceholderRelicReward(RewardData reward)
    {
        placeholderRelicReward = reward != null ? reward.Clone() : null;
    }

    private void ResetLastAppliedValues()
    {
        lastAppliedReward = null;
        lastAppliedHealAmount = 0;
        lastAppliedMaxHpAmount = 0;
    }
}
