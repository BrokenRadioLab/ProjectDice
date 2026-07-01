using UnityEngine;

public enum BattleTurnOwner
{
    PlayerTurn,
    Transition,
    EnemyTurn
}

public sealed class BattleTurnState : MonoBehaviour
{
    [SerializeField] private BattleTurnOwner currentTurnOwner = BattleTurnOwner.PlayerTurn;
    [SerializeField] private bool enemyTurnPending;

    public BattleTurnOwner CurrentTurnOwner => currentTurnOwner;
    public bool EnemyTurnPending => enemyTurnPending;
    public bool CanAcceptPlayerAction => currentTurnOwner == BattleTurnOwner.PlayerTurn && !enemyTurnPending;

    private void Awake()
    {
        BeginPlayerTurn();
    }

    public void BeginPlayerAction()
    {
        currentTurnOwner = BattleTurnOwner.Transition;
        enemyTurnPending = false;
    }

    public void PrepareEnemyTurn()
    {
        currentTurnOwner = BattleTurnOwner.EnemyTurn;
        enemyTurnPending = true;
    }

    public void BeginPlayerTurn()
    {
        currentTurnOwner = BattleTurnOwner.PlayerTurn;
        enemyTurnPending = false;
    }
}
