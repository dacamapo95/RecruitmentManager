using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Countries.GetCitiesByCountry;

public sealed record GetCitiesByCountryQuery(Guid CountryId) : IQuery<MasterEntityResponse<Guid>[]>;