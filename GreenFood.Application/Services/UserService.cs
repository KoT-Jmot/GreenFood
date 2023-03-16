using FluentValidation;
using GreenFood.Application.Validation;
using Microsoft.AspNetCore.Identity;
using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Utils;
using GreenFood.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Mapster;
using GreenFood.Application.RequestFeatures;
using GreenFood.Application.DTO.OutputDto;
using Microsoft.EntityFrameworkCore;
using GreenFood.Application.DTO.InputDto.UserDto;

namespace GreenFood.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegistrationUserValidator _registrationUserValidator;
        private readonly LoginUserValidator _loginUserValidator;
        private readonly JWTConfig _jwtConfig;

        public UserService(
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

        public async Task<string> SignUpAsync(
            UserForRegistrationDto userForRegistrationDto,
            CancellationToken cancellationToken)
        {
            await _registrationUserValidator.ValidateAndThrowAsync(userForRegistrationDto, cancellationToken);

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

            await _userManager.AddToRoleAsync(user, AccountRoles.GetDefaultRole);
            //await _userManager.AddToRoleAsync(user, AccountRoles.GetAdministratorRole);
            //await _userManager.AddToRoleAsync(user, AccountRoles.GetSuperAdministratorRole);

            return user.Id;
        }

        public async Task<string> SignInAsync(
            UserForLoginDto userForLoginDto,
            CancellationToken cancellationToken)
        {
            await _loginUserValidator.ValidateAndThrowAsync(userForLoginDto, cancellationToken);

            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);

            if(user is null || !isCorrectPassword)
                throw new LoginUserException();

            return await CreateTokenAsync(user);
        }

        public async Task<PagedList<OutputUserDto>> GetAllUsersAsync(
            UserQueryDto userQuery,
            CancellationToken cancellationToken)
        {
            var user = _userManager.Users;

            if (!userQuery.UserName.IsNullOrEmpty())
                user = user.Where(u => u.UserName.Contains(userQuery.UserName!));

            user = user.OrderBy(p => p.RegistrationDate);
            var totalCount = await user.CountAsync(cancellationToken);

            var pagingUsers = await user
                                    .Skip((userQuery.PageNumber - 1) * userQuery.PageSize)
                                    .Take(userQuery.PageSize)
                                    .ToListAsync(cancellationToken);

            var outputUsers = pagingUsers.Adapt<IEnumerable<OutputUserDto>>();
            var usersWithMetaData = PagedList<OutputUserDto>.ToPagedList(outputUsers, userQuery.PageNumber, totalCount, userQuery.PageSize);

            return usersWithMetaData;
        }

        public async Task<OutputUserDto> GetUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                throw new EntityNotFoundException("User was not found!");

            var outputUser = user.Adapt<OutputUserDto>();

            return outputUser;
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

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
