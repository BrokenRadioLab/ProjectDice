using System;
using System.Collections.Generic;

public enum RewardNodeType
{
    Battle,
    Elite,
    Treasure,
    Rest,
    Boss
}

public static class RewardGenerator
{
    public const int DefaultRewardOptionCount = 3;

    public static RewardData[] GenerateOptions(RewardPool rewardPool, RewardNodeType nodeType)
    {
        return GenerateOptions(rewardPool, nodeType, DefaultRewardOptionCount);
    }

    public static RewardData[] GenerateOptions(RewardPool rewardPool, RewardNodeType nodeType, int optionCount)
    {
        if (rewardPool == null || optionCount <= 0 || !IsRewardBearingNode(nodeType))
        {
            return Array.Empty<RewardData>();
        }

        RewardType[] preferredTypes = GetPreferredTypes(nodeType);
        List<RewardData> options = new List<RewardData>(optionCount);
        IReadOnlyList<RewardData> candidates = rewardPool.RewardCandidates;

        for (int i = 0; i < preferredTypes.Length && options.Count < optionCount; i++)
        {
            AddFirstMatchingCandidate(candidates, preferredTypes[i], options);
        }

        for (int i = 0; i < candidates.Count && options.Count < optionCount; i++)
        {
            RewardData candidate = candidates[i];
            if (candidate == null || ContainsReward(options, candidate.RewardId))
            {
                continue;
            }

            options.Add(candidate.Clone());
        }

        return options.ToArray();
    }

    public static bool IsRewardBearingNode(RewardNodeType nodeType)
    {
        return nodeType == RewardNodeType.Elite ||
            nodeType == RewardNodeType.Treasure ||
            nodeType == RewardNodeType.Boss;
    }

    private static RewardType[] GetPreferredTypes(RewardNodeType nodeType)
    {
        switch (nodeType)
        {
            case RewardNodeType.Elite:
                return new[] { RewardType.Face, RewardType.Relic, RewardType.Heal };
            case RewardNodeType.Treasure:
                return new[] { RewardType.Face, RewardType.Relic, RewardType.MaxHp };
            case RewardNodeType.Boss:
                return new[] { RewardType.Face, RewardType.Heal, RewardType.MaxHp, RewardType.Relic };
            default:
                return Array.Empty<RewardType>();
        }
    }

    private static void AddFirstMatchingCandidate(
        IReadOnlyList<RewardData> candidates,
        RewardType rewardType,
        List<RewardData> options)
    {
        for (int i = 0; i < candidates.Count; i++)
        {
            RewardData candidate = candidates[i];
            if (candidate == null ||
                candidate.RewardType != rewardType ||
                ContainsReward(options, candidate.RewardId))
            {
                continue;
            }

            options.Add(candidate.Clone());
            return;
        }
    }

    private static bool ContainsReward(IReadOnlyList<RewardData> rewards, string rewardId)
    {
        if (string.IsNullOrWhiteSpace(rewardId))
        {
            return false;
        }

        for (int i = 0; i < rewards.Count; i++)
        {
            RewardData reward = rewards[i];
            if (reward != null && reward.RewardId == rewardId)
            {
                return true;
            }
        }

        return false;
    }
}
