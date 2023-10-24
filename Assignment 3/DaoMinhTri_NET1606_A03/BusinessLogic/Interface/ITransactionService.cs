using BusinessLogic.Dto.Request.Renting;
using DataAccess.Models;

namespace BusinessLogic.Interface;

public interface ITransactionService
{
    IQueryable<RentingTransaction> GetAll();
    Task<RentingTransaction> CreateTransactionAsync(List<RentingDetailDto> request);
}