using Carter;
using MediatR;
using RecruitmentManager.API.Extensions;
using RecruitmentManager.Application.Features.States.Get;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.API.Endpoints;

public class States : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/states", GetStatesEndpoint)
            .WithName("GetStates")
            .WithSummary("Gets all states")
            .Produces<List<MasterEntityResponse<int>>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> GetStatesEndpoint(
        ISender mediator)
    {
        var result = await mediator.Send(new GetStatesQuery());
        return result.Match(Results.Ok, ResultExtension.ResultToResponse);
    }
}