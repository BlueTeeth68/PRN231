using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class RentingTransaction
    {
        public int RentingTransationId { get; set; }
        public DateTime? RentingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte? RentingStatus { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();
    }
}