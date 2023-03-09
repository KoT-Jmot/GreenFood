using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Web.features;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Route("Users")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IUserService _userManager;

        public UsersController(IUserService userService)
        {
            _userManager = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] UserQueryDto userQuery,
            CancellationToken cancellationToken)
        {
            var users = await _userManager.GetAllUsersAsync(userQuery,cancellationToken);

            return new PagingActionResult<PagedList<OutputUserDto>>(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync([FromRoute] string email)
        {
            var user = await _userManager.GetUserByEmail(email);

            return Ok(user);
        }
    }
}
