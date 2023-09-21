using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Request.Transaction;

public class CreateRentingRequest
{
    public decimal? TotalPrice { get; set; }
    [Required(ErrorMessage = "Customer id can not be null.")]
    public int CustomerId { get; set; }

    public byte? RentingStatus { get; set; } = 1;

    public virtual ICollection<RentingDetailRequest> RentingDetails { get; set; } = new List<RentingDetailRequest>();
}