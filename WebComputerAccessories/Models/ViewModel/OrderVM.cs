using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class OrderVM
    {
        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipPhoneNumber { get; set; }

        public string Note { get; set; }

        public Guid? IdUser { get; set; }

        public double TotalMoney { get; set; }


    }
}