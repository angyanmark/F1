using System.Net.Http.Json;
using System.Text.Json;

namespace F1.Services;

public sealed class F1Service(HttpClient _httpClient)
{
    private const int PageSize = 100;

    public async Task<Response<RaceTableMRData<RaceTable>>> GetRacesAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseRaceTableMRDataRaceTable,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<RaceTableMRData<RaceResultsRaceTable>>> GetRaceAsync(
        int year,
        int round,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/{round}/results/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseRaceTableMRDataRaceResultsRaceTable,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<StandingsTableMRData<DriverStandingsList>>> GetDriversAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/driverstandings/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseStandingsTableMRDataDriverStandingsList,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<RaceTableMRData<DriverResultsRaceTable>>> GetDriverAsync(
        int year,
        string driverId,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/drivers/{driverId}/results.json?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseRaceTableMRDataDriverResultsRaceTable,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<StandingsTableMRData<ConstructorStandingsList>>> GetConstructorsAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/constructorstandings/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseStandingsTableMRDataConstructorStandingsList,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<RaceTableMRData<ConstructorResultsRaceTable>>> GetConstructorAsync(
        int year,
        string constructorId,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/constructors/{constructorId}/results.json?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseRaceTableMRDataConstructorResultsRaceTable,
                cancellationToken)
            ?? throw new JsonException();
}
