using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Security.Claims;

namespace CVPZ.Application.Common.Behaviors;

public class UserTracker<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly HttpContext _httpContext;
    private readonly ILogger _logger;

    public UserTracker(IHttpContextAccessor httpContextAccessor, ILogger logger)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        // ToDo :: Sorting out claims identity.
        if (_httpContext.User.Identity != null)
        {
            ClaimsPrincipal principal = _httpContext.User;
            _logger.Information($"Request initiated by: {principal.Claims.Single(c => c.Type == "name").Value}");
        }

        return Task.CompletedTask;
    }
}