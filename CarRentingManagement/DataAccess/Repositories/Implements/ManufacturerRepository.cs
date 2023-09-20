using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class ManufacturerRepository:BaseRepository<Manufacturer>, IManufacturerRepository
{
    public ManufacturerRepository(AppDbContext context, ILogger<BaseRepository<Manufacturer>> logger) : base(context, logger)
    {
    }
}