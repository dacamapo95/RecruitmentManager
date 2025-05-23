using RecruitmentManager.Shared;
using RecruitmentManager.Application.Core.Abstractions;

namespace RecruitmentManager.Application.Features.Candidates.GetById;

public sealed record GetCandidateByIdQuery(Guid Id) : IQuery<CandidateResponse>;