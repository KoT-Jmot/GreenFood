using GreenFood.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Users")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet, Authorize]
        public IActionResult GetAllUsers()
        {

            return Ok(_userManager.Users);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUserByEmailAsync([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(user);
        }
    }
}
