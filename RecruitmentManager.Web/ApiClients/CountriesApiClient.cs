using System.Net.Http.Json;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ApiClients;

public class CountriesApiClient(HttpClient httpClient) : ICountriesApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public Task<List<MasterEntityResponse<Guid>>?> GetCitiesByCountryIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _httpClient.GetFromJsonAsync<List<MasterEntityResponse<Guid>>>($"api/countries/{id}/cities", cancellationToken);
    }

    public async Task<List<MasterEntityResponse<Guid>>?> GetCountriesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<MasterEntityResponse<Guid>>>("api/countries", cancellationToken);
    }
}