using System;
using UnityEngine;

public enum FaceRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public enum FaceCategory
{
    Attack,
    Defense,
    Recovery,
    Utility,
    Status,
    Summon,
    Special
}

[Serializable]
public struct FaceEffectParameters
{
    [SerializeField] private int primaryValue;
    [SerializeField] private int secondaryValue;
    [SerializeField] private int duration;
    [SerializeField] private float chance;

    public int PrimaryValue => primaryValue;
    public int SecondaryValue => secondaryValue;
    public int Duration => duration;
    public float Chance => chance;

    public FaceEffectParameters(int primaryValue, int secondaryValue = 0, int duration = 0, float chance = 1f)
    {
        this.primaryValue = Mathf.Max(0, primaryValue);
        this.secondaryValue = Mathf.Max(0, secondaryValue);
        this.duration = Mathf.Max(0, duration);
        this.chance = Mathf.Clamp01(chance);
    }
}

[Serializable]
public sealed class DiceFace
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private FaceCategory category;
    [SerializeField] private FaceRarity rarity;
    [SerializeField, Min(1)] private int faceTier = 1;
    [SerializeField] private FaceEffectType effectType;
    [SerializeField] private FaceEffectParameters effectParameters;
    [SerializeField] private string shortDescription;
    [SerializeField] private string flavorText;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool isStarterFace;
    [SerializeField] private bool isUnlockedByDefault;

    public string Id => id;
    public string DisplayName => displayName;
    public FaceCategory Category => category;
    public FaceRarity Rarity => rarity;
    public int Tier => FaceTier;
    public int FaceTier => Mathf.Max(1, faceTier);
    public FaceEffectType EffectType => effectType;
    public FaceEffectParameters EffectParameters => effectParameters;
    public string ShortDescription => shortDescription;
    public string FlavorText => flavorText;
    public Sprite Icon => icon;
    public bool IsStarterFace => isStarterFace;
    public bool IsUnlockedByDefault => isUnlockedByDefault;
    public int FixedThrowDamageValue => effectType == FaceEffectType.Damage ? effectParameters.PrimaryValue : 0;

    public DiceFace(
        string id,
        string displayName,
        FaceCategory category,
        FaceRarity rarity,
        int faceTier,
        FaceEffectType effectType,
        FaceEffectParameters effectParameters,
        string shortDescription,
        string flavorText,
        Sprite icon = null,
        bool isStarterFace = false,
        bool isUnlockedByDefault = false)
    {
        this.id = id ?? string.Empty;
        this.displayName = displayName ?? string.Empty;
        this.category = category;
        this.rarity = rarity;
        this.faceTier = Mathf.Max(1, faceTier);
        this.effectType = effectType;
        this.effectParameters = effectParameters;
        this.shortDescription = shortDescription ?? string.Empty;
        this.flavorText = flavorText ?? string.Empty;
        this.icon = icon;
        this.isStarterFace = isStarterFace;
        this.isUnlockedByDefault = isUnlockedByDefault;
    }

    public DiceFace(string id, string displayName, FaceCategory category, int fixedThrowDamageValue, int faceTier = 1)
        : this(
            id,
            displayName,
            category,
            FaceRarity.Common,
            faceTier,
            FaceEffectType.Damage,
            new FaceEffectParameters(fixedThrowDamageValue),
            string.Empty,
            string.Empty)
    {
    }

    public DiceFace Clone()
    {
        return new DiceFace(
            id,
            displayName,
            category,
            rarity,
            FaceTier,
            effectType,
            effectParameters,
            shortDescription,
            flavorText,
            icon,
            isStarterFace,
            isUnlockedByDefault);
    }
}
