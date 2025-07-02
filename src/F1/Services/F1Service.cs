using System.Net.Http.Json;
using System.Text.Json;

namespace F1.Services;

public sealed class F1Service(HttpClient _httpClient)
{
    private const int PageSize = 100;

    public async Task<Response<RaceTableMRData>> GetRaceTableAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseRaceTableMRData,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<StandingsTableMRData<DriverStandingsList>>> GetDriverStandingsTableAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/driverstandings/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseStandingsTableMRDataDriverStandingsList,
                cancellationToken)
            ?? throw new JsonException();

    public async Task<Response<StandingsTableMRData<ConstructorStandingsList>>> GetConstructorStandingsTableAsync(
        int year,
        CancellationToken cancellationToken = default) =>
            await _httpClient.GetFromJsonAsync(
                $"/ergast/f1/{year}/constructorstandings/?limit={PageSize}",
                ResponseJsonSerializerContext.Default.ResponseStandingsTableMRDataConstructorStandingsList,
                cancellationToken)
            ?? throw new JsonException();
}
