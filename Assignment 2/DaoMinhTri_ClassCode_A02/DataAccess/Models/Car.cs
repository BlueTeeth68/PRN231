using DataAccess.Enum;

namespace DataAccess.Models;

public class Car: BaseEntity
{

    public string CarName { get; set; } = string.Empty;
    public int CarModelYear { get; set; } = 2017;
    public string Color { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ImportDate { get; set; } = DateTime.Now;
    public int ProducerId { get; set; }
    public decimal RentPrice { get; set; }
    public CarStatus Status { get; set; } = CarStatus.Available;

    public virtual CarProducer Producer { get; set; } = null!;
}