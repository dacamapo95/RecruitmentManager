using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Infrastructure.Options;
using System.Linq.Expressions;

namespace RecruitmentManager.Infrastructure.Database.Repositories;
internal class CachedCityRepository(
    CityRepository cityRepository, 
    IMemoryCache cache, 
    IOptions<CacheOptions> cacheOptions) : ICityRepository
{
    private readonly CityRepository _cityRepository = cityRepository;
    private readonly IMemoryCache _cache = cache;
    private readonly CacheOptions _cacheOptions = cacheOptions.Value;

    public Task<IReadOnlyList<City>?> GetByCountryId(Guid countryId, CancellationToken cancellationToken = default)
    {
        if (!_cacheOptions.EnableCaching)
        {
            return _cityRepository.GetByCountryId(countryId, cancellationToken);
        }

        string cacheKey = $"country-{countryId}";

        return _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheOptions.ExpirationTimeInMinutes);
            var cities = await _cityRepository.GetByCountryId(countryId);
            return cities;
        });
    }

    public Task<int> CountAsync(Expression<Func<City, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        return _cityRepository.CountAsync(filter, cancellationToken);
    }

    public void DisableTracking()
    {
        _cityRepository.DisableTracking();
    }

    public Task<bool> ExistsAsync(Expression<Func<City, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _cityRepository.ExistsAsync(filter, cancellationToken);
    }

    public Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _cityRepository.GetAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<City>> GetAsync(Expression<Func<City, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _cityRepository.GetAsync(filter, cancellationToken);
    }

    public Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _cityRepository.GetByIdAsync(id, cancellationToken);
    }
}
