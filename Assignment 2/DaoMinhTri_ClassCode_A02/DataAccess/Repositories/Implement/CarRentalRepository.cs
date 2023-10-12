using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement;

public class CarRentalRepository:BaseRepository<CarRental>, ICarRentalRepository
{
    public CarRentalRepository(AppDbContext context) : base(context)
    {
    }
}