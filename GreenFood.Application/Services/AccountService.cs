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
using GreenFood.Domain.Utils;

namespace GreenFood.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegistrationUserValidator _registrationUserValidator;
        private readonly LoginUserValidator _loginUserValidator;
        private readonly JWTConfig _jwtConfig;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RegistrationUserValidator userValidator,
            LoginUserValidator loginUserValidator,
            JWTConfig jwtConfig)
        {
            _userManager = userManager;
            _registrationUserValidator = userValidator;
            _loginUserValidator = loginUserValidator;
            _jwtConfig = jwtConfig;
        }

        public async Task<bool> SignUpAsync(UserForRegistrationDto userForRegistrationDto)
        {
            await _registrationUserValidator.ValidateAndThrowAsync(userForRegistrationDto);

            var user = userForRegistrationDto.Adapt<ApplicationUser>();

            user.RegistrationDate = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user!, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var message = string.Empty;

                foreach (var error in result.Errors)
                    message += error.Description + "\n";

                throw new RegistrationUserException(message);
            }

            await _userManager.AddToRoleAsync(user, AccountRoles.GetDefaultRole());

            return true;
        }

        public async Task<string> SignInAsync(UserForLoginDto userForLoginDto)
        {
            await _loginUserValidator.ValidateAndThrowAsync(userForLoginDto);

            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);

            if(user is null || !isCorrectPassword)
                throw new LoginUserException();

            return await CreateToken(user);
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser _user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, _user.UserName)
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
            IEnumerable<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtConfig.GetValidIssuer(),
                audience: _jwtConfig.GetValidAudience(),
                claims: claims,
                expires:
                DateTime.Now.AddMinutes(_jwtConfig.GetExpires()),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
