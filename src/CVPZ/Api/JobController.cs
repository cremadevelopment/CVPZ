using CVPZ.Application.Job;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

public class JobController : BaseApiController
{
    public JobController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<CreateJob.Response> Post(CreateJob.Request request)
    {
        var response = await _mediator.Send(request);
        return response;
    }

    [HttpPut("End/{jobId}")]
    public async Task<EndJob.Response> Put([FromRoute]string jobId, EndJob.Request request)
    {
        var response = await _mediator.Send(request);
        return response;
    }
}
