using System;
using System.IO;
using System.Text;
using UnityEngine;

public sealed class PlaytestLogger : MonoBehaviour
{
    private const string DirectoryName = "Playtest";
    private const string FileName = "Playtest.log";
    private static int runCounter;

    private string logFilePath;
    private int currentRunNumber;

    public int CurrentRunNumber => currentRunNumber;

    public void LogSessionStart()
    {
        AppendBlock(
            "==================================================",
            "SESSION START",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            "==================================================");
    }

    public void LogRunStart()
    {
        currentRunNumber = ++runCounter;
        AppendBlock(
            "--------------------------------------------------",
            $"RUN #{currentRunNumber}",
            "--------------------------------------------------");
    }

    public void LogStarterDice(DiceModel dice)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Starter Dice");
        AppendActiveFaces(builder, dice);
        Append(builder.ToString());
    }

    public void LogBattleStart(int stageIndex, StageType stageType, BattleCombatState combatState)
    {
        AppendBlock(
            "Battle",
            $"Stage {stageIndex}",
            stageType.ToString(),
            $"Player HP : {GetPlayerHpText(combatState)}",
            $"Enemy HP : {(combatState != null ? combatState.EnemyCurrentHp.ToString() : "Unknown")}");
    }

    public void LogDiceRoll(DiceFace selectedFace, int baseThrowDamage, FaceEffectData faceEffect, int faceDamage, int totalDamage)
    {
        AppendBlock(
            "Roll",
            GetFaceName(selectedFace),
            $"Base : {baseThrowDamage}",
            $"Effect : {GetFaceEffectText(faceEffect, faceDamage)}",
            $"Total Damage : {totalDamage}");
    }

    public void LogBattleResult(string result, BattleCombatState combatState)
    {
        AppendBlock(
            result,
            $"Remaining Player HP : {GetPlayerHpText(combatState)}",
            $"Remaining Enemy HP : {(combatState != null ? combatState.EnemyCurrentHp.ToString() : "Unknown")}");
    }

    public void LogRewardOptions(RewardData[] rewards)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Reward Options");

        if (rewards != null)
        {
            for (int i = 0; i < rewards.Length; i++)
            {
                builder.AppendLine(GetRewardText(rewards[i]));
            }
        }

        Append(builder.ToString());
    }

    public void LogRewardSelection(RewardData reward)
    {
        AppendBlock(
            "Selected Reward",
            GetRewardText(reward));
    }

    public void LogRewardApply(RewardData reward, BattleCombatState combatState, int previousPlayerHp, int previousPlayerMaxHp)
    {
        if (reward == null)
        {
            return;
        }

        switch (reward.RewardType)
        {
            case RewardType.Heal:
                AppendBlock(
                    "Reward Apply",
                    "Heal",
                    "HP",
                    $"{previousPlayerHp} -> {(combatState != null ? combatState.PlayerCurrentHp : previousPlayerHp)}");
                break;
            case RewardType.MaxHp:
                AppendBlock(
                    "Reward Apply",
                    "Max HP",
                    $"{previousPlayerMaxHp} -> {(combatState != null ? combatState.PlayerMaxHp : previousPlayerMaxHp)}",
                    "HP",
                    $"{previousPlayerHp} -> {(combatState != null ? combatState.PlayerCurrentHp : previousPlayerHp)}");
                break;
            case RewardType.Face:
                AppendBlock(
                    "Reward Apply",
                    "Pending Face",
                    GetFaceName(reward.Face));
                break;
            case RewardType.Relic:
                AppendBlock(
                    "Reward Apply",
                    "Relic",
                    GetRewardText(reward));
                break;
        }
    }

    public void LogFaceReplacement(DiceFace removedFace, DiceFace addedFace, int slotIndex, DiceModel resultingDice)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Replacement");
        builder.AppendLine(GetFaceName(removedFace));
        builder.AppendLine("↓");
        builder.AppendLine(GetFaceName(addedFace));
        builder.AppendLine($"Slot {slotIndex + 1}");
        builder.AppendLine("Resulting Dice");
        AppendActiveFaces(builder, resultingDice);
        Append(builder.ToString());
    }

    public void LogRunEnd(string result, LinearStageRuntimeState stageState, BattleCombatState combatState)
    {
        AppendBlock(
            result,
            $"Final Stage : {(stageState != null ? stageState.CurrentStageIndex.ToString() : "Unknown")}",
            $"Final HP : {GetPlayerHpText(combatState)}");
    }

    private void Awake()
    {
        try
        {
            EnsureLogFilePath();
        }
        catch (Exception)
        {
            // Developer logging must never interrupt gameplay.
        }
    }

    private void AppendBlock(params string[] lines)
    {
        Append(string.Join(Environment.NewLine, lines) + Environment.NewLine);
    }

    private void Append(string text)
    {
        try
        {
            EnsureLogFilePath();
            File.AppendAllText(logFilePath, text + Environment.NewLine);
        }
        catch (Exception)
        {
            // Developer logging must never interrupt gameplay.
        }
    }

    private void EnsureLogFilePath()
    {
        if (!string.IsNullOrWhiteSpace(logFilePath))
        {
            return;
        }

        string directoryPath = Path.Combine(Application.persistentDataPath, DirectoryName);
        Directory.CreateDirectory(directoryPath);
        logFilePath = Path.Combine(directoryPath, FileName);
    }

    private static void AppendActiveFaces(StringBuilder builder, DiceModel dice)
    {
        if (dice == null)
        {
            builder.AppendLine("Unknown");
            return;
        }

        for (int i = 0; i < dice.ActiveFaceSlotCount; i++)
        {
            builder.AppendLine(GetFaceName(dice.GetFace(i)));
        }
    }

    private static string GetPlayerHpText(BattleCombatState combatState)
    {
        return combatState != null
            ? $"{combatState.PlayerCurrentHp} / {combatState.PlayerMaxHp}"
            : "Unknown";
    }

    private static string GetFaceEffectText(FaceEffectData faceEffect, int faceDamage)
    {
        if (faceEffect == null || !faceEffect.IsImplemented)
        {
            return "No Effect";
        }

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return $"+{faceDamage}";
        }

        if (faceEffect.EffectType == FaceEffectType.Guard)
        {
            return $"Enemy Damage -{faceEffect.IncomingDamageReductionAmount}";
        }

        if (faceEffect.EffectType == FaceEffectType.Mend)
        {
            return $"+{faceEffect.HealAmount} HP";
        }

        return faceEffect.EffectType.ToString();
    }

    private static string GetRewardText(RewardData reward)
    {
        if (reward == null)
        {
            return "None";
        }

        if (reward.RewardType == RewardType.Face && reward.Face != null)
        {
            return $"{GetFaceName(reward.Face)} Face";
        }

        if (reward.RewardType == RewardType.Heal)
        {
            return $"Heal +{reward.Value}";
        }

        if (reward.RewardType == RewardType.MaxHp)
        {
            return $"Max HP +{reward.Value}";
        }

        return string.IsNullOrWhiteSpace(reward.DisplayName) ? reward.RewardType.ToString() : reward.DisplayName;
    }

    private static string GetFaceName(DiceFace face)
    {
        return face == null || string.IsNullOrWhiteSpace(face.DisplayName) ? "Unknown" : face.DisplayName;
    }
}
