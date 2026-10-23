using F1.Client;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace F1.Web.Pages;

public sealed partial class MainPage
{
    private bool drawerOpen;
    private string title = "F1";
    private RaceTable? raceTable;
    private RaceResultsRaceTable? raceResultsRaceTable;
    private DriverStandingsList? driverStandingsList;
    private DriverResultsRaceTable? driverResultsRaceTable;
    private ConstructorStandingsList? constructorStandingsList;
    private ConstructorResultsRaceTable? constructorResultsRaceTable;

    [Parameter]
    public required string Category { get; init; }

    [Parameter]
    public required int Year { get; init; }

    [Parameter]
    public required string? CategoryId { get; init; }

    [Inject]
    private F1HttpClient F1HttpClient { get; init; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        raceTable = null;
        raceResultsRaceTable = null;
        driverStandingsList = null;
        driverResultsRaceTable = null;
        constructorStandingsList = null;
        constructorResultsRaceTable = null;

        title = (Category, CategoryId) switch
        {
            ("races", null) => await GetRacesAsync(),
            ("races", _) => await GetRaceAsync(int.Parse(CategoryId)),
            ("drivers", null) => await GetDriversAsync(),
            ("drivers", _) => await GetDriverAsync(CategoryId),
            ("constructors", null) => await GetConstructorsAsync(),
            ("constructors", _) => await GetConstructorAsync(CategoryId),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    private async Task<string> GetRacesAsync()
    {
        var response = await F1HttpClient.GetRacesAsync(Year);
        raceTable = response.MRData.RaceTable;
        return $"Races {Year}";
    }

    private async Task<string> GetRaceAsync(int round)
    {
        var response = await F1HttpClient.GetRaceAsync(Year, round);
        raceResultsRaceTable = response.MRData.RaceTable;
        var raceResult = raceResultsRaceTable.Races.FirstOrDefault();
        return raceResult is null ? $"Race {Year}" : $"{raceResult.RaceName} {Year}";
    }

    private async Task<string> GetDriversAsync()
    {
        var response = await F1HttpClient.GetDriversAsync(Year);
        driverStandingsList = response.MRData.StandingsTable.StandingsLists.FirstOrDefault();
        return $"Drivers {Year}";
    }

    private async Task<string> GetDriverAsync(string driverId)
    {
        var response = await F1HttpClient.GetDriverAsync(Year, driverId);
        driverResultsRaceTable = response.MRData.RaceTable;
        var driver = driverResultsRaceTable.Races.FirstOrDefault()?.Results.FirstOrDefault()?.Driver;
        return driver is null ? $"Driver {Year}" : $"{driver.GivenName} {driver.FamilyName} {Year}";
    }

    private async Task<string> GetConstructorsAsync()
    {
        var response = await F1HttpClient.GetConstructorsAsync(Year);
        constructorStandingsList = response.MRData.StandingsTable.StandingsLists.FirstOrDefault();
        return $"Constructors {Year}";
    }

    private async Task<string> GetConstructorAsync(string constructorId)
    {
        var response = await F1HttpClient.GetConstructorAsync(Year, constructorId);
        constructorResultsRaceTable = response.MRData.RaceTable;
        var constructor = constructorResultsRaceTable.Races.FirstOrDefault()?.Results.FirstOrDefault()?.Constructor;
        return constructor is null ? $"Constructor {Year}" : $"{constructor.Name} {Year}";
    }

    private void DrawerToggle() => drawerOpen = !drawerOpen;

    private MudTheme theme = new()
    {
        PaletteDark = new PaletteDark()
        {
            Primary = "#7e6fff",
            Surface = "#1e1e2d",
            Background = "#1a1a27",
            BackgroundGray = "#151521",
            AppbarText = "#92929f",
            AppbarBackground = "rgba(26,26,39,0.8)",
            DrawerBackground = "#1a1a27",
            ActionDefault = "#74718e",
            ActionDisabled = "#9999994d",
            ActionDisabledBackground = "#605f6d4d",
            TextPrimary = "#b2b0bf",
            TextSecondary = "#92929f",
            TextDisabled = "#ffffff33",
            DrawerIcon = "#92929f",
            DrawerText = "#92929f",
            GrayLight = "#2a2833",
            GrayLighter = "#1e1e2d",
            Info = "#4a86ff",
            Success = "#3dcb6c",
            Warning = "#ffb545",
            Error = "#ff3f5f",
            LinesDefault = "#33323e",
            TableLines = "#33323e",
            Divider = "#292838",
            OverlayLight = "#1e1e2d80",
        },
    };
}
