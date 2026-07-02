public static class StarterDiceFactory
{
    private const int StarterBaseThrowDamage = 5;
    private const int StarterAttackModifierDamage = 5;

    public static DiceModel CreateStarterDice()
    {
        return new DiceModel(CreateStarterFaces(), StarterBaseThrowDamage);
    }

    public static DiceFace[] CreateStarterFaces()
    {
        return new[]
        {
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_spark", "Spark", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_mend", "Mend", DiceFaceCategory.Skill, 0)
        };
    }
}
