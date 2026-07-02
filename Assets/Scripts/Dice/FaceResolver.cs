public static class FaceResolver
{
    private const string StarterAttackFaceId = "starter_attack";
    private const string StarterGuardFaceId = "starter_guard";
    private const int StarterGuardIncomingDamageReduction = 3;

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

        if (selectedFace.Id == StarterGuardFaceId)
        {
            return FaceEffectData.Guard(selectedFace, StarterGuardIncomingDamageReduction);
        }

        return FaceEffectData.None(selectedFace);
    }
}
