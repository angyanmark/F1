using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Constructors
{
    [Parameter]
    [EditorRequired]
    public required ConstructorStandingsList ConstructorStandingsList { get; init; }
}
