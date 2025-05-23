using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Errors;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.States.Get;

public class GetStatesQueryHandler(IStateRepository stateRepository)
    : IQueryHandler<GetStatesQuery, List<MasterEntityResponse<int>>>
{
    private readonly IStateRepository _stateRepository = stateRepository;

    public async Task<Result<List<MasterEntityResponse<int>>>> Handle(GetStatesQuery query, CancellationToken cancellationToken)
    {
        _stateRepository.DisableTracking();

        var states = await _stateRepository.GetAllAsync(cancellationToken);

        if (states.Count == 0)
        {
            return SharedErrors.MasterEntityNotFound(nameof(State));
        }

        return states.Select(s => new MasterEntityResponse<int>(s.Id, s.Name)).ToList();
    }
}