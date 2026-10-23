using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Races
{
    private int? nextRound;

    [Parameter]
    [EditorRequired]
    public required RaceTable RaceTable { get; init; }

    protected override void OnParametersSet() =>
        nextRound = RaceTable.Races.FirstOrDefault(IsFutureRace)?.Round;

    private static bool IsFutureRace(RaceBase race) =>
        new DateTime(race.Date, race.TimeOnly ?? TimeOnly.MaxValue, DateTimeKind.Utc) > DateTime.UtcNow;
}
