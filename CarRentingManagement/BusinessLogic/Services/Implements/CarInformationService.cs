using AutoMapper;
using BusinessLogic.DTOs.Request.CarInformation;
using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class CarInformationService : ICarInformationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerServices> _logger;
    private readonly IMapper _mapper;

    public CarInformationService(IUnitOfWork unitOfWork, ILogger<CustomerServices> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CarInformationResponse?> GetByIdAsync(int id)
    {
        var carInf = await _unitOfWork.CarInformationRepository.GetByIdAsync(id);
        var result = _mapper.Map<CarInformationResponse>(carInf);
        return result;
    }

    public async Task<List<CarInformation>> GetAllAsync()
    {
        var cars = await _unitOfWork.CarInformationRepository.GetAllAsync();
        var result = _mapper.Map<List<CarInformation>>(cars);
        return result;
    }

    public async Task<CarInformationResponse?> CreateNewAsync(CreateCarInformationRequest request)
    {
        if (!(await _unitOfWork.ManufacturerRepository.ExistById(request.ManufacturerId)))
        {
            throw new BadRequestException("Manufacturer does not exist.");
        }

        if (!(await _unitOfWork.SupplierRepository.ExistById(request.SupplierId)))
        {
            throw new BadRequestException("Supplier does not exist.");
        }

        var carObj = _mapper.Map<CarInformation>(request);
        await _unitOfWork.CarInformationRepository.AddAsync(carObj);
        var success = await _unitOfWork.SaveChangeAsync();
        if (success > 0)
        {
            var createdObj = await _unitOfWork.CarInformationRepository.GetByIdAsync(carObj.CarId);
            var result = _mapper.Map<CarInformationResponse>(createdObj);
            return result;
        }

        return null;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCarInformationRequest updatedCar)
    {
        var existingCar = await _unitOfWork.CarInformationRepository.GetByIdAsync(id);

        if (existingCar == null)
        {
            throw new NotFoundException($"Car {id} does not exist.");
        }

        existingCar.CarName = updatedCar.CarName ?? existingCar.CarName;
        existingCar.CarDescription = updatedCar.CarDescription ?? existingCar.CarDescription;
        existingCar.NumberOfDoors = updatedCar.NumberOfDoors ?? existingCar.NumberOfDoors;
        existingCar.SeatingCapacity = updatedCar.SeatingCapacity ?? existingCar.SeatingCapacity;
        existingCar.FuelType = updatedCar.FuelType ?? existingCar.FuelType;
        existingCar.Year = updatedCar.Year ?? existingCar.Year;
        existingCar.CarStatus = updatedCar.CarStatus ?? existingCar.CarStatus;
        existingCar.CarRentingPricePerDay = updatedCar.CarRentingPricePerDay ?? existingCar.CarRentingPricePerDay;

        if (updatedCar.ManufacturerId.HasValue)
        {
            var manufacturerExists = await _unitOfWork.ManufacturerRepository.ExistById(updatedCar.ManufacturerId);
            if (manufacturerExists)
            {
                existingCar.ManufacturerId = updatedCar.ManufacturerId.Value;
            }
        }

        if (updatedCar.SupplierId.HasValue)
        {
            var supplierExists = await _unitOfWork.SupplierRepository.ExistById(updatedCar.SupplierId);
            if (supplierExists)
            {
                existingCar.SupplierId = updatedCar.SupplierId.Value;
            }
        }
        _unitOfWork.CarInformationRepository.Update(existingCar);
        var result = await _unitOfWork.SaveChangeAsync();
        return result > 0;
    }
}