using DataAccess.Enum;

namespace Business_Logic.Dto.Request;

public class UpdateCarDto
{
    public string? CarName { get; set; }
    public int? CarModelYear { get; set; }
    public string? Color { get; set; }
    public int? Capacity { get; set; }
    public string? Description { get; set; }
    public int? ProducerId { get; set; }
    public decimal? RentPrice { get; set; }
    public CarStatus? Status { get; set; }
}