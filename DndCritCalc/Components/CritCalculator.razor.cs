namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private readonly int bonusDamage = 0;
    private readonly List<DiceRoll> diceRolls = [];
    private readonly int rolledDamage = 0;

    private string damageOutput = string.Empty;

    private void RollCrit()
    {
        var damageRoll = new CritDamageRoll(diceRolls, rolledDamage, bonusDamage);

        var maxDamage = damageRoll.DiceRolls
            .Where(dr => dr.Quantity > 0)
            .Sum(dr => dr.Quantity * (int)dr.Dice);

        var totalDamage = maxDamage + damageRoll.RolledDamage + damageRoll.BonusDamage;

        damageOutput = $"Total damage: {totalDamage}";
    }
}
