using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class RunFlowPresenter : MonoBehaviour
{
    private const float StageClearDurationSeconds = 0.55f;
    private const float NextStageDurationSeconds = 0.65f;
    private const float BattleResumeDurationSeconds = 0.45f;
    private const float RunCompleteDurationSeconds = 0.95f;
    private const float DefeatDurationSeconds = 0.85f;
    private static Font fallbackFont;

    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private Color panelColor = new Color(0.10f, 0.09f, 0.08f, 0.92f);
    [SerializeField] private Color textColor = new Color(0.96f, 0.90f, 0.70f, 1f);

    private RectTransform stageClearRoot;
    private Text stageClearText;
    private RectTransform nextStageRoot;
    private Text nextStageTitleText;
    private Text nextStageTypeText;
    private RectTransform battleResumeRoot;
    private Text battleResumeText;
    private RectTransform runCompleteRoot;
    private Text runCompleteText;
    private RectTransform defeatRoot;
    private Text defeatText;
    private bool nextStagePresentationPending;
    private bool runCompletePresentationShown;
    private bool defeatPresentationShown;

    public BattleOutcome LastObservedOutcome { get; private set; } = BattleOutcome.InProgress;
    public int LastObservedStageIndex { get; private set; }
    public StageType LastObservedStageType { get; private set; } = StageType.Normal;
    public bool LastObservedRunCompleted { get; private set; }
    public bool LastStageClearShown { get; private set; }
    public bool LastNextStageShown { get; private set; }
    public bool LastBattleResumeShown { get; private set; }
    public bool LastRunCompleteShown { get; private set; }
    public bool LastDefeatShown { get; private set; }

    public IEnumerator PlayBattleOutcome(
        BattleOutcomeState outcomeState,
        LinearStageRuntimeState stageRuntimeState,
        LinearRunState runState)
    {
        if (nextStagePresentationPending && stageRuntimeState != null)
        {
            yield return PlayNextStage(stageRuntimeState);
            yield break;
        }

        if (outcomeState == null || outcomeState.IsInProgress)
        {
            yield break;
        }

        LastObservedOutcome = outcomeState.CurrentOutcome;

        if (stageRuntimeState != null)
        {
            LastObservedStageIndex = stageRuntimeState.CurrentStageIndex;
            LastObservedStageType = stageRuntimeState.CurrentStageType;
        }

        LastObservedRunCompleted = runState != null && runState.IsCompleted;

        if (ShouldShowDefeat(outcomeState))
        {
            yield return PlayDefeat();
            yield break;
        }

        if (ShouldShowRunComplete(outcomeState, runState))
        {
            yield return PlayRunComplete();
            yield break;
        }

        if (ShouldShowStageClear(outcomeState, stageRuntimeState))
        {
            yield return PlayStageClear();
            nextStagePresentationPending = true;
        }
    }

    private bool ShouldShowStageClear(
        BattleOutcomeState outcomeState,
        LinearStageRuntimeState stageRuntimeState)
    {
        return outcomeState != null &&
            outcomeState.IsVictory &&
            stageRuntimeState != null &&
            !stageRuntimeState.IsBossStage;
    }

    private bool ShouldShowRunComplete(BattleOutcomeState outcomeState, LinearRunState runState)
    {
        return outcomeState != null &&
            outcomeState.IsVictory &&
            runState != null &&
            runState.IsCompleted &&
            !runCompletePresentationShown;
    }

    private bool ShouldShowDefeat(BattleOutcomeState outcomeState)
    {
        return outcomeState != null &&
            outcomeState.IsDefeat &&
            !defeatPresentationShown;
    }

    private IEnumerator PlayStageClear()
    {
        EnsureStageClearView();

        if (stageClearRoot == null)
        {
            yield break;
        }

        LastStageClearShown = true;
        stageClearRoot.gameObject.SetActive(true);

        if (stageClearText != null)
        {
            stageClearText.text = "Stage Cleared";
        }

        yield return new WaitForSeconds(StageClearDurationSeconds);

        if (stageClearRoot != null)
        {
            stageClearRoot.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayNextStage(LinearStageRuntimeState stageRuntimeState)
    {
        EnsureNextStageView();

        if (nextStageRoot == null || stageRuntimeState == null)
        {
            nextStagePresentationPending = false;
            yield break;
        }

        LastObservedStageIndex = stageRuntimeState.CurrentStageIndex;
        LastObservedStageType = stageRuntimeState.CurrentStageType;
        LastNextStageShown = true;
        nextStagePresentationPending = false;

        if (nextStageTitleText != null)
        {
            nextStageTitleText.text = $"Stage {LastObservedStageIndex}";
        }

        if (nextStageTypeText != null)
        {
            nextStageTypeText.text = LastObservedStageType.ToString();
        }

        nextStageRoot.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextStageDurationSeconds);

        if (nextStageRoot != null)
        {
            nextStageRoot.gameObject.SetActive(false);
        }

        yield return PlayBattleResume();
    }

    private IEnumerator PlayBattleResume()
    {
        EnsureBattleResumeView();

        if (battleResumeRoot == null)
        {
            yield break;
        }

        LastBattleResumeShown = true;
        battleResumeRoot.gameObject.SetActive(true);

        if (battleResumeText != null)
        {
            battleResumeText.text = "Battle Start";
        }

        yield return new WaitForSeconds(BattleResumeDurationSeconds);

        if (battleResumeRoot != null)
        {
            battleResumeRoot.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayRunComplete()
    {
        EnsureRunCompleteView();

        if (runCompleteRoot == null)
        {
            yield break;
        }

        LastRunCompleteShown = true;
        runCompletePresentationShown = true;
        runCompleteRoot.gameObject.SetActive(true);

        if (runCompleteText != null)
        {
            runCompleteText.text = "Run Complete";
        }

        yield return new WaitForSeconds(RunCompleteDurationSeconds);

        if (runCompleteRoot != null)
        {
            runCompleteRoot.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayDefeat()
    {
        EnsureDefeatView();

        if (defeatRoot == null)
        {
            yield break;
        }

        LastDefeatShown = true;
        defeatPresentationShown = true;
        defeatRoot.gameObject.SetActive(true);

        if (defeatText != null)
        {
            defeatText.text = "Defeat";
        }

        yield return new WaitForSeconds(DefeatDurationSeconds);

        if (defeatRoot != null)
        {
            defeatRoot.gameObject.SetActive(false);
        }
    }

    private void EnsureStageClearView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null || stageClearRoot != null)
        {
            return;
        }

        GameObject rootObject = new GameObject("Run Flow Stage Clear");
        rootObject.layer = displayRoot.gameObject.layer;
        rootObject.transform.SetParent(displayRoot, false);

        stageClearRoot = rootObject.AddComponent<RectTransform>();
        stageClearRoot.anchorMin = new Vector2(0.5f, 0.5f);
        stageClearRoot.anchorMax = new Vector2(0.5f, 0.5f);
        stageClearRoot.pivot = new Vector2(0.5f, 0.5f);
        stageClearRoot.anchoredPosition = new Vector2(0f, 58f);
        stageClearRoot.sizeDelta = new Vector2(360f, 76f);

        Image panelImage = rootObject.AddComponent<Image>();
        panelImage.raycastTarget = false;
        panelImage.color = panelColor;

        stageClearText = CreateText("Stage Clear Text", stageClearRoot, 28, TextAnchor.MiddleCenter);
        stageClearRoot.gameObject.SetActive(false);
    }

    private void EnsureNextStageView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null || nextStageRoot != null)
        {
            return;
        }

        GameObject rootObject = new GameObject("Run Flow Next Stage");
        rootObject.layer = displayRoot.gameObject.layer;
        rootObject.transform.SetParent(displayRoot, false);

        nextStageRoot = rootObject.AddComponent<RectTransform>();
        nextStageRoot.anchorMin = new Vector2(0.5f, 0.5f);
        nextStageRoot.anchorMax = new Vector2(0.5f, 0.5f);
        nextStageRoot.pivot = new Vector2(0.5f, 0.5f);
        nextStageRoot.anchoredPosition = new Vector2(0f, 58f);
        nextStageRoot.sizeDelta = new Vector2(360f, 88f);

        Image panelImage = rootObject.AddComponent<Image>();
        panelImage.raycastTarget = false;
        panelImage.color = panelColor;

        nextStageTitleText = CreateText("Next Stage Title Text", nextStageRoot, 27, TextAnchor.UpperCenter);
        nextStageTitleText.rectTransform.anchorMin = new Vector2(0f, 0.45f);
        nextStageTitleText.rectTransform.anchorMax = Vector2.one;
        nextStageTitleText.rectTransform.sizeDelta = new Vector2(-12f, -8f);

        nextStageTypeText = CreateText("Next Stage Type Text", nextStageRoot, 18, TextAnchor.LowerCenter);
        nextStageTypeText.rectTransform.anchorMin = Vector2.zero;
        nextStageTypeText.rectTransform.anchorMax = new Vector2(1f, 0.55f);
        nextStageTypeText.rectTransform.sizeDelta = new Vector2(-12f, -8f);

        nextStageRoot.gameObject.SetActive(false);
    }

    private void EnsureBattleResumeView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null || battleResumeRoot != null)
        {
            return;
        }

        GameObject rootObject = new GameObject("Run Flow Battle Resume");
        rootObject.layer = displayRoot.gameObject.layer;
        rootObject.transform.SetParent(displayRoot, false);

        battleResumeRoot = rootObject.AddComponent<RectTransform>();
        battleResumeRoot.anchorMin = new Vector2(0.5f, 0.5f);
        battleResumeRoot.anchorMax = new Vector2(0.5f, 0.5f);
        battleResumeRoot.pivot = new Vector2(0.5f, 0.5f);
        battleResumeRoot.anchoredPosition = new Vector2(0f, 58f);
        battleResumeRoot.sizeDelta = new Vector2(320f, 70f);

        Image panelImage = rootObject.AddComponent<Image>();
        panelImage.raycastTarget = false;
        panelImage.color = panelColor;

        battleResumeText = CreateText("Battle Resume Text", battleResumeRoot, 25, TextAnchor.MiddleCenter);
        battleResumeRoot.gameObject.SetActive(false);
    }

    private void EnsureRunCompleteView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null || runCompleteRoot != null)
        {
            return;
        }

        GameObject rootObject = new GameObject("Run Flow Run Complete");
        rootObject.layer = displayRoot.gameObject.layer;
        rootObject.transform.SetParent(displayRoot, false);

        runCompleteRoot = rootObject.AddComponent<RectTransform>();
        runCompleteRoot.anchorMin = new Vector2(0.5f, 0.5f);
        runCompleteRoot.anchorMax = new Vector2(0.5f, 0.5f);
        runCompleteRoot.pivot = new Vector2(0.5f, 0.5f);
        runCompleteRoot.anchoredPosition = new Vector2(0f, 58f);
        runCompleteRoot.sizeDelta = new Vector2(380f, 82f);

        Image panelImage = rootObject.AddComponent<Image>();
        panelImage.raycastTarget = false;
        panelImage.color = panelColor;

        runCompleteText = CreateText("Run Complete Text", runCompleteRoot, 28, TextAnchor.MiddleCenter);
        runCompleteRoot.gameObject.SetActive(false);
    }

    private void EnsureDefeatView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null || defeatRoot != null)
        {
            return;
        }

        GameObject rootObject = new GameObject("Run Flow Defeat");
        rootObject.layer = displayRoot.gameObject.layer;
        rootObject.transform.SetParent(displayRoot, false);

        defeatRoot = rootObject.AddComponent<RectTransform>();
        defeatRoot.anchorMin = new Vector2(0.5f, 0.5f);
        defeatRoot.anchorMax = new Vector2(0.5f, 0.5f);
        defeatRoot.pivot = new Vector2(0.5f, 0.5f);
        defeatRoot.anchoredPosition = new Vector2(0f, 58f);
        defeatRoot.sizeDelta = new Vector2(340f, 78f);

        Image panelImage = rootObject.AddComponent<Image>();
        panelImage.raycastTarget = false;
        panelImage.color = panelColor;

        defeatText = CreateText("Defeat Text", defeatRoot, 28, TextAnchor.MiddleCenter);
        defeatRoot.gameObject.SetActive(false);
    }

    private void EnsureDisplayRoot()
    {
        if (displayRoot != null)
        {
            return;
        }

        GameObject battleField = GameObject.Find("BattleField");
        displayRoot = battleField != null ? battleField.GetComponent<RectTransform>() : null;

        if (displayRoot != null)
        {
            return;
        }

        GameObject battleRoot = GameObject.Find("BattleRoot");
        displayRoot = battleRoot != null ? battleRoot.GetComponent<RectTransform>() : null;
    }

    private Text CreateText(string objectName, RectTransform parent, int fontSize, TextAnchor alignment)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.layer = parent.gameObject.layer;
        textObject.transform.SetParent(parent, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = Vector2.zero;
        textTransform.anchorMax = Vector2.one;
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = Vector2.zero;
        textTransform.sizeDelta = new Vector2(-12f, -8f);

        Text text = textObject.AddComponent<Text>();
        text.raycastTarget = false;
        text.alignment = alignment;
        text.fontSize = fontSize;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 16;
        text.resizeTextMaxSize = fontSize;
        text.color = textColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.font = fallbackFont;
        return text;
    }
}
