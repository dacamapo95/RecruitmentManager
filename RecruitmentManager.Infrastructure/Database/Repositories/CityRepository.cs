using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Infrastructure.Database.Repositories;
internal sealed class CityRepository(ApplicationDbContext dbContext) : Repository<City, Guid>(dbContext), ICityRepository
{
    public async Task<IReadOnlyList<City>?> GetByCountryId(Guid countryId, CancellationToken cancellationToken = default)
    {
        return await GetAsync(c => c.CountryId == countryId);
    }
}
