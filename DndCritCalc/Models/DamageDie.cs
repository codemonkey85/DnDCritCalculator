
namespace DndCritCalc.Models;

public class DamageDie
{
    public Dice Die { get; set; }

    public DamageType DamageType { get; set; }

    public int Quantity { get; set; }

    public int AttackModifier { get; set; }

    public int RolledResult { get; set; } = 1;

    public int MaxRolledDamage => (int)Die * Quantity;
}
