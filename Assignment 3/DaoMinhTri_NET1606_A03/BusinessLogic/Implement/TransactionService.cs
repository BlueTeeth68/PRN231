using System.Transactions;
using Application;
using Business_Logic.ExceptionHandler;
using BusinessLogic.Dto.Request.Renting;
using BusinessLogic.Interface;
using BusinessLogic.Mapper;
using DataAccess.Models;

namespace BusinessLogic.Implement;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public TransactionService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public IQueryable<RentingTransaction> GetAll()
    {
        return _unitOfWork.RentingTransactionRepository.GetAllOdataAsync();
    }

    public async Task<RentingTransaction> CreateTransactionAsync(List<RentingDetailDto> request)
    {
        var currentUserId = _jwtService.GetCurrentUserId();
        var currentUser = await _unitOfWork.CustomerRepository.GetByIdAsync(currentUserId)
            .ContinueWith(t => t.Result ?? throw new UnauthorizedException("Can not extract user"));

        if (request.Count < 1)
            throw new BadRequestException("At least 1 renting detail.");

        var entity = new RentingTransaction
        {
            RentingDate = DateTime.Now,
            CustomerId = currentUserId,
            RentingStatus = 1,
            RentingDetails = new List<RentingDetail>(),
            TotalPrice = 0
        };
        foreach (var dto in request)
        {
            //Need to check overlap later
            if (!await _unitOfWork.CarInformationRepository.ExistByIdAsync(dto.CarId))
                throw new BadRequestException($"Car {dto.CarId} does not exist.");

            if (dto.StartDate > dto.EndDate || dto.StartDate > DateTime.Now)
            {
                throw new BadRequestException(
                    $"Error on renting detail carId {dto.CarId}: Start date can not exceed end date.");
            }

            var tmp = TransactionMapper.ToRentingDetailEntity(dto, entity.RentingTransationId);
            entity.RentingDetails.Add(tmp);
            entity.TotalPrice += dto.Price;
        }

        entity = await _unitOfWork.RentingTransactionRepository.AddAsync(entity)
            .ContinueWith(t => t.Result ?? throw new BadRequestException("Something wrong."));
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}