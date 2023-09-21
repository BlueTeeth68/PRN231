using AutoMapper;
using BusinessLogic.DTOs.Request.Transaction;
using BusinessLogic.DTOs.Response.Transaction;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionService> _logger;
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, ILogger<TransactionService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RentingResponse> CreateRentingTransactionAsync(CreateRentingRequest request)
    {
        var rentingTransactionObj = _mapper.Map<RentingTransaction>(request);
        //Modify date before add
        rentingTransactionObj.RentingDate = DateTime.Now;
        decimal totalPrice = 0;
        foreach (var rd in rentingTransactionObj.RentingDetails)
        {
            if (!(await _unitOfWork.RentingDetailRepository.ExistById(rd.CarId)))
                throw new BadRequestException($"Car {rd.CarId} does not exist.");
            int result = DateTime.Compare(rd.StartDate, rd.EndDate);
            if (result >= 0)
            {
                throw new BadRequestException(
                    $"Error on renting detail carId {rd.CarId}: Start date can not exceed end date.");
            }

            totalPrice += rd?.Price ?? 0;
        }

        rentingTransactionObj.TotalPrice = totalPrice;

        await _unitOfWork.RentingTransactionRepository.AddAsync(rentingTransactionObj);
        var success = await _unitOfWork.SaveChangeAsync();
        if (success > 0)
        {
            var createObj =
                await _unitOfWork.RentingTransactionRepository.GetByIdAsync(rentingTransactionObj.RentingTransationId);
            return _mapper.Map<RentingResponse>(createObj);
        }

        throw new BadRequestException("Error when create renting request.");
    }

    public async Task<RentingResponse> GetByIdAsync(int id)
    {
        var rentingObj = await _unitOfWork.RentingTransactionRepository.GetByIdAsync(id);
        if (rentingObj == null)
        {
            throw new NotFoundException($"Renting {id} does not exist.");
        }

        return _mapper.Map<RentingResponse>(rentingObj);
    }

    public async Task<List<RentingResponse>> GetAllAsync()
    {
        var rentingObjs = await _unitOfWork.RentingTransactionRepository.GetAllSortByDateDescAsync();
        var result = _mapper.Map<List<RentingResponse>>(rentingObjs);
        return result;
    }

    public async Task<List<RentingResponse>> GetRentingTransactionHistoryAsync(int customerId)
    {
        var rentingTransactions = await _unitOfWork.RentingTransactionRepository.GetRentingHistoryAsync(customerId);
        return _mapper.Map<List<RentingResponse>>(rentingTransactions);
    }

    public async Task<List<RentingResponse>> GetRentingTransactionBetweenAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            DateTime tmp;
            tmp = startDate;
            startDate = endDate;
            endDate = tmp;
        }

        var rentingTransactions =
            await _unitOfWork.RentingTransactionRepository.GetByStartDateAndEndDateDescAsync(startDate, endDate);
        return _mapper.Map<List<RentingResponse>>(rentingTransactions);
    }
}