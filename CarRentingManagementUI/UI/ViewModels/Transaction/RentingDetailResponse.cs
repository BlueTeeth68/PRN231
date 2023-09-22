using UI.ViewModels.CarInformation;

namespace UI.ViewModels.Transaction;

public class RentingDetailResponse
{
    public int RentingTransactionId { get; set; }
    public int CarId { get; set; }
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public decimal? Price { get; set; }

    public CarInformationResponse Car { get; set; } = null!;
}