using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GreenFood.Web.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _account;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            IAccountService account,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _account = account;
            _userManager = userManager;
            _roleManager = roleManager;
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

        [HttpGet("CreateRole/{role}")]
        public async Task<IActionResult> CreateRoleAsync(string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
                throw new Exception("This role already exists!");

            await _roleManager.CreateAsync(new IdentityRole(role));

            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserForRegistrationDto user)
        {
            await _account.SignUpAsync(user);

            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] UserForLoginDto user)
        {
            var result = await _account.SignInAsync(user);

            return Ok(result);
        }
    }
}
