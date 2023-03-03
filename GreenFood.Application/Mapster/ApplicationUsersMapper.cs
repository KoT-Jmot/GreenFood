﻿using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class ApplicationUsersMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserForRegistrationDto, ApplicationUser>();
        }
    }
}
