using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ApiClients;

public class StatesApiClient(HttpClient httpClient) : IStatesApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<MasterEntityResponse<int>>?> GetStatesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<MasterEntityResponse<int>>>("api/states", cancellationToken);
    }
}