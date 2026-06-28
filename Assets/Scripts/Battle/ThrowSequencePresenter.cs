using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class ThrowSequencePresenter : MonoBehaviour
{
    [SerializeField] private RectTransform battleField;
    [SerializeField] private RectTransform heroSlot;
    [SerializeField] private RectTransform enemySlot;
    [SerializeField] private Graphic heroPlaceholder;
    [SerializeField] private Graphic enemyPlaceholder;
    [SerializeField, Min(0.01f)] private float heroFeedbackDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float projectileDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float enemyFlashDuration = 0.08f;
    [SerializeField, Min(1f)] private float projectileThickness = 3f;
    [SerializeField] private Color projectileColor = Color.white;
    [SerializeField] private Color heroFeedbackColor = new Color(0.95f, 0.95f, 1f, 0.28f);
    [SerializeField] private Color enemyFlashColor = new Color(1f, 0.95f, 0.88f, 0.55f);

    private Image projectileTrail;
    private Color originalHeroColor;
    private Color originalEnemyColor;

    private void Awake()
    {
        originalHeroColor = heroPlaceholder != null ? heroPlaceholder.color : Color.white;
        originalEnemyColor = enemyPlaceholder != null ? enemyPlaceholder.color : Color.white;
        EnsureProjectileTrail();
        HideProjectileTrail();
    }

    public IEnumerator Play()
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
}
