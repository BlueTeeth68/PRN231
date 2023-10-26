using System;
using System.Collections.Generic;

namespace DaoMinhTri_NET1606_A03_UI.Models
{
    public partial class RentingTransaction
    {
        public RentingTransaction()
        {
            RentingDetails = new HashSet<RentingDetail>();
        }

        public int RentingTransationId { get; set; }
        public DateTime? RentingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte? RentingStatus { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<RentingDetail> RentingDetails { get; set; }
    }
}
