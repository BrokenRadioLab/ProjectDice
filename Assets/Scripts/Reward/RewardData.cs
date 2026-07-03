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
    [SerializeField] private string shortDescription;
    [SerializeField, Min(0)] private int value;
    [SerializeField] private DiceFace face;

    public string RewardId => rewardId;
    public string DisplayName => displayName;
    public RewardType RewardType => rewardType;
    public string ShortDescription => shortDescription;
    public int Value => value;
    public DiceFace Face => face;
    public bool HasRewardId => !string.IsNullOrWhiteSpace(rewardId);

    public RewardData(
        string rewardId,
        string displayName,
        RewardType rewardType,
        string shortDescription = "",
        int value = 0,
        DiceFace face = null)
    {
        this.rewardId = rewardId ?? string.Empty;
        this.displayName = displayName ?? string.Empty;
        this.rewardType = rewardType;
        this.shortDescription = shortDescription ?? string.Empty;
        this.value = Mathf.Max(0, value);
        this.face = face;
    }

    public RewardData(
        string rewardId,
        string displayName,
        RewardType rewardType,
        int value,
        DiceFace face = null)
        : this(rewardId, displayName, rewardType, string.Empty, value, face)
    {
    }

    public RewardData Clone()
    {
        return new RewardData(
            rewardId,
            displayName,
            rewardType,
            shortDescription,
            value,
            face != null ? face.Clone() : null);
    }
}
