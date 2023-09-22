using BusinessLogic.DTOs.Response.CarInformation;

namespace BusinessLogic.Services.Interfaces;

public interface ISupplierService
{
    public Task<List<SupplierResponse>> GetAllAsync();
    public Task<SupplierResponse> GetByIdAsync(int id);
}