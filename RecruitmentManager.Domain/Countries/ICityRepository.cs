using RecruitmentManager.Domain.Interfaces;
namespace RecruitmentManager.Domain.Countries;

public interface ICityRepository : IReadRepository<City, Guid>
{
    Task<IReadOnlyList<City>?> GetByCountryId(Guid countryId, CancellationToken cancellationToken = default);
}
