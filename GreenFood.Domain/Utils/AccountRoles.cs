namespace GreenFood.Domain.Models.Utils
{
    public static class AccountRoles
    {
        public static string GetDefaultRole() => "User";
        public static string GetAdministratorRole() => "Admin";
    }
}
