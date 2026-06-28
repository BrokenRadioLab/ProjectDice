public static class StarterDiceFactory
{
    public static DiceModel CreateStarterDice()
    {
        return new DiceModel(CreateStarterFaces());
    }

    public static DiceFace[] CreateStarterFaces()
    {
        return new[]
        {
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, 5),
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, 5),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_spark", "Spark", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_mend", "Mend", DiceFaceCategory.Skill, 0)
        };
    }
}
