using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class RentingTransactionRepository:BaseRepository<RentingTransaction>, IRentingTransactionRepository
{
    public RentingTransactionRepository(AppDbContext context, ILogger<BaseRepository<RentingTransaction>> logger) : base(context, logger)
    {
    }
}