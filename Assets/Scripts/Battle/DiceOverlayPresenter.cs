using UnityEngine;
using UnityEngine.UI;

public sealed class DiceOverlayPresenter : MonoBehaviour
{
    [SerializeField] private RectTransform rollingDiceTransform;
    [SerializeField] private Text rollingStateText;
    [SerializeField] private Text unknownFaceText;
    [SerializeField] private float rollingRotationSpeed = 360f;

    private bool isRolling;

    public void TickRolling(float deltaTime)
    {
        if (!isRolling || rollingDiceTransform == null)
        {
            return;
        }

        rollingDiceTransform.Rotate(0f, 0f, -rollingRotationSpeed * deltaTime);
    }

    public void ShowRolling()
    {
        gameObject.SetActive(true);
        isRolling = true;

        if (rollingDiceTransform != null)
        {
            rollingDiceTransform.localRotation = Quaternion.identity;
        }

        if (rollingStateText != null)
        {
            rollingStateText.text = "ROLLING...\n[ ? ]";
            rollingStateText.enabled = true;
        }

        if (unknownFaceText != null)
        {
            unknownFaceText.text = "?";
            unknownFaceText.enabled = true;
        }
    }

    public void Hide()
    {
        isRolling = false;
        gameObject.SetActive(false);
    }
}
