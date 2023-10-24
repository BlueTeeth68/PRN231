namespace BusinessLogic.Dto.Response.Customers;

public class LoginCustomerDto
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? Telephone { get; set; }
    public string Email { get; set; } = null!;
    public DateTime? CustomerBirthday { get; set; }
    public string Token { get; set; } = string.Empty;
}