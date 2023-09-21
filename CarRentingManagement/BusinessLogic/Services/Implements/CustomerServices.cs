using AutoMapper;
using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class CustomerServices : ICustomerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerServices> _logger;
    private readonly IMapper _mapper;

    public CustomerServices(IUnitOfWork unitOfWork, ILogger<CustomerServices> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserResponse?> RegisterAsync(RegisterUserRequest request)
    {
        if (await _unitOfWork.CustomerRepository.ExistsByEmailAsync(request.Email))
        {
            return null;
        }

        var customerObj = _mapper.Map<Customer>(request);
        await _unitOfWork.CustomerRepository.AddAsync(customerObj);
        var success = await _unitOfWork.SaveChangeAsync();
        if (success > 0)
        {
            var createdCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerObj.CustomerId);
            var result = _mapper.Map<UserResponse>(createdCustomer);
            return result;
        }

        return null;
    }

    public async Task<UserResponse?> LoginAsync(LoginUserRequest request)
    {
        var customerObj =
            await _unitOfWork.CustomerRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);
        if (customerObj == null)
        {
            return null;
        }

        var result = _mapper.Map<UserResponse>(customerObj);
        return result;
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        var result = _mapper.Map<UserResponse>(user);
        return result;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _unitOfWork.CustomerRepository.GetAllAsync();
        var result = _mapper.Map<List<UserResponse>>(users);
        return result;
    }

    public async Task<int> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        if (user == null)
        {
            return -1;
        }

        if (request.Email != null)
        {
            var isExist = await _unitOfWork.CustomerRepository.ExistsByEmailAsync(request.Email);
            if (isExist)
                return -1;
            user.Email = request.Email;
        }

        if (request.Telephone != null)
        {
            user.Telephone = request.Telephone;
        }

        if (request.CustomerName != null)
        {
            user.CustomerName = request.CustomerName;
        }

        if (request.CustomerBirthday != null)
        {
            user.CustomerBirthday = request.CustomerBirthday;
        }

        _unitOfWork.CustomerRepository.Update(user);
        var result = await _unitOfWork.SaveChangeAsync();
        return result;
    }
}