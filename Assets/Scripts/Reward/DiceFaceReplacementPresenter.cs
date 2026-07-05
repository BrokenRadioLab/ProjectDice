using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class DiceFaceReplacementPresenter : MonoBehaviour
{
    private const int SlotCount = DiceModel.FaceSlotCount;
    private static Font fallbackFont;

    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private RectTransform panelRoot;
    [SerializeField] private Text titleText;
    [SerializeField] private Text newFaceTitleText;
    [SerializeField] private Text newFaceNameText;
    [SerializeField] private Text newFaceDescriptionText;
    [SerializeField] private Text chooseText;
    [SerializeField] private Button[] slotButtons = new Button[SlotCount];
    [SerializeField] private Text[] slotTexts = new Text[SlotCount];
    [SerializeField] private Color panelColor = new Color(0.08f, 0.07f, 0.06f, 0.96f);
    [SerializeField] private Color newFaceColor = new Color(0.26f, 0.20f, 0.12f, 0.98f);
    [SerializeField] private Color activeSlotColor = new Color(0.18f, 0.15f, 0.11f, 0.98f);
    [SerializeField] private Color lockedSlotColor = new Color(0.08f, 0.08f, 0.08f, 0.86f);
    [SerializeField] private Color textColor = new Color(0.96f, 0.90f, 0.72f, 1f);
    [SerializeField] private Color disabledTextColor = new Color(0.52f, 0.50f, 0.46f, 1f);

    private DiceFaceReplacementState replacementState;

    public bool IsShowing => panelRoot != null && panelRoot.gameObject.activeSelf;

    public IEnumerator Play(DiceFaceReplacementState state)
    {
        Bind(state);

        if (replacementState == null || !replacementState.IsReplacementActive)
        {
            yield break;
        }

        EnsureView();
        RefreshView();
        Show();

        while (replacementState != null &&
            replacementState.IsReplacementActive &&
            !replacementState.HasSelectedFaceSlot)
        {
            yield return null;
        }

        Hide();
    }

    public void Bind(DiceFaceReplacementState state)
    {
        replacementState = state;
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

    private void RefreshView()
    {
        if (titleText != null)
        {
            titleText.text = "Face Replacement";
        }

        if (newFaceTitleText != null)
        {
            newFaceTitleText.text = "New Face";
        }

        DiceFace pendingFace = replacementState != null ? replacementState.PendingFaceReward : null;
        if (newFaceNameText != null)
        {
            newFaceNameText.text = GetFaceName(pendingFace);
        }

        if (newFaceDescriptionText != null)
        {
            newFaceDescriptionText.text = GetFaceDescription(pendingFace);
        }

        if (chooseText != null)
        {
            chooseText.text = "Choose a Face to Replace";
        }

        DiceModel dice = replacementState != null ? replacementState.CurrentRuntimeDice : null;
        EnsureSlotArrays();
        for (int i = 0; i < SlotCount; i++)
        {
            bool isActiveSlot = dice != null && i < dice.ActiveFaceSlotCount;
            DiceFace face = dice != null && i < dice.FaceCount && isActiveSlot ? dice.GetFace(i) : null;
            bool isSelectable = replacementState != null && replacementState.IsReplacementCandidateSlot(i);

            Button slotButton = slotButtons[i];
            Text slotText = slotTexts[i];

            if (slotButton != null)
            {
                slotButton.gameObject.SetActive(true);
                slotButton.interactable = isSelectable;
                slotButton.onClick.RemoveAllListeners();

                Image slotImage = slotButton.GetComponent<Image>();
                if (slotImage != null)
                {
                    slotImage.color = isSelectable ? activeSlotColor : lockedSlotColor;
                }

                if (isSelectable)
                {
                    int slotIndex = i;
                    slotButton.onClick.AddListener(() => SelectReplacementSlot(slotIndex));
                }
            }

            if (slotText != null)
            {
                slotText.text = isActiveSlot ? GetSlotLabel(i, face) : $"Slot {i + 1}\nLocked";
                slotText.color = isSelectable ? textColor : disabledTextColor;
            }
        }
    }

    private void SelectReplacementSlot(int slotIndex)
    {
        if (replacementState == null || !replacementState.TrySelectReplacementSlot(slotIndex))
        {
            return;
        }

        Hide();
    }

    private void EnsureView()
    {
        EnsureDisplayRoot();
        EnsureEventSystem();

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
            panelRoot = FindRect(displayRoot, "Dice Face Replacement Panel");
        }

        if (panelRoot == null)
        {
            return false;
        }

        titleText = titleText != null ? titleText : FindText(panelRoot, "Replacement Title Text");
        newFaceTitleText = newFaceTitleText != null ? newFaceTitleText : FindText(panelRoot, "Replacement New Face Title Text");
        newFaceNameText = newFaceNameText != null ? newFaceNameText : FindText(panelRoot, "Replacement New Face Name Text");
        newFaceDescriptionText = newFaceDescriptionText != null
            ? newFaceDescriptionText
            : FindText(panelRoot, "Replacement New Face Description Text");
        chooseText = chooseText != null ? chooseText : FindText(panelRoot, "Replacement Choose Text");

        EnsureSlotArrays();
        for (int i = 0; i < SlotCount; i++)
        {
            slotButtons[i] = slotButtons[i] != null
                ? slotButtons[i]
                : FindButton(panelRoot, $"Replacement Slot {i + 1}");
            slotTexts[i] = slotTexts[i] != null
                ? slotTexts[i]
                : FindText(slotButtons[i] != null ? slotButtons[i].transform : panelRoot, $"Replacement Slot {i + 1} Text");
        }

        return true;
    }

    private void CreateFallbackView()
    {
        GameObject panelObject = new GameObject("Dice Face Replacement Panel");
        panelObject.transform.SetParent(displayRoot, false);
        panelRoot = panelObject.AddComponent<RectTransform>();
        panelRoot.anchorMin = new Vector2(0.5f, 0.5f);
        panelRoot.anchorMax = new Vector2(0.5f, 0.5f);
        panelRoot.pivot = new Vector2(0.5f, 0.5f);
        panelRoot.anchoredPosition = Vector2.zero;
        panelRoot.sizeDelta = new Vector2(900f, 500f);

        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.color = panelColor;
        panelImage.raycastTarget = true;

        titleText = CreateText("Replacement Title Text", panelRoot, 30, TextAnchor.MiddleCenter);
        SetAnchors(titleText.rectTransform, 0.04f, 0.88f, 0.96f, 0.98f);

        RectTransform newFacePanel = CreatePanel("Replacement New Face Panel", panelRoot, newFaceColor);
        SetAnchors(newFacePanel, 0.05f, 0.58f, 0.95f, 0.86f);

        newFaceTitleText = CreateText("Replacement New Face Title Text", newFacePanel, 18, TextAnchor.MiddleCenter);
        SetAnchors(newFaceTitleText.rectTransform, 0.04f, 0.68f, 0.24f, 0.96f);

        newFaceNameText = CreateText("Replacement New Face Name Text", newFacePanel, 28, TextAnchor.MiddleCenter);
        SetAnchors(newFaceNameText.rectTransform, 0.26f, 0.46f, 0.96f, 0.96f);

        newFaceDescriptionText = CreateText("Replacement New Face Description Text", newFacePanel, 18, TextAnchor.MiddleCenter);
        SetAnchors(newFaceDescriptionText.rectTransform, 0.26f, 0.06f, 0.96f, 0.48f);

        chooseText = CreateText("Replacement Choose Text", panelRoot, 22, TextAnchor.MiddleCenter);
        SetAnchors(chooseText.rectTransform, 0.04f, 0.46f, 0.96f, 0.56f);

        EnsureSlotArrays();
        for (int i = 0; i < SlotCount; i++)
        {
            slotButtons[i] = CreateSlotButton(i);
            slotTexts[i] = slotButtons[i].GetComponentInChildren<Text>();
        }
    }

    private Button CreateSlotButton(int slotIndex)
    {
        GameObject buttonObject = new GameObject($"Replacement Slot {slotIndex + 1}");
        buttonObject.transform.SetParent(panelRoot, false);

        RectTransform buttonTransform = buttonObject.AddComponent<RectTransform>();
        float slotWidth = 0.135f;
        float gap = 0.014f;
        float startX = 0.055f;
        float minX = startX + slotIndex * (slotWidth + gap);
        SetAnchors(buttonTransform, minX, 0.16f, minX + slotWidth, 0.43f);

        Image buttonImage = buttonObject.AddComponent<Image>();
        buttonImage.color = activeSlotColor;
        buttonImage.raycastTarget = true;

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = buttonImage;

        Text slotText = CreateText($"Replacement Slot {slotIndex + 1} Text", buttonTransform, 18, TextAnchor.MiddleCenter);
        slotText.rectTransform.anchorMin = Vector2.zero;
        slotText.rectTransform.anchorMax = Vector2.one;
        slotText.rectTransform.sizeDelta = new Vector2(-8f, -8f);

        return button;
    }

    private RectTransform CreatePanel(string objectName, RectTransform parent, Color color)
    {
        GameObject panelObject = new GameObject(objectName);
        panelObject.transform.SetParent(parent, false);
        RectTransform panelTransform = panelObject.AddComponent<RectTransform>();

        Image image = panelObject.AddComponent<Image>();
        image.color = color;
        image.raycastTarget = false;

        return panelTransform;
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
            GameObject canvasObject = new GameObject("Dice Face Replacement Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            displayRoot = canvasObject.GetComponent<RectTransform>();
        }
    }

    private static void EnsureEventSystem()
    {
        EventSystem eventSystem = FindAnyObjectByType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystem = eventSystemObject.AddComponent<EventSystem>();
        }

#if ENABLE_INPUT_SYSTEM
        InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (inputModule == null)
        {
            inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
        }

        if (inputModule.actionsAsset == null)
        {
            inputModule.AssignDefaultActions();
        }
#else
        if (eventSystem.GetComponent<StandaloneInputModule>() == null)
        {
            eventSystem.gameObject.AddComponent<StandaloneInputModule>();
        }
#endif
    }

    private void EnsureSlotArrays()
    {
        if (slotButtons == null || slotButtons.Length != SlotCount)
        {
            slotButtons = new Button[SlotCount];
        }

        if (slotTexts == null || slotTexts.Length != SlotCount)
        {
            slotTexts = new Text[SlotCount];
        }
    }

    private Text CreateText(string objectName, Transform parent, int fontSize, TextAnchor alignment)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.transform.SetParent(parent, false);

        RectTransform textTransform = textObject.AddComponent<RectTransform>();
        textTransform.anchorMin = Vector2.zero;
        textTransform.anchorMax = Vector2.one;
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = Vector2.zero;
        textTransform.sizeDelta = Vector2.zero;

        Text text = textObject.AddComponent<Text>();
        text.font = GetFallbackFont();
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = textColor;
        text.raycastTarget = false;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 10;
        text.resizeTextMaxSize = fontSize;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;

        return text;
    }

    private static void SetAnchors(RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
    {
        rectTransform.anchorMin = new Vector2(minX, minY);
        rectTransform.anchorMax = new Vector2(maxX, maxY);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    private static string GetSlotLabel(int slotIndex, DiceFace face)
    {
        return $"Slot {slotIndex + 1}\n{GetFaceName(face)}";
    }

    private static string GetFaceName(DiceFace face)
    {
        return face == null || string.IsNullOrWhiteSpace(face.DisplayName) ? "Empty" : face.DisplayName;
    }

    private static string GetFaceDescription(DiceFace face)
    {
        if (face == null)
        {
            return string.Empty;
        }

        return string.IsNullOrWhiteSpace(face.ShortDescription)
            ? "Choose an active Face to replace later."
            : face.ShortDescription;
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
