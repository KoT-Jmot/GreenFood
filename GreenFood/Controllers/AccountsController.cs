using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _account;

        public AccountsController(IAccountService account)
        {
            _account = account;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserForRegistrationDto userDto)
        {
            var userId = await _account.SignUpAsync(userDto);

            return Created(nameof(SignUpAsync), userId);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] UserForLoginDto userDto)
        {
            var jwtToken = await _account.SignInAsync(userDto);

            return Ok(jwtToken);
        }
    }
}
