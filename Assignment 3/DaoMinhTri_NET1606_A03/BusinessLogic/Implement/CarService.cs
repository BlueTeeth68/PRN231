using Application;
using Business_Logic.ExceptionHandler;
using BusinessLogic.Dto.Request.Cars;
using BusinessLogic.Interface;
using BusinessLogic.Mapper;
using DataAccess.Models;

namespace BusinessLogic.Implement;

public class CarService : ICarService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public CarService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public IQueryable<CarInformation> GetAllAsync()
    {
        return _unitOfWork.CarInformationRepository.GetAllOdataAsync();
    }

    public async Task<CarInformation> GetByIdAsync(int id)
    {
        return await _unitOfWork.CarInformationRepository.GetByIdAsync(id)
            .ContinueWith(t => t.Result ?? throw new NotFoundException($"Car {id} can not be found."));
    }

    public async Task<CarInformation> CreateCarAsync(CreateCarDto dto)
    {
        //Need to check admin role
        var userRole = _jwtService.GetCurrentUserRole();
        if (userRole != "Admin")
            throw new ForbiddenException("Access denied.");

        if (!await _unitOfWork.SupplierRepository.ExistByIdAsync(dto.SupplierId))
            throw new BadRequestException("Supplier does not exist.");

        if (!await _unitOfWork.ManufacturerRepository.ExistByIdAsync(dto.ManufacturerId))
            throw new BadRequestException("Manufacturer does not exist.");

        var entity = CarMapper.ToEntity(dto);
        entity.SupplierId = dto.SupplierId;
        entity.ManufacturerId = dto.ManufacturerId;

        var result = await _unitOfWork.CarInformationRepository.AddAsync(entity)
            .ContinueWith(t => t.Result ?? throw new BadRequestException("Something wrong when add new car."));
        await _unitOfWork.SaveChangeAsync();
        return result;
    }

    public async Task<CarInformation> UpdateAsync(int id, UpdateCarDto dto)
    {
        //Need to check admin role
        var userRole = _jwtService.GetCurrentUserRole();
        if (userRole != "Admin")
            throw new ForbiddenException("Access denied.");
        var entity = await _unitOfWork.CarInformationRepository.GetByIdAsync(id)
            .ContinueWith(t => t.Result ?? throw new NotFoundException($"Car {id} does not exist."));

        CarMapper.FromUpdateDtoToEntity(dto, ref entity);
        if (dto.ManufacturerId.HasValue)
        {
            if (!await _unitOfWork.ManufacturerRepository.ExistByIdAsync(dto.ManufacturerId.Value))
                throw new BadRequestException("Manufacturer does not exist.");
            entity.ManufacturerId = dto.ManufacturerId.Value;
        }

        if (dto.SupplierId.HasValue)
        {
            if (!await _unitOfWork.SupplierRepository.ExistByIdAsync(dto.SupplierId))
                throw new BadRequestException("Supplier does not exist.");
            entity.SupplierId = dto.SupplierId.Value;
        }

        _unitOfWork.CarInformationRepository.Update(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}