using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request
{
    public record CredentialDto
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; init; } = string.Empty;
    }
}
