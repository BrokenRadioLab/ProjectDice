using UnityEngine;

public enum StageType
{
    Normal,
    Elite,
    Boss
}

public sealed class LinearStageRuntimeState : MonoBehaviour
{
    private static readonly StageType[] StageOrder =
    {
        StageType.Normal,
        StageType.Normal,
        StageType.Normal,
        StageType.Elite,
        StageType.Boss
    };

    [SerializeField, Min(1)] private int currentStageIndex = 1;

    public int CurrentStageIndex => Mathf.Clamp(currentStageIndex, 1, StageOrder.Length);
    public StageType CurrentStageType => StageOrder[CurrentStageIndex - 1];
    public bool IsBossStage => CurrentStageType == StageType.Boss;

    public bool TryAdvanceToNextStage()
    {
        if (IsBossStage)
        {
            return false;
        }

        currentStageIndex = Mathf.Min(CurrentStageIndex + 1, StageOrder.Length);
        return true;
    }

    private void Awake()
    {
        ClampStageIndex();
    }

    private void OnValidate()
    {
        ClampStageIndex();
    }

    private void ClampStageIndex()
    {
        currentStageIndex = Mathf.Clamp(currentStageIndex, 1, StageOrder.Length);
    }
}
