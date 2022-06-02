using CVPZ.Application.Job.Commands;
using CVPZ.Application.Job.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

public static class JobApiExtensions
{
    public static WebApplication MapJobApi(this WebApplication app)
    {
        app.MapPost("/Job/Create", Create)
        .Produces<CreateJob.Response>()
        .WithTags("Job");

        app.MapPost("Job/End", End)
        .Produces<EndJob.Response>()
        .WithTags("Job");

        app.MapGet("Job", Search)
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
        var response = await mediator.Send(new SearchJobs.Request());
        return response.Match(
            response => Results.Ok(response),
            error => Results.BadRequest(error)
        );
    }
}
