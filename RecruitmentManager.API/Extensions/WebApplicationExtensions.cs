using Carter;

namespace RecruitmentManager.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UsePipeline(this WebApplication app)
    {

        app.UseExceptionHandler();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.MapCarter();

        return app;
    }
}