using CVPZ.Application.Configuration.Queries.GetUserInfo;
using CVPZ.Application.Job.Commands.CreateJob;
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

public class JobController : BaseApiController
{
    public JobController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<CreateJobResponse> Post(CreateJobRequest request)
    {
        var response = await _mediator.Send(request);
        return response;
    }
}
