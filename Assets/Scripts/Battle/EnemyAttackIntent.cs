using System;
using UnityEngine;

public enum EnemyAttackIntentType
{
    None,
    Damage
}

[Serializable]
public sealed class EnemyAttackIntent
{
    [SerializeField] private EnemyAttackIntentType intentType;
    [SerializeField, Min(0)] private int damageAmount;

    public EnemyAttackIntentType IntentType => intentType;
    public int DamageAmount => damageAmount;
    public bool HasDamage => intentType == EnemyAttackIntentType.Damage && damageAmount > 0;

    private EnemyAttackIntent(EnemyAttackIntentType intentType, int damageAmount)
    {
        this.intentType = intentType;
        this.damageAmount = Mathf.Max(0, damageAmount);
    }

    public static EnemyAttackIntent None()
    {
        return new EnemyAttackIntent(EnemyAttackIntentType.None, 0);
    }

    public static EnemyAttackIntent Damage(int damageAmount)
    {
        return new EnemyAttackIntent(EnemyAttackIntentType.Damage, damageAmount);
    }
}
