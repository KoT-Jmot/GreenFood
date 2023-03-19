using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace GreenFood.Infrastructure.Contexts
{
    public static class DbInitialize
    {
        public static async Task RolesInitialize(this RoleManager<IdentityRole> roleManager)
        {
            var userName = AccountRoles.GetDefaultRole;
            var userIsInitialize = await roleManager.RoleExistsAsync(userName);

            if (!userIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(userName));

            var adminName = AccountRoles.GetAdministratorRole;
            var adminIsInitialize = await roleManager.RoleExistsAsync(adminName);

            if (!adminIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(adminName));

            var superAdminName = AccountRoles.GetSuperAdministratorRole;
            var superAdminIsInitialize = await roleManager.RoleExistsAsync(superAdminName);

            if (!superAdminIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(superAdminName));

            var blockedRole = AccountRoles.GetBlockedRole;
            var blockedIsInitialize = await roleManager.RoleExistsAsync(blockedRole);

            if (!blockedIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(blockedRole));
        }

        public static async Task SuperAdminInitialize(this UserManager<ApplicationUser> userManager)
        {
            var superAdminEmail = Environment.GetEnvironmentVariable("ADMINEMAIL");
            var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdmin is null)
            {
                superAdmin = new ApplicationUser
                {
                    Email = superAdminEmail,
                    UserName = Environment.GetEnvironmentVariable("ADMINNAME"),
                    PhoneNumber = Environment.GetEnvironmentVariable("ADMINPHONE"),
                    RegistrationDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(superAdmin, Environment.GetEnvironmentVariable("ADMINPASSWORD"));

                if (result.Succeeded)
                {
                    string[] roles = {
                        AccountRoles.GetDefaultRole,
                        AccountRoles.GetAdministratorRole,
                        AccountRoles.GetSuperAdministratorRole
                    };

                    await userManager.AddToRolesAsync(superAdmin, roles);
                }
            }
        }
    }
}
