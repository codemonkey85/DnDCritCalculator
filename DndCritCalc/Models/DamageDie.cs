namespace DndCritCalc.Models;

public class DamageDie
{
    public Dice Die { get; set; } = Dice.D4;

    public DamageType DamageType { get; set; } = DamageType.Bludgeoning;

    public int Quantity { get; set; } = 1;

    public int AttackModifier { get; set; }

    public int RolledResult { get; set; } = 1;

    public int BonusDamage { get; set; }

    public bool IncludeAbilityModifier { get; set; }

    public bool IncludeAttackModifier { get; set; }

    public int MaxRolledDamage => (int)Die * Quantity;
}
