using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context)
    {
    }
}