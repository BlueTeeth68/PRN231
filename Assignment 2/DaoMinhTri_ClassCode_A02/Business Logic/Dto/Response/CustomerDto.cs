namespace Business_Logic.Dto.Response;

public class CustomerDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string IdentityCard { get; set; } = string.Empty;
    public string LicenceNumber { get; set; } = string.Empty;
    public DateTime LicenceDate { get; set; }
    public string Email { get; set; } = string.Empty;
}