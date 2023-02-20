using GreenFood.Domain.Models.Utils;
using Microsoft.AspNetCore.Identity;

namespace GreenFood.Domain.Utils
{
    public static class StaticInit
    {
        public static async void InitRole(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(AccountRoles.GetDefaultRole()))
                await roleManager.CreateAsync(new IdentityRole(AccountRoles.GetDefaultRole()));

            if (!await roleManager.RoleExistsAsync(AccountRoles.GetAdministratorRole()))
                await roleManager.CreateAsync(new IdentityRole(AccountRoles.GetAdministratorRole()));
        }
    }
}
