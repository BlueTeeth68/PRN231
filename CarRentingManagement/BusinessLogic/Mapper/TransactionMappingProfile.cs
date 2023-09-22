using AutoMapper;
using BusinessLogic.DTOs.Request.Transaction;
using BusinessLogic.DTOs.Response.Transaction;
using BusinessLogic.Utils;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<RentingDetailRequest, RentingDetail>();

        CreateMap<CreateRentingRequest, RentingTransaction>();

        CreateMap<RentingTransaction, RentingResponse>()
            .ForMember(dest => dest.RentingDate,
                src => src.MapFrom(src => DateTimeUtils.FormatDateTimeToDatetimeV1(src.RentingDate)))
            ;
        CreateMap<RentingDetail, RentingDetailResponse>()
            .ForMember(dest => dest.StartDate,
                src => src.MapFrom(src => DateTimeUtils.FormatDateTimeToDatetimeV1(src.StartDate)))
            .ForMember(dest => dest.EndDate,
                src => src.MapFrom(src => DateTimeUtils.FormatDateTimeToDatetimeV1(src.EndDate)))
            ;
    }
}