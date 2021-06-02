namespace WebComputerAccessories.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public Guid Id { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public Guid? IdOrder { get; set; }

        public Guid? IdProduct { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
