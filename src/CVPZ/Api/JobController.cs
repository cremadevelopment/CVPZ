using CVPZ.Application.Job.Commands.CreateJob;
using CVPZ.Application.Job.Commands.EndJob;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVPZ.Api;

public class JobController : BaseApiController
{
    public JobController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<CreateJobResponse> Post(CreateJobRequest request)
    {
        var response = await _mediator.Send(request);
        return response;
    }

    [HttpPut("End/{jobId}")]
    public async Task<EndJobResponse> Put([FromRoute]string jobId, EndJobRequest request)
    {
        var response = await _mediator.Send(request);
        return response;
    }
}
