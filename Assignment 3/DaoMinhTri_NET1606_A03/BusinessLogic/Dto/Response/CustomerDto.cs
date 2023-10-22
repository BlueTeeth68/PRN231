namespace BusinessLogic.Dto.Response
{
    public class CustomerDto
    {

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Telephone { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }

        //public virtual ICollection<RentingTransaction> RentingTransactions { get; set; }
    }
}
