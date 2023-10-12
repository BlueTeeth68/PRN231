using System.ComponentModel.DataAnnotations;

namespace Business_Logic.Dto.Request;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "Customer name is required.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile is required.")]
    public string Mobile { get; set; } = string.Empty;

    [Required(ErrorMessage = "Birth day is required.")]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "IdentityCard is required.")]
    public string IdentityCard { get; set; } = string.Empty;

    [Required(ErrorMessage = "LicenceNumber is required.")]
    public string LicenceNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "LicenceDate is required.")]
    [DataType(DataType.Date)]
    public DateTime LicenceDate { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}