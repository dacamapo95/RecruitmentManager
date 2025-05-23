using Carter;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentManager.API.Infrastructure;
using RecruitmentManager.Infrastructure.Options;

namespace RecruitmentManager.API.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>()
                .AddCarter()
                .AddMemoryCache();

        services.Configure<CacheOptions>(configuration.GetSection("Caching"));

        return services;
    }
}