public static class FaceResolver
{
    private const string StarterAttackFaceId = "starter_attack";

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

        return FaceEffectData.None(selectedFace);
    }
}
