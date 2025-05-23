using RecruitmentManager.Shared;

namespace RecruitmentManager.Web.ApiClients;

public interface IStatesApiClient
{
    Task<List<MasterEntityResponse<int>>?> GetStatesAsync(CancellationToken cancellationToken = default);
}