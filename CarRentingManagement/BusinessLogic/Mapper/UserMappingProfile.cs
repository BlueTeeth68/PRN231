using AutoMapper;
using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, Customer>()
            .ForMember(dest => dest.CustomerStatus, src => src.MapFrom(src => 1));

        CreateMap<Customer, LoginUserResponse>();
    }
}