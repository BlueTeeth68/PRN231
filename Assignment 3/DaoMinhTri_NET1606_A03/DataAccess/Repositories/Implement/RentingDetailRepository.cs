using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement
{
    public class RentingDetailRepository : BaseRepository<RentingDetail>, IRentingDetailRepository
    {
        public RentingDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
