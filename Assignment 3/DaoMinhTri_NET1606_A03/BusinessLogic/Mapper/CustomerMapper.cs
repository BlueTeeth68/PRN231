using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Request.Customers;
using BusinessLogic.Dto.Response;
using BusinessLogic.Dto.Response.Customers;
using DataAccess.Models;

namespace BusinessLogic.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer entity)
        {
            return new CustomerDto
            {
                CustomerId = entity.CustomerId,
                CustomerBirthday = entity.CustomerBirthday,
                CustomerName = entity.CustomerName,
                CustomerStatus = entity.CustomerStatus,
                Email = entity.Email,
                Telephone = entity.Telephone
            };
        }

        public static Customer ToEntity(CreateCustomerDto dto)
        {
            return new Customer
            {
                CustomerBirthday = dto.CustomerBirthday,
                CustomerName = dto.CustomerName ?? dto.Email,
                Email = dto.Email,
                Password = dto.Password,
                CustomerStatus = 1,
                Telephone = dto.Telephone
            };
        }

        public static LoginCustomerDto ToLoginDto(Customer entity)
        {
            return new LoginCustomerDto
            {
                CustomerId = entity.CustomerId,
                CustomerBirthday = entity.CustomerBirthday,
                Telephone = entity.Telephone,
                CustomerName = entity.CustomerName,
                Email = entity.Email,
                Token = ""
            };
        }

        public static void FromUpdateDtoToEntity(UpdateCustomerDto dto, ref Customer entity)
        {
            if (!string.IsNullOrEmpty(dto.CustomerName))
            {
                entity.CustomerName = dto.CustomerName;
            }

            if (!string.IsNullOrEmpty(dto.Telephone))
            {
                entity.Telephone = dto.Telephone;
            }

            if (dto.CustomerBirthday.HasValue)
            {
                var now = DateTime.Now;
                if (dto.CustomerBirthday.Value.Year <= now.Year - 10)
                    entity.CustomerBirthday = dto.CustomerBirthday.Value;
            }
        }
    }
}