using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class EnemyAttackPresenter : MonoBehaviour
{
    [SerializeField] private RectTransform battleField;
    [SerializeField] private RectTransform heroSlot;
    [SerializeField] private RectTransform enemySlot;
    [SerializeField] private Graphic heroPlaceholder;
    [SerializeField] private Graphic enemyPlaceholder;
    [SerializeField, Min(0.01f)] private float enemyWindupDuration = 0.08f;
    [SerializeField, Min(0.01f)] private float enemyStrikeDuration = 0.10f;
    [SerializeField, Min(0.01f)] private float heroHitFlashDuration = 0.08f;
    [SerializeField, Min(1f)] private float strikeTrailThickness = 4f;
    [SerializeField] private Color enemyWindupColor = new Color(1f, 0.78f, 0.64f, 0.72f);
    [SerializeField] private Color heroHitFlashColor = new Color(1f, 0.92f, 0.82f, 0.55f);
    [SerializeField] private Color strikeTrailColor = new Color(1f, 1f, 1f, 0.9f);

    private Image strikeTrail;
    private Color originalHeroColor;
    private Color originalEnemyColor;

    private void Awake()
    {
        originalHeroColor = heroPlaceholder != null ? heroPlaceholder.color : Color.white;
        originalEnemyColor = enemyPlaceholder != null ? enemyPlaceholder.color : Color.white;
        EnsureStrikeTrail();
        HideStrikeTrail();
    }

    public IEnumerator Play(EnemyAttackIntent attackIntent)
    {
        if (attackIntent == null || !attackIntent.HasDamage)
        {
            yield break;
        }

        CacheOriginalColors();

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = enemyWindupColor;
        }

        yield return new WaitForSeconds(enemyWindupDuration);

        ShowStrikeTrail();
        yield return new WaitForSeconds(enemyStrikeDuration);
        HideStrikeTrail();

        if (enemyPlaceholder != null)
        {
            enemyPlaceholder.color = originalEnemyColor;
        }

        if (heroPlaceholder != null)
        {
            heroPlaceholder.color = heroHitFlashColor;
        }

        yield return new WaitForSeconds(heroHitFlashDuration);

        if (heroPlaceholder != null)
        {
            heroPlaceholder.color = originalHeroColor;
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

    private void EnsureStrikeTrail()
    {
        if (strikeTrail != null || battleField == null)
        {
            return;
        }

        GameObject trailObject = new GameObject("Enemy Attack Trail");
        trailObject.layer = battleField.gameObject.layer;
        trailObject.transform.SetParent(battleField, false);

        RectTransform trailTransform = trailObject.AddComponent<RectTransform>();
        trailTransform.anchorMin = new Vector2(0.5f, 0.5f);
        trailTransform.anchorMax = new Vector2(0.5f, 0.5f);
        trailTransform.pivot = new Vector2(0f, 0.5f);

        strikeTrail = trailObject.AddComponent<Image>();
        strikeTrail.raycastTarget = false;
        strikeTrail.color = strikeTrailColor;
    }

    private void ShowStrikeTrail()
    {
        EnsureStrikeTrail();

        if (strikeTrail == null || battleField == null || heroSlot == null || enemySlot == null)
        {
            return;
        }

        RectTransform trailTransform = strikeTrail.rectTransform;
        Vector2 start = GetLocalEdgePoint(enemySlot, false);
        Vector2 end = GetLocalEdgePoint(heroSlot, true);
        Vector2 delta = end - start;

        trailTransform.anchoredPosition = start;
        trailTransform.sizeDelta = new Vector2(delta.magnitude, strikeTrailThickness);
        trailTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
        strikeTrail.color = strikeTrailColor;
        strikeTrail.gameObject.SetActive(true);
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

    private void HideStrikeTrail()
    {
        if (strikeTrail != null)
        {
            strikeTrail.gameObject.SetActive(false);
        }
    }
}
