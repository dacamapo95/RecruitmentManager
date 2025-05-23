using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Application.Features.Candidates.Mappings;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Candidates.GetById;

public class GetCandidateByIdQueryHandler(
    ICandidateRepository candidateRepository
) : IQueryHandler<GetCandidateByIdQuery, CandidateResponse>
{
    private readonly ICandidateRepository _candidateRepository = candidateRepository;

    public async Task<Result<CandidateResponse>> Handle(GetCandidateByIdQuery query, CancellationToken cancellationToken)
    {
        _candidateRepository.DisableTracking();

        var candidate = await _candidateRepository.GetWithExperiences(query.Id, cancellationToken);

        if (candidate is null) return CandidateErrors.CandidateNotFound(query.Id);

        return candidate.ToResponse();
    }
}