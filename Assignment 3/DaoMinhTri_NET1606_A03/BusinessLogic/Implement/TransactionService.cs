using System.Transactions;
using Application;
using BusinessLogic.Dto.Request.Renting;
using BusinessLogic.ExceptionHandler;
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

            var transaction = await _unitOfWork.RentingDetailRepository.GetAsync(
                filter: t => (t.StartDate <= dto.StartDate && dto.StartDate <= t.EndDate)
                             || (t.StartDate <= dto.EndDate && dto.EndDate <= t.EndDate),
                orderBy: null,
                includeProperties: "",
                disableTracking: false
            );
            if (transaction.Any())
                throw new BadRequestException("Overlap renting day.");

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

    public async Task<List<RentingTransaction>> ViewHistoryAsync()
    {
        var currentUserId = _jwtService.GetCurrentUserId();
        var currentUser = await _unitOfWork.CustomerRepository.GetByIdAsync(currentUserId)
            .ContinueWith(t => t.Result ?? throw new UnauthorizedException("Can not extract customer from token."));

        var result = await _unitOfWork.RentingTransactionRepository.GetAsync(
            filter: rt => rt.CustomerId == currentUserId,
            orderBy: q => q.OrderByDescending(rt => rt.RentingDate),
            includeProperties: $"{nameof(RentingTransaction.RentingDetails)}",
            disableTracking: true);
        return result.ToList();
    }
}