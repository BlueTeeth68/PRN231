namespace DataAccess.Models;

public class CarProducer: BaseEntity
{

    public string ProducerName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Country { get; set; } = string.Empty;
}