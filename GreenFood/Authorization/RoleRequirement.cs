using Microsoft.AspNetCore.Authorization;

namespace GreenFood.Web.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        protected internal string Role { get; set; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
