namespace CVPZ.Api;

public static class SystemApiExtensions
{
    public static WebApplication MapSystemApi(this WebApplication app)
    {
        app.MapGet("/System/Ping", () => "pong")
           .Produces<string>()
           .WithTags("System");

        return app;
    }
}
