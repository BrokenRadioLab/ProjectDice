using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class RewardPool
{
    private const int DefaultDiceTier = 1;
    private const int HealRewardAmount = 10;
    private const int MaxHpRewardAmount = 5;

    [SerializeField] private List<RewardData> rewardCandidates = new List<RewardData>();

    public IReadOnlyList<RewardData> RewardCandidates => rewardCandidates;
    public int Count => rewardCandidates.Count;

    public RewardPool(IEnumerable<RewardData> rewards)
    {
        SetRewards(rewards);
    }

    public static RewardPool CreateDefaultRunRewardPool()
    {
        return CreateDefaultRunRewardPool(DefaultDiceTier);
    }

    public static RewardPool CreateDefaultRunRewardPool(int currentDiceTier)
    {
        List<RewardData> rewards = new List<RewardData>();
        AddFaceRewards(rewards, currentDiceTier);
        AddRunOnlyRewards(rewards);
        return new RewardPool(rewards);
    }

    public RewardData[] GetRewardsSnapshot()
    {
        RewardData[] snapshot = new RewardData[rewardCandidates.Count];

        for (int i = 0; i < rewardCandidates.Count; i++)
        {
            snapshot[i] = rewardCandidates[i] != null ? rewardCandidates[i].Clone() : null;
        }

        return snapshot;
    }

    private void SetRewards(IEnumerable<RewardData> rewards)
    {
        rewardCandidates.Clear();

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

            rewardCandidates.Add(reward.Clone());
        }
    }

    private static void AddFaceRewards(List<RewardData> rewards, int currentDiceTier)
    {
        DiceFace[] faces = StarterDiceFactory.CreateUnlockedFacesForDiceTier(currentDiceTier);

        for (int i = 0; i < faces.Length; i++)
        {
            DiceFace face = faces[i];
            if (face == null)
            {
                continue;
            }

            rewards.Add(new RewardData(
                $"reward_face_{face.Id}",
                face.DisplayName,
                RewardType.Face,
                string.IsNullOrWhiteSpace(face.ShortDescription)
                    ? "Add this Face to this run."
                    : face.ShortDescription,
                0,
                face));
        }
    }

    private static void AddRunOnlyRewards(List<RewardData> rewards)
    {
        rewards.Add(new RewardData(
            "reward_heal_10",
            "Recover 10 HP",
            RewardType.Heal,
            "Recover HP for this run.",
            HealRewardAmount));

        rewards.Add(new RewardData(
            "reward_max_hp_5",
            "Max HP +5",
            RewardType.MaxHp,
            "Increase Max HP for this run.",
            MaxHpRewardAmount));

        rewards.Add(new RewardData(
            "reward_relic_placeholder",
            "Relic Placeholder",
            RewardType.Relic,
            "Future run modifier placeholder."));
    }
}
