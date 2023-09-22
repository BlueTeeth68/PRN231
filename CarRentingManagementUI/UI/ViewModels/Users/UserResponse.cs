using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Users;

public class UserResponse
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? Telephone { get; set; }
    public string Email { get; set; } = null!;
    public string? CustomerBirthday { get; set; }
    public byte? CustomerStatus { get; set; }
}