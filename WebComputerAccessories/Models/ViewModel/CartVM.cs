using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class CartVM
    {
            public Guid? IdCart { get; set; }

            public List<DetailsCartVM> Details { get; set; }

    }
}