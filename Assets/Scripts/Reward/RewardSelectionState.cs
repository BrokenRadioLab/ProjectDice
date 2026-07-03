using System.Collections.Generic;
using UnityEngine;

public sealed class RewardSelectionState : MonoBehaviour
{
    [SerializeField] private bool isSelectionActive;
    [SerializeField] private bool isRewardConsumed;
    [SerializeField] private List<RewardData> currentRewards = new List<RewardData>();
    [SerializeField] private RewardData selectedReward;

    public bool IsSelectionActive => isSelectionActive;
    public bool IsRewardConsumed => isRewardConsumed;
    public IReadOnlyList<RewardData> CurrentRewards => currentRewards;
    public RewardData SelectedReward => selectedReward;
    public bool HasSelectedReward => selectedReward != null;

    private void Awake()
    {
        ResetSelection();
    }

    public void OpenSelection(IEnumerable<RewardData> rewards)
    {
        currentRewards.Clear();
        selectedReward = null;
        isRewardConsumed = false;
        isSelectionActive = true;

        if (rewards == null)
        {
            return;
        }

        foreach (RewardData reward in rewards)
        {
            if (reward == null)
            {
                continue;
            }

            currentRewards.Add(reward.Clone());
        }
    }

    public bool TrySelectReward(int rewardIndex)
    {
        if (!isSelectionActive || rewardIndex < 0 || rewardIndex >= currentRewards.Count)
        {
            return false;
        }

        selectedReward = currentRewards[rewardIndex].Clone();
        currentRewards.Clear();
        isRewardConsumed = true;
        isSelectionActive = false;
        return true;
    }

    public void CloseSelection()
    {
        isSelectionActive = false;
    }

    public void ResetSelection()
    {
        isSelectionActive = false;
        isRewardConsumed = false;
        selectedReward = null;
        currentRewards.Clear();
    }
}
