using AutoMapper;
using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class ManufacturerService : IManufacturerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ManufacturerService> _logger;

    public ManufacturerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ManufacturerService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ManufacturerResponse>> GetAllAsync()
    {
        var manufacturers = await _unitOfWork.ManufacturerRepository.GetAllAsync();
        return _mapper.Map<List<ManufacturerResponse>>(manufacturers);
    }

    public async Task<ManufacturerResponse> GetByIdAsync(int id)
    {
        var manufacturer = await _unitOfWork.ManufacturerRepository.GetByIdAsync(id);
        if (manufacturer == null)
        {
            throw new NotFoundException($"Manufacturer {id} does not exist.");
        }

        return _mapper.Map<ManufacturerResponse>(manufacturer);
    }
}