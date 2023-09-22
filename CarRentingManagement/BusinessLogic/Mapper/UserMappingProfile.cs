using AutoMapper;
using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.Utils;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, Customer>()
            .ForMember(dest => dest.CustomerStatus, src => src.MapFrom(src => 1));

        CreateMap<Customer, UserResponse>()
            .ForMember(dest => dest.CustomerBirthday, src => src.MapFrom(src => DateTimeUtils.FormatDateTimeToDateV1(src.CustomerBirthday)));
    }
}