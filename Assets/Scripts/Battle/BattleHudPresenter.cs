using UnityEngine;
using UnityEngine.UI;

public sealed class BattleHudPresenter : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private Text playerHpText;
    [SerializeField] private Text enemyHpText;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
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

#if UNITY_EDITOR
    private void OnValidate()
    {
        Refresh();
    }
#endif
}
