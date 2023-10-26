using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Customers
{
    public class CreateCustomerDto
    {
        public string? CustomerName { get; set; }
        //Need to check regex
        [Required(ErrorMessage = "Phone is required.")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        public DateTime? CustomerBirthday { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
