using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Countries.Get;

public sealed record GetCountriesQuery : IQuery<MasterEntityResponse<Guid>[]>;