using Business_Logic.Dto.Request;
using DataAccess.Models;

namespace Business_Logic.Interface;

public interface ICarRentalService
{
    Task<CarRental> CreateAsync(CreateRentingDto dto);
    Task<List<CarRental>> GetAllAsync();
}