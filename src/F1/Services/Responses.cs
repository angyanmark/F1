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
    string Xmlns,
    string Series,
    Uri Url,
    int Limit,
    int Offset,
    int Total);

public sealed record RaceTableMRData<TRaceTable>(
    string Xmlns,
    string Series,
    Uri Url,
    int Limit,
    int Offset,
    int Total,
    TRaceTable RaceTable)
        : MRData(
            Xmlns,
            Series,
            Url,
            Limit,
            Offset,
            Total);

public sealed record StandingsTableMRData<TStandingsList>(
    string Xmlns,
    string Series,
    Uri Url,
    int Limit,
    int Offset,
    int Total,
    StandingsTable<TStandingsList> StandingsTable)
        : MRData(
            Xmlns,
            Series,
            Url,
            Limit,
            Offset,
            Total)
            where TStandingsList : StandingsList;

public sealed record StandingsTable<TStandingsList>(
    int Season,
    int? Round,
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
    Uri? Url,
    string GivenName,
    string FamilyName,
    DateOnly? DateOfBirth,
    string? Nationality);

public sealed record Constructor(
    string? ConstructorId,
    Uri? Url,
    string Name,
    string? Nationality);

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
    Uri? Url,
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
    Uri? Url,
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
            Url,
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
    Uri? Url,
    string RaceName,
    Circuit Circuit,
    DateOnly Date,
    string? Time,
    Result[] Results)
        : RaceBase(
            Season,
            Round,
            Url,
            RaceName,
            Circuit,
            Date,
            Time);

public sealed record Result(
    int Number,
    int Position,
    string PositionText,
    double Points,
    Driver Driver,
    Constructor? Constructor,
    int? Grid,
    int? Laps,
    string? Status,
    TimeObj? Time,
    FastestLap? FastestLap);

public sealed record TimeObj(
    int? Millis,
    string Time);

public sealed record FastestLap(
    int Rank,
    int Lap,
    TimeObj Time,
    AverageSpeed AverageSpeed);

public sealed record AverageSpeed(
    string Units,
    double Speed);

public sealed record Circuit(
    string CircuitId,
    Uri Url,
    string CircuitName,
    Location Location);

public sealed record Location(
    double Lat,
    double Long,
    string Locality,
    string Country);

public sealed record Session(
    DateOnly Date,
    string? Time)
{
    public TimeOnly? TimeOnly => Time is null ? null : System.TimeOnly.Parse(Time.TrimEnd('Z'));
}
