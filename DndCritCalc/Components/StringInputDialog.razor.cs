namespace DndCritCalc.Components;

public partial class StringInputDialog
{
    private string? input;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public string? InitialValue { get; set; }

    [Parameter]
    public string? InputHint { get; set; } = "Enter a value";

    protected override void OnInitialized() => input = InitialValue;

    private void Submit() => MudDialog.Close(DialogResult.Ok(input));

    private void Cancel() => MudDialog.Cancel();
}
