using System.ComponentModel.DataAnnotations;

namespace Business_Logic.Dto.Request;

public class CreateRentingDto
{
    [Required] public int CustomerId { get; set; }
    [Required] public int CarId { get; set; }
    [Required] [DataType(DataType.Date)] public DateTime PickupDate { get; set; }
    [Required] [DataType(DataType.Date)] public DateTime ReturnDate { get; set; }
    [Required] 
    [Range(0.1, int.MaxValue)]public decimal RentPrice { get; set; }
}