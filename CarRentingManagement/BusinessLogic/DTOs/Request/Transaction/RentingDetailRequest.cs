using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Request.Transaction;

public class RentingDetailRequest
{
    [Required(ErrorMessage = "Card id can not be null.")]
    public int CarId { get; set; }
    [Required (ErrorMessage = "Start date must be defined.")]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "End date must be defined.")]
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}