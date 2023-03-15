using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
using GreenFood.Domain.Utils;
using GreenFood.Web.features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Policy = "IsNotBlocked", Roles = AccountRoles.GetAdministratorRole)]
    [Route("Admin/Users")]
    public class UsersAdminController : Controller
    {
        private readonly IAccountRoleService _accountManager;
        private readonly IUserService _userManager;

        public UsersAdminController(
            IAccountRoleService accountManager,
            IUserService userManager)
        {
            _accountManager = accountManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] UserQueryDto userQuery,
            CancellationToken cancellationToken)
        {
            var users = await _userManager.GetAllUsersAsync(userQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputUserDto>>(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync([FromRoute] string email)
        {
            var user = await _userManager.GetUserByEmail(email);

            return Ok(user);
        }

        [HttpPost("{userId}/Block")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        {
            var isSuperAdmin = User.IsInRole(AccountRoles.GetSuperAdministratorRole);
            await _accountManager.BlockAccount(userId, isSuperAdmin);

            return Ok(userId);
        }

        [Authorize(Roles = AccountRoles.GetSuperAdministratorRole)]
        [HttpPost("{userId}/SetAdmin")]
        public async Task<IActionResult> SetAdmin([FromRoute] string userId)
        {
            await _accountManager.SetAdmin(userId);

            return Ok(userId);
        }
    }
}
