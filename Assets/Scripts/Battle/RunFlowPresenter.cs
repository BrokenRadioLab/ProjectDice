using System.Collections;
using UnityEngine;

public sealed class RunFlowPresenter : MonoBehaviour
{
    public BattleOutcome LastObservedOutcome { get; private set; } = BattleOutcome.InProgress;
    public int LastObservedStageIndex { get; private set; }
    public StageType LastObservedStageType { get; private set; } = StageType.Normal;
    public bool LastObservedRunCompleted { get; private set; }

    public IEnumerator PlayBattleOutcome(
        BattleOutcomeState outcomeState,
        LinearStageRuntimeState stageRuntimeState,
        LinearRunState runState)
    {
        if (outcomeState == null || outcomeState.IsInProgress)
        {
            yield break;
        }

        LastObservedOutcome = outcomeState.CurrentOutcome;

        if (stageRuntimeState != null)
        {
            LastObservedStageIndex = stageRuntimeState.CurrentStageIndex;
            LastObservedStageType = stageRuntimeState.CurrentStageType;
        }

        LastObservedRunCompleted = runState != null && runState.IsCompleted;

        yield break;
    }
}
