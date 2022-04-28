using CVPZ.Application.Configuration.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseApiController(IMediator mediator)
    {
        this._mediator = mediator;
    }
}

public class ConfigurationController : BaseApiController
{
    public ConfigurationController(IMediator mediator) : base(mediator) { }

    [HttpGet("GetUserInfo")]
    public async Task<UserInfo> GetUserInfo()
    {
        var request = new GetUserInfo();
        var response = await _mediator.Send(request);
        return response;
    }
}
