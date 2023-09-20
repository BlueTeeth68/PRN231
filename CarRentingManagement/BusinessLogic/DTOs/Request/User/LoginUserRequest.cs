using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Request.User;

public class LoginUserRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is incorrect format.")]
    public string Email { get; set; } = null!;

    [Required (ErrorMessage = "Password can not be null")]
    public string Password { get; set; } = null!;
}