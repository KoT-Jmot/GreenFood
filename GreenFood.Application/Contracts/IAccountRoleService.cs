using System.Security.Claims;

namespace GreenFood.Application.Contracts
{
    public interface IAccountRoleService
    {
        Task<string> BlockAccount(
            string userId,
            bool isSuperAdmin = false);
        Task<string> SetAdmin(string userId);
    }
}
