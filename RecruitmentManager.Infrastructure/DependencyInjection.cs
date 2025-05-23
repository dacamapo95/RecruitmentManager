using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Domain.Users;
using RecruitmentManager.Infrastructure.Database;
using RecruitmentManager.Infrastructure.Database.Interceptors;
using RecruitmentManager.Infrastructure.Database.Repositories;
using RecruitmentManager.Infrastructure.Database.Seed;
using RecruitmentManager.Infrastructure.Repositories;

namespace RecruitmentManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var inMemorySqliteConnection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:");
        inMemorySqliteConnection.Open();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
                   .UseSqlite(inMemorySqliteConnection)
        );

        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<DatabaseInitializer>();

        services
           .AddIdentityCore<User>()
           .AddRoles<Role>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<ICountryRepository, CountryRepository>();
        //services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IStateRepository, StateRepository>();

        services.AddScoped<CityRepository>();
        services.AddScoped<ICityRepository, CachedCityRepository>();

        return services;
    }
}
