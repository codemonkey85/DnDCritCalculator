using System.Text.Json;

namespace DndCritCalc.Components;

public partial class CritCalculator
{
    private readonly List<string> outputLines = [];
    private readonly List<AttackRoll> savedAttackRolls = [];

    private int abilityStat = 20;

    private List<DamageDie> damageDice = [];
    private AttackRoll? selectedSavedAttackRoll;

    private MudSelect<AttackRoll>? savedAttackRollsSelect;

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
        var name = "Custom Attack";
        var newAttackRoll = new AttackRoll(name, damageDice);
        savedAttackRolls.Add(newAttackRoll);
        await LocalStorage.SetItemAsStringAsync(Constants.SavedAttackRollsStorageKey, JsonSerializer.Serialize(savedAttackRolls));
        await LoadSavedAttackRolls();
    }

    private async Task LoadSavedAttackRolls()
    {
        savedAttackRolls.Clear();
        var storedValuesJson = await LocalStorage.GetItemAsStringAsync(Constants.SavedAttackRollsStorageKey);
        if (string.IsNullOrWhiteSpace(storedValuesJson))
        {
            return;
        }

        var storedValues = JsonSerializer.Deserialize<List<AttackRoll>>(storedValuesJson);
        if (storedValues is not null)
        {
            savedAttackRolls.AddRange(storedValues);
        }
    }

    private void LoadSelectedSavedAttackRoll()
    {
        if (selectedSavedAttackRoll is null)
        {
            return;
        }

        damageDice.Clear();
        damageDice.AddRange(selectedSavedAttackRoll.DamageDice);
    }

    private async Task RemoveSelectedSavedAttackRoll()
    {
        if (selectedSavedAttackRoll is null)
        {
            return;
        }

        savedAttackRolls.Remove(selectedSavedAttackRoll);
        await LocalStorage.SetItemAsStringAsync(Constants.SavedAttackRollsStorageKey, JsonSerializer.Serialize(savedAttackRolls));
        await LoadSavedAttackRolls();
        if (savedAttackRollsSelect is not null)
        {
            await savedAttackRollsSelect.ClearAsync();
        }
    }
}
