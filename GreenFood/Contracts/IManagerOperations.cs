using GreenFood.Web.TDOModels;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Contracts
{
    internal interface IManagerOperations
    {
        public Task<IActionResult> SignUpAsync(UserForRegistrationDto userForRegistration);
        public Task<bool> SignIn(UserForRegistrationDto user);
        public Task<string> CreateToken();

    }
}
