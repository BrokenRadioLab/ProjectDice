using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class BattleController : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private DiceOverlayPresenter diceOverlayPresenter;
    [SerializeField] private Text battleLogText;
    [SerializeField] private RectTransform throwButtonHitArea;
    [SerializeField, Min(0.1f)] private float rollingOverlayDuration = 0.65f;
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
            StartCoroutine(ThrowSequence());
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

    private IEnumerator ThrowSequence()
    {
        if (inputLocked || combatState == null)
        {
            yield break;
        }

        inputLocked = true;
        diceOverlayPresenter?.ShowRolling();
        SetBattleLog("Rolling...");

        yield return new WaitForSeconds(rollingOverlayDuration);

        int damage = combatState.ApplyFixedThrowDamageToEnemy();
        hudPresenter?.Refresh();
        diceOverlayPresenter?.Hide();

        if (combatState.IsEnemyDefeated)
        {
            SetBattleLog($"Throw dealt {damage}. Victory.");
            yield break;
        }

        SetBattleLog($"Throw dealt {damage}.");
        inputLocked = false;
    }

    private void SetBattleLog(string message)
    {
        if (battleLogText != null)
        {
            battleLogText.text = message;
        }
    }
}
