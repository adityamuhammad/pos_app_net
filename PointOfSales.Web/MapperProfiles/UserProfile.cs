using System;
using AutoMapper;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.RequestPayloads;

namespace PointOfSales.Web.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterRequest, UserRegisterDto>();
            CreateMap<UserLoginRequest, UserLoginDto>();
        }
    }
}
