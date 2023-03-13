using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.UserControllers
{
    [Route("Accounts")]
    public class AccountsController : Controller
    {
        private readonly IUserService _account;

        public AccountsController(IUserService account)
        {
            _account = account;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] UserForRegistrationDto userDto,
            CancellationToken cancellationToken)
        {
            var userId = await _account.SignUpAsync(userDto, cancellationToken);

            return Created(nameof(SignUpAsync), userId);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(
            [FromBody] UserForLoginDto userDto,
            CancellationToken cancellationToken)
        {
            var jwtToken = await _account.SignInAsync(userDto, cancellationToken);

            return Ok(jwtToken);
        }
    }
}
