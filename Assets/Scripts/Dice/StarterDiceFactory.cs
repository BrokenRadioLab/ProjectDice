public static class StarterDiceFactory
{
    private const int StarterBaseThrowDamage = 3;
    private const int StarterAttackModifierDamage = 5;
    private const int StarterLightningModifierDamage = 3;
    private const int WoodDiceActiveFaceSlotCount = 4;
    private const int WoodDiceTier = 1;

    public static DiceModel CreateStarterDice()
    {
        return new DiceModel(CreateStarterFaces(), StarterBaseThrowDamage, WoodDiceActiveFaceSlotCount);
    }

    public static DiceFace[] CreateStarterFaces()
    {
        DiceFace[] unlockedFaces = CreateUnlockedFacesForDiceTier(WoodDiceTier);
        return new[]
        {
            unlockedFaces[0].Clone(),
            unlockedFaces[1].Clone(),
            unlockedFaces[2].Clone(),
            unlockedFaces[3].Clone(),
            null,
            null
        };
    }

    public static DiceFace[] CreateUnlockedFacesForDiceTier(int currentDiceTier)
    {
        DiceFace[] unlockedFaces = CreatePermanentlyUnlockedFaces();
        int validCount = 0;

        for (int i = 0; i < unlockedFaces.Length; i++)
        {
            if (unlockedFaces[i].FaceTier <= currentDiceTier)
            {
                validCount++;
            }
        }

        DiceFace[] validFaces = new DiceFace[validCount];
        int nextIndex = 0;

        for (int i = 0; i < unlockedFaces.Length; i++)
        {
            if (unlockedFaces[i].FaceTier <= currentDiceTier)
            {
                validFaces[nextIndex] = unlockedFaces[i].Clone();
                nextIndex++;
            }
        }

        return validFaces;
    }

    private static DiceFace[] CreatePermanentlyUnlockedFaces()
    {
        return new[]
        {
            new DiceFace("starter_attack", "Attack", DiceFaceCategory.Weapon, StarterAttackModifierDamage),
            new DiceFace("starter_guard", "Guard", DiceFaceCategory.Skill, 0),
            new DiceFace("starter_lightning", "Lightning", DiceFaceCategory.Skill, StarterLightningModifierDamage),
            new DiceFace("starter_mend", "Mend", DiceFaceCategory.Skill, 0),
        };
    }
}
