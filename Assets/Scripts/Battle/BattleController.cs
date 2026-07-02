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
    [SerializeField] private LinearStageRuntimeState linearStageRuntimeState;
    [SerializeField] private LinearRunState linearRunState;
    [SerializeField] private BattleHudPresenter hudPresenter;
    [SerializeField] private ThrowSequencePresenter throwSequencePresenter;
    [SerializeField] private EnemyAttackPresenter enemyAttackPresenter;
    [SerializeField] private RunFlowPresenter runFlowPresenter;
    [SerializeField] private BattleDiceResultPresenter diceResultPresenter;
    [SerializeField] private Text battleLogText;
    [SerializeField] private RectTransform throwButtonHitArea;
    [SerializeField] private bool inputLocked;

    private EnemyAttackIntent pendingEnemyAttackIntent = EnemyAttackIntent.None();

    public EnemyAttackIntent PendingEnemyAttackIntent => pendingEnemyAttackIntent;

    private void Start()
    {
        EnsureRunFlowPresenter();
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
        int baseThrowDamage = GetBaseThrowDamage();
        int totalThrowDamage = GetTotalThrowDamage(baseThrowDamage, faceEffect);
        int pendingDamage = GetPendingDamage(totalThrowDamage);

        if (throwSequencePresenter != null)
        {
            yield return throwSequencePresenter.Play(selectedFace, faceEffect, pendingDamage, baseThrowDamage);
        }

        diceResultPresenter?.ShowResult(battleDiceState);

        int damage = ApplyThrowDamage(totalThrowDamage);
        int healing = ApplyFaceHealing(faceEffect);
        hudPresenter?.Refresh();

        ResolveEnemyDefeatOutcome();

        if (IsBattleVictory())
        {
            pendingEnemyAttackIntent = EnemyAttackIntent.None();
            yield return PlayRunFlowPresentation();
            bool preparedNextBattle = ResolvePostVictoryRunProgression();
            yield return PlayRunFlowPresentation();
            SetBattleLog(GetThrowLogMessage(faceEffect, baseThrowDamage, damage, healing));
            inputLocked = !preparedNextBattle;
            yield break;
        }

        battleTurnState?.BeginEnemyTurn();
        pendingEnemyAttackIntent = EnemyAttackResolver.Resolve(battleTurnState);
        if (enemyAttackPresenter != null)
        {
            yield return enemyAttackPresenter.Play(pendingEnemyAttackIntent);
        }

        int playerDamage = ApplyEnemyAttackIntent(pendingEnemyAttackIntent, faceEffect);
        pendingEnemyAttackIntent = EnemyAttackIntent.None();
        hudPresenter?.Refresh();

        ResolvePlayerDefeatOutcome();

        if (IsBattleDefeat())
        {
            yield return PlayRunFlowPresentation();
            SetBattleLog($"{GetThrowLogMessage(faceEffect, baseThrowDamage, damage, healing)} {GetEnemyAttackLogMessage(playerDamage)}");
            yield break;
        }

        battleTurnState?.BeginTransition();
        battleTurnState?.BeginPlayerTurn();
        SetBattleLog($"{GetThrowLogMessage(faceEffect, baseThrowDamage, damage, healing)} {GetEnemyAttackLogMessage(playerDamage)}");
        inputLocked = false;
    }

    private bool CanAcceptPlayerThrow()
    {
        if (linearRunState != null && linearRunState.IsCompleted)
        {
            return false;
        }

        if (battleOutcomeState != null && !battleOutcomeState.IsInProgress)
        {
            return false;
        }

        return battleTurnState == null || battleTurnState.CanAcceptPlayerAction;
    }

    private void EnsureRunFlowPresenter()
    {
        if (runFlowPresenter != null)
        {
            return;
        }

        runFlowPresenter = GetComponent<RunFlowPresenter>();

        if (runFlowPresenter == null)
        {
            runFlowPresenter = gameObject.AddComponent<RunFlowPresenter>();
        }
    }

    private IEnumerator PlayRunFlowPresentation()
    {
        EnsureRunFlowPresenter();

        if (runFlowPresenter == null)
        {
            yield break;
        }

        yield return runFlowPresenter.PlayBattleOutcome(
            battleOutcomeState,
            linearStageRuntimeState,
            linearRunState);
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

    private int ApplyThrowDamage(int totalThrowDamage)
    {
        if (combatState == null || totalThrowDamage <= 0)
        {
            return 0;
        }

        return combatState.ApplyDamageToEnemy(totalThrowDamage);
    }

    private int ApplyFaceHealing(FaceEffectData faceEffect)
    {
        if (combatState == null || faceEffect == null || faceEffect.EffectType != FaceEffectType.Mend)
        {
            return 0;
        }

        return combatState.HealPlayer(faceEffect.HealAmount);
    }

    private int ApplyEnemyAttackIntent(EnemyAttackIntent attackIntent, FaceEffectData playerFaceEffect)
    {
        if (combatState == null || attackIntent == null || attackIntent.IntentType != EnemyAttackIntentType.Damage)
        {
            return 0;
        }

        int reducedDamage = Mathf.Max(0, attackIntent.DamageAmount - GetIncomingDamageReduction(playerFaceEffect));
        return combatState.ApplyDamageToPlayer(reducedDamage);
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

    private bool ResolvePostVictoryRunProgression()
    {
        if (!IsBattleVictory() || linearStageRuntimeState == null)
        {
            return false;
        }

        if (linearStageRuntimeState.IsBossStage)
        {
            linearRunState?.MarkCompleted();
            return false;
        }

        if (!linearStageRuntimeState.TryAdvanceToNextStage())
        {
            return false;
        }

        PrepareNextBattleRuntime();
        return true;
    }

    private void PrepareNextBattleRuntime()
    {
        combatState?.PrepareNextEnemy();
        battleOutcomeState?.ResetOutcome();
        battleTurnState?.BeginPlayerTurn();
        pendingEnemyAttackIntent = EnemyAttackIntent.None();
        hudPresenter?.Refresh();
    }

    private void ResolvePlayerDefeatOutcome()
    {
        if (battleOutcomeState == null || !battleOutcomeState.IsInProgress || combatState == null)
        {
            return;
        }

        if (combatState.IsPlayerDefeated)
        {
            battleOutcomeState.MarkDefeat();
        }
    }

    private bool IsBattleDefeat()
    {
        return battleOutcomeState != null && battleOutcomeState.IsDefeat;
    }

    private int GetBaseThrowDamage()
    {
        DiceModel currentDice = battleDiceState != null ? battleDiceState.CurrentDice : null;
        return currentDice != null ? currentDice.BaseThrowDamage : 0;
    }

    private static int GetTotalThrowDamage(int baseThrowDamage, FaceEffectData faceEffect)
    {
        return Mathf.Max(0, baseThrowDamage) + GetFaceDamageModifier(faceEffect);
    }

    private static int GetFaceDamageModifier(FaceEffectData faceEffect)
    {
        if (faceEffect == null || faceEffect.EffectType != FaceEffectType.Damage)
        {
            return 0;
        }

        return faceEffect.DamageAmount;
    }

    private static int GetIncomingDamageReduction(FaceEffectData faceEffect)
    {
        if (faceEffect == null || faceEffect.EffectType != FaceEffectType.Guard)
        {
            return 0;
        }

        return faceEffect.IncomingDamageReductionAmount;
    }

    private int GetPendingDamage(int totalThrowDamage)
    {
        if (combatState == null || totalThrowDamage <= 0)
        {
            return 0;
        }

        return Mathf.Min(combatState.EnemyCurrentHp, totalThrowDamage);
    }

    private static string GetThrowLogMessage(FaceEffectData faceEffect, int baseThrowDamage, int appliedDamage, int appliedHealing)
    {
        if (faceEffect == null)
        {
            return $"Throw dealt {appliedDamage} from base {baseThrowDamage}.";
        }

        string faceName = string.IsNullOrWhiteSpace(faceEffect.SourceFaceDisplayName)
            ? "Unknown Face"
            : $"{faceEffect.SourceFaceDisplayName} Face";

        if (!faceEffect.IsImplemented)
        {
            return $"{faceName} added no effect; Throw dealt {appliedDamage} from base {baseThrowDamage}.";
        }

        if (faceEffect.EffectType == FaceEffectType.Damage)
        {
            return $"{faceName} modified the Throw for {appliedDamage} damage from base {baseThrowDamage}.";
        }

        if (faceEffect.EffectType == FaceEffectType.Guard)
        {
            return $"{faceName} guarded after a {appliedDamage} damage Throw.";
        }

        if (faceEffect.EffectType == FaceEffectType.Mend)
        {
            return $"{faceName} healed {appliedHealing} after a {appliedDamage} damage Throw.";
        }

        return $"{faceName} modified the Throw for {appliedDamage} damage from base {baseThrowDamage}.";
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
