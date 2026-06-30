using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class ThrowSequencePresenter : MonoBehaviour
{
    [SerializeField] private RectTransform battleField;
    [SerializeField] private RectTransform heroSlot;
    [SerializeField] private RectTransform enemySlot;
    [SerializeField] private RectTransform diceAnimationLayer;
    [SerializeField] private Graphic heroPlaceholder;
    [SerializeField] private Graphic enemyPlaceholder;
    [SerializeField, Min(0.01f)] private float heroFeedbackDuration = 0.05f;
    [SerializeField, Min(0.01f)] private float projectileDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float enemyFlashDuration = 0.05f;
    [SerializeField, Min(0.01f)] private float diceLayerAppearDuration = 0.1f;
    [SerializeField, Min(0.01f)] private float diceRollingDuration = 0.45f;
    [SerializeField, Min(0.01f)] private float diceRollFrameDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float faceRevealDuration = 0.2f;
    [SerializeField, Min(0.01f)] private float faceEffectDuration = 0.15f;
    [SerializeField, Min(0.01f)] private float damageNumberDuration = 0.15f;
    [SerializeField, Min(1f)] private float projectileThickness = 3f;
    [SerializeField] private Vector2 rollingDiceSize = new Vector2(48f, 48f);
    [SerializeField] private Color projectileColor = Color.white;
    [SerializeField] private Color heroFeedbackColor = new Color(0.95f, 0.95f, 1f, 0.28f);
    [SerializeField] private Color enemyFlashColor = new Color(1f, 0.95f, 0.88f, 0.55f);
    [SerializeField] private Color rollingDiceColor = new Color(0.94f, 0.91f, 0.78f, 1f);
    [SerializeField] private Color faceRevealTextColor = new Color(0.12f, 0.1f, 0.08f, 1f);
    [SerializeField] private Color faceEffectTextColor = new Color(0.86f, 0.93f, 1f, 1f);
    [SerializeField] private Color damageNumberTextColor = new Color(1f, 0.78f, 0.42f, 1f);

    private Image projectileTrail;
    private Image rollingDiceVisual;
    private Text faceRevealText;
    private Text faceEffectText;
    private Text damageNumberText;
    private Color originalHeroColor;
    private Color originalEnemyColor;
    private static Font fallbackFont;
    private static readonly Vector2[] RollingFrameOffsets =
    {
        new Vector2(-10f, 2f),
        new Vector2(-3f, -4f),
        new Vector2(5f, 3f),
        new Vector2(10f, -2f),
        new Vector2(4f, 0f),
        Vector2.zero
    };

    private void Awake()
    {
        originalHeroColor = heroPlaceholder != null ? heroPlaceholder.color : Color.white;
        originalEnemyColor = enemyPlaceholder != null ? enemyPlaceholder.color : Color.white;
        EnsureProjectileTrail();
        HideProjectileTrail();
        EnsureRollingDiceVisual();
        HideRollingDiceVisual();
        EnsureFaceRevealText();
        HideFaceRevealText();
        EnsureFaceEffectText();
        HideFaceEffectText();
        EnsureDamageNumberText();
        HideDamageNumberText();
        HideDiceAnimationLayer();
    }

    public IEnumerator Play(DiceFace selectedFace, FaceEffectData faceEffect, int damageAmount)
    {
        CacheOriginalColors();

        if (heroPlaceholder != null)
        {
            heroPlaceholder.color = heroFeedbackColor;
        }

        yield return new WaitForSeconds(heroFeedbackDuration);

        if (heroPlaceholder != null)
        {
            heroPlaceholder.color = originalHeroColor;
        }

        ShowProjectileTrail();
        yield return new WaitForSeconds(projectileDuration);
        HideProjectileTrail();

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = enemyFlashColor;
        }

        yield return new WaitForSeconds(enemyFlashDuration);

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = originalEnemyColor;
        }

        ShowDiceAnimationLayer();
        yield return new WaitForSeconds(diceLayerAppearDuration);
        yield return PlayRollingPresentation();
        yield return PlayFaceReveal(selectedFace);
        yield return PlayFaceEffect(faceEffect);
        yield return PlayDamageNumber(damageAmount);
        HideDiceAnimationLayer();
    }

    private void CacheOriginalColors()
    {
        if (heroPlaceholder != null)
        {
            originalHeroColor = heroPlaceholder.color;
        }

        if (enemyPlaceholder != null)
        {
            originalEnemyColor = enemyPlaceholder.color;
        }
    }

    private void EnsureProjectileTrail()
    {
        if (projectileTrail != null || battleField == null)
        {
            return;
        }

        GameObject trailObject = new GameObject("Throw Projectile Trail");
        trailObject.layer = battleField.gameObject.layer;
        trailObject.transform.SetParent(battleField, false);

        RectTransform trailTransform = trailObject.AddComponent<RectTransform>();
        trailTransform.anchorMin = new Vector2(0.5f, 0.5f);
        trailTransform.anchorMax = new Vector2(0.5f, 0.5f);
        trailTransform.pivot = new Vector2(0f, 0.5f);

        projectileTrail = trailObject.AddComponent<Image>();
        projectileTrail.raycastTarget = false;
        projectileTrail.color = projectileColor;
    }

    private void ShowProjectileTrail()
    {
        EnsureProjectileTrail();

        if (projectileTrail == null || battleField == null || heroSlot == null || enemySlot == null)
        {
            return;
        }

        RectTransform trailTransform = projectileTrail.rectTransform;
        Vector2 start = GetLocalEdgePoint(heroSlot, true);
        Vector2 end = GetLocalEdgePoint(enemySlot, false);
        Vector2 delta = end - start;

        trailTransform.anchoredPosition = start;
        trailTransform.sizeDelta = new Vector2(delta.magnitude, projectileThickness);
        trailTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
        projectileTrail.color = projectileColor;
        projectileTrail.gameObject.SetActive(true);
    }

    private Vector2 GetLocalEdgePoint(RectTransform source, bool rightEdge)
    {
        Vector3[] corners = new Vector3[4];
        source.GetWorldCorners(corners);

        Vector3 worldPoint = rightEdge
            ? (corners[2] + corners[3]) * 0.5f
            : (corners[0] + corners[1]) * 0.5f;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            battleField,
            RectTransformUtility.WorldToScreenPoint(null, worldPoint),
            null,
            out Vector2 localPoint);

        return localPoint;
    }

    private void HideProjectileTrail()
    {
        if (projectileTrail != null)
        {
            projectileTrail.gameObject.SetActive(false);
        }
    }

    private void ShowDiceAnimationLayer()
    {
        EnsureDiceAnimationLayer();

        if (diceAnimationLayer != null)
        {
            diceAnimationLayer.gameObject.SetActive(true);
        }

        EnsureRollingDiceVisual();
    }

    private void HideDiceAnimationLayer()
    {
        EnsureDiceAnimationLayer();

        if (diceAnimationLayer != null)
        {
            diceAnimationLayer.gameObject.SetActive(false);
        }

        HideRollingDiceVisual();
        HideFaceRevealText();
        HideFaceEffectText();
        HideDamageNumberText();
    }

    private void EnsureDiceAnimationLayer()
    {
        if (diceAnimationLayer != null)
        {
            return;
        }

        GameObject layerObject = null;

        if (battleField != null)
        {
            Transform layerTransform = battleField.Find("DiceAnimationLayer");
            layerObject = layerTransform != null ? layerTransform.gameObject : null;
        }

        layerObject ??= GameObject.Find("DiceAnimationLayer");

        if (layerObject != null)
        {
            diceAnimationLayer = layerObject.GetComponent<RectTransform>();
        }
    }

    private IEnumerator PlayRollingPresentation()
    {
        EnsureRollingDiceVisual();

        if (rollingDiceVisual == null)
        {
            yield break;
        }

        RectTransform diceTransform = rollingDiceVisual.rectTransform;
        rollingDiceVisual.gameObject.SetActive(true);

        float elapsed = 0f;
        int frameIndex = 0;

        while (elapsed < diceRollingDuration)
        {
            Vector2 offset = RollingFrameOffsets[frameIndex % RollingFrameOffsets.Length];
            diceTransform.anchoredPosition = offset;
            diceTransform.localEulerAngles = new Vector3(0f, 0f, (frameIndex % 4) * 90f);
            rollingDiceVisual.color = frameIndex % 2 == 0
                ? rollingDiceColor
                : new Color(rollingDiceColor.r * 0.82f, rollingDiceColor.g * 0.82f, rollingDiceColor.b * 0.82f, rollingDiceColor.a);

            float waitDuration = Mathf.Min(diceRollFrameDuration, diceRollingDuration - elapsed);
            yield return new WaitForSeconds(waitDuration);

            elapsed += waitDuration;
            frameIndex++;
        }

        diceTransform.anchoredPosition = Vector2.zero;
        diceTransform.localEulerAngles = Vector3.zero;
        rollingDiceVisual.color = rollingDiceColor;
        HideFaceRevealText();
    }

    private void EnsureRollingDiceVisual()
    {
        if (rollingDiceVisual != null)
        {
            return;
        }

        EnsureDiceAnimationLayer();

        if (diceAnimationLayer == null)
        {
            return;
        }

        GameObject diceObject = new GameObject("Rolling Dice Placeholder");
        diceObject.layer = diceAnimationLayer.gameObject.layer;
        diceObject.transform.SetParent(diceAnimationLayer, false);

        RectTransform diceTransform = diceObject.AddComponent<RectTransform>();
        diceTransform.anchorMin = new Vector2(0.5f, 0.5f);
        diceTransform.anchorMax = new Vector2(0.5f, 0.5f);
        diceTransform.pivot = new Vector2(0.5f, 0.5f);
        diceTransform.anchoredPosition = Vector2.zero;
        diceTransform.sizeDelta = rollingDiceSize;

        rollingDiceVisual = diceObject.AddComponent<Image>();
        rollingDiceVisual.raycastTarget = false;
        rollingDiceVisual.color = rollingDiceColor;
    }

    private void HideRollingDiceVisual()
    {
        if (rollingDiceVisual != null)
        {
            rollingDiceVisual.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayFaceReveal(DiceFace selectedFace)
    {
        EnsureRollingDiceVisual();
        EnsureFaceRevealText();

        if (rollingDiceVisual == null || faceRevealText == null)
        {
            yield break;
        }

        RectTransform diceTransform = rollingDiceVisual.rectTransform;
        diceTransform.anchoredPosition = Vector2.zero;
        diceTransform.localEulerAngles = Vector3.zero;
        rollingDiceVisual.color = rollingDiceColor;
        rollingDiceVisual.gameObject.SetActive(true);

        faceRevealText.text = selectedFace != null ? selectedFace.DisplayName : "?";
        faceRevealText.gameObject.SetActive(true);

        yield return new WaitForSeconds(faceRevealDuration);
    }

    private IEnumerator PlayDamageNumber(int damageAmount)
    {
        EnsureDamageNumberText();

        if (damageNumberText == null)
        {
            yield break;
        }

        RectTransform damageTransform = damageNumberText.rectTransform;
        damageTransform.anchoredPosition = new Vector2(0f, 42f);
        damageNumberText.text = damageAmount.ToString();
        damageNumberText.gameObject.SetActive(true);

        yield return new WaitForSeconds(damageNumberDuration);
    }

    private IEnumerator PlayFaceEffect(FaceEffectData faceEffect)
    {
        EnsureFaceEffectText();

        if (faceEffectText == null)
        {
            yield break;
        }

        RectTransform effectTransform = faceEffectText.rectTransform;
        effectTransform.anchoredPosition = new Vector2(0f, -42f);
        faceEffectText.text = GetFaceEffectText(faceEffect);
        faceEffectText.gameObject.SetActive(true);

        yield return new WaitForSeconds(faceEffectDuration);
    }

    private void EnsureFaceRevealText()
    {
        if (faceRevealText != null)
        {
            return;
        }

        EnsureRollingDiceVisual();

        if (rollingDiceVisual == null)
        {
            return;
        }

        GameObject textObject = new GameObject("Dice Face Reveal Text");
        textObject.layer = rollingDiceVisual.gameObject.layer;
        textObject.transform.SetParent(rollingDiceVisual.transform, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = Vector2.zero;
        textTransform.anchorMax = Vector2.one;
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = Vector2.zero;
        textTransform.sizeDelta = Vector2.zero;

        faceRevealText = textObject.AddComponent<Text>();
        faceRevealText.raycastTarget = false;
        faceRevealText.alignment = TextAnchor.MiddleCenter;
        faceRevealText.fontSize = 14;
        faceRevealText.resizeTextForBestFit = true;
        faceRevealText.resizeTextMinSize = 8;
        faceRevealText.resizeTextMaxSize = 14;
        faceRevealText.color = faceRevealTextColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        faceRevealText.font = fallbackFont;
    }

    private void HideFaceRevealText()
    {
        if (faceRevealText != null)
        {
            faceRevealText.gameObject.SetActive(false);
        }
    }

    private void EnsureFaceEffectText()
    {
        if (faceEffectText != null)
        {
            return;
        }

        EnsureDiceAnimationLayer();

        if (diceAnimationLayer == null)
        {
            return;
        }

        GameObject textObject = new GameObject("Face Effect Text");
        textObject.layer = diceAnimationLayer.gameObject.layer;
        textObject.transform.SetParent(diceAnimationLayer, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = new Vector2(0f, -42f);
        textTransform.sizeDelta = new Vector2(112f, 28f);

        faceEffectText = textObject.AddComponent<Text>();
        faceEffectText.raycastTarget = false;
        faceEffectText.alignment = TextAnchor.MiddleCenter;
        faceEffectText.fontSize = 16;
        faceEffectText.resizeTextForBestFit = true;
        faceEffectText.resizeTextMinSize = 10;
        faceEffectText.resizeTextMaxSize = 16;
        faceEffectText.color = faceEffectTextColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        faceEffectText.font = fallbackFont;
    }

    private void HideFaceEffectText()
    {
        if (faceEffectText != null)
        {
            faceEffectText.gameObject.SetActive(false);
        }
    }

    private static string GetFaceEffectText(FaceEffectData faceEffect)
    {
        if (faceEffect == null || !faceEffect.IsImplemented)
        {
            return "No Effect";
        }

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return "Damage";
        }

        return "Effect";
    }

    private void EnsureDamageNumberText()
    {
        if (damageNumberText != null)
        {
            return;
        }

        EnsureDiceAnimationLayer();

        if (diceAnimationLayer == null)
        {
            return;
        }

        GameObject textObject = new GameObject("Damage Number Text");
        textObject.layer = diceAnimationLayer.gameObject.layer;
        textObject.transform.SetParent(diceAnimationLayer, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = new Vector2(0f, 42f);
        textTransform.sizeDelta = new Vector2(96f, 32f);

        damageNumberText = textObject.AddComponent<Text>();
        damageNumberText.raycastTarget = false;
        damageNumberText.alignment = TextAnchor.MiddleCenter;
        damageNumberText.fontSize = 24;
        damageNumberText.resizeTextForBestFit = true;
        damageNumberText.resizeTextMinSize = 12;
        damageNumberText.resizeTextMaxSize = 24;
        damageNumberText.color = damageNumberTextColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        damageNumberText.font = fallbackFont;
    }

    private void HideDamageNumberText()
    {
        if (damageNumberText != null)
        {
            damageNumberText.gameObject.SetActive(false);
        }
    }
}
