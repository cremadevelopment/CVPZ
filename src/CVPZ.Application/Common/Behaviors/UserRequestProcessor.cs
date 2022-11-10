using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CVPZ.Application.Common.Behaviors;

public class UserRequestProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : UserRequest
{
    private readonly HttpContext _httpContext;

    public UserRequestProcessor(IHttpContextAccessor httpContextAccessor)
    { 
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_httpContext.User.Identity != null)
        {
            ClaimsPrincipal principal = _httpContext.User;
            var userObjectClaimName = "https://schemas.microsoft.com/identity/claims/objectidentifier";
            var userIdString = principal.Claims.Single(c => c.Type == userObjectClaimName).Value;
            request.SetUserId(new Guid(userIdString));
        }
    }
}