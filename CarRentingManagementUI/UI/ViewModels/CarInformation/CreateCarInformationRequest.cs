using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.CarInformation;

public class CreateCarInformationRequest
{
    [Required(ErrorMessage = "Car name can not be null")]
    public string CarName { get; set; } = null!;

    public string? CarDescription { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? SeatingCapacity { get; set; }
    public string? FuelType { get; set; }
    public int? Year { get; set; }

    [Required(ErrorMessage = "Manufacturer can not be null")]
    public int ManufacturerId { get; set; }

    [Required(ErrorMessage = "Supplier can not be null")]
    public int SupplierId { get; set; }
}