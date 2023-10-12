using Business_Logic.Dto.Request;
using Business_Logic.Dto.Response;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic.Mapper;

public static class CustomerMapper
{
    public static CustomerDto ToDto(Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            Mobile = entity.Mobile,
            Birthday = entity.Birthday,
            IdentityCard = entity.IdentityCard,
            LicenceNumber = entity.LicenceNumber,
            LicenceDate = entity.LicenceDate,
            Email = entity.Email
        };
    }

    public static Customer ToEntity(CreateCustomerDto dto)
    {
        return new Customer
        {
            CustomerName = dto.CustomerName,
            Mobile = dto.Mobile,
            Birthday = dto.Birthday,
            IdentityCard = dto.IdentityCard,
            LicenceNumber = dto.LicenceNumber,
            LicenceDate = dto.LicenceDate,
            Email = dto.Email,
            Password = dto.Password
        };
    }

    public static void UpdateCustomerToEntity(UpdateCustomerDto dto, ref Customer entity)
    {
        if (!string.IsNullOrEmpty(dto.CustomerName))
        {
            entity.CustomerName = dto.CustomerName;
        }

        if (!string.IsNullOrEmpty(dto.Mobile))
        {
            entity.Mobile = dto.Mobile;
        }

        if (dto.Birthday.HasValue)
        {
            entity.Birthday = dto.Birthday.Value;
        }

        if (!string.IsNullOrEmpty(dto.IdentityCard))
        {
            entity.IdentityCard = dto.IdentityCard;
        }

        if (!string.IsNullOrEmpty(dto.LicenceNumber))
        {
            entity.LicenceNumber = dto.LicenceNumber;
        }

        if (dto.LicenceDate.HasValue)
        {
            entity.LicenceDate = dto.LicenceDate.Value;
        }
    }
}