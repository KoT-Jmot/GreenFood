namespace GreenFood.Domain.Utils
{
    public static class AccountRoles
    {
        public static string GetDefaultRole => "User";
        public static string GetAdministratorRole => "Admin";
        public static string GetSuperAdministratorRole => "SuperAdmin";
    }
}
