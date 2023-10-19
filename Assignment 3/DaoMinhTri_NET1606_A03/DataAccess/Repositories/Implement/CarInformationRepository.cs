using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement
{
    public class CarInformationRepository : BaseRepository<CarInformation>, ICarInformationRepository
    {
        public CarInformationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
