using Business_Logic.Dto.Request;
using Business_Logic.ExceptionHandler;
using Business_Logic.Interface;
using DataAccess.Enum;
using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace Business_Logic.Implement;

public class CarRentalService : ICarRentalService
{
    private readonly ICarRentalRepository _carRentalRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;

    public CarRentalService(ICarRentalRepository carRentalRepository, ICustomerRepository customerRepository,
        ICarRepository carRepository)
    {
        _carRentalRepository = carRentalRepository;
        _customerRepository = customerRepository;
        _carRepository = carRepository;
    }

    public async Task<CarRental> CreateAsync(CreateRentingDto dto)
    {
        if (!await _customerRepository.ExistByIdAsync(dto.CustomerId))
            throw new BadRequestException($"Customer {dto.CustomerId} does not exist.");
        if (!await _carRepository.ExistByIdAsync(dto.CarId))
            throw new BadRequestException($"Car {dto.CarId} does not exist.");
        if (dto.PickupDate.Date < DateTime.Now.Date)
            throw new BadRequestException("Pick up date must be greater than today");
        if (dto.ReturnDate.Date < dto.PickupDate.AddDays(1).Date)
            throw new BadRequestException("Return day must be at least 1 day later than the picking day.");
        if (dto.ReturnDate.Date > dto.PickupDate.AddDays(14).Date)
            throw new BadRequestException("Return day must be at most 14 day later than the picking day.");
        if (dto.RentPrice < 0)
            throw new BadRequestException("Rent price must be greater than 0.");
        var entity = new CarRental
        {
            CustomerId = dto.CustomerId,
            CarId = dto.CarId,
            PickupDate = dto.PickupDate,
            ReturnDate = dto.ReturnDate,
            RentPrice = dto.RentPrice,
            Status = RentingStatus.Success
        };
        await _carRentalRepository.AddAsync(entity);
        await _carRepository.SaveChangeAsync();
        return entity;
    }

    public async Task<List<CarRental>> GetAllAsync()
    {
        return await _carRentalRepository.GetAllAsync();
    }
}