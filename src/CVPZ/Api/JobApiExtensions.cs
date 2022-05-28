using CVPZ.Application.Job;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

public static class JobApiExtensions
{
    public static WebApplication MapJobApi(this WebApplication app)
    {
        app.MapPost("/Job/Create", async ([FromServices] IMediator mediator, CreateJob.Request request) => {
            var result = await mediator.Send(request);
            return result.Match(
                response => Results.Ok(response),
                error => Results.BadRequest(error)
            );
        })
        .Produces<CreateJob.Response>()
        .WithTags("Job");

        app.MapPost("Job/End", async ([FromServices] IMediator mediator, EndJob.Request request) =>
        {
            var response = await mediator.Send(request);
            return response;
        })
        .Produces<EndJob.Response>()
        .WithTags("Job");

        return app;
    }
}
