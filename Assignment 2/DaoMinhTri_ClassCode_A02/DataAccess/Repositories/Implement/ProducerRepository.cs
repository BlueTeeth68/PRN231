using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories.Implement;

public class ProducerRepository:BaseRepository<CarProducer>, IProducerRepository
{
    public ProducerRepository(AppDbContext context) : base(context)
    {
    }
}