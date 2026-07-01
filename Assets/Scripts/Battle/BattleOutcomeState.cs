using UnityEngine;

public enum BattleOutcome
{
    InProgress,
    Victory,
    Defeat
}

public sealed class BattleOutcomeState : MonoBehaviour
{
    [SerializeField] private BattleOutcome currentOutcome = BattleOutcome.InProgress;

    public BattleOutcome CurrentOutcome => currentOutcome;
    public bool IsInProgress => currentOutcome == BattleOutcome.InProgress;
    public bool IsVictory => currentOutcome == BattleOutcome.Victory;
    public bool IsDefeat => currentOutcome == BattleOutcome.Defeat;

    private void Awake()
    {
        ResetOutcome();
    }

    public void ResetOutcome()
    {
        currentOutcome = BattleOutcome.InProgress;
    }

    public void MarkVictory()
    {
        currentOutcome = BattleOutcome.Victory;
    }

    public void MarkDefeat()
    {
        currentOutcome = BattleOutcome.Defeat;
    }
}
