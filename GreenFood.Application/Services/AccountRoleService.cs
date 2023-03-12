using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GreenFood.Application.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> BlockAccount(
            string userId,
            bool isSuperAdmin=false)
        {
            var user = await GetUserByIdAsync(userId);

            if (await _userManager.IsInRoleAsync(user, AccountRoles.GetSuperAdministratorRole))
                throw new UserRoleException("You can't block this account!");

            if (await _userManager.IsInRoleAsync(user, AccountRoles.GetAdministratorRole) && !isSuperAdmin)
                throw new UserRoleException("You can't block this account!");

            await SetNewRoleAsync(user, AccountRoles.GetBlockedRole);

            return userId;
        }

        public async Task<string> SetAdmin(string userId)
        {
            var user = await GetUserByIdAsync(userId);

            await SetNewRoleAsync(user, AccountRoles.GetAdministratorRole);

            return userId;
        }

        private async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new EntityNotFoundException("User was not found!");

            return user;
        }

        private async Task SetNewRoleAsync(
            ApplicationUser user,
            string role)
        {
            var hasThisRole = await _userManager.IsInRoleAsync(user, role);

            if (hasThisRole)
                throw new UserRoleException("User already has this role!");

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
