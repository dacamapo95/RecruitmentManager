using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Infrastructure.Database.Repositories;

internal sealed class CountryRepository(ApplicationDbContext context)
    : Repository<Country, Guid>(context), ICountryRepository
{
}
