using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.BLL.DtoModels.DtoMVC.Vendor
{
    public class VendorNameIdDto
    {
        public int? IdVendor { get; set; }
        public required string Name { get; set; }
    }
}
