namespace DndCritCalc.Models;

public enum Dice
{
    D4 = 4,
    D6 = 6,
    D8 = 8,
    D10 = 10,
    D12 = 12,
    D20 = 20
}

public record CritDamageRoll(Dice Die, int DiceQuantity, int RollResult, int BonusDamage);
