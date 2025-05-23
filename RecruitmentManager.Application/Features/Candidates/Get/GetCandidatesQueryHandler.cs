using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Application.Core.Extensions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Candidates.Get;

public sealed class GetCandidatesQueryHandler(IApplicationDbContext context) 
    : IQueryHandler<GetCandidatesQuery, PaginatedList<CandidateItemResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<PaginatedList<CandidateItemResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var candidatesQuery = _context.Candidates.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim().ToLower();
            candidatesQuery = candidatesQuery.Where(candidate =>
                candidate.FullName.FirstName.ToLower().Contains(term) ||
                candidate.FullName.SurName.ToLower().Contains(term) ||
                candidate.Email.Value.ToLower().Contains(term)
            );
        }

        if (request.StateId.HasValue)
        {
            candidatesQuery = candidatesQuery.Where(c => c.StateId == request.StateId.Value);
        }

        candidatesQuery = ApplySorting(candidatesQuery, request.SortColumn, request.SortOrder);

        var projectedQuery = candidatesQuery.Select(candidate => new CandidateItemResponse(
            candidate.Id,
            candidate.FullName.FirstName,
            candidate.FullName.SurName,
            candidate.Email.Value,
            candidate.StateId,
            candidate.CreatedAtUtc
        ));
       
        var pagedList = await PaginatedListExtensions.CreateAsync(
            projectedQuery,
            request.Page,
            request.PageSize,
            cancellationToken
        );

        return Result.Success(pagedList);
    }

    private IQueryable<Candidate> ApplySorting(
        IQueryable<Candidate> query,
        string? sortColumn,
        string? sortOrder)
    {
        var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);  

        return (sortColumn?.ToLower()) switch
        {
            "fullname" => isDesc
                ? query.OrderByDescending(c => c.FullName.FirstName)
                : query.OrderBy(c => c.FullName.FirstName),
            "surname" => isDesc
                ? query.OrderByDescending(c => c.FullName.SurName)
                : query.OrderBy(c => c.FullName.SurName),
            "email" => isDesc
                ? query.OrderByDescending(c => c.Email.Value)
                : query.OrderBy(c => c.Email.Value),
            "createdat" => isDesc
                ? query.OrderByDescending(c => c.CreatedAtUtc)
                : query.OrderBy(c => c.CreatedAtUtc),
            _ => query.OrderBy(c => c.CreatedAtUtc)
        };
    }
}
