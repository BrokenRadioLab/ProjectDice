using UnityEngine;

public sealed class BattleCombatState : MonoBehaviour
{
    [SerializeField, Min(1)] private int playerMaxHp = 30;
    [SerializeField, Min(0)] private int playerCurrentHp = 30;
    [SerializeField, Min(1)] private int enemyMaxHp = 20;
    [SerializeField, Min(0)] private int enemyCurrentHp = 20;

    public int PlayerMaxHp => playerMaxHp;
    public int PlayerCurrentHp => playerCurrentHp;
    public int EnemyMaxHp => enemyMaxHp;
    public int EnemyCurrentHp => enemyCurrentHp;
    public bool IsPlayerDefeated => playerCurrentHp <= 0;
    public bool IsEnemyDefeated => enemyCurrentHp <= 0;

    public int ApplyDamageToPlayer(int damage)
    {
        int previousHp = playerCurrentHp;
        playerCurrentHp = Mathf.Max(0, playerCurrentHp - Mathf.Max(0, damage));
        return previousHp - playerCurrentHp;
    }

    public int ApplyDamageToEnemy(int damage)
    {
        if (IsEnemyDefeated)
        {
            return 0;
        }

        int previousHp = enemyCurrentHp;
        enemyCurrentHp = Mathf.Max(0, enemyCurrentHp - Mathf.Max(0, damage));
        return previousHp - enemyCurrentHp;
    }

    public int HealPlayer(int healing)
    {
        int previousHp = playerCurrentHp;
        playerCurrentHp = Mathf.Min(playerMaxHp, playerCurrentHp + Mathf.Max(0, healing));
        return playerCurrentHp - previousHp;
    }

    public int IncreasePlayerMaxHpForRun(int amount)
    {
        int appliedAmount = Mathf.Max(0, amount);
        if (appliedAmount <= 0)
        {
            return 0;
        }

        playerMaxHp += appliedAmount;
        playerCurrentHp = Mathf.Min(playerMaxHp, playerCurrentHp + appliedAmount);
        return appliedAmount;
    }

    public void PrepareNextEnemy()
    {
        enemyCurrentHp = enemyMaxHp;
    }

    private void OnValidate()
    {
        playerCurrentHp = Mathf.Clamp(playerCurrentHp, 0, playerMaxHp);
        enemyCurrentHp = Mathf.Clamp(enemyCurrentHp, 0, enemyMaxHp);
    }
}
