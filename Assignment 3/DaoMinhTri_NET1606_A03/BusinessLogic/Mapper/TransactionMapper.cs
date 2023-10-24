using BusinessLogic.Dto.Request.Renting;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public static class TransactionMapper
{
    public static RentingDetail ToRentingDetailEntity(RentingDetailDto dto, int transactionId)
    {
        return new RentingDetail
        {
            RentingTransactionId = transactionId,
            CarId = dto.CarId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Price = dto.Price
        };
    }
}