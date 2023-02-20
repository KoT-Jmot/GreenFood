using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _account;

        public AccountController(IAccountService account)
        {
            _account = account;
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
