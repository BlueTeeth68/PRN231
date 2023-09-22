using UI.Models;

namespace UI.ViewModels.CarInformation;

public class CarInformationResponse
{
    public int CarId { get; set; }
    public string CarName { get; set; } = null!;
    public string? CarDescription { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? SeatingCapacity { get; set; }
    public string? FuelType { get; set; }
    public int? Year { get; set; }
    public byte? CarStatus { get; set; }
    public decimal? CarRentingPricePerDay { get; set; }

    public virtual ManufactureResponse Manufacturer { get; set; } = null!;
    public virtual SupplierResponse Supplier { get; set; } = null!;
}