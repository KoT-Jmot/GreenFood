using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace GreenFood.Application.Services
{
    public class AccountManagement : IAccountManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountManagement(UserManager<ApplicationUser> userManager)
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

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
