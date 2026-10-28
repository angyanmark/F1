using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Driver
{
    [Parameter, EditorRequired]
    public required DriverResultsRaceTable DriverResultsRaceTable { get; init; }
}
