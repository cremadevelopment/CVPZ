using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Serilog;

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
            _logger.Information("CVPZ Request from user: {@name}", _httpContext.User.Identity?.Name);

        return Task.CompletedTask;
    }
}