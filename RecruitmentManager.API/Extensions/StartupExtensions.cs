using Serilog;
using RecruitmentManager.Infrastructure;
using RecruitmentManager.Application;

namespace RecruitmentManager.API.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.RegisterApiServices(builder.Configuration)
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationServices();

        return builder.Build();
    }
}
