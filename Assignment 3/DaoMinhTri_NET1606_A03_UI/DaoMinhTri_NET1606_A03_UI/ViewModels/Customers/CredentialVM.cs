using System.ComponentModel.DataAnnotations;

namespace DaoMinhTri_NET1606_A03_UI.ViewModels.Customers
{
    public class CredentialVM
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; init; } = string.Empty;
    }
}
