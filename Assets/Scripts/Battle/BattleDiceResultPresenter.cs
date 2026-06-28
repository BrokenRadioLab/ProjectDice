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
        rectTransform.anchoredPosition = new Vector2(24f, 24f);
        rectTransform.sizeDelta = new Vector2(360f, 40f);

        resultText = textObject.AddComponent<Text>();
        resultText.raycastTarget = false;
        resultText.alignment = TextAnchor.MiddleLeft;
        resultText.fontSize = 24;
        resultText.color = new Color(0.95f, 0.91f, 0.72f, 1f);
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
