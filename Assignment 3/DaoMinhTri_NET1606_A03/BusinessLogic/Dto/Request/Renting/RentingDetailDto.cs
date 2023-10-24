using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Renting;

public class RentingDetailDto
{
    
    [Required(ErrorMessage = "CarId is required.")]
    public int CarId { get; set; }
    [Required(ErrorMessage = "StartDate is required.")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "EndDate is required.")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    [Required(ErrorMessage = "Price is required.")]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }
}