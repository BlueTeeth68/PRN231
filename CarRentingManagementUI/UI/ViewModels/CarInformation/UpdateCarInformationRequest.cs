namespace UI.ViewModels.CarInformation;

public class UpdateCarInformationRequest
{
    public string? CarName { get; set; }
    public string? CarDescription { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? SeatingCapacity { get; set; }
    public string? FuelType { get; set; }
    public int? Year { get; set; }
    public int? ManufacturerId { get; set; }
    public int? SupplierId { get; set; }
    public byte? CarStatus { get; set; }
    public decimal? CarRentingPricePerDay { get; set; }
}