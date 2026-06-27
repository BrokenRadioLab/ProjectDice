using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class BattleController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private Text battleLogText;
    [SerializeField] private bool inputLocked;

    private void Start()
    {
        hudPresenter?.Refresh();
        SetBattleLog("Ready to throw.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ThrowOnce();
    }

    private void ThrowOnce()
    {
        if (inputLocked || combatState == null)
        {
            return;
        }

        int damage = combatState.ApplyFixedThrowDamageToEnemy();
        hudPresenter?.Refresh();

        if (combatState.IsEnemyDefeated)
        {
            inputLocked = true;
            SetBattleLog($"Throw dealt {damage}. Victory.");
            return;
        }

        SetBattleLog($"Throw dealt {damage}.");
    }

    private void SetBattleLog(string message)
    {
        if (battleLogText != null)
        {
            battleLogText.text = message;
        }
    }
}
