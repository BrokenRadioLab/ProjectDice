using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class BattleController : MonoBehaviour
{
    [SerializeField] private BattleCombatState combatState;
    [SerializeField] private BattleDiceState battleDiceState;
    [SerializeField] private BattleTurnState battleTurnState;
    [SerializeField] private BattleOutcomeState battleOutcomeState;
    [SerializeField] private EnemyGroupState enemyGroupState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private ThrowSequencePresenter throwSequencePresenter;
    [SerializeField] private EnemyAttackPresenter enemyAttackPresenter;
    [SerializeField] private BattleDiceResultPresenter diceResultPresenter;
    [SerializeField] private Text battleLogText;
    [SerializeField] private RectTransform throwButtonHitArea;
    [SerializeField] private bool inputLocked;

    private EnemyAttackIntent pendingEnemyAttackIntent = EnemyAttackIntent.None();

    public EnemyAttackIntent PendingEnemyAttackIntent => pendingEnemyAttackIntent;

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

        if (UnityEngine.Input.touchCount > 0)
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                return IsInsideThrowArea(touch.position);
            }
        }

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            return IsInsideThrowArea(UnityEngine.Input.mousePosition);
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
        if (inputLocked || combatState == null || !CanAcceptPlayerThrow())
        {
            yield break;
        }

        inputLocked = true;
        battleTurnState?.BeginPlayerAction();
        battleDiceState?.BeginThrowRoll();
        DiceFace selectedFace = SelectDiceResult();
        FaceEffectData faceEffect = FaceResolver.Resolve(selectedFace);
        int pendingDamage = GetPendingDamage(faceEffect);

        if (throwSequencePresenter != null)
        {
            yield return throwSequencePresenter.Play(selectedFace, faceEffect, pendingDamage);
        }

        diceResultPresenter?.ShowResult(battleDiceState);

        int damage = ApplyFaceEffect(faceEffect);
        hudPresenter?.Refresh();

        ResolveEnemyDefeatOutcome();

        if (IsBattleVictory())
        {
            pendingEnemyAttackIntent = EnemyAttackIntent.None();
            SetBattleLog(GetFaceEffectLogMessage(faceEffect, damage));
            yield break;
        }

        battleTurnState?.BeginEnemyTurn();
        pendingEnemyAttackIntent = EnemyAttackResolver.Resolve(battleTurnState);
        if (enemyAttackPresenter != null)
        {
            yield return enemyAttackPresenter.Play(pendingEnemyAttackIntent);
        }

        int playerDamage = ApplyEnemyAttackIntent(pendingEnemyAttackIntent);
        pendingEnemyAttackIntent = EnemyAttackIntent.None();
        hudPresenter?.Refresh();

        battleTurnState?.BeginTransition();
        battleTurnState?.BeginPlayerTurn();
        SetBattleLog($"{GetFaceEffectLogMessage(faceEffect, damage)} {GetEnemyAttackLogMessage(playerDamage)}");
        inputLocked = false;
    }

    private bool CanAcceptPlayerThrow()
    {
        if (battleOutcomeState != null && !battleOutcomeState.IsInProgress)
        {
            return false;
        }

        return battleTurnState == null || battleTurnState.CanAcceptPlayerAction;
    }

    private DiceFace SelectDiceResult()
    {
        if (battleDiceState == null)
        {
            return null;
        }

        int resultSlotIndex = DiceRoller.SelectResultSlot(battleDiceState.CurrentDice);
        battleDiceState.StopAtResultSlot(resultSlotIndex);
        battleDiceState.RevealResult();

        return battleDiceState.LastSelectedFace;
    }

    private int ApplyFaceEffect(FaceEffectData faceEffect)
    {
        if (combatState == null || faceEffect == null)
        {
            return 0;
        }

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return combatState.ApplyDamageToEnemy(faceEffect.DamageAmount);
        }

        return 0;
    }

    private int ApplyEnemyAttackIntent(EnemyAttackIntent attackIntent)
    {
        if (combatState == null || attackIntent == null || attackIntent.IntentType != EnemyAttackIntentType.Damage)
        {
            return 0;
        }

        return combatState.ApplyDamageToPlayer(attackIntent.DamageAmount);
    }

    private void ResolveEnemyDefeatOutcome()
    {
        if (battleOutcomeState == null || !battleOutcomeState.IsInProgress || enemyGroupState == null)
        {
            return;
        }

        if (enemyGroupState.AreAllEnemiesDefeated)
        {
            battleOutcomeState.MarkVictory();
        }
    }

    private bool IsBattleVictory()
    {
        return battleOutcomeState != null && battleOutcomeState.IsVictory;
    }

    private int GetPendingDamage(FaceEffectData faceEffect)
    {
        if (combatState == null || faceEffect == null)
        {
            return 0;
        }

        if (faceEffect.EffectType != FaceEffectType.Damage)
        {
            return 0;
        }

        return Mathf.Min(combatState.EnemyCurrentHp, faceEffect.DamageAmount);
    }

    private static string GetFaceEffectLogMessage(FaceEffectData faceEffect, int appliedDamage)
    {
        if (faceEffect == null)
        {
            return "Unknown Face has no effect yet.";
        }

        string faceName = string.IsNullOrWhiteSpace(faceEffect.SourceFaceDisplayName)
            ? "Unknown Face"
            : $"{faceEffect.SourceFaceDisplayName} Face";

        if (!faceEffect.IsImplemented)
        {
            return $"{faceName} has no effect yet.";
        }

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return $"{faceName} dealt {appliedDamage}.";
        }

        return $"{faceName} resolved.";
    }

    private static string GetEnemyAttackLogMessage(int appliedDamage)
    {
        return appliedDamage > 0 ? $"Enemy dealt {appliedDamage}." : "Enemy attack had no effect.";
    }

    private void SetBattleLog(string message)
    {
        if (battleLogText != null)
        {
            battleLogText.text = message;
        }
    }
}
