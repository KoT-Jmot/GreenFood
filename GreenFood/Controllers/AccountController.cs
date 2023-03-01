using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.ServicesDto;
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
        public async Task<IActionResult> SignUpAsync([FromBody] UserForRegistrationDto userDto)
        {
            await _account.SignUpAsync(userDto);

            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] UserForLoginDto userDto)
        {
            var result = await _account.SignInAsync(userDto);

            return Ok(result);
        }
    }
}
