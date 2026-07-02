using System;
using UnityEngine;

public enum FaceEffectType
{
    None,
    Damage,
    Guard
}

[Serializable]
public sealed class FaceEffectData
{
    [SerializeField] private string sourceFaceId;
    [SerializeField] private string sourceFaceDisplayName;
    [SerializeField] private FaceEffectType effectType;
    [SerializeField, Min(0)] private int damageAmount;
    [SerializeField, Min(0)] private int incomingDamageReductionAmount;
    [SerializeField] private bool isImplemented;

    public string SourceFaceId => sourceFaceId;
    public string SourceFaceDisplayName => sourceFaceDisplayName;
    public FaceEffectType EffectType => effectType;
    public int DamageAmount => damageAmount;
    public int IncomingDamageReductionAmount => incomingDamageReductionAmount;
    public bool IsImplemented => isImplemented;

    public FaceEffectData(
        string sourceFaceId,
        string sourceFaceDisplayName,
        FaceEffectType effectType,
        int damageAmount,
        int incomingDamageReductionAmount,
        bool isImplemented)
    {
        this.sourceFaceId = sourceFaceId;
        this.sourceFaceDisplayName = sourceFaceDisplayName;
        this.effectType = effectType;
        this.damageAmount = Mathf.Max(0, damageAmount);
        this.incomingDamageReductionAmount = Mathf.Max(0, incomingDamageReductionAmount);
        this.isImplemented = isImplemented;
    }

    public static FaceEffectData None(DiceFace sourceFace)
    {
        return FromFace(sourceFace, FaceEffectType.None, 0, 0, false);
    }

    public static FaceEffectData Damage(DiceFace sourceFace, int damageAmount)
    {
        return FromFace(sourceFace, FaceEffectType.Damage, damageAmount, 0, true);
    }

    public static FaceEffectData Guard(DiceFace sourceFace, int incomingDamageReductionAmount)
    {
        return FromFace(sourceFace, FaceEffectType.Guard, 0, incomingDamageReductionAmount, true);
    }

    private static FaceEffectData FromFace(
        DiceFace sourceFace,
        FaceEffectType effectType,
        int damageAmount,
        int incomingDamageReductionAmount,
        bool isImplemented)
    {
        return new FaceEffectData(
            sourceFace != null ? sourceFace.Id : string.Empty,
            sourceFace != null ? sourceFace.DisplayName : string.Empty,
            effectType,
            damageAmount,
            incomingDamageReductionAmount,
            isImplemented);
    }
}
