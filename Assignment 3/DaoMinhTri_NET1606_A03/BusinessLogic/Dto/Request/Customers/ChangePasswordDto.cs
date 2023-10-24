using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Customers;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Old password is required.")]
    public string OldPass { get; set; } = string.Empty;
    [Required(ErrorMessage = "New password is required.")]
    public string NewPass { get; set; } = string.Empty;
}