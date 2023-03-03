using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Users")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userManager.Users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(user);
        }
    }
}
