using System.Security.Claims;

namespace CVPZ.Core;

public static class ClaimsPrincipalExtensions
{
    public static string GetClaim(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.Single(c => c.Type == claimType).Value;
    }
}
