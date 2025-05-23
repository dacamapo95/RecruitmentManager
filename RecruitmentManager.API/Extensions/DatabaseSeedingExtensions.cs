using RecruitmentManager.Infrastructure.Database.Seed;

namespace RecruitmentManager.API.Extensions;

public static class DatabaseSeedingExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        try
        {
            logger.LogInformation("Beginning database seeding...");
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
            await databaseInitializer.InitializeAsync();
            logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}