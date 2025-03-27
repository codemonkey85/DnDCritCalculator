namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private int abilityStat = 20;
    private int bonusDamage = 0;
    private string damageOutput = string.Empty;
    private int diceQuantity = 1;
    private int rolledResult = 1;
    private Dice selectedDie = Dice.D4;
    private int weaponModifier = 0;

    private void RollCrit()
    {
        var abilityModifier = (abilityStat - 10) / 2;

        var maxDamage = (int)selectedDie * diceQuantity;

        var totalDamage = maxDamage + rolledResult + abilityModifier + weaponModifier + bonusDamage;

        damageOutput = $"Total damage: {totalDamage}";
    }

    private void AfterSelectedDieChanged()
    {
        if (rolledResult > (int)selectedDie)
        {
            rolledResult = (int)selectedDie;
        }
    }
}
