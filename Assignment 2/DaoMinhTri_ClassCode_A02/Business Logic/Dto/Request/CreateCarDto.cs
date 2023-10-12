using System.ComponentModel.DataAnnotations;

namespace Business_Logic.Dto.Request;

public class CreateCarDto
{
    [Required(ErrorMessage = "CarName is required.")]
    public string CarName { get; set; } = string.Empty;

    [Required(ErrorMessage = "CarModelYear is required.")]
    public int CarModelYear { get; set; } = 2017;

    [Required(ErrorMessage = "Color is required.")]
    public string Color { get; set; } = string.Empty;

    [Required(ErrorMessage = "Capacity is required.")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "ProducerId is required.")]
    public int ProducerId { get; set; }

    [Required(ErrorMessage = "RentPrice is required.")]
    public decimal RentPrice { get; set; }
}