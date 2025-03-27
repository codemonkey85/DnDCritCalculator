namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private readonly List<string> outputLines = [];
    private readonly List<AttackRoll> savedAttackRolls = [];

    private int abilityStat = 20;

    private List<DamageDie> damageDice = [];
    private AttackRoll? selectedSavedAttackRoll;

    private int AbilityModifier => (abilityStat - 10) / 2;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadSavedAttackRolls();
    }

    private void AddDamageDice() => damageDice.Add(new());

    private void RemoveDamageDie(int index) => damageDice.RemoveAt(index);

    private void RollCrit()
    {
        outputLines.Clear();

        Dictionary<DamageType, int> damageTotals = [];
        foreach (var damageGroup in damageDice.GroupBy(d => d.DamageType))
        {
            foreach (var damageDie in damageGroup)
            {
                var maxDamage = damageDie.MaxRolledDamage;
                var totalDamage = maxDamage + damageDie.RolledResult + damageDie.BonusDamage;

                if (damageDie.IncludeAbilityModifier)
                {
                    totalDamage += AbilityModifier;
                }

                if (damageDie.IncludeAttackModifier)
                {
                    totalDamage += damageDie.AttackModifier;
                }

                if (!damageTotals.TryAdd(damageGroup.Key, totalDamage))
                {
                    damageTotals[damageGroup.Key] += totalDamage;
                }
            }
        }

        foreach (var total in damageTotals)
        {
            outputLines.Add($"Total {total.Key} damage: {total.Value}");
        }
    }

    private async Task SaveCurrentAttackRoll()
    {
        var newAttackRoll = new AttackRoll("Custom Attack", damageDice);
    }

    private async Task LoadSavedAttackRolls()
    {
        savedAttackRolls.Clear();

        savedAttackRolls.Add(new("Warhammer +2",
        [
            new()
            {
                Die = Dice.D10,
                DamageType = DamageType.Bludgeoning,
                Quantity = 1,
                AttackModifier = 2,
                IncludeAbilityModifier = true,
                IncludeAttackModifier = true
            }
        ]));
    }

    private void OnSelectedAttackChanged()
    {
        if (selectedSavedAttackRoll is null)
        {
            return;
        }

        damageDice.Clear();
        damageDice.AddRange(selectedSavedAttackRoll.DamageDice);
    }
}
