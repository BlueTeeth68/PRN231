using System;
using System.Collections.Generic;

namespace DaoMinhTri_NET1606_A03_UI.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            CarInformations = new HashSet<CarInformation>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = null!;
        public string? Description { get; set; }
        public string? ManufacturerCountry { get; set; }

        public virtual ICollection<CarInformation> CarInformations { get; set; }
    }
}
