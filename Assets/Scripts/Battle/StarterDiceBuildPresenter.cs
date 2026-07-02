using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif

public sealed class StarterDiceBuildPresenter : MonoBehaviour
{
    private const int ActiveSlotCount = 4;
    private const int BuildSlotCount = DiceModel.FaceSlotCount;
    private const int StarterBaseThrowDamage = 3;

    [SerializeField] private BattleDiceState battleDiceState;
    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private Color overlayColor = new Color(0.07f, 0.06f, 0.05f, 0.96f);
    [SerializeField] private Color panelColor = new Color(0.14f, 0.12f, 0.10f, 0.96f);
    [SerializeField] private Color buttonColor = new Color(0.26f, 0.22f, 0.17f, 1f);
    [SerializeField] private Color disabledButtonColor = new Color(0.11f, 0.10f, 0.09f, 0.92f);
    [SerializeField] private Color textColor = new Color(0.94f, 0.90f, 0.78f, 1f);

    private RectTransform overlayRoot;
    private RectTransform mainMenuRoot;
    private RectTransform buildRoot;
    private Text titleText;
    private Text probabilityText;
    private Button startBattleButton;
    private Text[] activeSlotTexts;
    private Text[] lockedSlotTexts;
    private Button[] poolButtons;
    private Text[] poolButtonTexts;
    private DiceFace[] starterPool;
    private readonly List<int> selectedPoolIndexes = new List<int>(ActiveSlotCount);
    private bool isComplete;
    private static Font fallbackFont;

    public bool IsComplete => isComplete;

    private void Awake()
    {
        EnsureReferences();
        EnsureEventSystem();
        EnsureView();
    }

    public void Configure(BattleDiceState diceState)
    {
        battleDiceState = diceState;
    }

    public void Begin()
    {
        EnsureView();
        isComplete = false;
        selectedPoolIndexes.Clear();

        if (overlayRoot != null)
        {
            overlayRoot.gameObject.SetActive(true);
        }

        ShowMainMenu();
    }

