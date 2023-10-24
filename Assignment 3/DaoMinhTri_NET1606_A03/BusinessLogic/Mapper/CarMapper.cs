using BusinessLogic.Dto.Request.Cars;
using DataAccess.Models;

namespace BusinessLogic.Mapper;

public static class CarMapper
{
    public static CarInformation ToEntity(CreateCarDto dto)
    {
        return new CarInformation
        {
            CarName = dto.CarName,
            CarDescription = dto.CarDescription,
            NumberOfDoors = dto.NumberOfDoors,
            SeatingCapacity = dto.SeatingCapacity,
            FuelType = dto.FuelType,
            Year = dto.Year,
            CarStatus = dto.CarStatus
        };
    }

    public static void FromUpdateDtoToEntity(UpdateCarDto dto, ref CarInformation entity)
    {
        if (!string.IsNullOrEmpty(dto.CarName))
            entity.CarName = dto.CarName.Trim();
        if (!string.IsNullOrEmpty(dto.CarDescription))
            entity.CarDescription = dto.CarDescription.Trim();
        if (dto.NumberOfDoors.HasValue)
            entity.NumberOfDoors = dto.NumberOfDoors.Value;
        if (dto.SeatingCapacity.HasValue)
            entity.SeatingCapacity = dto.SeatingCapacity.Value;
        if (!string.IsNullOrEmpty(dto.FuelType))
            entity.FuelType = dto.FuelType.Trim();
        if (dto.Year.HasValue)
            entity.Year = dto.Year.Value;
        if (dto.CarStatus.HasValue)
            entity.CarStatus = dto.CarStatus.Value;
        
    }
}