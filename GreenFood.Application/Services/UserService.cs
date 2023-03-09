using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _manager;

        public UserService(UserManager<ApplicationUser> manager)
        {
            _manager = manager;
        }

        public async Task<PagedList<OutputUserDto>> GetAllUsersAsync(
            UserQueryDto userQuery,
            CancellationToken cancellationToken)
        {
            var user = _manager.Users;

            if (!userQuery.UserName.IsNullOrEmpty())
                user = user.Where(u => u.UserName.Contains(userQuery.UserName!));

            user = user.OrderBy(p => p.RegistrationDate);
            var totalCount = await  user.CountAsync();

            var pagingUsers = await user
                                    .Skip((userQuery.pageNumber - 1) * userQuery.pageSize)
                                    .Take(userQuery.pageSize)
                                    .ToListAsync(cancellationToken);

            var outputUsers = pagingUsers.Adapt<IEnumerable<OutputUserDto>>();
            var usersWithMetaData = PagedList<OutputUserDto>.ToPagedList(outputUsers, userQuery.pageNumber, totalCount, userQuery.pageSize);

            return usersWithMetaData;
        }

        public async Task<OutputUserDto> GetUserByEmail(string userEmail)
        {
            var user = await _manager.FindByEmailAsync(userEmail);

            if (user is null)
                throw new EntityNotFoundException("User was not found!");

            var outputUser = user.Adapt<OutputUserDto>();

            return outputUser;
        }
    }
}
