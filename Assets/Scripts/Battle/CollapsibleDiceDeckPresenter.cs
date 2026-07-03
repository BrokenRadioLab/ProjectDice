using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class CollapsibleDiceDeckPresenter : MonoBehaviour
{
    private const float ToggleWidth = 156f;
    private const float ToggleHeight = 72f;
    private const float ToggleLeftPadding = 28f;
    private const float PanelGap = 12f;
    private const float PanelWidth = 760f;
    private const float PanelHeight = 128f;
    private const float SlotSize = 104f;
    private const float SlotSpacing = 10f;

    [SerializeField] private BattleDiceState battleDiceState;
    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private bool startsExpanded;
    [SerializeField] private Color collapsedButtonColor = new Color(0.18f, 0.17f, 0.15f, 0.86f);
    [SerializeField] private Color expandedPanelColor = new Color(0.12f, 0.11f, 0.10f, 0.92f);
    [SerializeField] private Color rowColor = new Color(0.25f, 0.22f, 0.18f, 0.86f);
    [SerializeField] private Color textColor = new Color(0.94f, 0.90f, 0.78f, 1f);

    [SerializeField] private RectTransform toggleRoot;
    [SerializeField] private Text toggleText;
    [SerializeField] private RectTransform panelRoot;
    [SerializeField] private Text[] slotTexts;
    private bool isExpanded;
    private static Font fallbackFont;

    private void Awake()
    {
        EnsureReferences();
        EnsureEventSystem();
        EnsureView();
        SetExpanded(startsExpanded);
    }

    private void Update()
    {
        if (isExpanded)
        {
            RefreshSlots();
        }
    }

    public void Toggle()
    {
        SetExpanded(!isExpanded);
    }

    public void SetExpanded(bool expanded)
    {
        EnsureView();

        isExpanded = expanded;

        if (panelRoot != null)
        {
            panelRoot.gameObject.SetActive(isExpanded);
        }

        if (toggleText != null)
        {
            toggleText.text = isExpanded ? "Close Deck" : "Dice Deck";
        }

        if (isExpanded)
        {
            RefreshSlots();
        }
    }

    private void EnsureReferences()
    {
        if (battleDiceState == null)
        {
            battleDiceState = FindAnyObjectByType<BattleDiceState>();
        }

        if (displayRoot == null)
        {
            GameObject bottomHud = GameObject.Find("BottomHUD");
            displayRoot = bottomHud != null ? bottomHud.GetComponent<RectTransform>() : null;
        }

        if (displayRoot == null)
        {
            GameObject battleRoot = GameObject.Find("BattleRoot");
            displayRoot = battleRoot != null ? battleRoot.GetComponent<RectTransform>() : null;
        }
    }

    private void EnsureView()
    {
        EnsureReferences();
        EnsureEventSystem();

        if (displayRoot == null)
        {
            return;
        }

        EnsureToggle();
        EnsurePanel();
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

    private void EnsureToggle()
    {
        if (toggleRoot != null)
        {
            BindToggleButton();
            return;
        }

        Transform existingToggle = displayRoot.Find("Dice Deck Toggle");
        if (existingToggle != null)
        {
            toggleRoot = existingToggle.GetComponent<RectTransform>();
            toggleText = toggleText != null ? toggleText : existingToggle.GetComponentInChildren<Text>(true);
            BindToggleButton();
            return;
        }

        GameObject toggleObject = new GameObject("Dice Deck Toggle");
        toggleObject.layer = displayRoot.gameObject.layer;
        toggleObject.transform.SetParent(displayRoot, false);

        toggleRoot = toggleObject.AddComponent<RectTransform>();
        toggleRoot.anchorMin = new Vector2(0f, 0.5f);
        toggleRoot.anchorMax = new Vector2(0f, 0.5f);
        toggleRoot.pivot = new Vector2(0f, 0.5f);
        toggleRoot.anchoredPosition = new Vector2(ToggleLeftPadding, 0f);
        toggleRoot.sizeDelta = new Vector2(ToggleWidth, ToggleHeight);

        Image image = toggleObject.AddComponent<Image>();
        image.color = collapsedButtonColor;

        Button button = toggleObject.AddComponent<Button>();
        button.targetGraphic = image;

        toggleText = CreateText("Dice Deck Toggle Text", toggleRoot, 16, TextAnchor.MiddleCenter);
        toggleText.rectTransform.anchorMin = Vector2.zero;
        toggleText.rectTransform.anchorMax = Vector2.one;
        toggleText.rectTransform.sizeDelta = Vector2.zero;
        toggleText.text = "Dice Deck";
        BindToggleButton();
    }

    private void EnsurePanel()
    {
        if (panelRoot != null)
        {
            BindExistingSlotTexts();
            return;
        }

        Transform existingPanel = displayRoot.Find("Dice Deck Expanded View");
        if (existingPanel != null)
        {
            panelRoot = existingPanel.GetComponent<RectTransform>();
            BindExistingSlotTexts();
            return;
        }

        GameObject panelObject = new GameObject("Dice Deck Expanded View");
        panelObject.layer = displayRoot.gameObject.layer;
        panelObject.transform.SetParent(displayRoot, false);

        panelRoot = panelObject.AddComponent<RectTransform>();
        panelRoot.anchorMin = new Vector2(0f, 1f);
        panelRoot.anchorMax = new Vector2(0f, 1f);
        panelRoot.pivot = new Vector2(0f, 0f);
        panelRoot.anchoredPosition = new Vector2(ToggleLeftPadding, PanelGap);
        panelRoot.sizeDelta = new Vector2(PanelWidth, PanelHeight);

        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.raycastTarget = true;
        panelImage.color = expandedPanelColor;

        HorizontalLayoutGroup layoutGroup = panelObject.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.padding = new RectOffset(16, 16, 12, 12);
        layoutGroup.spacing = SlotSpacing;
        layoutGroup.childAlignment = TextAnchor.MiddleLeft;
        layoutGroup.childControlWidth = false;
        layoutGroup.childControlHeight = false;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;

        slotTexts = new Text[DiceModel.FaceSlotCount];

        for (int i = 0; i < DiceModel.FaceSlotCount; i++)
        {
            slotTexts[i] = CreateSlot(i);
        }
    }

    private Text CreateSlot(int slotIndex)
    {
        GameObject rowObject = new GameObject($"Dice Deck Slot {slotIndex + 1}");
        rowObject.layer = panelRoot.gameObject.layer;
        rowObject.transform.SetParent(panelRoot, false);

        RectTransform rowTransform = rowObject.AddComponent<RectTransform>();
        rowTransform.sizeDelta = new Vector2(SlotSize, SlotSize);

        Image rowImage = rowObject.AddComponent<Image>();
        rowImage.raycastTarget = false;
        rowImage.color = rowColor;

        Text rowText = CreateText("Dice Deck Slot Text", rowTransform, 26, TextAnchor.MiddleCenter);
        rowText.rectTransform.anchorMin = Vector2.zero;
        rowText.rectTransform.anchorMax = Vector2.one;
        rowText.rectTransform.anchoredPosition = Vector2.zero;
        rowText.rectTransform.sizeDelta = new Vector2(-12f, -12f);
        return rowText;
    }

    private void BindToggleButton()
    {
        if (toggleRoot == null)
        {
            return;
        }

        Button button = toggleRoot.GetComponent<Button>();
        if (button == null)
        {
            button = toggleRoot.gameObject.AddComponent<Button>();
        }

        Graphic targetGraphic = button.targetGraphic != null ? button.targetGraphic : toggleRoot.GetComponent<Graphic>();
        if (targetGraphic == null)
        {
            Image image = toggleRoot.gameObject.AddComponent<Image>();
            image.color = collapsedButtonColor;
            targetGraphic = image;
        }

        button.targetGraphic = targetGraphic;
        button.onClick.RemoveListener(Toggle);
        button.onClick.AddListener(Toggle);
    }

    private void BindExistingSlotTexts()
    {
        if (panelRoot == null)
        {
            return;
        }

        if (slotTexts != null && slotTexts.Length == DiceModel.FaceSlotCount)
        {
            return;
        }

        slotTexts = new Text[DiceModel.FaceSlotCount];
        for (int i = 0; i < DiceModel.FaceSlotCount; i++)
        {
            Transform slot = panelRoot.Find($"Dice Deck Slot {i + 1}");
            slotTexts[i] = slot != null ? slot.GetComponentInChildren<Text>(true) : null;
        }
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
        textTransform.sizeDelta = Vector2.zero;

        Text text = textObject.AddComponent<Text>();
        text.raycastTarget = false;
        text.alignment = alignment;
        text.fontSize = fontSize;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 9;
        text.resizeTextMaxSize = fontSize;
        text.color = textColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.font = fallbackFont;
        return text;
    }

    private void RefreshSlots()
    {
        if (slotTexts == null)
        {
            return;
        }

        DiceModel dice = battleDiceState != null ? battleDiceState.CurrentDice : null;

        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (slotTexts[i] == null)
            {
                continue;
            }

            bool isLocked = dice != null && i >= dice.ActiveFaceSlotCount;
            DiceFace face = dice != null && i < dice.FaceCount && !isLocked ? dice.GetFace(i) : null;
            slotTexts[i].text = isLocked ? "Locked" : GetFaceLabel(face);
        }
    }

    private static string GetFaceLabel(DiceFace face)
    {
        if (face == null || string.IsNullOrWhiteSpace(face.DisplayName))
        {
            return "Empty";
        }

        return face.DisplayName;
    }
}
