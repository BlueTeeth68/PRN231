namespace DataAccess.Models
{
    public class Customer:BaseEntity
    {

        public string CustomerName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string IdentityCard { get; set; } = string.Empty;
        public string LicenceNumber { get; set; } = string.Empty;
        public DateTime LicenceDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}