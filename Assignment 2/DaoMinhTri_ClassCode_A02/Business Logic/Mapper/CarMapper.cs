using Business_Logic.Dto.Request;
using DataAccess.Enum;
using DataAccess.Models;

namespace Business_Logic.Mapper;

public static class CarMapper
{
    public static Car ToEntity(CreateCarDto dto)
    {
        return new Car
        {
            CarName = dto.CarName,
            CarModelYear = dto.CarModelYear,
            Color = dto.Color,
            Capacity = dto.Capacity,
            Description = dto.Description,
            ImportDate = DateTime.Now,
            ProducerId = dto.ProducerId,
            RentPrice = dto.RentPrice,
            Status = CarStatus.Available
        };
    }

    public static void UpdateCarToEntity(UpdateCarDto dto, ref Car entity)
    {
        if (!string.IsNullOrEmpty(dto.CarName))
        {
            entity.CarName = dto.CarName;
        }

        if (dto.CarModelYear.HasValue)
        {
            entity.CarModelYear = dto.CarModelYear.Value;
        }

        if (!string.IsNullOrEmpty(dto.Color))
        {
            entity.Color = dto.Color;
        }

        if (dto.Capacity.HasValue)
        {
            entity.Capacity = dto.Capacity.Value;
        }

        if (!string.IsNullOrEmpty(dto.Description))
        {
            entity.Description = dto.Description;
        }

        if (dto.RentPrice.HasValue)
        {
            entity.RentPrice = dto.RentPrice.Value;
        }

        if (dto.Status.HasValue)
        {
            entity.Status = dto.Status.Value;
        }
    }
}