using BusinessLogic.DTOs.Response.User;
using DataAccess.Models;

namespace BusinessLogic.DTOs.Response.Transaction;

public class RentingResponse
{
    public int RentingTransationId { get; set; }
    public DateTime? RentingDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public byte? RentingStatus { get; set; }

    public virtual UserResponse Customer { get; set; } = null!;
    public virtual ICollection<RentingDetailResponse> RentingDetails { get; set; } = new List<RentingDetailResponse>();
}