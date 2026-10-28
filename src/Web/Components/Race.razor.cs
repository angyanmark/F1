using F1.Client;

using Microsoft.AspNetCore.Components;

namespace F1.Web.Components;

public sealed partial class Race
{
    private RaceResult? raceResult;
    private int? laps;

    [Parameter, EditorRequired]
    public required RaceResultsRaceTable RaceResultsRaceTable { get; init; }

    protected override void OnParametersSet()
    {
        raceResult = RaceResultsRaceTable.Races.FirstOrDefault();
        laps = raceResult?.Results.FirstOrDefault()?.Laps;
    }
}
