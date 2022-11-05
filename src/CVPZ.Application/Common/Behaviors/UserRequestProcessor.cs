using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Serilog;
using CVPZ.Application.User;
using CVPZ.Infrastructure.Data;

namespace CVPZ.Application.Common.Behaviors;

public class UserRequestProcessor : IRequestPreProcessor<UserRequest>
{
    private readonly HttpContext _httpContext;
    private readonly ILogger _logger;
    //this would be inherited on any request initiated by a user, where a context update will take place soliciting a user object 'stamp'
    public UserRequestProcessor(IHttpContextAccessor accessor, ILogger logger)
    {
        _httpContext = accessor.HttpContext;
        _logger = logger;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        //if all good, send request, or maybe just use cancellation token if not good, idk
        var req = request as UserRequest;
        if (req != null && _httpContext.User.Identity != null)
        {
            ClaimsPrincipal principal = _httpContext.User;
            var objectId = principal.Claims.Single(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            req.UserId = objectId;
            _logger.Information($"user request with id: {objectId}");
        }
        //return Task.CompletedTask;
    }
}
public abstract record UserRequest{ public abstract string? UserId { get; set; } }
