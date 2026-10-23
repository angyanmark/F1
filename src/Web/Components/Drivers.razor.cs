using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Drivers
{
    [Parameter]
    [EditorRequired]
    public required DriverStandingsList DriverStandingsList { get; init; }
}
