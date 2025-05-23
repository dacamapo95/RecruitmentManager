using RecruitmentManager.Shared;
using RecruitmentManager.Application.Core.Abstractions;

namespace RecruitmentManager.Application.Features.States.Get;

public sealed record GetStatesQuery() : IQuery<List<MasterEntityResponse<int>>>;