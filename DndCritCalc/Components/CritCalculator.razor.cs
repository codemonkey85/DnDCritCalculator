namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private readonly List<AttackRoll> savedAttackRolls = [];
    private AttackRoll? selectedSavedAttackRoll;

    private int abilityStat = 20;

    private int bonusDamage = 0;

    private List<DamageDie> DamageDice = [];
    private readonly List<string> outputLines = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadSavedAttackRolls();
    }

    private void RollCrit()
    {
        outputLines.Clear();
        var abilityModifier = (abilityStat - 10) / 2;

        Dictionary<DamageType, int> damageTotals = [];
        foreach (var damageGroup in DamageDice.GroupBy(d => d.DamageType))
        {
            foreach (var damageDie in damageGroup)
            {
                var maxDamage = damageDie.MaxRolledDamage;
                var totalDamage = maxDamage + damageDie.RolledResult + abilityModifier + damageDie.AttackModifier + bonusDamage;
                if (damageTotals.ContainsKey(damageGroup.Key))
                {
                    damageTotals[damageGroup.Key] += totalDamage;
                }
                else
                {
                    damageTotals.Add(damageGroup.Key, totalDamage);
                }
            }
        }

        foreach(var total in damageTotals)
        {
            outputLines.Add($"Total {total.Key} damage: {total.Value}");
        }

        //var abilityModifier = (abilityStat - 10) / 2;
        //var maxDamage = (int)selectedDie * diceQuantity;
        //var totalDamage = maxDamage + rolledResult + abilityModifier + attackModifier + bonusDamage;
        //outputLines.Add($"Total damage: {totalDamage}");
        //outputLines.Add($"Max Damage for {diceQuantity}{selectedDie.ToString().ToLower()} = {maxDamage}");
        //outputLines.Add($"Rolled Damage: {rolledResult}");
        //outputLines.Add($"Total Modifier: {abilityModifier} + {attackModifier} = {abilityModifier + attackModifier}");
        //if (bonusDamage > 0)
        //{
        //    outputLines.Add($"Bonus Damage: {bonusDamage}");
        //}
    }

    private async Task LoadSavedAttackRolls()
    {
        savedAttackRolls.Clear();

        savedAttackRolls.Add(new("Warhammer +2", [new() { Die = Dice.D10, DamageType = DamageType.Bludgeoning, Quantity = 1, AttackModifier = 2 }]));
    }

    private void OnSelectedAttackChanged()
    {
        if (selectedSavedAttackRoll is null)
        {
            return;
        }

        DamageDice = selectedSavedAttackRoll.DamageDice;
    }
}
