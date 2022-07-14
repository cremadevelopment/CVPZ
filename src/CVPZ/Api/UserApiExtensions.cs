using CVPZ.Application.Configuration.Queries.GetUserInfo;

namespace CVPZ.Api;

public static class UserApiExtensions
{
    public static WebApplication MapUserApi(this WebApplication app)
    {
        app.MapGet("GetUserInfo", async ([FromServices] IMediator mediator) => {
            return await mediator.Send(new GetUserInfo());
        }).Produces<UserInfo>();

        return app;
    }
}