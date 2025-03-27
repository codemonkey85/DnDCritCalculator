namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private readonly List<string> outputLines = [];
    private int abilityStat = 20;
    private int bonusDamage = 0;
    private int diceQuantity = 1;
    private int rolledResult = 1;
    private Dice selectedDie = Dice.D4;
    private int weaponModifier = 0;

    private void RollCrit()
    {
        outputLines.Clear();

        var abilityModifier = (abilityStat - 10) / 2;

        var maxDamage = (int)selectedDie * diceQuantity;

        var totalDamage = maxDamage + rolledResult + abilityModifier + weaponModifier + bonusDamage;

        outputLines.Add($"Total damage: {totalDamage}");

        outputLines.Add($"Max Damage for {diceQuantity}{selectedDie.ToString().ToLower()} = {maxDamage}");

        outputLines.Add($"Rolled Damage: {rolledResult}");

        outputLines.Add($"Total Modifier: {abilityModifier} + {weaponModifier} = {abilityModifier + weaponModifier}");

        if (bonusDamage > 0)
        {
            outputLines.Add($"Bonus Damage: {bonusDamage}");
        }
    }

    private void AfterSelectedDieChanged()
    {
        if (rolledResult > (int)selectedDie)
        {
            rolledResult = (int)selectedDie;
        }
    }
}
