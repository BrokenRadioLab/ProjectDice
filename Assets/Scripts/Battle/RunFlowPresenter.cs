using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class RunFlowPresenter : MonoBehaviour
{
    private const float StageClearDurationSeconds = 0.55f;
    private static Font fallbackFont;

    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private Color panelColor = new Color(0.10f, 0.09f, 0.08f, 0.92f);
    [SerializeField] private Color textColor = new Color(0.96f, 0.90f, 0.70f, 1f);

    private RectTransform stageClearRoot;
    private Text stageClearText;

    public BattleOutcome LastObservedOutcome { get; private set; } = BattleOutcome.InProgress;
    public int LastObservedStageIndex { get; private set; }
    public StageType LastObservedStageType { get; private set; } = StageType.Normal;
    public bool LastObservedRunCompleted { get; private set; }
    public bool LastStageClearShown { get; private set; }

    public IEnumerator PlayBattleOutcome(
        BattleOutcomeState outcomeState,
        LinearStageRuntimeState stageRuntimeState,
        LinearRunState runState)
    {
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

        if (ShouldShowStageClear(outcomeState, stageRuntimeState))
        {
            yield return PlayStageClear();
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
