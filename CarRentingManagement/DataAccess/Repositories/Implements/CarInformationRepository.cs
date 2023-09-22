using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class CarInformationRepository : BaseRepository<CarInformation>, ICarInformationRepository
{
    public CarInformationRepository(AppDbContext context, ILogger<BaseRepository<CarInformation>> logger) : base(
        context, logger)
    {
    }

    public async Task<List<CarInformation>> SearchByNameAsync(string name)
    {
        var nameNormalize = name.Trim().ToLower();
        var result = await _dbSet.Where(c => c.CarName.ToLower().Contains(nameNormalize))
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .OrderBy(c => c.CarName).ToListAsync();
        return result;
    }

    public async Task<List<CarInformation>> GetAllAsync()
    {
        return await _dbSet.Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .ToListAsync();
    }

    public async Task<CarInformation?> GetByIdAsync(object id)
    {
        if (id is int intId)
        {
            return await _dbSet.Include(c => c.Supplier)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.CarId.Equals(intId));
        }

        return null;
    }
}