using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Request.User;

public class UpdatePasswordRequest
{
    [Required(ErrorMessage = "Old password can not be null.")]
    public string OldPassword { get; set; } = null!;
    [Required(ErrorMessage = "New password can not be null.")]
    public string NewPassword { get; set; } = null!;
}