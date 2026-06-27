using UnityEngine;
using UnityEngine.UI;

public sealed class BattleHudPresenter : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private Text playerHpText;
    [SerializeField] private Text enemyHpText;

    private static Font fallbackFont;

    private void OnEnable()
    {
        EnsureSceneTextHasFont();
        Refresh();
    }

    public void Refresh()
    {
        EnsureTextHasFont(playerHpText);
        EnsureTextHasFont(enemyHpText);

        if (combatState == null)
        {
            return;
        }

        if (playerHpText != null)
        {
            playerHpText.text = $"PLAYER HP {combatState.PlayerCurrentHp} / {combatState.PlayerMaxHp}";
        }

        if (enemyHpText != null)
        {
            enemyHpText.text = $"ENEMY HP {combatState.EnemyCurrentHp} / {combatState.EnemyMaxHp}";
        }
    }

    private static void EnsureSceneTextHasFont()
    {
        Text[] sceneTexts = FindObjectsOfType<Text>(true);
        for (int i = 0; i < sceneTexts.Length; i++)
        {
            EnsureTextHasFont(sceneTexts[i]);
        }
    }

    private static void EnsureTextHasFont(Text text)
    {
        if (text == null)
        {
            return;
        }

        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");

        if (fallbackFont != null)
        {
            text.font = fallbackFont;
            text.enabled = true;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        EnsureTextHasFont(playerHpText);
        EnsureTextHasFont(enemyHpText);
        Refresh();
    }
#endif
}
