using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class CarInformationRepository:BaseRepository<CarInformation>, ICarInformationRepository
{
    public CarInformationRepository(AppDbContext context, ILogger<BaseRepository<CarInformation>> logger) : base(context, logger)
    {
    }
}