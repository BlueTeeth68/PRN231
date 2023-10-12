using Business_Logic.Dto.Request;
using DataAccess.Models;

namespace Business_Logic.Interface;

public interface ICarService
{
    Task<Car> GetByIdAsync(int id);

    Task<List<Car>> GetAllAsync();

    Task<Car> AddAsync(CreateCarDto car);
    Task<Car> UpdateAsync(int id, UpdateCarDto dto);

    Task DeleteCarAsync(int id);
}