using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Cars;

public class CreateCarDto
{
    [Required(ErrorMessage = "Car name is required.")]
    public string CarName { get; set; } = null!;
    public string? CarDescription { get; set; }
    [Range(1,20)]
    public int? NumberOfDoors { get; set; }
    [Range(1,100)]
    public int? SeatingCapacity { get; set; }
    public string? FuelType { get; set; }
    [Range(2000,2023)]
    public int? Year { get; set; }
    [Required(ErrorMessage = "ManufacturerId is required.")]
    public int ManufacturerId { get; set; }
    [Required(ErrorMessage = "SupplierId is required.")]
    public int SupplierId { get; set; }
    public byte CarStatus { get; set; } = 1;
}