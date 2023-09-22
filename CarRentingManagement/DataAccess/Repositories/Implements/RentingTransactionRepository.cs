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
        var result = await _dbSet
            .Include(rt => rt.Customer)
            .Include(rt => rt.RentingDetails).ThenInclude(rd => rd.Car)
            .OrderByDescending(rt => rt.RentingDate).ToListAsync();
        return result;
    }

    public async Task<List<RentingTransaction>> GetRentingHistoryAsync(int customerId)
    {
        return await _dbSet.Where(rt => rt.CustomerId == customerId)
            .Include(rt => rt.Customer)
            .Include(rt => rt.RentingDetails).ThenInclude(rd => rd.Car)
            .OrderByDescending(rt => rt.RentingDate)
            .ToListAsync();
    }

    public async Task<List<RentingTransaction>> GetByStartDateAndEndDateDescAsync(DateTime startDate, DateTime endDate)
    {
        var result = await _dbSet
            .Where(rt => rt.RentingDate.HasValue && rt.RentingDate.Value >= startDate
                                                 && rt.RentingDate.Value <= endDate)
            .Include(rt => rt.Customer)
            .Include(rt => rt.RentingDetails).ThenInclude(rd => rd.Car)
            .OrderByDescending(rt => rt.RentingDate)
            .ToListAsync();
        return result;
    }

    public async Task<List<RentingTransaction>> GetAllAsync()
    {
        return await _dbSet.Include(rt => rt.Customer)
            .Include(rt => rt.RentingDetails).ThenInclude(rd => rd.Car)
            .ToListAsync();
    }

    public async Task<RentingTransaction?> GetByIdAsync(object id)
    {
        if (id is int intId)
        {
            return await _dbSet
                .Include(rt => rt.Customer)
                .Include(rt => rt.RentingDetails).ThenInclude(rd => rd.Car)
                .FirstOrDefaultAsync(c => c.RentingTransationId.Equals(intId));
        }

        return null;
    }
}