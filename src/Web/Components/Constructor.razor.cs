using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Constructor
{
    [Parameter, EditorRequired]
    public required ConstructorResultsRaceTable ConstructorResultsRaceTable { get; init; }
}
