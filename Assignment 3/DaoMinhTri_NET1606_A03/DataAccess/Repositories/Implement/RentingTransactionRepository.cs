using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement
{
    public class RentingTransactionRepository : BaseRepository<RentingTransaction>, IRentingTransactionRepository
    {
        public RentingTransactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
