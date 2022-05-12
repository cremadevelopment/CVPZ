using CVPZ.Application.Configuration.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

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
