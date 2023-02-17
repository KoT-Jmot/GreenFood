using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GreenFood.Web.Controllers
{
    [Route("AccountOperation")]
    public class AccountOperationController : Controller
    {
        private readonly IAccountService _account;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountOperationController(
            IAccountService account,
            UserManager<ApplicationUser> userManager)
        {
            _account = account;
            _userManager = userManager;
        }

        [HttpGet("GetUser"), Authorize]
        public IActionResult GetUser()
        {
            string jsonString = JsonSerializer.Serialize(_userManager.Users);

            return Ok(jsonString);
        }

        [HttpGet("GetUser/{email}"), Authorize]
        public async Task<IActionResult> GetUserAsync(string email)
        {
            var a = await _userManager.FindByEmailAsync(email);
            string jsonString = JsonSerializer.Serialize(a);

            return Ok(jsonString);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] UserForRegistrationDto user)
        {
            await _account.SignUpAsync(user);

            return Ok();
        }

        [Route("SignIn")]
        public async Task<IActionResult> SignInAsync(
            [FromBody] UserForLoginDto user)
        {
            var result = await _account.SignInAsync(user);

            return Ok(result);
        }
    }
}
