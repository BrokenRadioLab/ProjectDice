using UnityEngine;

public sealed class BattleDiceState : MonoBehaviour
{
    [SerializeField] private DiceModel currentDice = StarterDiceFactory.CreateStarterDice();

    public DiceModel CurrentDice => currentDice;

    public void EnsureStarterDice()
    {
        if (!HasValidStarterDice(currentDice))
        {
            currentDice = StarterDiceFactory.CreateStarterDice();
        }
    }

    private void Awake()
    {
        EnsureStarterDice();
    }

    private static bool HasValidStarterDice(DiceModel dice)
    {
        if (dice == null || dice.FaceCount != DiceModel.FaceSlotCount)
        {
            return false;
        }

        if (dice.RuntimePhase != DiceRuntimePhase.Ready || dice.LastResultSlotIndex != -1)
        {
            return false;
        }

        for (int i = 0; i < DiceModel.FaceSlotCount; i++)
        {
            if (dice.GetFace(i) == null)
            {
                return false;
            }
        }

        return true;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        EnsureStarterDice();
    }
#endif
}
