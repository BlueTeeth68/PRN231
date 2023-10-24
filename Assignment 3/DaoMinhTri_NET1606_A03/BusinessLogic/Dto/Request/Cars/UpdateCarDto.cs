using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Cars;

public class UpdateCarDto
{
    public string? CarName { get; set; }
    public string? CarDescription { get; set; }
    [Range(1, 20)]
    public int? NumberOfDoors { get; set; }
    [Range(1, 100)]
    public int? SeatingCapacity { get; set; }
    public string? FuelType { get; set; }
    [Range(2000, 2023)]
    public int? Year { get; set; }
    public int? ManufacturerId { get; set; }
    public int? SupplierId { get; set; }
    public byte? CarStatus { get; set; }
}