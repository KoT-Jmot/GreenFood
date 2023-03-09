using GreenFood.Application.Contracts;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Route("Admin/Accounts")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class AccountsADminController : Controller
    {
        private readonly IAccountManagement _accountManager;

        public AccountsADminController(IAccountManagement accountManager)
        {
            _accountManager = accountManager;
        }


        [HttpPost("Block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        {
            await _accountManager.BlockUserById(userId);

            return Ok(userId);
        }

        [Authorize(Roles = AccountRoles.GetSuperAdministratorRole)]
        [HttpPost("SetAdmin/{userId}")]
        public async Task<IActionResult> SetAdmin([FromRoute] string userId)
        {
            await _accountManager.SetAdminByUserID(userId);

            return Ok(userId);
        }

    }
}
