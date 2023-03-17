using System.Security.Claims;

namespace GreenFood.Domain.Utils
{
    public static class AccountRoles
    {
        public const string GetDefaultRole = "User";
        public const string GetAdministratorRole = "Admin";
        public const string GetSuperAdministratorRole = "SuperAdmin";
        public const string GetBlockedRole = "Blocked";

        public static IEnumerable<string> GetSuperAdminRoles()
        {
            string[] roles = {
                GetDefaultRole,
                GetAdministratorRole,
                GetSuperAdministratorRole
            };

            return roles;
        }
    }
}
