﻿using GreenFood.Application.DTO.InputDto.UserDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;

namespace GreenFood.Application.Contracts
{
    public interface IUserService
    {
        Task<string> SignUpAsync(
            UserForRegistrationDto userForRegistrationDto,
            CancellationToken cancellationToken);
        Task<string> SignInAsync(
            UserForLoginDto userForLoginDto,
            CancellationToken cancellationToken);
        Task<PagedList<OutputUserDto>> GetAllUsersAsync(
            UserQueryDto userQuery,
            CancellationToken cancellationToken);
        Task<OutputUserDto> GetUserByEmail(string userEmail);
    }
}
