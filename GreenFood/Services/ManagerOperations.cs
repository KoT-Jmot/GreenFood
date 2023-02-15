using AutoMapper;
using FluentValidation;
using GreenFood.Domain.Models;
using GreenFood.Web.Contracts;
using GreenFood.Web.TDOModels;
using GreenFood.Web.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Services
{
    public class ManagerOperations : Controller, IManagerOperations 
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagerOperations(
               IMapper mapper,
               UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> SignUpAsync(UserForRegistrationDto userForRegistration)
        {
            new UserValidator().ValidateAndThrow(userForRegistration);

            var user = _mapper.Map<ApplicationUser>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            //if (!result.Succeeded)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.TryAddModelError(error.Code, error.Description);
            //    }
            //    return BadRequest(ModelState);
            //}
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }

        public Task<bool> SignIn(UserForRegistrationDto user)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }
    }
}
