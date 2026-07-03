using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class RewardSelectionPresenter : MonoBehaviour
{
    private const int OptionCount = 3;
    private static Font fallbackFont;

    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private RectTransform panelRoot;
    [SerializeField] private Text titleText;
    [SerializeField] private Text chooseOneText;
    [SerializeField] private Button[] optionButtons = new Button[OptionCount];
    [SerializeField] private Text[] optionTexts = new Text[OptionCount];
    [SerializeField] private Color panelColor = new Color(0.08f, 0.07f, 0.06f, 0.96f);
    [SerializeField] private Color buttonColor = new Color(0.18f, 0.15f, 0.11f, 0.98f);
    [SerializeField] private Color textColor = new Color(0.96f, 0.90f, 0.72f, 1f);

    private RewardSelectionState rewardSelectionState;

    public bool IsShowing => panelRoot != null && panelRoot.gameObject.activeSelf;

    public IEnumerator Play(RewardSelectionState state)
    {
        Bind(state);

        if (rewardSelectionState == null || !rewardSelectionState.IsSelectionActive)
        {
            yield break;
        }

        EnsureView();
        RefreshOptions();
        Show();

        while (rewardSelectionState != null && rewardSelectionState.IsSelectionActive)
        {
            yield return null;
        }

        Hide();
    }

    public void Bind(RewardSelectionState state)
    {
        rewardSelectionState = state;
    }

    private void Show()
    {
        if (panelRoot != null)
        {
            panelRoot.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        if (panelRoot != null)
        {
            panelRoot.gameObject.SetActive(false);
        }
    }

    private void RefreshOptions()
    {
        if (titleText != null)
        {
            titleText.text = "Reward Selection";
        }

        if (chooseOneText != null)
        {
            chooseOneText.text = "Choose One";
        }

        for (int i = 0; i < OptionCount; i++)
        {
            RewardData reward = rewardSelectionState != null &&
                rewardSelectionState.CurrentRewards != null &&
                i < rewardSelectionState.CurrentRewards.Count
                ? rewardSelectionState.CurrentRewards[i]
                : null;

            Button optionButton = optionButtons != null && i < optionButtons.Length ? optionButtons[i] : null;
            Text optionText = optionTexts != null && i < optionTexts.Length ? optionTexts[i] : null;

            if (optionButton != null)
            {
                optionButton.gameObject.SetActive(reward != null);
                optionButton.onClick.RemoveAllListeners();

                int rewardIndex = i;
                optionButton.onClick.AddListener(() => SelectReward(rewardIndex));
            }

            if (optionText != null)
            {
                optionText.text = reward != null ? GetRewardLabel(reward) : string.Empty;
            }
        }
    }

    private void SelectReward(int rewardIndex)
    {
        if (rewardSelectionState == null || !rewardSelectionState.TrySelectReward(rewardIndex))
        {
            return;
        }

        Hide();
    }

    private static string GetRewardLabel(RewardData reward)
    {
        string rewardName = string.IsNullOrWhiteSpace(reward.DisplayName) ? "Reward" : reward.DisplayName;
        string rewardDescription = GetRewardDescription(reward);
        return $"{rewardName}\n{reward.RewardType}\n{rewardDescription}";
    }

    private static string GetRewardDescription(RewardData reward)
    {
        if (!string.IsNullOrWhiteSpace(reward.ShortDescription))
        {
            return reward.ShortDescription;
        }

        if (reward.RewardType == RewardType.Face && reward.Face != null)
        {
            return string.IsNullOrWhiteSpace(reward.Face.ShortDescription)
                ? "Add this Face to this run."
                : reward.Face.ShortDescription;
        }

        if (reward.RewardType == RewardType.Heal)
        {
            return $"Recover {reward.Value} HP.";
        }

        if (reward.RewardType == RewardType.MaxHp)
        {
            return $"Increase Max HP by {reward.Value}.";
        }

        return "Future run modifier.";
    }

    private void EnsureView()
    {
        EnsureDisplayRoot();

        if (displayRoot == null)
        {
            return;
        }

        if (TryBindExistingView())
        {
            Hide();
            return;
        }

        CreateFallbackView();
        Hide();
    }

    private bool TryBindExistingView()
    {
        if (panelRoot == null)
        {
            panelRoot = FindRect(displayRoot, "Reward Selection Panel");
        }

        if (panelRoot == null)
        {
            return false;
        }

        titleText = titleText != null ? titleText : FindText(panelRoot, "Reward Title Text");
        chooseOneText = chooseOneText != null ? chooseOneText : FindText(panelRoot, "Reward Choose One Text");

        EnsureOptionArrays();
        for (int i = 0; i < OptionCount; i++)
        {
            optionButtons[i] = optionButtons[i] != null
                ? optionButtons[i]
                : FindButton(panelRoot, $"Reward Option {i + 1}");
            optionTexts[i] = optionTexts[i] != null
                ? optionTexts[i]
                : FindText(optionButtons[i] != null ? optionButtons[i].transform : panelRoot, $"Reward Option {i + 1} Text");
        }

        return true;
    }

    private void CreateFallbackView()
    {
        GameObject panelObject = new GameObject("Reward Selection Panel");
        panelObject.transform.SetParent(displayRoot, false);
        panelRoot = panelObject.AddComponent<RectTransform>();
        panelRoot.anchorMin = new Vector2(0.5f, 0.5f);
        panelRoot.anchorMax = new Vector2(0.5f, 0.5f);
        panelRoot.pivot = new Vector2(0.5f, 0.5f);
        panelRoot.anchoredPosition = Vector2.zero;
        panelRoot.sizeDelta = new Vector2(860f, 430f);

        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.color = panelColor;
        panelImage.raycastTarget = true;

        titleText = CreateText("Reward Title Text", panelRoot, 30, TextAnchor.MiddleCenter);
        titleText.rectTransform.anchorMin = new Vector2(0f, 0.80f);
        titleText.rectTransform.anchorMax = new Vector2(1f, 1f);
        titleText.rectTransform.sizeDelta = new Vector2(-32f, -14f);

        chooseOneText = CreateText("Reward Choose One Text", panelRoot, 22, TextAnchor.MiddleCenter);
        chooseOneText.rectTransform.anchorMin = new Vector2(0f, 0.68f);
        chooseOneText.rectTransform.anchorMax = new Vector2(1f, 0.83f);
        chooseOneText.rectTransform.sizeDelta = new Vector2(-32f, -10f);

        EnsureOptionArrays();
        for (int i = 0; i < OptionCount; i++)
        {
            optionButtons[i] = CreateOptionButton(i);
            optionTexts[i] = optionButtons[i].GetComponentInChildren<Text>();
        }
    }

    private Button CreateOptionButton(int optionIndex)
    {
        GameObject buttonObject = new GameObject($"Reward Option {optionIndex + 1}");
        buttonObject.transform.SetParent(panelRoot, false);

        RectTransform buttonTransform = buttonObject.AddComponent<RectTransform>();
        buttonTransform.anchorMin = new Vector2(0.05f + optionIndex * 0.31f, 0.15f);
        buttonTransform.anchorMax = new Vector2(0.30f + optionIndex * 0.31f, 0.64f);
        buttonTransform.sizeDelta = Vector2.zero;

        Image buttonImage = buttonObject.AddComponent<Image>();
        buttonImage.color = buttonColor;
        buttonImage.raycastTarget = true;

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = buttonImage;

        Text optionText = CreateText($"Reward Option {optionIndex + 1} Text", buttonTransform, 19, TextAnchor.MiddleCenter);
        optionText.rectTransform.anchorMin = Vector2.zero;
        optionText.rectTransform.anchorMax = Vector2.one;
        optionText.rectTransform.sizeDelta = new Vector2(-18f, -16f);

        return button;
    }

    private void EnsureDisplayRoot()
    {
        if (displayRoot != null)
        {
            return;
        }

        GameObject battleRoot = GameObject.Find("BattleRoot");
        if (battleRoot != null)
        {
            displayRoot = battleRoot.GetComponent<RectTransform>();
        }

        if (displayRoot == null)
        {
            Canvas existingCanvas = FindAnyObjectByType<Canvas>();
            if (existingCanvas != null)
            {
                displayRoot = existingCanvas.GetComponent<RectTransform>();
            }
        }

        if (displayRoot == null)
        {
            GameObject canvasObject = new GameObject("Reward Selection Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            displayRoot = canvasObject.GetComponent<RectTransform>();
        }
    }

    private void EnsureOptionArrays()
    {
        if (optionButtons == null || optionButtons.Length != OptionCount)
        {
            optionButtons = new Button[OptionCount];
        }

        if (optionTexts == null || optionTexts.Length != OptionCount)
        {
            optionTexts = new Text[OptionCount];
        }
    }

    private Text CreateText(string objectName, Transform parent, int fontSize, TextAnchor alignment)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.transform.SetParent(parent, false);

        Text text = textObject.AddComponent<Text>();
        text.font = GetFallbackFont();
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = textColor;
        text.raycastTarget = false;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;

        return text;
    }

    private static Font GetFallbackFont()
    {
        if (fallbackFont == null)
        {
            fallbackFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        return fallbackFont;
    }

    private static RectTransform FindRect(Transform parent, string objectName)
    {
        if (parent == null)
        {
            return null;
        }

        Transform child = parent.Find(objectName);
        return child != null ? child.GetComponent<RectTransform>() : null;
    }

    private static Text FindText(Transform parent, string objectName)
    {
        if (parent == null)
        {
            return null;
        }

        Transform child = parent.Find(objectName);
        return child != null ? child.GetComponent<Text>() : null;
    }

    private static Button FindButton(Transform parent, string objectName)
    {
        if (parent == null)
        {
            return null;
        }

        Transform child = parent.Find(objectName);
        return child != null ? child.GetComponent<Button>() : null;
    }
}
