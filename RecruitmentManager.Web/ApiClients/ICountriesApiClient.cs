using RecruitmentManager.Shared;
using System.Threading.Tasks;

namespace RecruitmentManager.Web.ApiClients;

public interface ICountriesApiClient
{
    Task<List<MasterEntityResponse<Guid>>?> GetCountriesAsync(CancellationToken cancellationToken = default);

    Task<List<MasterEntityResponse<Guid>>?> GetCitiesByCountryIdAsync(Guid id, CancellationToken cancellationToken = default);
}