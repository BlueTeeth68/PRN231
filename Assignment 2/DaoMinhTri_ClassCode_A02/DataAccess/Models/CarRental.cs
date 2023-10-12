using DataAccess.Enum;

namespace DataAccess.Models;

public class CarRental : BaseEntity
{
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal RentPrice { get; set; }
    public RentingStatus Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;
    public virtual Car Car { get; set; } = null!;
}