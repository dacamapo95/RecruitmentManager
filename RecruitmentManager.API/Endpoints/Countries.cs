using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManager.API.Extensions;
using RecruitmentManager.Application.Features.Countries.Get;
using RecruitmentManager.Application.Features.Countries.GetCitiesByCountry;
using RecruitmentManager.Shared;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.API.Endpoints;

public class Countries : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/countries")
            .WithTags(nameof(Countries))
            .WithOpenApi();

        group.MapGet("/", GetCountries)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<MasterEntityResponse<Guid>>(StatusCodes.Status200OK)
            .WithSummary("Get all countries");

        group.MapGet("/{countryId:guid}/cities", GetCitiesByCountryId)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<MasterEntityResponse<Guid>>(StatusCodes.Status200OK)
            .WithSummary("Get cities by country.");
    }

    private async Task<IResult> GetCountries(ISender sender)
    {
        var result = await sender.Send(new GetCountriesQuery());
        return result.Match(Results.Ok, ResultExtension.ResultToResponse);
    }

    private async Task<IResult> GetCitiesByCountryId(ISender sender, [FromRoute]Guid countryId)
    {
        var result = await sender.Send(new GetCitiesByCountryQuery(countryId));
        return result.Match(Results.Ok, ResultExtension.ResultToResponse);
    }
}

       
