using FluentValidation;
using GreenFood.Application.DTO;
using GreenFood.Application.Validation;
using Microsoft.AspNetCore.Identity;
using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models.Utils;
using GreenFood.Domain.Models;
using GreenFood.Application.Mapster;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace GreenFood.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegistrationUserValidator _RegUserValidator;
        private readonly LoginUserValidator _LoginUserValidator;
        private readonly IUserMapper _userMapper;
        private readonly IConfiguration _configuration;
        private ApplicationUser _user = null!;

        public AccountService(
               UserManager<ApplicationUser> userManager,
               RegistrationUserValidator userValidator,
               IUserMapper userMapper,
               LoginUserValidator loginUserValidator,
               IConfiguration configuration)
        {
            _userManager = userManager;
            _RegUserValidator = userValidator;
            _userMapper = userMapper;
            _LoginUserValidator = loginUserValidator;
            _configuration = configuration;
        }
        public async Task<bool> SignUpAsync(UserForRegistration userForRegistration)
        {
            await _RegUserValidator.ValidateAndThrowAsync(userForRegistration);

            var user = _userMapper.MapTo(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                string message = string.Empty;
                foreach (var error in result.Errors)
                    message += error.Description + "\n";
                throw new RegistrationUserException(message);
            }
            await _userManager.AddToRoleAsync(user, AccountRoles.GetDefaultRole());
            return true;
        }

        public async Task<string> SignInAsync(UserForLoginDto user)
        {
            await _LoginUserValidator.ValidateAndThrowAsync(user);
            _user = await _userManager.FindByEmailAsync(user.Email);
            if (_user != null && await _userManager.CheckPasswordAsync(_user, user.Password))
                return await CreateToken();
            else
                throw new LoginUserException();

        }

        private async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
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
            var jwtSettings = _configuration.GetSection("JwtSettings");
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
