using Microsoft.AspNetCore.Components;

namespace DndCritCalc.Components;

public partial class DamageDieComponent
{
    [Parameter, EditorRequired]
    public DamageDie DamageDie { get; set; } = new();
}
