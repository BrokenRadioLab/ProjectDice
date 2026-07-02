using UnityEngine;

public sealed class BattleDiceState : MonoBehaviour
{
    [SerializeField] private DiceModel currentDice = StarterDiceFactory.CreateStarterDice();

    public DiceModel CurrentDice => currentDice;
    public int LastResultSlotIndex => currentDice != null ? currentDice.LastResultSlotIndex : -1;

    public DiceFace LastSelectedFace
    {
        get
        {
            if (currentDice == null || LastResultSlotIndex < 0)
            {
                return null;
            }

            return currentDice.GetFace(LastResultSlotIndex);
        }
    }

    public void EnsureStarterDice()
    {
        if (!HasPlayableDice(currentDice))
        {
            currentDice = StarterDiceFactory.CreateStarterDice();
        }
    }

    public void SetCurrentDice(DiceModel dice)
    {
        if (!HasPlayableDice(dice))
        {
            currentDice = StarterDiceFactory.CreateStarterDice();
            return;
        }

        currentDice = new DiceModel(dice.GetFacesSnapshot(), dice.BaseThrowDamage, dice.ActiveFaceSlotCount);
    }

    public void BeginThrowRoll()
    {
        EnsureStarterDice();
        currentDice.BeginRoll();
    }

    public void StopAtResultSlot(int slotIndex)
    {
        EnsureStarterDice();
        currentDice.RecordResultSlot(slotIndex);
    }

    public void RevealResult()
    {
        EnsureStarterDice();
        currentDice.RevealResult();
    }

    private void Awake()
    {
        EnsureStarterDice();
    }

    private static bool HasPlayableDice(DiceModel dice)
    {
        if (dice == null || dice.FaceCount != DiceModel.FaceSlotCount || dice.BaseThrowDamage <= 0)
        {
            return false;
        }

        for (int i = 0; i < dice.ActiveFaceSlotCount; i++)
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
