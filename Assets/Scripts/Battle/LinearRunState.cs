using UnityEngine;

public enum LinearRunStatus
{
    InProgress,
    Completed
}

public sealed class LinearRunState : MonoBehaviour
{
    [SerializeField] private LinearRunStatus currentStatus = LinearRunStatus.InProgress;

    public LinearRunStatus CurrentStatus => currentStatus;
    public bool IsInProgress => currentStatus == LinearRunStatus.InProgress;
    public bool IsCompleted => currentStatus == LinearRunStatus.Completed;

    private void Awake()
    {
        ResetRun();
    }

    public void ResetRun()
    {
        currentStatus = LinearRunStatus.InProgress;
    }

    public void MarkCompleted()
    {
        currentStatus = LinearRunStatus.Completed;
    }
}
