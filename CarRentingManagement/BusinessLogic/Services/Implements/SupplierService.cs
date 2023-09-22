using AutoMapper;
using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class SupplierService : ISupplierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SupplierService> _logger;
    private readonly IMapper _mapper;

    public SupplierService(IUnitOfWork unitOfWork, ILogger<SupplierService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<SupplierResponse>> GetAllAsync()
    {
        var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
        var result = _mapper.Map<List<SupplierResponse>>(suppliers);
        return result;
    }

    public async Task<SupplierResponse> GetByIdAsync(int id)
    {
        var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException($"Supplier {id} does not exist.");
        return _mapper.Map<SupplierResponse>(supplier);
    }
}