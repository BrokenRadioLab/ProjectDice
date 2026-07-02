public static class StarterDiceFactory
{
    private const int StarterBaseThrowDamage = 3;
    private const int StarterAttackModifierDamage = 5;
    private const int StarterLightningModifierDamage = 3;
    private const int WoodDiceActiveFaceSlotCount = 4;

    public static DiceModel CreateStarterDice()
    {
        return new DiceModel(CreateStarterFaces(), StarterBaseThrowDamage, WoodDiceActiveFaceSlotCount);
    }

    public static DiceFace[] CreateStarterFaces()
    {
        return new[]
        {
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_mend", "Mend", DiceFaceCategory.Skill, 0),
            null,
            null
        };
    }

    public static DiceFace[] CreateStarterFacePool()
    {
        return new[]
        {
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_mend", "Mend", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_lightning", "Lightning", DiceFaceCategory.Skill, StarterLightningModifierDamage)
        };
    }
}
