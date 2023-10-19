using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            CarInformations = new HashSet<CarInformation>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? SupplierDescription { get; set; }
        public string? SupplierAddress { get; set; }

        public virtual ICollection<CarInformation> CarInformations { get; set; }
    }
}
