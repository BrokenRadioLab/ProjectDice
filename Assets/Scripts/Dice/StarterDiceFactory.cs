public static class StarterDiceFactory
{
    private const int StarterBaseThrowDamage = 3;
    private const int StarterAttackModifierDamage = 5;
    private const int StarterGuardIncomingDamageReduction = 3;
    private const int StarterLightningModifierDamage = 3;
    private const int StarterMendHealAmount = 5;
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
            new DiceFace(
                "starter_attack",
                "Attack",
                FaceCategory.Attack,
                FaceRarity.Common,
                WoodDiceTier,
                FaceEffectType.Damage,
                new FaceEffectParameters(StarterAttackModifierDamage),
                "Deal +5 damage.",
                string.Empty,
                null,
                true,
                true),
            new DiceFace(
                "starter_guard",
                "Guard",
                FaceCategory.Defense,
                FaceRarity.Common,
                WoodDiceTier,
                FaceEffectType.Guard,
                new FaceEffectParameters(StarterGuardIncomingDamageReduction),
                "Reduce incoming enemy damage by 3.",
                string.Empty,
                null,
                true,
                true),
            new DiceFace(
                "starter_lightning",
                "Lightning",
                FaceCategory.Attack,
                FaceRarity.Common,
                WoodDiceTier,
                FaceEffectType.Damage,
                new FaceEffectParameters(StarterLightningModifierDamage),
                "Deal +3 lightning damage.",
                string.Empty,
                null,
                true,
                true),
            new DiceFace(
                "starter_mend",
                "Mend",
                FaceCategory.Recovery,
                FaceRarity.Common,
                WoodDiceTier,
                FaceEffectType.Heal,
                new FaceEffectParameters(StarterMendHealAmount),
                "Heal 5 HP.",
                string.Empty,
                null,
                true,
                true),
        };
    }
}
