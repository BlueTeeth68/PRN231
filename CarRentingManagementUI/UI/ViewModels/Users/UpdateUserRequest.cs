using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Users;

public class UpdateUserRequest
{
    [EmailAddress(ErrorMessage = "Email is not correct format")]
    public string? Email { get; set; }
    public string? CustomerName { get; set; }
    public string? Telephone { get; set; }
    public DateTime? CustomerBirthday { get; set; }
}