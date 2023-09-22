using BusinessLogic.DTOs.Response.CarInformation;

namespace BusinessLogic.Services.Interfaces;

public interface IManufacturerService
{
    public Task<List<ManufacturerResponse>> GetAllAsync();
    public Task<ManufacturerResponse> GetByIdAsync(int id);
}