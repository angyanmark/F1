using System.Text.Json.Serialization;

namespace F1.Services;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Response<RaceTableMRData<RaceTable>>))]
[JsonSerializable(typeof(Response<RaceTableMRData<RaceResultsRaceTable>>))]
[JsonSerializable(typeof(Response<RaceTableMRData<DriverResultsRaceTable>>))]
[JsonSerializable(typeof(Response<RaceTableMRData<ConstructorResultsRaceTable>>))]
[JsonSerializable(typeof(Response<StandingsTableMRData<DriverStandingsList>>))]
[JsonSerializable(typeof(Response<StandingsTableMRData<ConstructorStandingsList>>))]
public sealed partial class ResponseJsonSerializerContext : JsonSerializerContext;

public sealed record Response<TMRData>(
    TMRData MRData)
    where TMRData : MRData;

public abstract record MRData(
    string Series);

public sealed record RaceTableMRData<TRaceTable>(
    string Series,
    TRaceTable RaceTable)
        : MRData(
            Series);

public sealed record StandingsTableMRData<TStandingsList>(
    string Series,
    StandingsTable<TStandingsList> StandingsTable)
        : MRData(
            Series)
            where TStandingsList : StandingsList;

public sealed record StandingsTable<TStandingsList>(
    int Season,
    TStandingsList[] StandingsLists)
    where TStandingsList : StandingsList;

public abstract record StandingsList(
    int Season,
    int Round);

public sealed record DriverStandingsList(
    int Season,
    int Round,
    DriverStanding[] DriverStandings)
        : StandingsList(
            Season,
            Round);

public sealed record ConstructorStandingsList(
    int Season,
    int Round,
    ConstructorStanding[] ConstructorStandings)
        : StandingsList(
            Season,
            Round);

public abstract record Standing(
    int? Position,
    string PositionText,
    double Points,
    int Wins);

public sealed record DriverStanding(
    int? Position,
    string PositionText,
    double Points,
    int Wins,
    Driver Driver,
    Constructor[] Constructors)
        : Standing(
            Position,
            PositionText,
            Points,
            Wins);

public sealed record ConstructorStanding(
    int? Position,
    string PositionText,
    double Points,
    int Wins,
    Constructor Constructor)
        : Standing(
            Position,
            PositionText,
            Points,
            Wins);

public sealed record Driver(
    string DriverId,
    int? PermanentNumber,
    string? Code,
    string GivenName,
    string FamilyName,
    string? Nationality);

public sealed record Constructor(
    string? ConstructorId,
    string Name);

public sealed record RaceTable(
    int Season,
    Race[] Races);

public sealed record RaceResultsRaceTable(
    int Season,
    int Round,
    RaceResult[] Races);

public sealed record DriverResultsRaceTable(
    int Season,
    string DriverId,
    RaceResult[] Races);

public sealed record ConstructorResultsRaceTable(
    int Season,
    string ConstructorId,
    RaceResult[] Races);

public abstract record RaceBase(
    int Season,
    int Round,
    string RaceName,
    Circuit Circuit,
    DateOnly Date,
    string? Time)
{
    public TimeOnly? TimeOnly => Time is null ? null : System.TimeOnly.Parse(Time.TrimEnd('Z'));
}

public sealed record Race(
    int Season,
    int Round,
    string RaceName,
    Circuit Circuit,
    DateOnly Date,
    string? Time,
    Session? FirstPractice,
    Session? SecondPractice,
    Session? ThirdPractice,
    Session? Qualifying,
    Session? Sprint,
    Session? SprintQualifying,
    Session? SprintShootout)
        : RaceBase(
            Season,
            Round,
            RaceName,
            Circuit,
            Date,
            Time)
{
    public Dictionary<string, Session?> Sessions => new()
    {
        { "Practice 1", FirstPractice },
        { "Practice 2", SecondPractice },
        { "Practice 3", ThirdPractice },
        { "Sprint Qualifying", SprintQualifying },
        { "Sprint Shootout", SprintShootout },
        { "Sprint", Sprint },
        { "Qualifying", Qualifying },
        { "Race", new(Date, Time) },
    };
}

public sealed record RaceResult(
    int Season,
    int Round,
    string RaceName,
    Circuit Circuit,
    DateOnly Date,
    string? Time,
    Result[] Results)
        : RaceBase(
            Season,
            Round,
            RaceName,
            Circuit,
            Date,
            Time);

public sealed record Result(
    int Position,
    string PositionText,
    double Points,
    Driver Driver,
    Constructor? Constructor,
    int? Laps,
    string? Status,
    TimeObj? Time);

public sealed record TimeObj(
    string Time);

public sealed record Circuit(
    string CircuitName,
    Location Location);

public sealed record Location(
    string Locality,
    string Country);

public sealed record Session(
    DateOnly Date,
    string? Time)
{
    public TimeOnly? TimeOnly => Time is null ? null : System.TimeOnly.Parse(Time.TrimEnd('Z'));
}
