using BusinessLogic.Dto.Request.Cars;
using DataAccess.Models;

namespace BusinessLogic.Interface;

public interface ICarService
{
    IQueryable<CarInformation> GetAllAsync();

    Task<CarInformation> GetByIdAsync(int id);

    Task<CarInformation> CreateCarAsync(CreateCarDto dto);

    Task<CarInformation> UpdateAsync(int id, UpdateCarDto dto);
}