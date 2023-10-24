using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Request.Customers;

public class UpdateCustomerDto
{
    
    public string? CustomerName { get; set; }
    //Need to check regex
    public string? Telephone { get; set; }
        
    [DataType(DataType.Date)]
    public DateTime? CustomerBirthday { get; set; }
}