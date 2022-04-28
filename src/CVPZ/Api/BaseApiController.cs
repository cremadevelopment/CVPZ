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
