using CVPZ.Application.User;
using CVPZ.Core;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Security.Claims;

namespace CVPZ.Application.Common.Behaviors;

public class UserTracker<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly HttpContext _httpContext;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public UserTracker(IHttpContextAccessor httpContextAccessor, IMediator mediator, ILogger logger)
    {
        _httpContext = httpContextAccessor.HttpContext;
        this._mediator = mediator;
        _logger = logger;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_httpContext.User.Identity != null)
        {
            ClaimsPrincipal principal = _httpContext.User;
            await _mediator.Publish(new UserVisited.Event(principal), cancellationToken);
            _logger.Information($"Request initiated by: {principal.GetClaim("name")}");
        }
    }
}