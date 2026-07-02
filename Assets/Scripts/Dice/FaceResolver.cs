public static class FaceResolver
{
    private const string StarterAttackFaceId = "starter_attack";
    private const string StarterGuardFaceId = "starter_guard";
    private const string StarterLightningFaceId = "starter_lightning";
    private const string StarterMendFaceId = "starter_mend";
    private const int StarterGuardIncomingDamageReduction = 3;
    private const int StarterMendHealAmount = 5;

    public static FaceEffectData Resolve(DiceFace selectedFace)
    {
        if (selectedFace == null)
        {
            return FaceEffectData.None(null);
        }

        if (selectedFace.Id == StarterAttackFaceId)
        {
            return FaceEffectData.Damage(selectedFace, selectedFace.FixedThrowDamageValue);
        }

        if (selectedFace.Id == StarterLightningFaceId)
        {
            return FaceEffectData.Damage(selectedFace, selectedFace.FixedThrowDamageValue);
        }

        if (selectedFace.Id == StarterGuardFaceId)
        {
            return FaceEffectData.Guard(selectedFace, StarterGuardIncomingDamageReduction);
        }

        if (selectedFace.Id == StarterMendFaceId)
        {
            return FaceEffectData.Mend(selectedFace, StarterMendHealAmount);
        }

        return FaceEffectData.None(selectedFace);
    }
}
