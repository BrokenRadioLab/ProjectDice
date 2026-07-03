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
    private const int CurrentDiceTier = 1;
    private const int PoolColumns = 5;
    private const float PoolButtonStartX = -372f;
    private const float PoolButtonStartY = 136f;
    private const float PoolButtonStepX = 186f;
    private const float PoolButtonStepY = 82f;
    private const float DiceSlotStartX = -390f;
    private const float DiceSlotStepX = 156f;

    [SerializeField] private BattleDiceState battleDiceState;
    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private Color overlayColor = new Color(0.07f, 0.06f, 0.05f, 0.96f);
    [SerializeField] private Color panelColor = new Color(0.14f, 0.12f, 0.10f, 0.96f);
    [SerializeField] private Color buttonColor = new Color(0.26f, 0.22f, 0.17f, 1f);
    [SerializeField] private Color disabledButtonColor = new Color(0.11f, 0.10f, 0.09f, 0.92f);
    [SerializeField] private Color textColor = new Color(0.94f, 0.90f, 0.78f, 1f);

    [SerializeField] private RectTransform overlayRoot;
    [SerializeField] private RectTransform mainMenuRoot;
    [SerializeField] private RectTransform buildRoot;
    [SerializeField] private Text titleText;
    [SerializeField] private Button startRunButton;
    [SerializeField] private Text probabilityText;
    [SerializeField] private Button startBattleButton;
    [SerializeField] private Text[] activeSlotTexts;
    [SerializeField] private Text[] lockedSlotTexts;
    [SerializeField] private Button[] poolButtons;
    [SerializeField] private Text[] poolButtonTexts;
    private DiceFace[] unlockedFaces;
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

        if (displayRoot == null)
        {
            return;
        }

        unlockedFaces = StarterDiceFactory.CreateUnlockedFacesForDiceTier(CurrentDiceTier);

        if (overlayRoot != null)
        {
            TryBindExistingView();
            BindViewEvents();
            RefreshBuildView();
            overlayRoot.gameObject.SetActive(false);
            return;
        }

        if (TryBindExistingView())
        {
            BindViewEvents();
            RefreshBuildView();
            overlayRoot.gameObject.SetActive(false);
            return;
        }

        overlayRoot = CreateRoot("Starter Dice Build Overlay", displayRoot, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        Image overlayImage = overlayRoot.gameObject.AddComponent<Image>();
        overlayImage.color = overlayColor;

        mainMenuRoot = CreateCenteredPanel("Run Start Panel", new Vector2(560f, 300f));
        buildRoot = CreateCenteredPanel("Starter Dice Build Panel", new Vector2(1040f, 560f));

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

        startRunButton = CreateButton("Start Run Button", mainMenuRoot, new Vector2(0f, -74f), new Vector2(240f, 82f), "Start Run", 28);
        BindStartRunButton();
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

        poolButtons = new Button[unlockedFaces.Length];
        poolButtonTexts = new Text[unlockedFaces.Length];

        for (int i = 0; i < unlockedFaces.Length; i++)
        {
            int poolIndex = i;
            int column = i % PoolColumns;
            int row = i / PoolColumns;
            Button button = CreateButton(
                $"Starter Face Pool Button {i + 1}",
                buildRoot,
                new Vector2(PoolButtonStartX + (column * PoolButtonStepX), PoolButtonStartY - (row * PoolButtonStepY)),
                new Vector2(164f, 70f),
                unlockedFaces[i].DisplayName,
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
                new Vector2(DiceSlotStartX + (i * DiceSlotStepX), -6f),
                new Vector2(132f, 88f),
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
                new Vector2(DiceSlotStartX + ((ActiveSlotCount + i) * DiceSlotStepX), -6f),
                new Vector2(132f, 88f),
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
        probabilityText.rectTransform.anchorMin = new Vector2(0.06f, 0.06f);
        probabilityText.rectTransform.anchorMax = new Vector2(0.66f, 0.30f);
        probabilityText.text = string.Empty;

        startBattleButton = CreateButton("Start Battle Button", buildRoot, new Vector2(330f, -192f), new Vector2(260f, 76f), "Start Battle", 24);
        BindStartBattleButton();
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
        if (selectedPoolIndexes.Count >= ActiveSlotCount || !IsValidPoolIndex(poolIndex))
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
            faces[i] = unlockedFaces[selectedPoolIndexes[i]].Clone();
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
                DiceFace face = i < selectedPoolIndexes.Count ? unlockedFaces[selectedPoolIndexes[i]] : null;
                activeSlotTexts[i].text = face != null ? face.DisplayName : "Empty";
            }
        }

        if (poolButtons != null)
        {
            for (int i = 0; i < poolButtons.Length; i++)
            {
                int selectedCount = CountSelectedPoolIndex(i);
                poolButtons[i].interactable = selectedPoolIndexes.Count < ActiveSlotCount;
                if (poolButtonTexts[i] != null)
                {
                    string countText = selectedCount > 0 ? $"\nx{selectedCount}" : string.Empty;
                    poolButtonTexts[i].text = $"{unlockedFaces[i].DisplayName}{countText}";
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
            string faceName = unlockedFaces[selectedPoolIndexes[i]].DisplayName;
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

    private bool IsValidPoolIndex(int poolIndex)
    {
        if (unlockedFaces == null || poolIndex < 0 || poolIndex >= unlockedFaces.Length)
        {
            return false;
        }

        DiceFace face = unlockedFaces[poolIndex];
        return face != null && face.FaceTier <= CurrentDiceTier;
    }

    private int CountSelectedPoolIndex(int poolIndex)
    {
        int count = 0;

        for (int i = 0; i < selectedPoolIndexes.Count; i++)
        {
            if (selectedPoolIndexes[i] == poolIndex)
            {
                count++;
            }
        }

        return count;
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

    private bool TryBindExistingView()
    {
        Transform existingOverlay = displayRoot.Find("Starter Dice Build Overlay");
        overlayRoot = existingOverlay != null ? existingOverlay.GetComponent<RectTransform>() : overlayRoot;

        if (overlayRoot == null)
        {
            return false;
        }

        mainMenuRoot = mainMenuRoot != null ? mainMenuRoot : FindRect(overlayRoot, "Run Start Panel");
        buildRoot = buildRoot != null ? buildRoot : FindRect(overlayRoot, "Starter Dice Build Panel");
        titleText = titleText != null ? titleText : FindText(mainMenuRoot, "Run Start Title");
        startRunButton = startRunButton != null ? startRunButton : FindButton(mainMenuRoot, "Start Run Button");
        probabilityText = probabilityText != null ? probabilityText : FindText(buildRoot, "Starter Dice Probability Text");
        startBattleButton = startBattleButton != null ? startBattleButton : FindButton(buildRoot, "Start Battle Button");

        BindPoolButtonsFromExistingView();
        BindSlotTextsFromExistingView();
        return mainMenuRoot != null && buildRoot != null;
    }

    private void BindViewEvents()
    {
        BindStartRunButton();
        BindStartBattleButton();

        if (poolButtons != null)
        {
            for (int i = 0; i < poolButtons.Length; i++)
            {
                int poolIndex = i;
                if (poolButtons[i] == null)
                {
                    continue;
                }

                poolButtons[i].onClick.RemoveAllListeners();
                poolButtons[i].onClick.AddListener(() => AddFace(poolIndex));
            }
        }
    }

    private void BindStartRunButton()
    {
        if (startRunButton == null)
        {
            return;
        }

        startRunButton.onClick.RemoveAllListeners();
        startRunButton.onClick.AddListener(ShowBuildView);
    }

    private void BindStartBattleButton()
    {
        if (startBattleButton == null)
        {
            return;
        }

        startBattleButton.onClick.RemoveAllListeners();
        startBattleButton.onClick.AddListener(CompleteBuild);
    }

    private void BindPoolButtonsFromExistingView()
    {
        if (buildRoot == null || unlockedFaces == null)
        {
            return;
        }

        poolButtons = poolButtons != null && poolButtons.Length == unlockedFaces.Length
            ? poolButtons
            : new Button[unlockedFaces.Length];
        poolButtonTexts = poolButtonTexts != null && poolButtonTexts.Length == unlockedFaces.Length
            ? poolButtonTexts
            : new Text[unlockedFaces.Length];

        for (int i = 0; i < unlockedFaces.Length; i++)
        {
            poolButtons[i] = poolButtons[i] != null
                ? poolButtons[i]
                : FindButton(buildRoot, $"Starter Face Pool Button {i + 1}");
            poolButtonTexts[i] = poolButtonTexts[i] != null && poolButtons[i] != null
                ? poolButtonTexts[i]
                : poolButtons[i]?.GetComponentInChildren<Text>(true);
        }
    }

    private void BindSlotTextsFromExistingView()
    {
        if (buildRoot == null)
        {
            return;
        }

        activeSlotTexts = activeSlotTexts != null && activeSlotTexts.Length == ActiveSlotCount
            ? activeSlotTexts
            : new Text[ActiveSlotCount];

        for (int i = 0; i < ActiveSlotCount; i++)
        {
            Button slotButton = FindButton(buildRoot, $"Starter Active Slot {i + 1}");
            int slotIndex = i;
            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => RemoveSlot(slotIndex));
            }

            activeSlotTexts[i] = activeSlotTexts[i] != null
                ? activeSlotTexts[i]
                : slotButton?.GetComponentInChildren<Text>(true);
        }

        lockedSlotTexts = lockedSlotTexts != null && lockedSlotTexts.Length == 2
            ? lockedSlotTexts
            : new Text[2];

        for (int i = 0; i < lockedSlotTexts.Length; i++)
        {
            Button lockedButton = FindButton(buildRoot, $"Starter Locked Slot {i + 1}");
            lockedSlotTexts[i] = lockedSlotTexts[i] != null
                ? lockedSlotTexts[i]
                : lockedButton?.GetComponentInChildren<Text>(true);
        }
    }

    private static RectTransform FindRect(RectTransform parent, string objectName)
    {
        Transform child = parent != null ? parent.Find(objectName) : null;
        return child != null ? child.GetComponent<RectTransform>() : null;
    }

    private static Text FindText(RectTransform parent, string objectName)
    {
        Transform child = parent != null ? parent.Find(objectName) : null;
        return child != null ? child.GetComponent<Text>() : null;
    }

    private static Button FindButton(RectTransform parent, string objectName)
    {
        Transform child = parent != null ? parent.Find(objectName) : null;
        return child != null ? child.GetComponent<Button>() : null;
    }
}
