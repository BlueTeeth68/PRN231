using BusinessLogic.DTOs.Request.CarInformation;
using BusinessLogic.DTOs.Response.CarInformation;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces;

public interface ICarInformationService
{

    public Task<CarInformationResponse?> GetByIdAsync(int id);
    public Task<List<CarInformationResponse>> GetAllAsync();
    public Task<CarInformationResponse?> CreateNewAsync(CreateCarInformationRequest request);
    public Task<bool> UpdateAsync(int id, UpdateCarInformationRequest request);
    public Task<List<CarInformationResponse>> SearchByNameAsync(string name);
}