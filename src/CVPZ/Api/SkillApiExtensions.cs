using CVPZ.Application.Skill.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

public static class SkillApiExtensions
{
    public static WebApplication MapSkillApi(this WebApplication app)
    {
        app.MapPost("/Skill/Create", Create)
        .Produces<CreateSkill.Response>()
        .WithTags("Skill");

        return app;
    }

    public static async Task<IResult> Create([FromServices] IMediator mediator, CreateSkill.Request request)
    {
        var response = await mediator.Send(request);
        return response.Match(
            response => Results.Ok(response),
            error => Results.BadRequest(error)
        );
    }
}
