namespace BusinessLogic.DTOs.Response.User;

public class LoginUserResponse
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? Telephone { get; set; }
    public string Email { get; set; } = null!;
    public DateTime? CustomerBirthday { get; set; }
    public byte? CustomerStatus { get; set; }
}