using CVPZ.Core;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Serilog;
namespace CVPZ.Application.Common.Behaviors;

public class UserRequestProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : UserRequest
{
    private readonly HttpContext _httpContext;
    private readonly ILogger _logger;
    public UserRequestProcessor(IHttpContextAccessor accessor, ILogger logger)
    {
        _httpContext = accessor.HttpContext;
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_httpContext.User.Identity != null)
        {
            ClaimsPrincipal principal = _httpContext.User;
            var userObjectClaimName = "http://schemas.microsoft.com/identity/claims/objectidentifier";
            var userId = new Guid(principal.GetClaim(userObjectClaimName));
            request.SetUserId(userId);
            _logger.Information("user request: {@request}", request);
        }
    }
}
