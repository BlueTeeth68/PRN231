using UI.ViewModels.Users;

namespace UI.ViewModels.Transaction;

public class RentingReponse
{
    public int RentingTransationId { get; set; }
    public string? RentingDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public byte? RentingStatus { get; set; }

    public UserResponse Customer { get; set; } = null!;
    public ICollection<RentingDetailResponse> RentingDetails { get; set; } = new List<RentingDetailResponse>();
}