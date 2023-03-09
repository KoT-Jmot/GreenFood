using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace GreenFood.Application.Services
{
    public class AccountForAdminService : IAccountForAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountForAdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> BlockUserById(string userId)
        {
            await SetNewRole(userId, AccountRoles.GetBlockedRole);

            return userId;
        }

        public async Task<string> SetAdminByUserID(string userId)
        {
            await SetNewRole(userId, AccountRoles.GetAdministratorRole);

            return userId;
        }

        private async Task SetNewRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new EntityNotFoundException("User was not found!");

            var hasThisRole = await _userManager.IsInRoleAsync(user, role);

            if (hasThisRole)
                throw new UserRoleException("User already has this role!");

            await _userManager.AddToRoleAsync(user, role);

        }
    }
}
