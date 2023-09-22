using AutoMapper;
using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class CustomerService : ICustomerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerService> _logger;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger, IMapper mapper)
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

    public async Task<bool> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new BadRequestException($"User {id} does not exist.");
        }

        if (request.Email != null)
        {
            if (!request.Email.Trim().ToLower().Equals(user.Email.Trim().ToLower()))
            {
                var isExist = await _unitOfWork.CustomerRepository.ExistsByEmailAsync(request.Email);
                if (isExist)
                    throw new ConflictException($"Email {request.Email} has been existed.");
                user.Email = request.Email;
            }
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
        return result > 0;
    }

    public async Task<bool> ChangePasswordAsync(int id, UpdatePasswordRequest request)
    {
        var user = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException($"User {id} does not exist.");
        }

        if (!user.Password.Equals(request.OldPassword))
        {
            throw new BadRequestException("Password is incorrect.");
        }

        user.Password = request.NewPassword;
        _unitOfWork.CustomerRepository.Update(user);
        var result = await _unitOfWork.SaveChangeAsync();
        return result > 0;
    }

    public async Task<List<UserResponse>> GetByNameAscAsync(string name)
    {
        var customerObjs = await _unitOfWork.CustomerRepository.GetCustomerByNameAscAsync(name);
        return _mapper.Map<List<UserResponse>>(customerObjs);
    }
}