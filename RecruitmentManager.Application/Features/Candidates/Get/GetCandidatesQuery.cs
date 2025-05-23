using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Candidates.Get;

public sealed record GetCandidatesQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize,
    int? StateId 
) : IQuery<PaginatedList<CandidateItemResponse>>;