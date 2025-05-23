using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManager.API.Extensions;
using RecruitmentManager.Application.Features.Candidates.GetById;
using RecruitmentManager.Application.Features.Candidates.Mappings;
using RecruitmentManager.Application.Features.Candidates.Get;
using RecruitmentManager.Application.Features.Candidates.Delete;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Shared;

namespace RecruitmentManager.API.Endpoints;

public class Candidates : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/candidates")
            .WithTags("Candidates");

        group.MapPost("", CreateCandidateEndpoint)
            .WithName("CreateCandidate")
            .WithSummary("Creates a new candidate")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapPut("/{id:guid}", EditCandidateEndpoint)
            .WithName("EditCandidate")
            .WithSummary("Edits an existing candidate")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("/{id:guid}", GetCandidateByIdEndpoint)
            .WithName("GetCandidateById")
            .WithSummary("Gets a candidate by id")
            .Produces<CandidateResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("", GetCandidatesEndpoint)
            .WithName("GetCandidates")
            .WithSummary("Gets a paginated list of candidates")
            .Produces<PaginatedList<CandidateItemResponse>>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapDelete("/{id:guid}", DeleteCandidateEndpoint)
            .WithName("DeleteCandidate")
            .WithSummary("Deletes a candidate by id")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private async Task<IResult> CreateCandidateEndpoint(
        CreateCandidateRequest request,
        ISender mediator,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await mediator.Send(command, cancellationToken);

        return result.Match(id => 
            Results.CreatedAtRoute("GetCandidateById", new { id = id }, id),
            ResultExtension.ResultToResponse);
    }

    private async Task<IResult> EditCandidateEndpoint(
        [FromRoute]Guid id,
        EditCandidateRequest request,
        ISender mediator)
    {
        var command = request.ToCommand(id);
        var result = await mediator.Send(command);

        return result.Match(Results.NoContent, ResultExtension.ResultToResponse);
    }

    private async Task<IResult> GetCandidateByIdEndpoint(
        [FromRoute]Guid id,
        ISender mediator)
    {
        var result = await mediator.Send(new GetCandidateByIdQuery(id));
        return result.Match(Results.Ok, ResultExtension.ResultToResponse);
    }

    private async Task<IResult> GetCandidatesEndpoint(
        ISender mediator,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] int? stateId = null)
    {
        var query = new GetCandidatesQuery(
            searchTerm,
            sortColumn,
            sortOrder,
            page,
            pageSize,
            stateId
        );

        var result = await mediator.Send(query);

        return result.Match(Results.Ok, ResultExtension.ResultToResponse);
    }

    private async Task<IResult> DeleteCandidateEndpoint(
        [FromRoute] Guid id,
        ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCandidateCommand(id), cancellationToken);
        return result.Match(() => Results.NoContent(), ResultExtension.ResultToResponse);
    }
}
