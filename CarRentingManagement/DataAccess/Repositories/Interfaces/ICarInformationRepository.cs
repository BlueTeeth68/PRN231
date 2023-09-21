using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ICarInformationRepository: IBaseRepository<CarInformation>
{
    public Task<List<CarInformation>> SearchByNameAsync(string name);
}