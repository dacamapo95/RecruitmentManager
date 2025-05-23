using RecruitmentManager.Domain.Interfaces;

namespace RecruitmentManager.Domain.Countries;

public interface ICountryRepository : IReadRepository<Country, Guid>;