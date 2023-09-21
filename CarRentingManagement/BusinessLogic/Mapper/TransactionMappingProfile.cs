using AutoMapper;
using BusinessLogic.DTOs.Request.Transaction;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<RentingDetailRequest, RentingDetail>();

        CreateMap<CreateRentingRequest, RentingTransaction>();
    }
}