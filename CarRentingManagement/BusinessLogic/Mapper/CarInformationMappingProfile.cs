using AutoMapper;
using BusinessLogic.DTOs.Request.CarInformation;
using BusinessLogic.DTOs.Response.CarInformation;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public class CarInformationMappingProfile : Profile
{
    public CarInformationMappingProfile()
    {
        CreateMap<CreateCarInformationRequest, CarInformation>()
            .ForMember(dest => dest.CarStatus, src => src.MapFrom(src => 1));
        CreateMap<CarInformation, CarInformationResponse>();
    }
}