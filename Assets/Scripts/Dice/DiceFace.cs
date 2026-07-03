using System;
using UnityEngine;

public enum DiceFaceCategory
{
    Weapon,
    Skill
}

[Serializable]
public sealed class DiceFace
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private DiceFaceCategory category;
    [SerializeField, Min(1)] private int faceTier = 1;
    [SerializeField, Min(0)] private int fixedThrowDamageValue;

    public string Id => id;
    public string DisplayName => displayName;
    public DiceFaceCategory Category => category;
    public int FaceTier => Mathf.Max(1, faceTier);
    public int FixedThrowDamageValue => fixedThrowDamageValue;

    public DiceFace(string id, string displayName, DiceFaceCategory category, int fixedThrowDamageValue, int faceTier = 1)
    {
        this.id = id;
        this.displayName = displayName;
        this.category = category;
        this.faceTier = Mathf.Max(1, faceTier);
        this.fixedThrowDamageValue = Mathf.Max(0, fixedThrowDamageValue);
    }

    public DiceFace Clone()
    {
        return new DiceFace(id, displayName, category, fixedThrowDamageValue, FaceTier);
    }
}
