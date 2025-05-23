using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ApiClients;

public interface ICandidatesApiClient
{
    Task<PaginatedList<CandidateItemResponse>?> GetCandidatesAsync(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int page = 1,
        int pageSize = 20,
        int? stateId = null,
        CancellationToken cancellationToken = default);

    Task<CandidateResponse?> GetCandidateByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Guid?> CreateCandidateAsync(CreateCandidateRequest request, CancellationToken cancellationToken = default);

    Task<bool> EditCandidateAsync(Guid id, EditCandidateRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteCandidateAsync(Guid id, CancellationToken cancellationToken = default);
}