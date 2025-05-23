using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Domain.Errors;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Countries.GetCitiesByCountry;
public sealed class GetCitiesByCountryQueryHandler(ICityRepository cityRepository) : IQueryHandler<GetCitiesByCountryQuery, MasterEntityResponse<Guid>[]>
{
    private readonly ICityRepository _cityRepository = cityRepository;

    public async Task<Result<MasterEntityResponse<Guid>[]>> Handle(GetCitiesByCountryQuery request, CancellationToken cancellationToken)
    {
        _cityRepository.DisableTracking();

        var cities = await _cityRepository.GetByCountryId(request.CountryId, cancellationToken);

        if ( cities.Count == 0)
        {
            return SharedErrors.MasterEntityNotFound(nameof(City));
        }

        return cities.Select(x => new MasterEntityResponse<Guid>(x.Id, x.Name)).ToArray();
    }
}