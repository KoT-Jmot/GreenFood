using System.Security.Claims;

namespace GreenFood.Domain.Utils
{
    public static class ClaimsConfiguration
    {
        public static string GetUserId(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        }
    }
}
