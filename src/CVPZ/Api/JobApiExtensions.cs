using CVPZ.Application.Job;

namespace CVPZ.Api;

public static class JobApiExtensions
{
    public static WebApplication MapJobApi(this WebApplication app)
    {
        app.MapPost("/api/Job/Create", Create)
           .Produces<CreateJob.Response>()
           .WithTags("Job");

        app.MapPost("/api/Job/End", End)
           .Produces<EndJob.Response>()
           .WithTags("Job");

        app.MapGet("/api/Job", Search)
           .Produces<SearchJobs.Response>()
           .WithTags("Job");

        return app;
    }

    public static async Task<IResult> Create([FromServices] IMediator mediator, CreateJob.Request request)
    {
        var response = await mediator.Send(request);
        return response.Match(
            response => Results.Ok(response),
            error => Results.BadRequest(error)
        );
    }

    public static async Task<IResult> End([FromServices] IMediator mediator, EndJob.Request request)
    {
        var response = await mediator.Send(request);
        return response.Match(
            response => Results.Ok(response),
            error => Results.BadRequest(error)
        );
    }

    public static async Task<IResult> Search([FromServices] IMediator mediator, [FromQuery] string? title, [FromQuery] string? employer)
    {
        var request = new SearchJobs.Request(title, employer);
        var response = await mediator.Send(request);
        return response.Match(
            response => Results.Ok(response),
            error => Results.BadRequest(error)
        );
    }
}
