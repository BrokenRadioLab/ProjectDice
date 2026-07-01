using UnityEngine;

public sealed class EnemyGroupState : MonoBehaviour
{
    [SerializeField] private BattleCombatState singleEnemyCombatState;
    [SerializeField] private bool enemySlot01Active = true;
    [SerializeField] private bool enemySlot02Active;
    [SerializeField] private bool enemySlot03Active;

    public bool EnemySlot01Active => enemySlot01Active;
    public bool EnemySlot02Active => enemySlot02Active;
    public bool EnemySlot03Active => enemySlot03Active;

    public bool AreAllEnemiesDefeated
    {
        get
        {
            bool hasActiveEnemy = false;

            if (enemySlot01Active)
            {
                hasActiveEnemy = true;
                if (singleEnemyCombatState == null || !singleEnemyCombatState.IsEnemyDefeated)
                {
                    return false;
                }
            }

            if (enemySlot02Active || enemySlot03Active)
            {
                return false;
            }

            return hasActiveEnemy;
        }
    }
}
