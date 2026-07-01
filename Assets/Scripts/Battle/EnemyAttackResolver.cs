public static class EnemyAttackResolver
{
    public const int MvpFixedDamage = 5;

    public static EnemyAttackIntent Resolve(BattleTurnState battleTurnState)
    {
        if (battleTurnState == null ||
            battleTurnState.CurrentTurnOwner != BattleTurnOwner.EnemyTurn ||
            !battleTurnState.EnemyTurnPending)
        {
            return EnemyAttackIntent.None();
        }

        return EnemyAttackIntent.Damage(MvpFixedDamage);
    }
}
