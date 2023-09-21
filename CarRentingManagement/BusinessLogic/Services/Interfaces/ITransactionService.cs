using BusinessLogic.DTOs.Request.Transaction;
using BusinessLogic.DTOs.Response.Transaction;

namespace BusinessLogic.Services.Interfaces;

public interface ITransactionService
{
    public Task<RentingResponse> CreateRentingTransactionAsync(CreateRentingRequest request);
    public Task<RentingResponse> GetByIdAsync(int id);
    public Task<List<RentingResponse>> GetAllAsync();
    public Task<List<RentingResponse>> GetRentingTransactionHistoryAsync(int customerId);
    public Task<List<RentingResponse>> GetRentingTransactionBetweenAsync(DateTime startDate, DateTime endDate);
}