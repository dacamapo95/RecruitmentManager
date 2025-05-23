using System.Net.Http.Json;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ApiClients;

public class CandidatesApiClient(HttpClient httpClient) : ICandidatesApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PaginatedList<CandidateItemResponse>?> GetCandidatesAsync(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int page = 1,
        int pageSize = 20,
        int? stateId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(searchTerm)) query.Add($"searchTerm={Uri.EscapeDataString(searchTerm)}");
        if (!string.IsNullOrWhiteSpace(sortColumn)) query.Add($"sortColumn={Uri.EscapeDataString(sortColumn)}");
        if (!string.IsNullOrWhiteSpace(sortOrder)) query.Add($"sortOrder={Uri.EscapeDataString(sortOrder)}");
        if (page > 0) query.Add($"page={page}");
        if (pageSize > 0) query.Add($"pageSize={pageSize}");
        if (stateId.HasValue) query.Add($"stateId={stateId.Value}");

        var url = "api/candidates";
        if (query.Count > 0)
            url += "?" + string.Join("&", query);

        return await _httpClient.GetFromJsonAsync<PaginatedList<CandidateItemResponse>>(url, cancellationToken);
    }

    public async Task<CandidateResponse?> GetCandidateByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<CandidateResponse>($"api/candidates/{id}", cancellationToken);
    }

    public async Task<Guid?> CreateCandidateAsync(CreateCandidateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/candidates", request, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
        }
        return null;
    }

    public async Task<bool> EditCandidateAsync(Guid id, EditCandidateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/candidates/{id}", request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCandidateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"api/candidates/{id}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}