    private void EnsureReferences()
    {
        if (battleDiceState == null)
        {
            battleDiceState = FindAnyObjectByType<BattleDiceState>();
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

        if (displayRoot == null || overlayRoot != null)
        {
            return;
        }

        starterPool = StarterDiceFactory.CreateStarterFacePool();
        overlayRoot = CreateRoot("Starter Dice Build Overlay", displayRoot, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        Image overlayImage = overlayRoot.gameObject.AddComponent<Image>();
        overlayImage.color = overlayColor;

        mainMenuRoot = CreateCenteredPanel("Run Start Panel", new Vector2(560f, 300f));
        buildRoot = CreateCenteredPanel("Starter Dice Build Panel", new Vector2(900f, 520f));

        BuildMainMenu();
        BuildStarterDiceBuildView();
        overlayRoot.gameObject.SetActive(false);
    }

    private void BuildMainMenu()
    {
        titleText = CreateText("Run Start Title", mainMenuRoot, 40, TextAnchor.MiddleCenter);
        titleText.rectTransform.anchorMin = new Vector2(0f, 0.62f);
        titleText.rectTransform.anchorMax = new Vector2(1f, 0.95f);
        titleText.text = "Project Dice";

        Button startRunButton = CreateButton("Start Run Button", mainMenuRoot, new Vector2(0f, -74f), new Vector2(240f, 82f), "Start Run", 28);
        startRunButton.onClick.AddListener(ShowBuildView);
    }

    private void BuildStarterDiceBuildView()
    {
        Text buildTitle = CreateText("Starter Dice Build Title", buildRoot, 30, TextAnchor.MiddleCenter);
        buildTitle.rectTransform.anchorMin = new Vector2(0f, 0.86f);
        buildTitle.rectTransform.anchorMax = new Vector2(1f, 1f);
        buildTitle.text = "Starter Dice Build";

        Text poolTitle = CreateText("Starter Face Pool Title", buildRoot, 22, TextAnchor.MiddleLeft);
        poolTitle.rectTransform.anchorMin = new Vector2(0.05f, 0.72f);
        poolTitle.rectTransform.anchorMax = new Vector2(0.48f, 0.82f);
        poolTitle.text = "Face Pool";

        poolButtons = new Button[starterPool.Length];
        poolButtonTexts = new Text[starterPool.Length];

        for (int i = 0; i < starterPool.Length; i++)
        {
            int poolIndex = i;
            Button button = CreateButton(
                $"Starter Face Pool Button {i + 1}",
                buildRoot,
                new Vector2(-320f + (i * 160f), 122f),
                new Vector2(138f, 72f),
                starterPool[i].DisplayName,
                20);
            button.onClick.AddListener(() => AddFace(poolIndex));
            poolButtons[i] = button;
            poolButtonTexts[i] = button.GetComponentInChildren<Text>();
        }

        Text diceTitle = CreateText("Starter Dice Preview Title", buildRoot, 22, TextAnchor.MiddleLeft);
        diceTitle.rectTransform.anchorMin = new Vector2(0.05f, 0.48f);
        diceTitle.rectTransform.anchorMax = new Vector2(0.5f, 0.58f);
        diceTitle.text = "Wood Dice";

        activeSlotTexts = new Text[ActiveSlotCount];
        for (int i = 0; i < ActiveSlotCount; i++)
        {
            int slotIndex = i;
            Button slotButton = CreateButton(
                $"Starter Active Slot {i + 1}",
                buildRoot,
                new Vector2(-300f + (i * 156f), 4f),
                new Vector2(136f, 90f),
                "Empty",
                20);
            slotButton.onClick.AddListener(() => RemoveSlot(slotIndex));
            activeSlotTexts[i] = slotButton.GetComponentInChildren<Text>();
        }

        lockedSlotTexts = new Text[2];
        for (int i = 0; i < lockedSlotTexts.Length; i++)
        {
            Button lockedButton = CreateButton(
                $"Starter Locked Slot {i + 1}",
                buildRoot,
                new Vector2(324f + (i * 156f), 4f),
                new Vector2(136f, 90f),
                "Locked",
                18);
            lockedButton.interactable = false;
            Image lockedImage = lockedButton.targetGraphic as Image;
            if (lockedImage != null)
            {
                lockedImage.color = disabledButtonColor;
            }

            lockedSlotTexts[i] = lockedButton.GetComponentInChildren<Text>();
        }

        probabilityText = CreateText("Starter Dice Probability Text", buildRoot, 22, TextAnchor.UpperLeft);
        probabilityText.rectTransform.anchorMin = new Vector2(0.08f, 0.07f);
        probabilityText.rectTransform.anchorMax = new Vector2(0.66f, 0.34f);
        probabilityText.text = string.Empty;

        startBattleButton = CreateButton("Start Battle Button", buildRoot, new Vector2(300f, -174f), new Vector2(250f, 76f), "Start Battle", 24);
        startBattleButton.onClick.AddListener(CompleteBuild);
        RefreshBuildView();
    }

    private void ShowMainMenu()
    {
        if (mainMenuRoot != null)
        {
            mainMenuRoot.gameObject.SetActive(true);
        }

        if (buildRoot != null)
        {
            buildRoot.gameObject.SetActive(false);
        }
    }

    private void ShowBuildView()
    {
        if (mainMenuRoot != null)
        {
            mainMenuRoot.gameObject.SetActive(false);
        }

        if (buildRoot != null)
        {
            buildRoot.gameObject.SetActive(true);
        }

        RefreshBuildView();
    }

    private void AddFace(int poolIndex)
    {
        if (selectedPoolIndexes.Count >= ActiveSlotCount || selectedPoolIndexes.Contains(poolIndex))
        {
            return;
        }

        selectedPoolIndexes.Add(poolIndex);
        RefreshBuildView();
    }

    private void RemoveSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= selectedPoolIndexes.Count)
        {
            return;
        }

        selectedPoolIndexes.RemoveAt(slotIndex);
        RefreshBuildView();
    }

    private void CompleteBuild()
    {
        if (selectedPoolIndexes.Count != ActiveSlotCount)
        {
            return;
        }

        DiceFace[] faces = new DiceFace[BuildSlotCount];
        for (int i = 0; i < ActiveSlotCount; i++)
        {
            faces[i] = starterPool[selectedPoolIndexes[i]].Clone();
        }

        battleDiceState?.SetCurrentDice(new DiceModel(faces, StarterBaseThrowDamage, ActiveSlotCount));
        isComplete = true;

        if (overlayRoot != null)
        {
            overlayRoot.gameObject.SetActive(false);
        }
    }

