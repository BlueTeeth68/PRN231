using System.ComponentModel.DataAnnotations;

namespace Business_Logic.Dto.Request;

public class UpdateCustomerDto
{
    public string? CustomerName { get; set; }
    public string? Mobile { get; set; }
    [DataType(DataType.Date)] public DateTime? Birthday { get; set; }
    public string? IdentityCard { get; set; }
    public string? LicenceNumber { get; set; }
    [DataType(DataType.Date)] public DateTime? LicenceDate { get; set; }
}