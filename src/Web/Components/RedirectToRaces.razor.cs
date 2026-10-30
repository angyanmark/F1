using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class RedirectToRaces
{
    [Inject]
    private NavigationManager NavigationManager { get; init; } = default!;

    protected override void OnInitialized() =>
        NavigationManager.NavigateTo($"races/{DateTime.Now.Year}", replace: true);
}
