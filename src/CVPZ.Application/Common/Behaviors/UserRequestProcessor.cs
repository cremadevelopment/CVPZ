using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Serilog;
using CVPZ.Application.User;
using CVPZ.Infrastructure.Data;
using CVPZ.Core;

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
            Guid objectId = new(principal.GetClaim("http://schemas.microsoft.com/identity/claims/objectidentifier"));
            request.SetUserId(objectId);
            _logger.Information("user request: {@request}", request);
        }
    }
}
