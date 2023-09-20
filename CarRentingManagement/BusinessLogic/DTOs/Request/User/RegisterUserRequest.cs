using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Request.User;

public class RegisterUserRequest
{
    [Required(ErrorMessage = "Email can not be null")]
    [EmailAddress(ErrorMessage = "Email is incorrect format")]
    public string Email { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string? Telephone { get; set; }
    public DateTime? CustomerBirthday { get; set; }
    [Required(ErrorMessage = "Password can not be null.")]
    public string Password { get; set; } = null!;
}