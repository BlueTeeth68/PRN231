using BusinessLogic.DTOs.Response.CarInformation;
using DataAccess.Models;

namespace BusinessLogic.DTOs.Response.Transaction;

public class RentingDetailResponse
{
    public int RentingTransactionId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Price { get; set; }

    public virtual CarInformationResponse Car { get; set; } = null!;
}