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
    [SerializeField] private Image heroSpriteImage;
    [SerializeField] private Graphic enemyPlaceholder;
    [SerializeField] private Image enemySpriteImage;
    [SerializeField] private Texture2D[] heroIdleTextures;
    [SerializeField] private Texture2D[] heroThrowTextures;
    [SerializeField] private Texture2D[] heroHitTextures;
    [SerializeField] private Texture2D[] enemyIdleTextures;
    [SerializeField, Min(0.01f)] private float heroIdleFrameDuration = 0.18f;
    [SerializeField, Min(0.01f)] private float heroThrowFrameDuration = 0.05f;
    [SerializeField, Min(0.01f)] private float heroHitFrameDuration = 0.06f;
    [SerializeField, Min(0.01f)] private float enemyIdleFrameDuration = 0.18f;
    [SerializeField, Min(0)] private int projectileSpawnThrowFrame = 5;
    [SerializeField, Min(0.01f)] private float projectileDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float enemyFlashDuration = 0.12f;
    [SerializeField, Min(0.01f)] private float diceLayerAppearDuration = 0.1f;
    [SerializeField, Min(0.01f)] private float diceRollingDuration = 0.45f;
    [SerializeField, Min(0.01f)] private float diceRollFrameDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float faceRevealDuration = 0.2f;
    [SerializeField, Min(0.01f)] private float faceEffectDuration = 1.25f;
    [SerializeField, Min(0.01f)] private float damageNumberDuration = 0.9f;
    [SerializeField, Min(0f)] private float damageNumberHoldDuration = 0.18f;
    [SerializeField, Min(1f)] private float projectileThickness = 3f;
    [SerializeField] private Vector2 rollingDiceSize = new Vector2(288f, 288f);
    [SerializeField] private Vector2 diceResultPosition = new Vector2(0f, -56f);
    [SerializeField] private Vector2 enemyDamageNumberOffset = new Vector2(0f, 72f);
    [SerializeField] private Vector2 enemyDamageNumberFloatOffset = new Vector2(0f, 42f);
    [SerializeField] private Vector2 heroPopupOffset = new Vector2(0f, 76f);
    [SerializeField] private Vector2 heroPopupFloatOffset = new Vector2(0f, 42f);
    [SerializeField] private Vector2 enemyHitShakeOffset = new Vector2(10f, 0f);
    [SerializeField] private Color projectileColor = Color.white;
    [SerializeField] private Color enemyFlashColor = new Color(1f, 0.95f, 0.88f, 0.55f);
    [SerializeField] private Color diceFrameColor = new Color(0.16f, 0.13f, 0.12f, 0.96f);
    [SerializeField] private Color diceBackingColor = new Color(0.42f, 0.35f, 0.27f, 0.94f);
    [SerializeField] private Color rollingDiceColor = new Color(0.94f, 0.91f, 0.78f, 1f);
    [SerializeField] private Color faceRevealTextColor = new Color(0.12f, 0.1f, 0.08f, 1f);
    [SerializeField] private Color faceEffectTextColor = new Color(0.86f, 0.93f, 1f, 1f);
    [SerializeField] private Color damageNumberTextColor = new Color(1f, 0.78f, 0.42f, 1f);

    private Image projectileTrail;
    private Image diceFrame;
    private Image diceBacking;
    private Image rollingDiceVisual;
    private Text faceRevealText;
    private Text faceEffectText;
    private Text damageNumberText;
    private Text heroPopupText;
    private Image hitSpark;
    private Color originalEnemyColor;
    private Sprite[] heroIdleFrames;
    private Sprite[] heroThrowFrames;
    private Sprite[] heroHitFrames;
    private Sprite[] enemyIdleFrames;
    private float idleFrameTimer;
    private float enemyIdleFrameTimer;
    private int idleFrameIndex;
    private int enemyIdleFrameIndex;
    private bool isPlayingHeroThrow;
    private bool isPlayingHeroHit;
    private static Font fallbackFont;
    private static readonly Vector2 MinimumReadableDiceSize = new Vector2(288f, 288f);
    private static readonly Vector2[] RollingFrameOffsets =
    {
        new Vector2(-26f, -50f),
        new Vector2(-9f, -66f),
        new Vector2(16f, -48f),
        new Vector2(27f, -60f),
        new Vector2(10f, -56f),
        new Vector2(0f, -56f)
    };

    private void Awake()
    {
        heroSpriteImage = heroSpriteImage != null ? heroSpriteImage : heroPlaceholder as Image;
        enemySpriteImage = enemySpriteImage != null ? enemySpriteImage : enemyPlaceholder as Image;
        originalEnemyColor = enemyPlaceholder != null ? enemyPlaceholder.color : Color.white;
        BuildAnimationSprites();
        ConfigureHeroSpriteImage();
        ConfigureEnemySpriteImage();
        ShowHeroIdleFrame(0);
        ShowEnemyIdleFrame(0);
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
        EnsureHeroPopupText();
        HideHeroPopupText();
        EnsureHitSpark();
        HideHitSpark();
        HideDiceAnimationLayer();
    }

    private void Update()
    {
        PlayIdleLoop();
        PlayEnemyIdleLoop();
    }

    public IEnumerator Play(DiceFace selectedFace, FaceEffectData faceEffect, int damageAmount, int baseThrowDamage)
    {
        CacheOriginalColors();
        yield return PlayHeroThrowAnimation();
        yield return PlayEnemyHitFeedback();
        yield return PlayEnemyPopup(baseThrowDamage.ToString());

        ShowDiceAnimationLayer();
        yield return new WaitForSeconds(diceLayerAppearDuration);
        yield return PlayRollingPresentation();
        yield return PlayFaceReveal(selectedFace);
        yield return PlayFaceEffect(faceEffect, baseThrowDamage, damageAmount);
        yield return PlayFaceSpecificFeedback(faceEffect);
        HideDiceAnimationLayer();
    }

    public IEnumerator PlayPlayerDamagePopup(int damageAmount)
    {
        if (damageAmount <= 0)
        {
            yield break;
        }

        yield return PlayHeroPopup(damageAmount.ToString(), damageNumberTextColor);
    }

    private void CacheOriginalColors()
    {
        if (enemyPlaceholder != null)
        {
            originalEnemyColor = enemyPlaceholder.color;
        }
    }

    private IEnumerator PlayEnemyHitFeedback()
    {
        RectTransform enemyTransform = enemySlot;
        Vector2 originalEnemyPosition = enemyTransform != null ? enemyTransform.anchoredPosition : Vector2.zero;
        float elapsed = 0f;

        ShowHitSpark();

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = enemyFlashColor;
        }

        while (elapsed < enemyFlashDuration)
        {
            elapsed += Time.deltaTime;
            float phase = Mathf.Clamp01(elapsed / enemyFlashDuration);

            if (enemyTransform != null)
            {
                float direction = phase < 0.5f ? 1f : -1f;
                enemyTransform.anchoredPosition = originalEnemyPosition + enemyHitShakeOffset * direction;
            }

            yield return null;
        }

        if (enemyTransform != null)
        {
            enemyTransform.anchoredPosition = originalEnemyPosition;
        }

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = originalEnemyColor;
        }

        HideHitSpark();
    }

    public IEnumerator PlayHeroHitAnimation()
    {
        isPlayingHeroHit = true;

        if (heroSpriteImage == null || heroHitFrames == null || heroHitFrames.Length == 0)
        {
            isPlayingHeroHit = false;
            yield break;
        }

        for (int i = 0; i < heroHitFrames.Length; i++)
        {
            if (heroHitFrames[i] != null)
            {
                heroSpriteImage.sprite = heroHitFrames[i];
            }

            yield return new WaitForSeconds(heroHitFrameDuration);
        }

        isPlayingHeroHit = false;
        ShowHeroIdleFrame(idleFrameIndex);
    }

    private void BuildAnimationSprites()
    {
        heroIdleFrames = BuildSprites(heroIdleTextures);
        heroThrowFrames = BuildSprites(heroThrowTextures);
        heroHitFrames = BuildSprites(heroHitTextures);
        enemyIdleFrames = BuildSprites(enemyIdleTextures);
    }

    private static Sprite[] BuildSprites(Texture2D[] textures)
    {
        if (textures == null || textures.Length == 0)
        {
            return System.Array.Empty<Sprite>();
        }

        Sprite[] sprites = new Sprite[textures.Length];

        for (int i = 0; i < textures.Length; i++)
        {
            Texture2D texture = textures[i];
            if (texture == null)
            {
                continue;
            }

            sprites[i] = Sprite.Create(
                texture,
                new Rect(0f, 0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f,
                0,
                SpriteMeshType.FullRect);
        }

        return sprites;
    }

    private void ConfigureHeroSpriteImage()
    {
        if (heroSpriteImage == null)
        {
            return;
        }

        heroSpriteImage.raycastTarget = false;
        heroSpriteImage.preserveAspect = true;
        heroSpriteImage.color = Color.white;
    }

    private void ConfigureEnemySpriteImage()
    {
        if (enemySpriteImage == null)
        {
            return;
        }

        enemySpriteImage.raycastTarget = false;
        enemySpriteImage.preserveAspect = true;
        enemySpriteImage.color = Color.white;
    }

    private void PlayIdleLoop()
    {
        if (isPlayingHeroThrow || isPlayingHeroHit || heroSpriteImage == null || heroIdleFrames == null || heroIdleFrames.Length == 0)
        {
            return;
        }

        idleFrameTimer += Time.deltaTime;

        if (idleFrameTimer < heroIdleFrameDuration)
        {
            return;
        }

        idleFrameTimer = 0f;
        idleFrameIndex = (idleFrameIndex + 1) % heroIdleFrames.Length;
        ShowHeroIdleFrame(idleFrameIndex);
    }

    private void PlayEnemyIdleLoop()
    {
        if (enemySpriteImage == null || enemyIdleFrames == null || enemyIdleFrames.Length == 0)
        {
            return;
        }

        enemyIdleFrameTimer += Time.deltaTime;

        if (enemyIdleFrameTimer < enemyIdleFrameDuration)
        {
            return;
        }

        enemyIdleFrameTimer = 0f;
        enemyIdleFrameIndex = (enemyIdleFrameIndex + 1) % enemyIdleFrames.Length;
        ShowEnemyIdleFrame(enemyIdleFrameIndex);
    }

    private IEnumerator PlayHeroThrowAnimation()
    {
        isPlayingHeroThrow = true;

        if (heroSpriteImage == null || heroThrowFrames == null || heroThrowFrames.Length == 0)
        {
            ShowProjectileTrail();
            yield return new WaitForSeconds(projectileDuration);
            HideProjectileTrail();
            isPlayingHeroThrow = false;
            ShowHeroIdleFrame(idleFrameIndex);
            yield break;
        }

        int spawnFrame = Mathf.Clamp(projectileSpawnThrowFrame, 0, heroThrowFrames.Length - 1);
        float projectileElapsed = -1f;

        for (int i = 0; i < heroThrowFrames.Length; i++)
        {
            if (heroThrowFrames[i] != null)
            {
                heroSpriteImage.sprite = heroThrowFrames[i];
            }

            if (i == spawnFrame)
            {
                ShowProjectileTrail();
                projectileElapsed = 0f;
            }

            float elapsed = 0f;
            while (elapsed < heroThrowFrameDuration)
            {
                float deltaTime = Mathf.Min(Time.deltaTime, heroThrowFrameDuration - elapsed);
                elapsed += deltaTime;

                if (projectileElapsed >= 0f)
                {
                    projectileElapsed += deltaTime;
                    if (projectileElapsed >= projectileDuration)
                    {
                        HideProjectileTrail();
                        projectileElapsed = -1f;
                    }
                }

                yield return null;
            }
        }

        if (projectileElapsed >= 0f)
        {
            float remainingProjectileTime = Mathf.Max(0f, projectileDuration - projectileElapsed);
            if (remainingProjectileTime > 0f)
            {
                yield return new WaitForSeconds(remainingProjectileTime);
            }

            HideProjectileTrail();
        }

        isPlayingHeroThrow = false;
        ShowHeroIdleFrame(idleFrameIndex);
    }

    private void ShowHeroIdleFrame(int frameIndex)
    {
        if (heroSpriteImage == null || heroIdleFrames == null || heroIdleFrames.Length == 0)
        {
            return;
        }

        int safeIndex = Mathf.Clamp(frameIndex, 0, heroIdleFrames.Length - 1);
        if (heroIdleFrames[safeIndex] != null)
        {
            heroSpriteImage.sprite = heroIdleFrames[safeIndex];
        }
    }

    private void ShowEnemyIdleFrame(int frameIndex)
    {
        if (enemySpriteImage == null || enemyIdleFrames == null || enemyIdleFrames.Length == 0)
        {
            return;
        }

        int safeIndex = Mathf.Clamp(frameIndex, 0, enemyIdleFrames.Length - 1);
        if (enemyIdleFrames[safeIndex] != null)
        {
            enemySpriteImage.sprite = enemyIdleFrames[safeIndex];
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

        EnsureDiceFrame();
        ShowDiceFrame();
        EnsureRollingDiceVisual();
    }

    private void HideDiceAnimationLayer()
    {
        EnsureDiceAnimationLayer();

        if (diceAnimationLayer != null)
        {
            diceAnimationLayer.gameObject.SetActive(false);
        }

        HideDiceFrame();
        HideRollingDiceVisual();
        HideFaceRevealText();
        HideFaceEffectText();
        HideDamageNumberText();
        HideHeroPopupText();
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
            diceTransform.anchoredPosition = RollingFrameOffsets[frameIndex % RollingFrameOffsets.Length];
            diceTransform.localEulerAngles = new Vector3(0f, 0f, (frameIndex % 4) * 90f);
            rollingDiceVisual.color = frameIndex % 2 == 0
                ? rollingDiceColor
                : new Color(rollingDiceColor.r * 0.82f, rollingDiceColor.g * 0.82f, rollingDiceColor.b * 0.82f, rollingDiceColor.a);

            float waitDuration = Mathf.Min(diceRollFrameDuration, diceRollingDuration - elapsed);
            yield return new WaitForSeconds(waitDuration);

            elapsed += waitDuration;
            frameIndex++;
        }

        diceTransform.anchoredPosition = diceResultPosition;
        diceTransform.localEulerAngles = Vector3.zero;
        rollingDiceVisual.color = rollingDiceColor;
        HideFaceRevealText();
    }

    private void EnsureRollingDiceVisual()
    {
        if (rollingDiceVisual != null)
        {
            rollingDiceVisual.rectTransform.sizeDelta = GetReadableDiceSize();
            return;
        }

        EnsureDiceAnimationLayer();

        if (diceAnimationLayer == null)
        {
            return;
        }

        EnsureDiceFrame();

        GameObject diceObject = new GameObject("Rolling Dice Placeholder");
        diceObject.layer = diceAnimationLayer.gameObject.layer;
        diceObject.transform.SetParent(diceAnimationLayer, false);

        RectTransform diceTransform = diceObject.AddComponent<RectTransform>();
        diceTransform.anchorMin = new Vector2(0.5f, 0.5f);
        diceTransform.anchorMax = new Vector2(0.5f, 0.5f);
        diceTransform.pivot = new Vector2(0.5f, 0.5f);
        diceTransform.anchoredPosition = diceResultPosition;
        diceTransform.sizeDelta = GetReadableDiceSize();

        rollingDiceVisual = diceObject.AddComponent<Image>();
        rollingDiceVisual.raycastTarget = false;
        rollingDiceVisual.color = rollingDiceColor;
    }

    private void EnsureDiceFrame()
    {
        if (diceFrame != null && diceBacking != null)
        {
            UpdateDiceFrameSize();
            return;
        }

        EnsureDiceAnimationLayer();

        if (diceAnimationLayer == null)
        {
            return;
        }

        if (diceFrame == null)
        {
            GameObject frameObject = new GameObject("Dice Result Frame");
            frameObject.layer = diceAnimationLayer.gameObject.layer;
            frameObject.transform.SetParent(diceAnimationLayer, false);

            RectTransform frameTransform = frameObject.AddComponent<RectTransform>();
            frameTransform.anchorMin = new Vector2(0.5f, 0.5f);
            frameTransform.anchorMax = new Vector2(0.5f, 0.5f);
            frameTransform.pivot = new Vector2(0.5f, 0.5f);
            frameTransform.anchoredPosition = diceResultPosition;

            diceFrame = frameObject.AddComponent<Image>();
            diceFrame.raycastTarget = false;
            diceFrame.color = diceFrameColor;
        }

        if (diceBacking == null)
        {
            GameObject backingObject = new GameObject("Dice Result Backing");
            backingObject.layer = diceAnimationLayer.gameObject.layer;
            backingObject.transform.SetParent(diceAnimationLayer, false);

            RectTransform backingTransform = backingObject.AddComponent<RectTransform>();
            backingTransform.anchorMin = new Vector2(0.5f, 0.5f);
            backingTransform.anchorMax = new Vector2(0.5f, 0.5f);
            backingTransform.pivot = new Vector2(0.5f, 0.5f);
            backingTransform.anchoredPosition = diceResultPosition;

            diceBacking = backingObject.AddComponent<Image>();
            diceBacking.raycastTarget = false;
            diceBacking.color = diceBackingColor;
        }

        UpdateDiceFrameSize();
    }

    private void UpdateDiceFrameSize()
    {
        Vector2 diceSize = GetReadableDiceSize();

        if (diceFrame != null)
        {
            diceFrame.rectTransform.sizeDelta = diceSize + new Vector2(32f, 32f);
            diceFrame.rectTransform.anchoredPosition = diceResultPosition;
        }

        if (diceBacking != null)
        {
            diceBacking.rectTransform.sizeDelta = diceSize + new Vector2(20f, 20f);
            diceBacking.rectTransform.anchoredPosition = diceResultPosition;
        }
    }

    private Vector2 GetReadableDiceSize()
    {
        return new Vector2(
            Mathf.Max(rollingDiceSize.x, MinimumReadableDiceSize.x),
            Mathf.Max(rollingDiceSize.y, MinimumReadableDiceSize.y));
    }

    private void HideDiceFrame()
    {
        if (diceFrame != null)
        {
            diceFrame.gameObject.SetActive(false);
        }

        if (diceBacking != null)
        {
            diceBacking.gameObject.SetActive(false);
        }
    }

    private void ShowDiceFrame()
    {
        if (diceFrame != null)
        {
            diceFrame.gameObject.SetActive(true);
        }

        if (diceBacking != null)
        {
            diceBacking.gameObject.SetActive(true);
        }
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
        diceTransform.anchoredPosition = diceResultPosition;
        diceTransform.localEulerAngles = Vector3.zero;
        rollingDiceVisual.color = rollingDiceColor;
        rollingDiceVisual.gameObject.SetActive(true);

        faceRevealText.text = selectedFace != null ? selectedFace.DisplayName : "?";
        faceRevealText.gameObject.SetActive(true);

        yield return new WaitForSeconds(faceRevealDuration);
    }

    private IEnumerator PlayFaceSpecificFeedback(FaceEffectData faceEffect)
    {
        if (faceEffect == null || !faceEffect.IsImplemented)
        {
            yield break;
        }

        if (faceEffect.EffectType == FaceEffectType.Damage && faceEffect.DamageAmount > 0)
        {
            yield return PlayEnemyPopup($"+{faceEffect.DamageAmount}");
            yield break;
        }

        if (faceEffect.EffectType == FaceEffectType.Mend && faceEffect.HealAmount > 0)
        {
            yield return PlayHeroPopup($"+{faceEffect.HealAmount} HP", new Color(0.52f, 1f, 0.58f, 1f));
        }
    }

    private IEnumerator PlayEnemyPopup(string popupText)
    {
        EnsureDamageNumberText();

        if (damageNumberText == null || string.IsNullOrWhiteSpace(popupText))
        {
            yield break;
        }

        RectTransform damageTransform = damageNumberText.rectTransform;
        Vector2 startPosition = GetEnemyPopupPosition(enemyDamageNumberOffset);
        Vector2 endPosition = startPosition + enemyDamageNumberFloatOffset;
        Color startColor = damageNumberTextColor;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        damageTransform.anchoredPosition = startPosition;
        damageNumberText.text = popupText;
        damageNumberText.color = startColor;
        damageNumberText.gameObject.SetActive(true);

        yield return AnimatePopup(damageNumberText, damageTransform, startPosition, endPosition, startColor, endColor);
        HideDamageNumberText();
    }

    private IEnumerator PlayHeroPopup(string popupText, Color popupColor)
    {
        EnsureHeroPopupText();

        if (heroPopupText == null || string.IsNullOrWhiteSpace(popupText))
        {
            yield break;
        }

        RectTransform popupTransform = heroPopupText.rectTransform;
        Vector2 startPosition = GetHeroPopupPosition(heroPopupOffset);
        Vector2 endPosition = startPosition + heroPopupFloatOffset;
        Color endColor = new Color(popupColor.r, popupColor.g, popupColor.b, 0f);

        popupTransform.anchoredPosition = startPosition;
        heroPopupText.text = popupText;
        heroPopupText.color = popupColor;
        heroPopupText.gameObject.SetActive(true);

        yield return AnimatePopup(heroPopupText, popupTransform, startPosition, endPosition, popupColor, endColor);
        HideHeroPopupText();
    }

    private IEnumerator AnimatePopup(
        Text popupText,
        RectTransform popupTransform,
        Vector2 startPosition,
        Vector2 endPosition,
        Color startColor,
        Color endColor)
    {
        float holdDuration = Mathf.Min(damageNumberHoldDuration, damageNumberDuration);
        if (holdDuration > 0f)
        {
            popupTransform.anchoredPosition = startPosition;
            popupText.color = startColor;
            yield return new WaitForSeconds(holdDuration);
        }

        float moveDuration = Mathf.Max(0.01f, damageNumberDuration - holdDuration);
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float phase = Mathf.Clamp01(elapsed / moveDuration);
            popupTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, phase);
            popupText.color = Color.Lerp(startColor, endColor, phase);
            yield return null;
        }
    }

    private IEnumerator PlayFaceEffect(FaceEffectData faceEffect, int baseThrowDamage, int enemyDamageAmount)
    {
        EnsureFaceEffectText();

        if (faceEffectText == null)
        {
            yield break;
        }

        RectTransform effectTransform = faceEffectText.rectTransform;
        effectTransform.anchoredPosition = diceResultPosition + new Vector2(0f, -168f);
        faceEffectText.text = GetFaceEffectText(faceEffect, baseThrowDamage, enemyDamageAmount);
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
        faceRevealText.fontSize = 44;
        faceRevealText.resizeTextForBestFit = true;
        faceRevealText.resizeTextMinSize = 18;
        faceRevealText.resizeTextMaxSize = 44;
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
        textTransform.anchoredPosition = diceResultPosition + new Vector2(0f, -168f);
        textTransform.sizeDelta = new Vector2(320f, 96f);

        faceEffectText = textObject.AddComponent<Text>();
        faceEffectText.raycastTarget = false;
        faceEffectText.alignment = TextAnchor.MiddleCenter;
        faceEffectText.fontSize = 24;
        faceEffectText.resizeTextForBestFit = true;
        faceEffectText.resizeTextMinSize = 10;
        faceEffectText.resizeTextMaxSize = 24;
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

    private static string GetFaceEffectText(FaceEffectData faceEffect, int baseThrowDamage, int enemyDamageAmount)
    {
        int safeBaseDamage = Mathf.Max(0, baseThrowDamage);
        int safeEnemyDamage = Mathf.Max(0, enemyDamageAmount);

        if (faceEffect == null || !faceEffect.IsImplemented)
        {
            return $"Unknown\nBase {safeBaseDamage}\n= {safeEnemyDamage} Damage";
        }

        string faceName = string.IsNullOrWhiteSpace(faceEffect.SourceFaceDisplayName)
            ? "Face"
            : faceEffect.SourceFaceDisplayName;

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return $"{faceName} +{faceEffect.DamageAmount}\n= {safeEnemyDamage} Total Damage";
        }

        if (faceEffect.EffectType == FaceEffectType.Guard)
        {
            return $"{faceName}\nEnemy Damage -{faceEffect.IncomingDamageReductionAmount}";
        }

        if (faceEffect.EffectType == FaceEffectType.Mend)
        {
            return $"{faceName}\nHeal +{faceEffect.HealAmount} HP";
        }

        return $"{faceName}\nBase {safeBaseDamage}\n= {safeEnemyDamage} Damage";
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

        GameObject textObject = new GameObject("Enemy Damage Number Text");
        textObject.layer = battleField != null ? battleField.gameObject.layer : diceAnimationLayer.gameObject.layer;
        textObject.transform.SetParent(battleField != null ? battleField : diceAnimationLayer, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = GetEnemyPopupPosition(enemyDamageNumberOffset);
        textTransform.sizeDelta = new Vector2(180f, 72f);

        damageNumberText = textObject.AddComponent<Text>();
        damageNumberText.raycastTarget = false;
        damageNumberText.alignment = TextAnchor.MiddleCenter;
        damageNumberText.fontSize = 54;
        damageNumberText.resizeTextForBestFit = true;
        damageNumberText.resizeTextMinSize = 12;
        damageNumberText.resizeTextMaxSize = 54;
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

    private void EnsureHeroPopupText()
    {
        if (heroPopupText != null)
        {
            return;
        }

        if (battleField == null)
        {
            return;
        }

        GameObject textObject = new GameObject("Hero Popup Text");
        textObject.layer = battleField.gameObject.layer;
        textObject.transform.SetParent(battleField, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = GetHeroPopupPosition(heroPopupOffset);
        textTransform.sizeDelta = new Vector2(200f, 72f);

        heroPopupText = textObject.AddComponent<Text>();
        heroPopupText.raycastTarget = false;
        heroPopupText.alignment = TextAnchor.MiddleCenter;
        heroPopupText.fontSize = 48;
        heroPopupText.resizeTextForBestFit = true;
        heroPopupText.resizeTextMinSize = 12;
        heroPopupText.resizeTextMaxSize = 48;
        heroPopupText.color = damageNumberTextColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        heroPopupText.font = fallbackFont;
    }

    private void HideHeroPopupText()
    {
        if (heroPopupText != null)
        {
            heroPopupText.gameObject.SetActive(false);
        }
    }

    private void EnsureHitSpark()
    {
        if (hitSpark != null)
        {
            return;
        }

        if (battleField == null)
        {
            return;
        }

        GameObject sparkObject = new GameObject("Enemy Hit Spark");
        sparkObject.layer = battleField.gameObject.layer;
        sparkObject.transform.SetParent(battleField, false);

        RectTransform sparkTransform = sparkObject.AddComponent<RectTransform>();
        sparkTransform.anchorMin = new Vector2(0.5f, 0.5f);
        sparkTransform.anchorMax = new Vector2(0.5f, 0.5f);
        sparkTransform.pivot = new Vector2(0.5f, 0.5f);
        sparkTransform.anchoredPosition = GetEnemyPopupPosition(Vector2.zero);
        sparkTransform.sizeDelta = new Vector2(28f, 28f);

        hitSpark = sparkObject.AddComponent<Image>();
        hitSpark.raycastTarget = false;
        hitSpark.color = new Color(1f, 0.84f, 0.32f, 0.95f);
    }

    private void ShowHitSpark()
    {
        EnsureHitSpark();

        if (hitSpark == null)
        {
            return;
        }

        RectTransform sparkTransform = hitSpark.rectTransform;
        sparkTransform.anchoredPosition = GetEnemyPopupPosition(new Vector2(-22f, 8f));
        hitSpark.gameObject.SetActive(true);
    }

    private void HideHitSpark()
    {
        if (hitSpark != null)
        {
            hitSpark.gameObject.SetActive(false);
        }
    }

    private Vector2 GetEnemyPopupPosition(Vector2 offset)
    {
        if (enemySlot != null)
        {
            return enemySlot.anchoredPosition + offset;
        }

        return new Vector2(180f, 32f) + offset;
    }

    private Vector2 GetHeroPopupPosition(Vector2 offset)
    {
        if (heroSlot != null)
        {
            return heroSlot.anchoredPosition + offset;
        }

        return new Vector2(-180f, 32f) + offset;
    }
}
