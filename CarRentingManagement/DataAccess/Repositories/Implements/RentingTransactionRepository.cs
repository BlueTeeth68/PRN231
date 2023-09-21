using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class RentingTransactionRepository : BaseRepository<RentingTransaction>, IRentingTransactionRepository
{
    public RentingTransactionRepository(AppDbContext context, ILogger<BaseRepository<RentingTransaction>> logger) :
        base(context, logger)
    {
    }

    public async Task<List<RentingTransaction>> GetAllSortByDateDescAsync()
    {
        var result = await _dbSet.OrderByDescending(rt => rt.RentingDate).ToListAsync();
        return result;
    }

    public async Task<List<RentingTransaction>> GetRentingHistoryAsync(int customerId)
    {
        return await _dbSet.Where(rt => rt.CustomerId == customerId)
            .OrderByDescending(rt => rt.RentingDate)
            .ToListAsync();
    }

    public async Task<List<RentingTransaction>> GetByStartDateAndEndDateDescAsync(DateTime startDate, DateTime endDate)
    {
        var result = await _dbSet
            .Where(rt => rt.RentingDate.HasValue && rt.RentingDate.Value >= startDate
                                                 && rt.RentingDate.Value <= endDate)
            .OrderByDescending(rt => rt.RentingDate)
            .ToListAsync();
        return result;
    }
}