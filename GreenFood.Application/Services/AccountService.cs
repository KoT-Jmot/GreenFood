using FluentValidation;
using GreenFood.Application.DTO;
using GreenFood.Application.Validation;
using Microsoft.AspNetCore.Identity;
using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models.Utils;
using GreenFood.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Mapster;

namespace GreenFood.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegistrationUserValidator _registrationUserValidator;
        private readonly LoginUserValidator _loginUserValidator;
        private readonly IGetFromConfiguration _configurationData;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RegistrationUserValidator userValidator,
            LoginUserValidator loginUserValidator,
            IGetFromConfiguration configurationData)
        {
            _userManager = userManager;
            _registrationUserValidator = userValidator;
            _loginUserValidator = loginUserValidator;
            _configurationData = configurationData;
        }

        public async Task<bool> SignUpAsync(UserForRegistrationDto userForRegistration)
        {
            await _registrationUserValidator.ValidateAndThrowAsync(userForRegistration);

            var user = userForRegistration.Adapt<ApplicationUser>();

            user.UserName = userForRegistration.FullName;
            user.RegDate = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user!, userForRegistration.Password);

            if (!result.Succeeded)
            {
                string message = string.Empty;

                foreach (var error in result.Errors)
                    message += error.Description + "\n";

                throw new RegistrationUserException(message);
            }

            await _userManager.AddToRoleAsync(user!, AccountRoles.GetDefaultRole());

            return true;
        }

        public async Task<string> SignInAsync(UserForLoginDto user)
        {
            await _loginUserValidator.ValidateAndThrowAsync(user);

            var _user = await _userManager.FindByEmailAsync(user.Email);

            var a = await _userManager.CheckPasswordAsync(_user, user.Password);

            if (_user != null && a)
                return await CreateToken(_user);
            else
                throw new LoginUserException();
        }

        private async Task<string> CreateToken(ApplicationUser _user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(_user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var a = Environment.GetEnvironmentVariable("SECRET");
            var key = Encoding.UTF8.GetBytes(a);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser _user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            List<Claim> claims)
        {
            var jwtSettings = _configurationData.GetJWTSettings();

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires:
                DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
