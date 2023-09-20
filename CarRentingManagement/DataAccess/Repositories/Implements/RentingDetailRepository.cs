using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class RentingDetailRepository:BaseRepository<RentingDetail>, IRentingDetailRepository
{
    public RentingDetailRepository(AppDbContext context, ILogger<BaseRepository<RentingDetail>> logger) : base(context, logger)
    {
    }
}