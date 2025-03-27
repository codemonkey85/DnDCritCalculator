namespace DndCritCalc.Components;

public partial class DamageDieComponent
{
    [Parameter, EditorRequired]
    public DamageDie DamageDie { get; set; } = new();

    [Parameter, EditorRequired]
    public EventCallback<MouseEventArgs> OnRemove { get; set; }
}
