using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class BattleController : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private ThrowSequencePresenter throwSequencePresenter;
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
            StartCoroutine(ThrowOnce());
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

    private IEnumerator ThrowOnce()
    {
        if (inputLocked || combatState == null)
        {
            yield break;
        }

        inputLocked = true;

        if (throwSequencePresenter != null)
        {
            yield return throwSequencePresenter.Play();
        }

        int damage = combatState.ApplyFixedThrowDamageToEnemy();
        hudPresenter?.Refresh();

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
