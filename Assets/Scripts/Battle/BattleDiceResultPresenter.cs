using UnityEngine;
using UnityEngine.UI;

public sealed class BattleDiceResultPresenter : MonoBehaviour
{
    [SerializeField] private RectTransform displayRoot;
    [SerializeField] private Text resultText;

    private static Font fallbackFont;

    private void Awake()
    {
        EnsureResultText();
        Hide();
    }

    public void ShowResult(BattleDiceState diceState)
    {
        EnsureResultText();

        if (resultText == null || diceState == null || diceState.LastSelectedFace == null)
        {
            return;
        }

        int displaySlot = diceState.LastResultSlotIndex + 1;
        DiceFace face = diceState.LastSelectedFace;
        resultText.text = $"RESULT S{displaySlot}: {face.DisplayName}";
        resultText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    private void EnsureResultText()
    {
        if (resultText != null)
        {
            return;
        }

        RectTransform root = displayRoot != null ? displayRoot : FindBattleField();

        if (root == null)
        {
            return;
        }

        GameObject textObject = new GameObject("Dice Result Validation Text");
        textObject.layer = root.gameObject.layer;
        textObject.transform.SetParent(root, false);

        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(0f, 0f);
        rectTransform.pivot = new Vector2(0f, 0f);
        rectTransform.anchoredPosition = new Vector2(16f, 14f);
        rectTransform.sizeDelta = new Vector2(220f, 24f);

        resultText = textObject.AddComponent<Text>();
        resultText.raycastTarget = false;
        resultText.alignment = TextAnchor.MiddleLeft;
        resultText.fontSize = 13;
        resultText.resizeTextForBestFit = true;
        resultText.resizeTextMinSize = 8;
        resultText.resizeTextMaxSize = 13;
        resultText.color = new Color(0.72f, 0.68f, 0.52f, 0.85f);
        fallbackFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        fallbackFont ??= Resources.GetBuiltinResource<Font>("Arial.ttf");
        resultText.font = fallbackFont;
        resultText.text = string.Empty;
    }

    private static RectTransform FindBattleField()
    {
        GameObject battleField = GameObject.Find("BattleField");
        return battleField != null ? battleField.GetComponent<RectTransform>() : null;
    }
}