    private void RefreshBuildView()
    {
        if (activeSlotTexts != null)
        {
            for (int i = 0; i < activeSlotTexts.Length; i++)
            {
                DiceFace face = i < selectedPoolIndexes.Count ? starterPool[selectedPoolIndexes[i]] : null;
                activeSlotTexts[i].text = face != null ? face.DisplayName : "Empty";
            }
        }

        if (poolButtons != null)
        {
            for (int i = 0; i < poolButtons.Length; i++)
            {
                bool isSelected = selectedPoolIndexes.Contains(i);
                poolButtons[i].interactable = !isSelected && selectedPoolIndexes.Count < ActiveSlotCount;
                if (poolButtonTexts[i] != null)
                {
                    poolButtonTexts[i].text = isSelected ? $"{starterPool[i].DisplayName}\nPicked" : starterPool[i].DisplayName;
                }
            }
        }

        if (startBattleButton != null)
        {
            startBattleButton.interactable = selectedPoolIndexes.Count == ActiveSlotCount;
        }

        if (probabilityText != null)
        {
            probabilityText.text = BuildProbabilityText();
        }
    }

    private string BuildProbabilityText()
    {
        if (selectedPoolIndexes.Count == 0)
        {
            return "Probability\nChoose 4 active Faces.";
        }

        Dictionary<string, int> counts = new Dictionary<string, int>();
        for (int i = 0; i < selectedPoolIndexes.Count; i++)
        {
            string faceName = starterPool[selectedPoolIndexes[i]].DisplayName;
            counts.TryGetValue(faceName, out int currentCount);
            counts[faceName] = currentCount + 1;
        }

        string text = "Probability";
        foreach (KeyValuePair<string, int> pair in counts)
        {
            int percent = Mathf.RoundToInt(pair.Value * 100f / ActiveSlotCount);
            text += $"\n{pair.Key} x{pair.Value}  {percent}%";
        }

        return text;
    }

    private RectTransform CreateCenteredPanel(string objectName, Vector2 size)
    {
        RectTransform root = CreateRoot(objectName, overlayRoot, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), size);
        Image image = root.gameObject.AddComponent<Image>();
        image.color = panelColor;
        return root;
    }

    private static RectTransform CreateRoot(
        string objectName,
        RectTransform parent,
        Vector2 anchorMin,
        Vector2 anchorMax,
        Vector2 pivot,
        Vector2 size)
    {
        GameObject rootObject = new GameObject(objectName);
        rootObject.layer = parent.gameObject.layer;
        rootObject.transform.SetParent(parent, false);

        RectTransform root = rootObject.AddComponent<RectTransform>();
        root.anchorMin = anchorMin;
        root.anchorMax = anchorMax;
        root.pivot = pivot;
        root.anchoredPosition = Vector2.zero;
        root.sizeDelta = size;
        return root;
    }

    private Button CreateButton(string objectName, RectTransform parent, Vector2 anchoredPosition, Vector2 size, string label, int fontSize)
    {
        RectTransform root = CreateRoot(objectName, parent, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), size);
        root.anchoredPosition = anchoredPosition;

        Image image = root.gameObject.AddComponent<Image>();
        image.color = buttonColor;

        Button button = root.gameObject.AddComponent<Button>();
        button.targetGraphic = image;

        Text text = CreateText($"{objectName} Text", root, fontSize, TextAnchor.MiddleCenter);
        text.text = label;
        return button;
    }

    private Text CreateText(string objectName, RectTransform parent, int fontSize, TextAnchor alignment)
    {
        RectTransform root = CreateRoot(objectName, parent, Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f), Vector2.zero);

        Text text = root.gameObject.AddComponent<Text>();
        text.raycastTarget = false;
        text.alignment = alignment;
        text.fontSize = fontSize;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 10;
        text.resizeTextMaxSize = fontSize;
        text.color = textColor;
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.font = fallbackFont;
        return text;
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
}
