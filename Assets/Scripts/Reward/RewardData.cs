using System;
using UnityEngine;

public enum RewardType
{
    Face,
    Heal,
    MaxHp,
    Relic
}

[Serializable]
public sealed class RewardData
{
    [SerializeField] private string rewardId;
    [SerializeField] private string displayName;
    [SerializeField] private RewardType rewardType;
    [SerializeField, Min(0)] private int value;
    [SerializeField] private DiceFace face;

    public string RewardId => rewardId;
    public string DisplayName => displayName;
    public RewardType RewardType => rewardType;
    public int Value => value;
    public DiceFace Face => face;
    public bool HasRewardId => !string.IsNullOrWhiteSpace(rewardId);

    public RewardData(
        string rewardId,
        string displayName,
        RewardType rewardType,
        int value = 0,
        DiceFace face = null)
    {
        this.rewardId = rewardId ?? string.Empty;
        this.displayName = displayName ?? string.Empty;
        this.rewardType = rewardType;
        this.value = Mathf.Max(0, value);
        this.face = face;
    }

    public RewardData Clone()
    {
        return new RewardData(
            rewardId,
            displayName,
            rewardType,
            value,
            face != null ? face.Clone() : null);
    }
}
