using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IRentingTransactionRepository:IBaseRepository<RentingTransaction>
{
    public Task<List<RentingTransaction>> GetAllSortByDateDescAsync();
    public Task<List<RentingTransaction>> GetRentingHistoryAsync(int customerId);
    public Task<List<RentingTransaction>> GetByStartDateAndEndDateDescAsync(DateTime startDate, DateTime endDate);
}