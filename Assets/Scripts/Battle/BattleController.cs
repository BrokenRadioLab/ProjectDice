using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class BattleController : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private Text battleLogText;
    [SerializeField] private RectTransform throwButtonHitArea;
    [SerializeField] private bool inputLocked;

    private void Start()
    {
        hudPresenter?.Refresh();
        SetBattleLog("Ready to throw.");
    }

    private void Update()
    {
        if (WasThrowPressed())
        {
            ThrowOnce();
        }
    }

    private bool WasThrowPressed()
    {
        Vector2 screenPosition;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
            return IsInsideThrowArea(screenPosition);
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            return IsInsideThrowArea(screenPosition);
        }

        return false;
    }

    private bool IsInsideThrowArea(Vector2 screenPosition)
    {
        if (throwButtonHitArea == null)
        {
            return true;
        }

        return RectTransformUtility.RectangleContainsScreenPoint(throwButtonHitArea, screenPosition);
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
