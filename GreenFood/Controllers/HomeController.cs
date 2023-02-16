using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenFood.Web.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IAccountService _account;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            IAccountService account,
            UserManager<ApplicationUser> userManager)
        {
            _account = account;
            _userManager = userManager;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            _userManager.AddClaimAsync(1, new Claim(ClaimTypes.Role, "User"));
            var user = new UserForRegistration 
            {
                FullName = "Ivan",
                Email = "testpo4ta@gmail.com",
                Password = "Aa123456789!",
                PhoneNumber = "+375299267880"
            };
            await _account.SignUpAsync(user);
            //var a = await _userManager.FindByEmailAsync("testpo4ta@gmail.com");
            return Ok("awdwad");
        }
    }
}
