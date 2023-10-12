using Business_Logic.Dto.Request;
using Business_Logic.ExceptionHandler;
using Business_Logic.Interface;
using Business_Logic.Mapper;
using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace Business_Logic.Implement;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IProducerRepository _producerRepository;

    public CarService(ICarRepository carRepository, IProducerRepository producerRepository)
    {
        _carRepository = carRepository;
        _producerRepository = producerRepository;
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        return await _carRepository
            .GetByIdAsync(id, includeProperties: $"{nameof(Car.Producer)}", disableTracking: true)
            .ContinueWith(t => t.Result ?? throw new NotFoundException($"Car {id} does not exist."));
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _carRepository.GetAllAsync(includeProperties: $"{nameof(Car.Producer)}");
    }

    public async Task<Car> AddAsync(CreateCarDto car)
    {
        var carEntity = CarMapper.ToEntity(car);
        var result = await _carRepository.AddAsync(carEntity)
            .ContinueWith(t => t.Result ?? throw new ConflictException("Can not create car"));
        await _carRepository.SaveChangeAsync();
        return result;
    }

    public async Task<Car> UpdateAsync(int id, UpdateCarDto dto)
    {
        var carEntity = await _carRepository.GetByIdAsync(id, disableTracking: false)
            .ContinueWith(t => t.Result ?? throw new NotFoundException($"Car {id} does not exist."));
        CarMapper.UpdateCarToEntity(dto, ref carEntity);
        if (dto.ProducerId.HasValue)
        {
            if (!await _producerRepository.ExistByIdAsync(dto.ProducerId.Value))
            {
                throw new NotFoundException($"Car producer {dto.ProducerId.Value} does not exist.");
            }

            carEntity.ProducerId = dto.ProducerId.Value;
        }

        await _carRepository.SaveChangeAsync();

        return carEntity;
    }

    public async Task DeleteCarAsync(int id)
    {
        await _carRepository.DeleteByIdAsync(id);
        await _carRepository.SaveChangeAsync();
    }
}