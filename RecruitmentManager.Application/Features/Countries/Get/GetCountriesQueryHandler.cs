using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Domain.Errors;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Countries.Get;

public sealed class GetCountriesQueryHandler(ICountryRepository countryRepository) : IQueryHandler<GetCountriesQuery, MasterEntityResponse<Guid>[]>
{
    private readonly ICountryRepository _countryRepository = countryRepository;

    public async Task<Result<MasterEntityResponse<Guid>[]>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        _countryRepository.DisableTracking();

        var countries = await _countryRepository.GetAllAsync(cancellationToken);

        if (countries.Count == 0)
            return SharedErrors.MasterEntityNotFound(nameof(Country));

        return countries.Select(country => new MasterEntityResponse<Guid>(country.Id, country.Name)).ToArray();
    }
}