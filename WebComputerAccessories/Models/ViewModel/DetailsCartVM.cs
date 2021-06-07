using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class DetailsCartVM
    {
        public Guid Id { get; set; }
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Stock { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }

        public int? IdCategory { get; set; }
    }
}