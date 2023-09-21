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
            .OrderBy(c => c.CarName).ToListAsync();
        return result;
    }
}